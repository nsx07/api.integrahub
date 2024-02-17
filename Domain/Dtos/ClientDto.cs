using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Domain.Dtos
{
    public class ClientDto
    {
        public int? Id { get; set; }
        public required string Name { get; set; }
    }
}
