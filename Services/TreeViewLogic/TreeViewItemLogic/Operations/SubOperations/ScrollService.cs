using Avalonia;
using Avalonia.Input;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.InteractionData.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations.SubOperations.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations.SubOperations
{
    public class ScrollService : IScrollService
    {
        private readonly ITreeViewData _treeViewData;

        private DateTime _lastScrollTime = DateTime.MinValue;
        private readonly TimeSpan _scrollInterval = TimeSpan.FromMilliseconds(50);
        private CancellationTokenSource? _currentScrollCancellation;

        public ScrollService(ITreeViewData treeViewData)
        {
            _treeViewData = treeViewData;
        }

        public void ScrollToDroppedItem(DragEventArgs e)
        {
            _treeViewData.DragStartPosition = e.GetPosition(_treeViewData.Control);
            ScrollZone scrollZone = GetScrollZone();

            if (scrollZone.ShouldScroll)
            { 
                Scroll(scrollZone.IsInTopZone);
            }
            else
            {
                StopScrolling();
            }
        }

        public void StopScrolling()
        {
            _currentScrollCancellation?.Cancel();
            _currentScrollCancellation = null;
        }

        private async void Scroll(bool isInTopZone)
        {
            CancelPreviousAnimation();

            if (!CanStartScroll())
            {
                return;
            }

            ScrollParameters scrollParams = CalculateScrollParameters(isInTopZone);
            if (!scrollParams.IsValid)
            {
                return;
            }

            await ExecuteScrollAnimation(scrollParams);
        }

        private void CancelPreviousAnimation()
        {
            _currentScrollCancellation?.Cancel();
            _currentScrollCancellation = new CancellationTokenSource();
        }

        private bool CanStartScroll()
        {
            if (DateTime.Now - _lastScrollTime < _scrollInterval) 
            { 
                return false; 
            }

            if (_treeViewData.DraggedItemView == null)
            { 
                return false; 
            }

            _lastScrollTime = DateTime.Now;
            return true;
        }

        private ScrollParameters CalculateScrollParameters(bool isInTopZone)
        {
            Visual? itemView = _treeViewData.DraggedItemView!;
            double itemHeight = itemView.Bounds.Height;
            double currentOffset = _treeViewData.ScrollViewer.Offset.Y;

            double scrollStep = itemHeight * 0.7;
            var targetOffset = currentOffset + scrollStep * (isInTopZone ? -1 : 1);
            targetOffset = Math.Clamp(targetOffset, 0, _treeViewData.ScrollViewer.ScrollBarMaximum.Y);

            return new ScrollParameters(currentOffset, targetOffset);
        }

        private async Task ExecuteScrollAnimation(ScrollParameters parameters)
        {
            CancellationToken token = _currentScrollCancellation!.Token;

            try
            {
                await AnimateScroll(parameters, token);
                SetFinalScrollPosition(parameters.TargetOffset, token);
            }
            catch (TaskCanceledException)
            {
            }
        }

        private async Task AnimateScroll(ScrollParameters parameters, CancellationToken token)
        {
            const double duration = 250;
            DateTime startTime = DateTime.Now;

            while ((DateTime.Now - startTime).TotalMilliseconds < duration)
            {
                if (token.IsCancellationRequested) return;

                double progress = CalculateAnimationProgress(startTime, duration);
                double currentOffset = CalculateCurrentOffset(parameters, progress);

                SetScrollOffset(currentOffset);
                await Task.Delay(16, token);
            }
        }

        private double CalculateAnimationProgress(DateTime startTime, double duration)
        {
            double progress = (DateTime.Now - startTime).TotalMilliseconds / duration;
            return Math.Min(1.0, progress);
        }

        private double CalculateCurrentOffset(ScrollParameters parameters, double progress)
        {
            double easedProgress = EaseOutCubic(progress);
            return parameters.StartOffset + 
                (parameters.TargetOffset - parameters.StartOffset) * easedProgress;
        }

        private void SetScrollOffset(double offset)
        {
            _treeViewData.ScrollViewer.Offset = new Vector(_treeViewData.ScrollViewer.Offset.X, offset);
        }

        private void SetFinalScrollPosition(double targetOffset, CancellationToken token)
        {
            if (!token.IsCancellationRequested)
            {
                SetScrollOffset(targetOffset);
            }
        }

        private ScrollZone GetScrollZone()
        {
            Visual? itemView = _treeViewData.DraggedItemView;
            if (itemView == null)
            {
                return ScrollZone.None;
            }

            double startPosition = _treeViewData.DragStartPosition.Y;
            double itemHeight = itemView.Bounds.Height;
            double controlHeight = _treeViewData.Control.Bounds.Height;

            bool isInBottomZone = startPosition > controlHeight - itemHeight - 5;
            bool isInTopZone = startPosition < itemHeight - 5;

            return new ScrollZone(isInTopZone, isInBottomZone);
        }

        private double EaseOutCubic(double progress) => 1 - Math.Pow(1 - progress, 3);

        private record struct ScrollZone(bool IsInTopZone, bool IsInBottomZone)
        {
            public static ScrollZone None => new(false, false);
            public bool ShouldScroll => IsInTopZone || IsInBottomZone;
        }

        private record struct ScrollParameters(double StartOffset, double TargetOffset)
        {
            public bool IsValid => Math.Abs(TargetOffset - StartOffset) >= 1.0;
        }
    }
}
