using System;

namespace Contracts.Models.RequestModels
{
    public class CommentRequestModel
    { 
        public int Rating { get; set; }
        public string Text { get; set; } 
    }
}