using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Contracts.Models.RequestModels;
using RestAPI.Modals;
using RestAPI.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistences.Modals.WriteModals;
using Persistences.Repositories;

namespace RestAPI.Clients
{
    public class FirebaseClient : IFirebaseClient
    {
        private readonly HttpClient _httpClient;
        private readonly Firebase _firebase;
        private readonly IUserRepository _userRepository;

        public FirebaseClient(HttpClient httpClient, IOptions<Firebase> firebase, IUserRepository userRepository)
        {
            _httpClient = httpClient;
            _userRepository = userRepository;
            _firebase = firebase.Value;
        }
        public async Task<UserResponseModel> Signup(UserRequestModel userLogin)
        {
            string url = $"v1/accounts:signUp?key={_firebase.ApiKey}";
            var response = await _httpClient.PostAsJsonAsync(url, userLogin);
            var newuser = await response.Content.ReadFromJsonAsync<UserResponseModel>();
            await _userRepository.SaveUserAsync(new WriteUserModel
            {
                Id = Guid.NewGuid(),
                FirebaseId = newuser.LocalId,
                Email = newuser.Email
            });
            return newuser;
        }

        public async Task<UserResponseModel> Signin(UserRequestModel userLogin)
        {
            string url = $"v1/accounts:signInWithPassword?key={_firebase.ApiKey}";
            var response = await _httpClient.PostAsJsonAsync(url, userLogin);
            var loginuser = await response.Content.ReadFromJsonAsync<UserResponseModel>();
            return loginuser;
        }
    }
}