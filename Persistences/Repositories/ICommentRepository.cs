using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistences.Modals.ReadModals;
using Persistences.Modals.WriteModals;

namespace Persistences.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<ReadCommentModel>> GetAllAsync(Guid campgroundId);

        Task<ReadCommentModel> GetAsync(Guid id, Guid userId);
        
        Task<int> SaveOrUpdateAsync(WriteCommentModel model);

        Task<int> DeleteAsync(Guid id);
    }
}