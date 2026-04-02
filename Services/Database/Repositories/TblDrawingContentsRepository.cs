using DatabaseTask.Models.AppData;
using DatabaseTask.Services.Database.Repositories.Interfaces;
using DatabaseTask.Services.Database.Utils.Interfaces;
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

        public List<string?>? GetExistedPaths()
        {
            using var context = new DataContext(_stringData.ConnectionString);
            return context.TblDrawingContents
                      .Where(dc => dc.ContentDevice != null &&
                                   context.TblDevices.Any(d => d.DeviceId == dc.ContentDevice))
                      .Select(t => t.ContentDocument)
                      .ToList();
        }

        public void UpdatePath(string oldPath, string newPath)
        {
            using var context = new DataContext(_stringData.ConnectionString);
            UpdatePathImplementation(null, oldPath, newPath);
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
            //var records = GetRecordsByPath(allRecords, oldPath);

            var relativeOldPath = oldPath.StartsWith(@".\")
                ? oldPath[2..]
                : oldPath;

            var relativeNewPath = newPath.StartsWith(@".\")
                ? newPath[2..]
                : newPath;

            foreach (var record in records)
            {
                if (string.IsNullOrEmpty(record.ContentDocument))
                    continue;

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

        public void CopyItems(string oldPath, string newPath)
        {
            using var context = new DataContext(_stringData.ConnectionString);
            CopyItemsImplementation(context, null, oldPath, newPath);
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
            //context.SaveChanges();
        }

        public void DeleteItem(string path)
        {
            using var context = new DataContext(_stringData.ConnectionString);
            DeleteItemImplementation(context, null, path);
        }

        public void DeleteItemContext(DataContext context, List<TblDrawingContent> allRecords, string path)
        {
            DeleteItemImplementation(context, allRecords, path);
        }

        private void DeleteItemImplementation(DataContext context, List<TblDrawingContent> records, string path)
        {
            context.TblDrawingContents.RemoveRange(records);
            //context.SaveChanges();
        }

        private List<TblDrawingContent> GetRecordsByPath(
            List<TblDrawingContent> allRecords,
            string path)
        {
            var relativePath = path.StartsWith(@".\")
                ? path[2..]
                : path;

            return allRecords
                .Where(item =>
                    !string.IsNullOrEmpty(item.ContentDocument)
                    && (
                        item.ContentDocument.Contains(
                            path,
                            StringComparison.OrdinalIgnoreCase)
                        ||
                        item.ContentDocument.Contains(
                            $@"\dwg\{relativePath}",
                            StringComparison.OrdinalIgnoreCase)
                    ))
                .ToList();
        }
    }
}
