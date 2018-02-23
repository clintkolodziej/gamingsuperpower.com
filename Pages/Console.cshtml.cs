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
    public class ConsoleModel : PageModel
    {
        private readonly IFileProvider _files;
        private readonly IMarkdownService _md;
        private string console;
        public string revision;

        public ConsoleModel(IFileProvider files, IMarkdownService md)
        {
            _files = files;
            _md = md;
        }

        public IActionResult OnGet(string id, string rev)
        {
            IDirectoryContents file;

            // get the appropriate markdown folder for the console and revision to interrogate if it exists
            if(rev != null)
                file = _files.GetDirectoryContents("markdown/consoles/" + id + "/" + rev);
            else
                file = _files.GetDirectoryContents("markdown/consoles/" + id);

            // if revision is invalid redirect to generic console
            if(rev != null && !file.Exists)
                return Redirect("/console/" + id);

            // if generic console is invalid redirect to home page
            if(!file.Exists)
                return Redirect("/");

            console = id;
            revision = rev;

            return Page();
        }

        public bool SectionExists(string section)
        {
            var revisionMD = "markdown/consoles/" + console + "/"+ revision + "/" + section + ".md";
            var consoleMD = "markdown/consoles/" + console + "/" + section + ".md";

            // If there is no revision, return if the generic console markdown file exists
            if (revision == null)
                return _files.GetFileInfo(consoleMD).Exists;

            // If there is a revision, return if either the revision markdown file exists or the generic console markdown exists
            return _files.GetFileInfo(revisionMD).Exists || _files.GetFileInfo(consoleMD).Exists;
        }

        public HtmlString GetSection(string section)
        {
            //
            // Note: This method assumes you have already called SectionExists in the razor page and it returned true
            //

            var revisionMD = "markdown/consoles/" + console + "/"+ revision + "/" + section + ".md";
            var consoleMD = "markdown/consoles/" + console + "/" + section + ".md";

            // If there is a revision and the revision markdown file exists then return it
            if (revision != null && _files.GetFileInfo(revisionMD).Exists)
                return new HtmlString(_md.ConvertMarkdownToHtml(revisionMD));

            // Otherwise just return the markdown file for the generic console
            else
                return new HtmlString(_md.ConvertMarkdownToHtml(consoleMD));
        }
    }
}
