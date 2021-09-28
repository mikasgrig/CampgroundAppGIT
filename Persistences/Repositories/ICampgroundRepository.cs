using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistences.Modals.ReadModals;
using Persistences.Modals.WriteModals;

namespace Persistences.Repositories
{
    public interface ICampgroundRepository
    {
        Task<IEnumerable<ReadCampgroundModel>> GetAllAsync();

        Task<ReadCampgroundModel> GetAsync(Guid id, Guid userId);
        Task<ReadCampgroundModel> GetAsync(Guid id);
        
        Task<int> SaveOrUpdateAsync(WriteCampgroundModel model);

        Task<int> DeleteAsync(Guid id);
    }
}