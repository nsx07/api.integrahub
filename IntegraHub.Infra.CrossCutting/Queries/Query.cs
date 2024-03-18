using HotChocolate.Authorization;
using IntegraHub.Domain.Entities;
using IntegraHub.Infra.Data.Context;

namespace IntegraHub.Infra.Data.Queries
{
    public class Query
    {
        [UseOffsetPaging(MaxPageSize = 100, IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<User> GetUsers(PostgresContext dbContext) => dbContext.User;

        [UseOffsetPaging(MaxPageSize = 100, IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<Company> GetCompanies(PostgresContext dbContext) => dbContext.Company;

        [UseOffsetPaging(MaxPageSize = 100, IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<CompanyParameter> GetCompanyParameters(PostgresContext dbContext) => dbContext.CompanyParameter;
    }
}
