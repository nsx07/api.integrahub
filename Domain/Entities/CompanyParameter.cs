using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Domain.Entities
{
    public class CompanyParameter: BaseEntity<long>
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
