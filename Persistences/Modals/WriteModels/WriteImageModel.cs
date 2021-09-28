using System;

namespace Persistences.Modals.WriteModals
{
    public class WriteImageModel
    {
        public Guid Id { get; set; }
        public Guid CampgroundId { get; set; }
        public string Url { get; set; }
    }
}