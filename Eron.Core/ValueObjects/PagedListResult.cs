using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Eron.Core.ManagementSettings;

namespace Eron.Core.ValueObjects
{
    public class PagedListResult<T>: IPagedListResult<T>
    {
        public List<T> Result { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int TotalCount { get; set; }

        public IPagedListResult<T> Create(IEnumerable<T> input,int totalCount, int pageSize = ApplicationSettings.Pagination.PageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            Result = input.ToList();
            return this;
        }

        public IPagedListResult<T> Create(IEnumerable<T> input)
        {
            this.Result = input.ToList();
            return this;
        }
    }
}