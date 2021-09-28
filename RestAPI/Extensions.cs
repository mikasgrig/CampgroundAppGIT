using Contracts.Models.RequestModels;
using Contracts.Models.ResponseModels;
using Persistences.Modals.ReadModals;
using Persistences.Modals.WriteModals;
using RestAPI.Modals;

namespace RestAPI
{
    public static class Extensions
    {
        public static CampgroundResponseModel MapToCampgroundResponse(this CampgroundModel model)
        {
            return new CampgroundResponseModel
            {
                Id = model.Id,
                UserId = model.UserId,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };
        }
        public static CommentResponseModel MapToCommentResponse(this WriteCommentModel model)
        {
            return new CommentResponseModel
            {
                Id = model.Id,
                CampgroundId = model.CampgroundId,
                Rating = model.Rating,
                Text = model.Text,
                UserId = model.UserId,
                DateCreated = model.DateCreated
            };
        }
        public static CampgroundModel MapToCampgroundModel(this ReadCampgroundModel model)
        {
            return new CampgroundModel
            {
                Id = model.Id,
                UserId = model.UserId,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = "null"
            };
        }

        public static CommentResponseModel MapTiCommentResponseModel(this ReadCommentModel model)
        {
            return new CommentResponseModel
            {
                Id = model.Id,
                CampgroundId = model.CampgroundId,
                Rating = model.Rating,
                Text = model.Text,
                UserId = model.UserId,
                DateCreated = model.DateCreated
            };
        }
    }
}