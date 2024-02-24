using IntegraHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Infra.Data.Subscriptions
{
    public class Subscriptions
    {
        [Subscribe]
        public User UserAdded([EventMessage] User user) => user;
    }
}
