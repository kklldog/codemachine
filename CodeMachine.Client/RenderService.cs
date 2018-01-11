using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodeMachine.Db.Models;
using CodeMachine.Render;

namespace CodeMachine.Client
{
    public class RenderService
    {
        private IRender _render = new RazorRender();
        public void Output(string templateName, string nameTemplate,
            string contentTemplate, Table model,
            string outputDir, Action<string> logCallback, Action complate)
        {
            var fileName = RenderFileName(templateName, nameTemplate, model);
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = model.Name + templateName;
            }
            var content = RenderContent(templateName, contentTemplate, model);

            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
            outputDir += "/" + templateName;
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            var path = outputDir + "/" + fileName;

            File.WriteAllText(path, content);

            if (logCallback != null)
            {
                logCallback(path + "生成成功.");
            }

            if (complate != null)
            {
                complate();
            }

        }

        private string RenderFileName(string templateName, string template, Table model)
        {
            if (string.IsNullOrEmpty(template))
            {
                return "";
            }

            var fileName = _render.RenderByContent(string.Format("fileName_{0}", templateName), template, model);

            return fileName;
        }
        private string RenderContent(string templateName, string template, Table model)
        {
            var content = _render.RenderByContent(string.Format("content_{0}", templateName), template, model);

            return content;
        }

        private static RenderService _Instance;

        public static RenderService Instance
        {
            get { return _Instance ?? (_Instance = new RenderService()); }
        }

        private RenderService()
        {

        }
    }
}
