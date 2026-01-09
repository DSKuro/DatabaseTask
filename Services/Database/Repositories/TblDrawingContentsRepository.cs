using DatabaseTask.Models.AppData;
using DatabaseTask.Services.Database.Repositories.Interfaces;
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
    }
}
