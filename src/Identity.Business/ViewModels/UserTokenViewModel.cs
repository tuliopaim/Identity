namespace Identity.Business.ViewModels
{
    public class UserTokenViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }
}