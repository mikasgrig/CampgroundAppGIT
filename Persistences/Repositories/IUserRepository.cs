using System;
using System.Threading.Tasks;
using Persistences.Modals.ReadModals;
using Persistences.Modals.WriteModals;

namespace Persistences.Repositories
{
    public interface IUserRepository
    {
        Task<ReadUserModel> GetAsync(string userFirebaseId);
        Task<int> SaveUserAsync(WriteUserModel user);
    }
}