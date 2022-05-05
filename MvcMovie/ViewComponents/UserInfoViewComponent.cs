using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.ViewComponents
{
    public class UserInfoViewComponent:ViewComponent
    {
        public UserInfoViewComponent()
        {

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
