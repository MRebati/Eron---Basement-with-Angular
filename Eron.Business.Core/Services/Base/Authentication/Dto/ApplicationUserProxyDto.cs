using System.ComponentModel.DataAnnotations;
using AutoMapper.Attributes;
using Eron.Core.Entities.User;

namespace Eron.Business.Core.Services.Base.Authentication.Dto
{
    [MapsFrom(typeof(ApplicationUser), ReverseMap = true)]
    [MapsFrom(typeof(ApplicationUserCreateDto), ReverseMap = true)]
    public class ApplicationUserProxyDto
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

        public string[] SelectedRoles { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "این فیلد با فیلد رمز عبور همخوانی ندارد")]
        public string ConfirmPassword { get; set; }
    }
}