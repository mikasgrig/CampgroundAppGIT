using System;

namespace Persistences.Modals.WriteModals
{
    public class WriteUserModel
    {
        public Guid Id { get; set; }
        public string FirebaseId { get; set; }
        public string Email { get; set; }
    }
}