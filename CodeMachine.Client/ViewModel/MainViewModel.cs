using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Input;
using CodeMachine.Db.Models;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Threading;
using System.Collections.ObjectModel;

namespace CodeMachine.Client.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            SelectAll = new RelayCommand(() =>
            {
                foreach (var vmTable in _tables)
                {
                    vmTable.IsChecked = true;
                }
            });
            UnSelectAll = new RelayCommand(() =>
            {
                foreach (var vmTable in _tables)
                {
                    vmTable.IsChecked = false;
                }
            });
            RenderCode = new RelayCommand(() =>
            {
                var renderService = RenderService.Instance;
                var templates = _templates.Where(t => t.IsChecked.HasValue && t.IsChecked.Value);
                var tables = _tables.Where(t => t.IsChecked.HasValue && t.IsChecked.Value);
                Task.Run(() =>
                {
                    try
                    {
                        foreach (var templateViewModel in templates)
                        {
                            foreach (var tableViewModel in tables)
                            {
                                renderService.Output(templateViewModel.Name,
                                    templateViewModel.FileNameTemplate,
                                    templateViewModel.ContentTemplate,
                                    tableViewModel.Table, _outPutDir, (log) =>
                                    {
                                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                        {
                                            this.Logs.Insert(0, new LogViewModel()
                                            {
                                                Text = log
                                            });
                                        });
                                    }, null);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            this.Logs.Insert(0, new LogViewModel()
                            {
                                Text = e.Message
                            });
                            this.Logs.Insert(0, new LogViewModel()
                            {
                                Text = e.StackTrace
                            });
                        });
                    }
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        this.Logs.Insert(0,new LogViewModel()
                        {
                            Text = "全部完成."
                        });
                    });
                });
               
            });
            InitData();

        }

        private void InitData()
        {
            const string connName = "defaultConn";
            var conn = ConfigurationManager.ConnectionStrings[connName];
            this.Connection = string.Format("Conn = {0} \r\n Provider = {1}", conn.ConnectionString, conn.ProviderName);
            var dbDissecter = Db.DbDissecter.Get(connName);
            var db = dbDissecter.Get();
            _tables = new List<TableViewModel>();
            foreach (var dbTable in db.Tables)
            {
                _tables.Add(new TableViewModel()
                {
                    IsChecked = true,
                    Name = dbTable.Name,
                    Table = dbTable
                });
            }
            _templates = new List<TemplateViewModel>();
            var templateDir = AppDomain.CurrentDomain.BaseDirectory + "templates";
            foreach (var filePath in Directory.GetFiles(templateDir))
            {
                var file = new FileInfo(filePath);
                var lines = File.ReadAllLines(filePath);
                var vm = new TemplateViewModel()
                {
                    IsChecked = true,
                    Name = file.Name,
                    FileNameTemplate = lines[0]
                };
                for (int i = 1; i < lines.Length; i++)
                {
                    vm.ContentTemplate += lines[i] + "\r\n";
                }
                _templates.Add(vm);
            }
            _logs = new ObservableCollection<LogViewModel>();
            _outPutDir = AppDomain.CurrentDomain.BaseDirectory + "output";
        }

        private List<TemplateViewModel> _templates;
        public List<TemplateViewModel> Templates
        {
            get { return _templates; }
            set { Set(ref _templates, value); }
        }

        private string _connection;
        public string Connection
        {
            get { return _connection; }
            set { Set(ref _connection, value); }
        }


        private ObservableCollection<LogViewModel> _logs;
        public ObservableCollection<LogViewModel> Logs
        {
            get { return _logs; }
            set { Set(ref _logs, value); }
        }

        private List<TableViewModel> _tables;
        public List<TableViewModel> Tables
        {
            get { return _tables; }
            set { Set(ref _tables, value); }
        }

        private string _outPutDir;
        public string OutPutDir
        {
            get { return _outPutDir; }
            set { Set(ref _outPutDir, value); }
        }

        public ICommand SelectAll { get; set; }
        public ICommand UnSelectAll { get; set; }

        public ICommand RenderCode { get; set; }

    }
}