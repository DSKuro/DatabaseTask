using DatabaseTask.Models.AppData;
using DatabaseTask.Services.Database.Repositories.Interfaces;
using DatabaseTask.Services.Database.Utils.Interfaces;
using ExCSS;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.Services.Database.Repositories
{
    public class TblDrawingContentsRepository : ITblDrawingContentsRepository
    {
        private readonly ConnectionStringData _stringData;

        public TblDrawingContentsRepository(ConnectionStringData stringData)
        {
            _stringData = stringData;
        }

        public TblDrawingContent? GetFirstItem()
        {
            if (string.IsNullOrEmpty(_stringData.ConnectionString))
            {
                return null;
            }

            using var context = new DataContext(_stringData.ConnectionString);
            return context.TblDrawingContents.FirstOrDefault();
        }

        public List<string?>? GetExistedPaths()
        {
            using var context = new DataContext(_stringData.ConnectionString);
            return context.TblDrawingContents
                      .Where(dc => dc.ContentDevice != null &&
                                   context.TblDevices.Any(d => d.DeviceId == dc.ContentDevice))
                      .Select(t => t.ContentDocument)
                      .ToList();
        }

        public Dictionary<string, List<TblDrawingContent>> GetPathIndex(DataContext context, List<string> paths)
        {
            List<TblDrawingContent>? records = GetPaths(context, paths);

            if (records is null)
            {
                return new Dictionary<string, List<TblDrawingContent>>();
            }

            return BuildPathIndex(records, paths);
        }

        private List<TblDrawingContent>? GetPaths(DataContext context, List<string> paths)
        {
            var predicate = PredicateBuilder.New<TblDrawingContent>(false);

            foreach (var path in paths)
            {
                string localPath = path;

                predicate = predicate.Or(x =>
                                x.ContentDocument != null &&
                                EF.Functions.Like(x.ContentDocument, $"%{localPath}%"));
            }

            var records = context.TblDrawingContents
                .AsExpandable()
                .Where(x => !string.IsNullOrEmpty(x.ContentDocument))
                .Where(predicate)
                .ToList();

            return records;
        }

        private Dictionary<string, List<TblDrawingContent>> BuildPathIndex(
                List<TblDrawingContent> allRecords,
                List<string> paths)
        {
            var index = new Dictionary<string, List<TblDrawingContent>>(
                StringComparer.OrdinalIgnoreCase);

            foreach (var path in paths)
            {
                var relativePath = path.StartsWith(@".\")
                    ? path[2..]
                    : path;

                index[path] = allRecords
                    .Where(r =>
                        r.ContentDocument!.Contains(path,
                            StringComparison.OrdinalIgnoreCase)
                        ||
                        r.ContentDocument.Contains(
                            $@"\dwg\{relativePath}",
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return index;
        }

        public void UpdatePathContext(DataContext context, List<TblDrawingContent> allRecords, string oldPath, string newPath)
        {
            UpdatePathImplementation(allRecords, oldPath, newPath);
        }

        private void UpdatePathImplementation(
            List<TblDrawingContent> records,
            string oldPath,
            string newPath)
        {
            var relativeOldPath = oldPath.StartsWith(@".\")
                ? oldPath[2..]
                : oldPath;

            var relativeNewPath = newPath.StartsWith(@".\")
                ? newPath[2..]
                : newPath;

            foreach (var record in records)
            {
                if (string.IsNullOrEmpty(record.ContentDocument))
                {
                    continue;
                }

                if (record.ContentDocument.Contains(oldPath,
                    StringComparison.OrdinalIgnoreCase))
                {
                    record.ContentDocument =
                        record.ContentDocument.Replace(oldPath, newPath);
                }
                else
                {
                    record.ContentDocument =
                        record.ContentDocument.Replace(
                            relativeOldPath,
                            relativeNewPath,
                            StringComparison.OrdinalIgnoreCase);
                }
            }
        }

        public void CopyItemsContext(DataContext context, List<TblDrawingContent> records, string oldPath, string newPath)
        {
            CopyItemsImplementation(context, records, oldPath, newPath);
        }

        private void CopyItemsImplementation(DataContext context, List<TblDrawingContent> records, string oldPath, string newPath)
        {
            foreach (var record in records)
            {
                record.ContentId = 0;
                record.ContentDocument = record.ContentDocument!.Replace(oldPath, newPath);
            }

            context.TblDrawingContents.AddRange(records);
        }

        public void DeleteItemContext(DataContext context, List<TblDrawingContent> allRecords, string path)
        {
            context.TblDrawingContents.RemoveRange(allRecords);
        }
    }
}
