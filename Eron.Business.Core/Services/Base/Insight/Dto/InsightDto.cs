using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;

namespace Eron.Business.Core.Services.Base.Insight.Dto
{
    public class InsightDto: EntityDto<long>
    {
        public DateTime DateTime { get; set; }

        public double? Value { get; set; }
    }

    public class MonthInsightDto: EntityDto<long>
    {
        public DateTime DateTime { get; set; }

        public double? Value { get; set; }
    }
}
