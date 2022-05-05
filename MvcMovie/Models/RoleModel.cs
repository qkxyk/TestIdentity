using Microsoft.AspNetCore.Identity;

namespace MvcMovie.Models
{
    public class RoleModel:IdentityRole
    {
        public bool IsAdmin { get; set; }=false;
    }
}
