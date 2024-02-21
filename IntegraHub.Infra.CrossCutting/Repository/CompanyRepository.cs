using IntegraHub.Domain.Entities;
using IntegraHub.Domain.Interfaces;
using IntegraHub.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Infra.Data.Repository
{
    public class CompanyRepository(PostgresContext pgContext) : BaseRepository<Company, long>(pgContext), ICompanyRepository
    {
    }
}
