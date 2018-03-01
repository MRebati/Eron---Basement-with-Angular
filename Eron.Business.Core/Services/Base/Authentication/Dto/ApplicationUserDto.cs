using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Core.Entities.User;

namespace Eron.Business.Core.Services.Base.Authentication.Dto
{
    [MapsFrom(typeof(ApplicationUser), ReverseMap = true)]
    public class ApplicationUserDto : EntityDto<string>
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string PostalCode { get; set; }

        public string SocialNumber { get; set; }

        public string Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public string ImageId { get; set; }

        public string CompanyName { get; set; }

        public string CityName { get; set; }

        public string ProvinceName { get; set; }

        public string FaxNumber { get; set; }
    }

    [MapsFrom(typeof(ApplicationUser), ReverseMap = true)]
    public class ApplicationUserSummeryDto: EntityDto<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string ProvinceName { get; set; }

        public string CityName { get; set; }
    }

}
