using System;
using Eron.Core.Infrastructure;

namespace Eron.Business.Core.Infrastructure
{
    public class EntityDto<TPrimaryKey>: CommonDto, IEntityDto
    {
        public EntityDto()
        {
        }

        public EntityDto(TPrimaryKey id)
        {
            this.Id = id;
        }
        
        public TPrimaryKey Id { get; set; }

        public DateTime? CreateDateTime { get; set; }
    }
}