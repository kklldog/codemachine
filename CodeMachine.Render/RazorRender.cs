using System;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;

namespace CodeMachine.Render
{
    public class RazorRender : IRender
    {
        /// <summary>
        /// 渲染
        /// </summary>
        /// <param name="templateName">模板名称</param>
        /// <param name="templatePath">模板文件路径</param>
        /// <param name="model">Viewmodel</param>
        /// <returns></returns>
        public string Render(string templateName, string templatePath,object model)
        {
            string template = File.ReadAllText(templatePath);

            return RenderByContent(templateName, template, model);

        }

        /// <summary>
        /// 渲染
        /// </summary>
        /// <param name="templateName">模板名称</param>
        /// <param name="templateContent">模板文件内容</param>
        /// <param name="model">Viewmodel</param>
        /// <returns></returns>
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
