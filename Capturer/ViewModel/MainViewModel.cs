using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Library;
using System.Collections.ObjectModel;
using System.Timers;
using System;
using System.Xml;
using System.IO;

namespace Capturer.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Objects.Log> logsList;
        public ObservableCollection<Objects.Log> LogsList
        {
            get { return logsList; }
            set
            {
                logsList = value;
                RaisePropertyChanged("LogsList");
            }
        }

        public RelayCommand NewCommand { get; set; }


        void InitiateCommands()
        {
            NewCommand = new RelayCommand(new_Task, canCreateNewTask);
        }

        private bool canCreateNewTask()
        {
            return true;
        }

        private void new_Task()
        {
            Objects.Task t = new Objects.Task();
            Objects.Log log = new Objects.Log();
            log.text = t.GetDuration();
            log.type = 1;
            LogsList.Add(log);
        }

        void load_data()
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (File.Exists(Directory.GetCurrentDirectory() + "config.xml")) xmlDoc.LoadXml(Directory.GetCurrentDirectory() + "config.xml");
        }

        public MainViewModel()
        {
            InitiateCommands();
            logsList = new ObservableCollection<Objects.Log>();            
        }

        
    }
}