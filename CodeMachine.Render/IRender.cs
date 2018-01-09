using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMachine.Render
{
    public interface IRender
    {
        string Render(string templateName, string templatePath, object model);
        string RenderByContent(string templateName, string templateContent, object model);

    }
}
