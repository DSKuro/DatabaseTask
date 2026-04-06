using DatabaseTask.Models.AppData;
using DatabaseTask.Services.Database.Repositories.Interfaces;
using DatabaseTask.Services.Database.Utils.Interfaces;
using ExCSS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.Services.Database.Repositories
{
    public class TblDrawingContentsRepository : ITblDrawingContentsRepository
    {
        private readonly ConnectionStringData _stringData;
        private readonly IDatabasePath _databasePath;

        public TblDrawingContentsRepository(ConnectionStringData stringData,
            IDatabasePath databasePath)
        {
            _stringData = stringData;
            _databasePath = databasePath;
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

        public List<string>? GetExistedPathsWithoutDuplicates()
        {
            try
            {
                using var context = new DataContext(_stringData.ConnectionString);
                return context.TblDrawingContents
                          .Where(x => !string.IsNullOrEmpty(x.ContentDocument))
                          .Select(x => (string)(object)x.ContentDocument!)
                          .Distinct()
                          .ToList();
            }
            catch (Exception)
            {
                return null;
            }

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
            try
            {
                var normalizedPaths = paths
               .Select(p => _databasePath.DenormalizePath(p))
               .ToList();

                var records = context.TblDrawingContents
                    .Where(x => !string.IsNullOrEmpty(x.ContentDocument))
                    .Where(x =>
                        paths.Contains((string)(object)x.ContentDocument!)
                        || normalizedPaths.Contains((string)(object)x.ContentDocument!))
                    .ToList();

                return records;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Dictionary<string, List<TblDrawingContent>> BuildPathIndex(
                List<TblDrawingContent> allRecords,
                List<string> paths)
        {
            var index = new Dictionary<string, List<TblDrawingContent>>(
                StringComparer.OrdinalIgnoreCase);

            foreach (var path in paths)
            {
                var relativePath = ToRelativePath(path);

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
            var relativeOldPath = ToRelativePath(oldPath);

            var relativeNewPath = ToRelativePath(newPath);

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
                        newPath;
                }
            }
        }

        private string ToRelativePath(string path)
        {
            return path.StartsWith(@".\")
                ? path[2..]
                : path;
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
