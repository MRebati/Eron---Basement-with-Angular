using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Core.ManagementSettings;
using Eron.Core.ValueObjects;

namespace Eron.SharedKernel.Helpers.ListExtensions
{
    public static class QueryableExtensions
    {
        public static IPagedListResult<T> ToPagedList<T>(this IQueryable<T> source, int totalCount, int pageSize = ApplicationSettings.Pagination.PageSize)
        {
            return new PagedListResult<T>().Create(source.ToList());
        }

        public static async Task<IPagedListResult<T>> ToPagedListAsync<T>(this IQueryable<T> source, int totalCount, int pageSize = ApplicationSettings.Pagination.PageSize)
        {
            return new PagedListResult<T>().Create(await source.ToListAsync(), totalCount, pageSize);
        }
    }
}
