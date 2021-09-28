using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistences.Modals.ReadModals;
using Persistences.Modals.WriteModals;

namespace Persistences.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ISqlClient _sqlClient;
        private const string TableName = "comments";

        public CommentRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }
        public Task<IEnumerable<ReadCommentModel>> GetAllAsync(Guid campgroundId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE CampgroundId = @CampgroundId";
            return _sqlClient.QueryAsync<ReadCommentModel>(sql, new
            {
                CampgroundId = campgroundId
            });
        }

        public Task<ReadCommentModel> GetAsync(Guid id, Guid userId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Id = @Id AND UserId = @UserId";
            return _sqlClient.QuerySingleOrDefaultAsync<ReadCommentModel>(sql, new
            {
                Id = id,
                UserId = userId
            });
        }

        public Task<int> SaveOrUpdateAsync(WriteCommentModel model)
        {
            var sql = @$"INSERT INTO {TableName} (Id, CampgroundId, Rating, Text, UserId, DateCreated) 
                        VALUES (@Id, @CampgroundId, @Rating, @Text, @UserId, @DateCreated)
                        ON DUPLICATE KEY UPDATE CampgroundId = @CampgroundId, Rating = @Rating, Text = @Text, UserId = @UserId, DateCreated = @DateCreated";

            return _sqlClient.ExecuteAsync(sql, model);
        }

        public Task<int> DeleteAsync(Guid id)
        {
            var sql = $"DELETE FROM {TableName} WHERE Id = @Id";
            return _sqlClient.ExecuteAsync(sql, new {Id = id});
        }
    }
}