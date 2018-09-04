using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Library;
using System.Collections.ObjectModel;
using System.Timers;
using System;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.ServiceProcess;

namespace Capturer.ViewModel
{    
    public class MainViewModel : ViewModelBase
    {
        #region INotifyVariables

        public ObservableCollection<Types.Task> Tasks
        {
            get { return Tasker.Tasks.ToObservableCollection(); }
        }

        public ObservableCollection<Types.Log> LogsList
        {
            get { return Logger.Logs.ToObservableCollection(); }
        }

        private string destPath;
        public string DestPath
        {
            get { return destPath; }
            set
            {
                destPath = value;
                NewCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged("DestPath");
            }
        }

        private string ip;
        public string IP
        {
            get { return ip; }
            set
            {
                ip = value;
                NewCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged("IP");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NewCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged("Name");
            }
        }

        private int days;
        public int Days
        {
            get { return days; }
            set
            {
                days = value;
                NewCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged("Days");
            }
        }

        private int type;
        public int Type
        {
            get { return type; }
            set
            {
                type = value;
                NewCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged("Type");
            }
        }
        #endregion

        #region commands

        public RelayCommand NewCommand { get; set; }
        public RelayCommand BrowseCommand { get; set; }

        void CreateCommands()
        {
            NewCommand = new RelayCommand(new_Task, canCreateNewTask);
            BrowseCommand = new RelayCommand(browse);
        }

        private void browse()
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                DestPath = folderBrowserDialog1.SelectedPath;
            }
        }

        private bool canCreateNewTask()
        {
            if ((name != null) && (destPath != null) && (ip != null))
                if ((name.Length > 0) && (type != -1) && (destPath.Length > 0) && (ip.Length > 0) && (days >= 0)) return true;
            return false;
        }

        private void new_Task()
        {
            Types.Task t = new Types.Task();
            t.name = name;
            t.type = type;
            t._destinationPath = destPath;
            t._sourceIP = ip;
            Tasks.Add(t);

            Config.save(Tasks.ToList());
            ServiceCtrl.restartService();
        }

        #endregion

        public void SynchronizeView()
        {
            RaisePropertyChanged("Tasks");
            RaisePropertyChanged("LogsList");
            RaisePropertyChanged("Type");
            RaisePropertyChanged("Days");
            RaisePropertyChanged("Name");
            RaisePropertyChanged("IP");
            RaisePropertyChanged("DestPath");
        }

        public MainViewModel()
        {
            CreateCommands();
            Tasker.Tasks = Config.load();
            SynchronizeView();
        }
    }
}