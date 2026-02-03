using AutoMapper;
using InterUniversity.Domain.Dtos;

namespace InterUniversity.Application.Mappings;

public static class PagedResultMappings
{
    public static PagedResult<TDestination> MapTo<TSource, TDestination>(this PagedResult<TSource> paged, IMapper mapper)
    {
        return new PagedResult<TDestination>
        {
            CurrentPage = paged.CurrentPage,
            PageCount = paged.PageCount,
            PageSize = paged.PageSize,
            RowsCount = paged.RowsCount,
            Results = mapper.Map<IEnumerable<TDestination>>(paged.Results).ToList()
        };
    }
}
