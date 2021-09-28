using System;

namespace Persistences.Modals.WriteModals
{
    public class WriteCampgroundModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}