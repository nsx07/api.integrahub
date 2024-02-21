﻿namespace IntegraHub.Domain.Entities
{
    public class User : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public long CompanyId { get; set; } 

        public Company Company { get; set; }
    }
}
