using System.IO;
using System.Text;
using CommonMark;
using CommonMark.Syntax;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace gamingsuperpower.Services
{
    public class CommonMarkDotNetService : IMarkdownService
    {
        private readonly IHostingEnvironment _env;
        private readonly IFileProvider _files;

        public CommonMarkDotNetService(IHostingEnvironment env, IFileProvider files)
        {
            _env = env;
            _files = files;
        }

        public string ConvertMarkdownToHtml(string filename)
        {
            string markdownContent;

            IFileInfo file = _files.GetFileInfo(filename);

            using (var stream = file.CreateReadStream())
            using (var reader = new StreamReader(stream))
            {
                markdownContent = reader.ReadToEnd();
                //var output = await reader.ReadToEndAsync();
                //await context.Response.WriteAsync(output.ToString());
            }

            var commonMarkSettings = CommonMarkSettings.Default.Clone();
            commonMarkSettings.OutputDelegate = (doc, output, settings) =>
                    new CustomHtmlFormatter(output, settings).WriteDocument(doc);

            var htmlResult = CommonMarkConverter.Convert(markdownContent, commonMarkSettings);

            return htmlResult;
        }

        private class CustomHtmlFormatter : CommonMark.Formatters.HtmlFormatter
        {
            public CustomHtmlFormatter(System.IO.TextWriter target, CommonMarkSettings settings)
                : base(target, settings)
            {
            }

            protected override void WriteBlock(Block block, bool isOpening, bool isClosing, out bool ignoreChildNodes)
            {
                if ((block.Tag == BlockTag.FencedCode || block.Tag == BlockTag.IndentedCode)
                        && !this.RenderPlainTextInlines.Peek())
                {
                    ignoreChildNodes = false;
                    if (isOpening)
                    {
                        this.Write("<?prettify?>");
                    }
                }
                base.WriteBlock(block, isOpening, isClosing, out ignoreChildNodes);
            }
        }
    }
}