using System;
using System.Collections.Generic;

namespace Identity.API.ViewModels
{
    public class UserTokenViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }
}