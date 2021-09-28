using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistences.Modals.ReadModals;
using Persistences.Modals.WriteModals;

namespace Persistences.Repositories
{
    public class CampgroundRepository : ICampgroundRepository
    {
        private const string TableName = "campgrounds";
        private readonly ISqlClient _sqlClient;

        public CampgroundRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }
        public Task<IEnumerable<ReadCampgroundModel>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {TableName}";
            return _sqlClient.QueryAsync<ReadCampgroundModel>(sql);
        }

        public Task<ReadCampgroundModel> GetAsync(Guid id, Guid userId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Id = @Id AND UserId = @UserId";
            return _sqlClient.QuerySingleOrDefaultAsync<ReadCampgroundModel>(sql, new
            {
                Id = id,
                UserId = userId
            });
        }
        public Task<ReadCampgroundModel> GetAsync(Guid id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Id = @Id";
            return _sqlClient.QuerySingleOrDefaultAsync<ReadCampgroundModel>(sql, new
            {
                Id = id
            });
        }

        public Task<int> SaveOrUpdateAsync(WriteCampgroundModel model)
        {
            var sql = @$"INSERT INTO {TableName} (Id, UserId, Name, Price, Description) 
                        VALUES (@Id, @UserId, @Name, @Price, @Description)
                        ON DUPLICATE KEY UPDATE UserId = @UserId, Name = @Name, Price = @Price, Description = @Description";

            return _sqlClient.ExecuteAsync(sql, model);
        }

        public Task<int> DeleteAsync(Guid id)
        {
            var sql = $"DELETE FROM {TableName} WHERE Id = @Id";
            
            return _sqlClient.ExecuteAsync(sql, new {Id = id});
        }
    }
}