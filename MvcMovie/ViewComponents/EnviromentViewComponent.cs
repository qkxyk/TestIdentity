using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.ViewComponents
{
    public class EnviromentViewComponent:ViewComponent
    {
        public EnviromentViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
