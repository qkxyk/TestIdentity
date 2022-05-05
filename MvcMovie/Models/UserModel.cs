using Microsoft.AspNetCore.Identity;

namespace MvcMovie.Models
{
    public class UserModel : IdentityUser
    {
        public int Sex { get; set; }=0;
    }
}
