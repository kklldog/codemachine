using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeMachine.Db.Models;
using GalaSoft.MvvmLight;

namespace CodeMachine.Client.ViewModel
{
    public class TemplateViewModel : ViewModelBase
    {
        public string Name { get; set; }

        public string FileNameTemplate { get; set; }

        public string ContentTemplate { get; set; }

        private bool? _IsChecked;

        public bool? IsChecked
        {
            get { return _IsChecked; }
            set { Set(ref _IsChecked, value); }
        }
    }
}
