using System.Threading.Tasks;
using Persistences.Modals.ReadModals;
using Persistences.Modals.WriteModals;

namespace Persistences.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string TableName = "users";
        private readonly ISqlClient _sqlClient;

        public UserRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }
        public Task<ReadUserModel> GetAsync(string userFirebaseId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE FirebaseId = @FirebaseId";
            
            return _sqlClient.QuerySingleOrDefaultAsync<ReadUserModel>(sql, new {FirebaseId = userFirebaseId});
        }

        public Task<int> SaveUserAsync(WriteUserModel user)
        {
            var sql = @$"INSERT INTO {TableName} (Id, FirebaseId, Email) 
                        VALUES (@Id, @FirebaseId, @Email)";

            return _sqlClient.ExecuteAsync(sql, user);
        }
    }
}