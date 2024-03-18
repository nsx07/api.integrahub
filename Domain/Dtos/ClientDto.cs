using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Domain.Dtos
{
    public class ClientDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string DomainName { get; set; }
        public string? LogoUrl { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
