using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace gamingsuperpower.Pages
{
    public class ErrorModel : PageModel
    {
        public string Status;
        public string Description;

        public void OnGet(int id = 0)
        {
            if(id == 0)
            {
                Status = "Kablammo!!!";
                Description = "We're sorry, we'll try harder next time!";
            }
            else
            {
                Status = id.ToString() + " - " + ReasonPhrases.GetReasonPhrase(id);
                Description = "We're sorry, we'll try harder next time!";
            }
        }
    }
}
