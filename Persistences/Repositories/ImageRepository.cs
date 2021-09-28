using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistences.Modals.ReadModals;
using Persistences.Modals.WriteModals;

namespace Persistences.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ISqlClient _sqlClient;
        private const string TableName = "images";

        public ImageRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }
        public Task<IEnumerable<ReadImageModel>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {TableName}";
            return _sqlClient.QueryAsync<ReadImageModel>(sql);
        }

        public Task<ReadImageModel> GetAsync(Guid campgroundId)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveOrUpdateAsync(WriteImageModel model)
        {
            var sql = @$"INSERT INTO {TableName} (Id, CampgroundId, Url) 
                        VALUES (@Id, @CampgroundId, @Url)
                        ON DUPLICATE KEY UPDATE CampgroundId = @CampgroundId, Url = @Url";

            return _sqlClient.ExecuteAsync(sql, model);
        }

        public Task<int> DeleteAsync(Guid campgroundId)
        {
            throw new NotImplementedException();
        }
    }
}