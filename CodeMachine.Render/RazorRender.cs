using System;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;

namespace CodeMachine.Render
{
    public class RazorRender : IRender
    {
        public string Render(string templateName, string templatePath,object model)
        {
            string template = File.ReadAllText(templatePath);

            return RenderByContent(templateName, template, model);

        }

        public string RenderByContent(string templateName, string templateContent, object model)
        {
            var cached = Engine.Razor.IsTemplateCached(templateName, model.GetType());
            if (cached)
            {
               return Engine.Razor.RunCompile(templateName, model.GetType(), model);
            }

            return Engine.Razor.RunCompile(templateContent, templateName, model.GetType(), model);
        }
    }
}
