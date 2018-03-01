using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nelibur.ObjectMapper;

namespace Eron.SharedKernel.Helpers.Mapper
{
    public static class ObjectMapper
    {
        public static TTarget MapTo<TTarget>(this object data)
        {
            return AutoMapper.Mapper.Map<TTarget>(data);
            //return TinyMapper.Map<TTarget>(data);
        }
    }
}
