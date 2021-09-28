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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICampgroundRepository _campgroundRepository;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, ICampgroundRepository campgroundRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _campgroundRepository = campgroundRepository;
        }


        public async Task<CommentResponseModel> CreateComment(CommentRequestModel model, string firebase, Guid campgroundId)
        {
            var user = await _userRepository.GetAsync(firebase);
            if (user is null)
            {
                throw new BadHttpRequestException($"User token does not exists!", statusCode:404);
            }

            var campground = await _campgroundRepository.GetAsync(campgroundId);
            if (campground is null)
            {
                throw new BadHttpRequestException($"Campground with Id {campgroundId} does not exists!", statusCode:404);
            }
            if (model.Rating > 5)
            {
                model.Rating = 5;
            }

            var comment = new WriteCommentModel
            {
                Id = Guid.NewGuid(),
                CampgroundId = campgroundId,
                Rating = model.Rating,
                Text = model.Text,
                UserId = user.Id,
                DateCreated = DateTime.Now
            };
            await _commentRepository.SaveOrUpdateAsync(comment);
            return comment.MapToCommentResponse();

        }

        public async Task<IEnumerable<CommentResponseModel>> GetAllCommentByCampground(Guid campgroundId)
        {
            var campground = await _campgroundRepository.GetAsync(campgroundId);
            if (campground is null)
            {
                throw new BadHttpRequestException($"Campground with Id {campgroundId} does not exists!", statusCode:404);
            }
            var commentList = await _commentRepository.GetAllAsync(campgroundId);
            
            return commentList.Select(comment => comment.MapTiCommentResponseModel());
        }
        public async Task<CommentResponseModel> UpdateComment(CommentUptadeServiseModel model)
        {
            if (model is null)
            {
                throw new BadHttpRequestException($"Wrong request", statusCode:400);
            }
            var user = await _userRepository.GetAsync(model.FirebaseId);
            
            var comment = await _commentRepository.GetAsync(model.Id, user.Id);
            
            if (comment is null)
            {
                throw new BadHttpRequestException($"User with Email {user.Email} can't update this Comment", statusCode:404);
            }
            if (model.Rating > 5)
            {
                model.Rating = 5;
            }

            var commentWrite = new WriteCommentModel
            {
                Id = comment.Id,
                CampgroundId = comment.CampgroundId,
                Rating = model.Rating,
                Text = model.Text,
                UserId = comment.UserId,
                DateCreated = comment.DateCreated
            };
            await _commentRepository.SaveOrUpdateAsync(commentWrite);
            return commentWrite.MapToCommentResponse();
        }
        public async Task<int> DeleteComment(Guid id, string firebase)
        {
            var user = await _userRepository.GetAsync(firebase);
            var comment = await _commentRepository.GetAsync(id, user.Id);

            if (comment is null)
            {
                throw new BadHttpRequestException($"Comment with Id {comment.Id} does not exists!", statusCode:404);
            }

            return await _commentRepository.DeleteAsync(id);
        }
    }
    
}