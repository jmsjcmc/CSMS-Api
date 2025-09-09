using AutoMapper;
using AutoMapper.QueryableExtensions;
using csms_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace csms_backend.Utils
{
    public class PaginationHelper
    {
        public static async Task<List<TDestination>> PaginateAndProject<TSource, TDestination>(
            IQueryable<TSource> query,
            int pageNumber,
            int pageSize,
            IMapper mapper)
        {
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TDestination>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public static async Task<Pagination<T>> Paginate<T>(
            IQueryable<T> query,
            int pageNumber,
            int pageSize)
        {
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return PaginatedResponse(items, totalCount, pageNumber, pageSize);
        }

        public static Pagination<T> PaginatedResponse<T>(
           List<T> items,
           int totalCount,
           int pageNumber,
           int pageSize)
        {
            return new Pagination<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public static async Task<Pagination<TDestination>> PaginateAndMap<TSource, TDestination>(
            IQueryable<TSource> query,
            int pageNumber,
            int pageSize,
            IMapper mapper)
        {
            var totalCount = await query.CountAsync();
            var items = await PaginateAndProject<TSource, TDestination>(query, pageNumber, pageSize, mapper);
            return PaginatedResponse(items, totalCount, pageNumber, pageSize);
        }
    }
}
