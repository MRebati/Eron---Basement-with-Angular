using System;
using System.ComponentModel.DataAnnotations.Schema;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Financial.Order;
using Eron.Core.Entities.Financial.Shop;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Base
{
    public class EronFile : Entity<Guid>
    {
        public string FileName { get; set; }

        public string FileUrl { get; set; }

        public byte[] FileData { get; set; }

        public FileType FileType { get; set; }

        public DateTime UploadDateTime { get; set; }

        public bool Deleted { get; set; }

        #region Navigation

        public long? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public Guid? OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        #endregion
    }
}