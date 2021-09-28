using System;

namespace Persistences.Modals.ReadModals
{
    public class ReadImageModel
    {
        public Guid Id { get; set; }
        public Guid CampgroundId { get; set; }
        public string Url { get; set; }
    }
}