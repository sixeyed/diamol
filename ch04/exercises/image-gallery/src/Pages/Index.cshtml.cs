using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace src.Pages
{
    public class IndexModel : PageModel
    {
        public string ImageUrl {get; private set;}
        public string Caption {get; private set;}
        public string Copyright {get; private set;}

        public void OnGet()
        {
            ImageUrl = "https://www.youtube.com/embed/YYxPw_T8Vlk";
            Caption ="Virtual Flight over Asteroid Vesta";
        }
    }
}
