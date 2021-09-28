using System;

namespace Contracts.Models.ResponseModels
{
    public class CampgroundResponseModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}