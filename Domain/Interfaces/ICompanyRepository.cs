using IntegraHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Domain.Interfaces
{
    public interface ICompanyRepository: IBaseRepository<Company, long>
    {
    }
}
