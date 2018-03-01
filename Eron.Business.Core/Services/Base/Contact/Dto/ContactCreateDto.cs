using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Core.Entities.Financial.Base;

namespace Eron.Business.Core.Services.Base.Contact.Dto
{
    [MapsFrom(typeof(UserMessage), ReverseMap = true)]
    public class ContactCreateDto: EntityEntryDto<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string EmailAddress { get; set; }

        public bool Seen { get; set; }

        public string Recaptcha { get; set; }
    }
}
