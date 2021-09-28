using System;

namespace RestAPI.Modals
{
    public class CommentUptadeServiseModel
    {
        public Guid Id { get; set; }
        public Guid CampgroundId { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public string FirebaseId { get; set; }
    }
}