using System.Threading.Tasks;
using Contracts.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Clients;
using RestAPI.Modals;
using RestAPI.Options;
using Microsoft.Extensions.DependencyInjection;

namespace RestAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IFirebaseClient _firebaseClient;
        public AuthController(IFirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }
        [HttpPost]
        [Route("SingUp")]
        public async Task<UserResponseModel> SingUp(UserRequestModel request)
        {
            var user = await _firebaseClient.Signup(request);
            return  user;
        }
        [HttpPost]
        [Route("SingIn")]
        public async Task<UserResponseModel> SingIn(UserRequestModel request)
        {
            var user = await _firebaseClient.Signin(request);
            return  user;
        }
    }
}