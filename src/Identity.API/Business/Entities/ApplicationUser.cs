using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Business.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
    }
}