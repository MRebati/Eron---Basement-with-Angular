using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eron.Presentation.WebApplication.WebApi.Infrastructure
{
    public class EronWebApiResponse<T>
    {
        public T Data { get; set; }

        public string[] Error { get; set; }

        public string Message { get; set; }
    }
}