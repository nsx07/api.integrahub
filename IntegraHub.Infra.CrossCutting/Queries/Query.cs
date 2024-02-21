using IntegraHub.Domain.Entities;
using IntegraHub.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Infra.Data.Queries
{
    public class Query
    {
        [UseOffsetPaging(MaxPageSize = 100, IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<User> GetUsers(PostgresContext dbContext) => dbContext.User;

        [UseOffsetPaging(MaxPageSize = 100, IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Company> GetCompanies(PostgresContext dbContext) => dbContext.Company;

        [UseOffsetPaging(MaxPageSize = 100, IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<CompanyParameter> GetCompanyParameters(PostgresContext dbContext) => dbContext.CompanyParameter;
    }
}
