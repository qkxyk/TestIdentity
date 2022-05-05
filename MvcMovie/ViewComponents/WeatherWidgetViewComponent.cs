using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Views.Shared.Components.WeatherWidget
{
    public class WeatherWidgetViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string cityName)
        {
            return View("Default", cityName);
        }
    }
}
