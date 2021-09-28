using System.Threading.Tasks;
using Contracts.Models.RequestModels;
using RestAPI.Modals;

namespace RestAPI.Clients
{
    public interface IFirebaseClient
    {
        Task<UserResponseModel> Signup(UserRequestModel userLogin);
        Task<UserResponseModel> Signin(UserRequestModel userLogin);
    }
}