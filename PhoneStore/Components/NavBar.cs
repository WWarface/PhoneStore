using Microsoft.AspNetCore.Mvc;
namespace PhoneStore.Components
{
    public class NavBar : ViewComponent
    {
        private readonly List<string> _keys;
        public NavBar()
        {
            _keys = new List<string>()
            {
                "Home","About","Brand","Specials","Contact"
            };
        }

        public IViewComponentResult Invoke()
        {
            return View(_keys);
        }
    }
}
