using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models.RequestModels;
using Contracts.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Modals;
using RestAPI.Services;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("campground{campgroundId}/comment")]
    public class CommentController : ControllerBase
    {
        
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CommentResponseModel>> CrateComment(CommentRequestModel request, Guid campgroundId)
        {
            
            try
            {
                var firebaseId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;
                var campground = await _commentService.CreateComment(request, firebaseId, campgroundId);
                return campground;
            }
            catch (BadHttpRequestException exception)
            {
                switch (exception.StatusCode)
                {
                    case 404:
                        return NotFound(exception.Message);
                        break;
                    default: throw;
                }
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CommentResponseModel>>> GetCommentByCampgroundId(Guid campgroundId)
        {
            try
            {
                return Ok(await _commentService.GetAllCommentByCampground(campgroundId));
            }
            catch (BadHttpRequestException exception)
            {
                switch (exception.StatusCode)
                {
                    case 404:
                        return NotFound(exception.Message);
                        break;
                    default: throw;
                }
            }
            
        }

        [HttpPut]
        [Route("{commentId}")]
        [Authorize]
        public async Task<ActionResult<CommentResponseModel>> UpdateComment(CommentRequestModel request,
            Guid campgroundId, Guid commentId)
        {
            try
            {
                var firebaseId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;
                var updateComment = new CommentUptadeServiseModel
                {
                    Id = commentId,
                    CampgroundId = campgroundId,
                    Rating = request.Rating,
                    Text = request.Text,
                    FirebaseId = firebaseId
                };

                return Ok(await _commentService.UpdateComment(updateComment)) ;
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
        [HttpDelete]
        [Authorize]
        [Route("{commentId}")]
        public async Task<ActionResult> DeleteComment(Guid commentId)
        {
            try
            {
                var firebaseId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;
                return Ok(await _commentService.DeleteComment(commentId, firebaseId));
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
    }
    
    
}