using AutoMapper.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Core.ValueObjects;

namespace Eron.Business.Core.Services.Common.Dto
{
    [MapsFrom(typeof(SelectListObject))]
    public class SelectListObjectDto
    {
        [MapsToAndFromProperty(typeof(SelectListObject), "Id")]
        public string Value { get; set; }

        [MapsToAndFromProperty(typeof(SelectListObject), "DisplayName")]
        public string Title { get; set; }

        public bool IsSelected { get; set; }
    }
}
