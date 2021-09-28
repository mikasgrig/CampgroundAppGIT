using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models.RequestModels;
using Contracts.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Persistences.Modals.WriteModals;
using Persistences.Repositories;
using RestAPI.Modals;

namespace RestAPI.Services
{
    public class CampgroundService :ICampgroundService
    {
        private readonly ICampgroundRepository _campgroundRepository;
        private readonly IUserRepository _userRepository;
        private readonly IImageRepository _imageRepository;

        public CampgroundService(ICampgroundRepository campgroundRepository, IImageRepository imageRepository, IUserRepository userRepository)
        {
            _campgroundRepository = campgroundRepository;
            _imageRepository = imageRepository;
            _userRepository = userRepository;
        }

        public async Task<CampgroundModel> CreateCampground(CampgroundRequestModel model, string firebase)
        {
            var user = await _userRepository.GetAsync(firebase);
            if (user is null)
            {
                throw new BadHttpRequestException($"User token does not exists!", statusCode:404);
            }

            var campgroundWriteModel = new WriteCampgroundModel
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description
            };
            var campgroundImage = new WriteImageModel
            {
                Id = Guid.NewGuid(),
                CampgroundId = campgroundWriteModel.Id,
                Url = model.ImageUrl
            };
            await _imageRepository.SaveOrUpdateAsync(campgroundImage);
            await _campgroundRepository.SaveOrUpdateAsync(campgroundWriteModel);
            return new CampgroundModel
            {
                Id = campgroundWriteModel.Id,
                UserId = campgroundWriteModel.UserId,
                Name = campgroundWriteModel.Name,
                Price = campgroundWriteModel.Price,
                Description = campgroundWriteModel.Description,
                ImageUrl = campgroundImage.Url
            };
        }

        public async Task<IEnumerable<CampgroundResponseModel>> GetAllCampground()
        {

            var campgroundReadModels = await _campgroundRepository.GetAllAsync();
            var imageModels = await _imageRepository.GetAllAsync();
            var campgroundModel = campgroundReadModels.Select(campground => campground.MapToCampgroundModel());
            
            foreach (var item in campgroundModel)
            {
                foreach (var image in imageModels)
                {
                    if (image.CampgroundId == item.Id)
                    {
                        item.ImageUrl = image.Url;
                    }
                }
            }
            var campgroundResponseModel = campgroundModel.Select(item => item.MapToCampgroundResponse());
            return campgroundResponseModel;
        }
    }
}