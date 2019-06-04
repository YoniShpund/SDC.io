using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SDC.io.Pages
{
    public class IndexModel : PageModel
    {
        public List<String> ModelList { get; set; }

        public void OnGet()
        {
            ModelList = new List<string> { "en-de", "de-en" };
        }
    }
}
