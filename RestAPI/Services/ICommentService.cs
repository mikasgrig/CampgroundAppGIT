using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models.RequestModels;
using Contracts.Models.ResponseModels;
using Persistences.Modals.ReadModals;
using RestAPI.Modals;

namespace RestAPI.Services
{
    public interface ICommentService
    {
        Task<CommentResponseModel> CreateComment(CommentRequestModel model, string firebase, Guid campgroundId);
        Task<IEnumerable<CommentResponseModel>> GetAllCommentByCampground(Guid campgroundId);
        Task<CommentResponseModel> UpdateComment(CommentUptadeServiseModel model);
        Task<int> DeleteComment(Guid id, string firebase);
    }
}