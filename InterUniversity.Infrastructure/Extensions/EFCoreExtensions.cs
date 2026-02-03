using InterUniversity.Domain.Dtos;
using Microsoft.EntityFrameworkCore;

namespace InterUniversity.Infrastructure.Extensions;

public static class EFCoreExtensions
{
    public static async Task<PagedResult<TEntity>> GetPagedResultAsync<TEntity>(this IQueryable<TEntity> source, int pageSize, int currentPage)
        where TEntity : class
    {
        var rows = await source.CountAsync();
        var results = await source
            .Skip(pageSize * (currentPage - 1))
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<TEntity>
        {
            CurrentPage = currentPage,
            PageCount = (int)Math.Ceiling((double)rows / pageSize),
            PageSize = pageSize,
            Results = results,
            RowsCount = rows
        };
    }
}
