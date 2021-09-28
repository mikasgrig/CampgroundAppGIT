using System;

namespace Persistences.Modals.ReadModals
{
    public class ReadUserModel
    {
        public Guid Id { get; set; }
        public string FirebaseId { get; set; }
        public string Email { get; set; }
    }
}