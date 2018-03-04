using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileProviders;

using gamingsuperpower.Services;

namespace gamingsuperpower.Pages
{
    public class MarkdownPageModel : PageModel
    {
        private readonly IFileProvider _files;
        private readonly IMarkdownService _md;

        public MarkdownPageModel(IFileProvider files, IMarkdownService md)
        {
            _files = files;
            _md = md;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public bool SectionExists(string section)
        {
            var markdownDoc = "markdown" + Request.Path + "/" + section + ".md";

            return _files.GetFileInfo(markdownDoc).Exists;
        }

        public HtmlString GetSection(string section)
        {
            //
            // Note: This method assumes you have already called SectionExists in the razor page and it returned true
            //

            var markdownDoc = "markdown" + Request.Path + "/" + section + ".md";

            return new HtmlString(_md.ConvertMarkdownToHtml(markdownDoc));
        }
    }
}
