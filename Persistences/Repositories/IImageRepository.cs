using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistences.Modals.ReadModals;
using Persistences.Modals.WriteModals;

namespace Persistences.Repositories
{
    public interface IImageRepository
    {
        Task<IEnumerable<ReadImageModel>> GetAllAsync();

        Task<ReadImageModel> GetAsync(Guid campgroundId);
        
        Task<int> SaveOrUpdateAsync(WriteImageModel model);

        Task<int> DeleteAsync(Guid campgroundId);
    }
}