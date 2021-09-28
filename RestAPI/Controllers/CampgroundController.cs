using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Contracts.Models.RequestModels;
using Contracts.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistences.Modals.WriteModals;
using Persistences.Repositories;
using RestAPI.Modals;
using RestAPI.Services;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("campground")]
    public class CampgroundController : ControllerBase
    {
        private readonly ICampgroundService _campgroundService;

        public CampgroundController(ICampgroundService campgroundService)
        {
            _campgroundService = campgroundService;
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CampgroundResponseModel>> CrateCampground(CampgroundRequestModel request)
        {
            
            try
            {
                var firebaseId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;
                var campground = await _campgroundService.CreateCampground(request, firebaseId);
                return campground.MapToCampgroundResponse();
            }
            catch (BadHttpRequestException exception)
            {
                switch (exception.StatusCode)
                {
                    case 404:
                        return NotFound(exception.Message);
                        break;
                    case 400:
                        return NotFound(exception.Message);
                        break;
                    default: throw;
                }
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<CampgroundResponseModel>> GetAllCampground()
        {
            return await _campgroundService.GetAllCampground();
        }
    }
}