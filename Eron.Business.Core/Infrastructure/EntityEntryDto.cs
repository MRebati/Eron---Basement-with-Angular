using System;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Eron.Business.Core.Infrastructure
{
    public class EntityEntryDto<TPrimaryKey> : EntityDto<TPrimaryKey>, IEntityEntryDto
    {
        public EntityEntryDto()
        {
            this.CreateDateTime = DateTime.Now;
        }

        //public bool IsUpdateDto
        //{
        //    get { return IsUpdateEntry(); }
        //    set
        //    {
        //        if (!IsUpdateEntry())
        //        {
        //            CreatorUserId = User.Identity.GetUserId();
        //        }
        //        else
        //        {
        //            value = IsUpdateEntry();
        //        }
        //    }
        //}

        //public string CreatorUserId { get; set; }

        public bool IsUpdateEntry()
        {
            return !Compare(Id, default(TPrimaryKey));
        }

        private bool Compare<TPrimaryKey>(TPrimaryKey x, TPrimaryKey y)
        {
            try
            {
                return x.ToString() == y.ToString();
            }
            catch
            {
                return false;
            }
            
        }
    }
}