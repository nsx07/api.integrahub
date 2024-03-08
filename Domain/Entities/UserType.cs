using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Domain.Entities
{
    public class UserType: BaseEntity<long>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public IList<User> Users { get; set; }
    }
}
