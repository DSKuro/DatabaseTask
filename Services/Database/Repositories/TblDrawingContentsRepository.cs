using DatabaseTask.Models.AppData;
using DatabaseTask.Services.Database.Repositories.Interfaces;
using DatabaseTask.Services.Database.Utils.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
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
            UpdatePathImplementation(context, oldPath, newPath);
        }

        public void UpdatePathContext(DataContext context, string oldPath, string newPath)
        {
            UpdatePathImplementation(context, oldPath, newPath);
        }

        private void UpdatePathImplementation(DataContext context, string oldPath, string newPath)
        {
            var records = GetRecordsByPath(context, oldPath);

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

                if (record.ContentDocument.Contains(oldPath, StringComparison.OrdinalIgnoreCase))
                {
                    record.ContentDocument = record.ContentDocument.Replace(oldPath, newPath);
                }
                else if (record.ContentDocument.Contains(@"\dwg\", StringComparison.OrdinalIgnoreCase))
                {
                    record.ContentDocument = record.ContentDocument.Replace(
                        relativeOldPath,
                        relativeNewPath,
                        StringComparison.OrdinalIgnoreCase);
                }
            }

            context.SaveChanges();
        }

        public void CopyItems(string oldPath, string newPath)
        {
            using var context = new DataContext(_stringData.ConnectionString);
            CopyItemsImplementation(context, oldPath, newPath);
        }

        public void CopyItemsContext(DataContext context, string oldPath, string newPath)
        {
            CopyItemsImplementation(context, oldPath, newPath);
        }

        private void CopyItemsImplementation(DataContext context, string oldPath, string newPath)
        {
            var records =
                GetRecordsByPath(context, oldPath)
                .AsNoTracking()
                .ToList();

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
            DeleteItemImplementation(context, path);
        }

        public void DeleteItemContext(DataContext context, string path)
        {
            DeleteItemImplementation(context, path);
        }

        private void DeleteItemImplementation(DataContext context, string path)
        {
            var records =
                GetRecordsByPath(context, path);
            context.TblDrawingContents.RemoveRange(records);
            //context.SaveChanges();
        }

        private IQueryable<TblDrawingContent> GetRecordsByPath(DataContext context, string path)
        {
            var relativePath = path.StartsWith(@".\")
                ? path.Substring(2)
                : path;

            var baseRecords = context.TblDrawingContents
                .Where(item => !string.IsNullOrEmpty(item.ContentDocument)
                        && (
                            EF.Functions.Like(item.ContentDocument, $"%{path}%")
                            || EF.Functions.Like(item.ContentDocument, $"%\\dwg\\%{relativePath}%")
                        ));

            return baseRecords;
        }
    }
}
