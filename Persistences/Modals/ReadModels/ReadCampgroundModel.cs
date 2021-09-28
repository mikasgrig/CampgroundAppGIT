using System;

namespace Persistences.Modals.ReadModals
{
    public class ReadCampgroundModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}