using Microsoft.AspNetCore.Identity;

namespace ExamBilet2.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
