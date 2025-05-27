using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebService.API.Data.Entity
{
    public class User: IdentityUser
    {
        public User()
        {
        }

        public User(string userName) : base(userName)
        {
        }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? SicilNo { get; set; }
        public bool? IsLdap { get; set; }

        public string? CreatedBy { get; set; }

        public string? ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public bool? IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
