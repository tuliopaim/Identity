using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
    }
}