using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models.RequestModels;
using Contracts.Models.ResponseModels;
using RestAPI.Modals;

namespace RestAPI.Services
{
    public interface ICampgroundService
    {
        Task<CampgroundModel> CreateCampground(CampgroundRequestModel model, string firebase);
        Task<IEnumerable<CampgroundResponseModel>> GetAllCampground();
    }
}