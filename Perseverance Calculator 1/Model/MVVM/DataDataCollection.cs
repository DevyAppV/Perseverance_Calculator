using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perseverance_Calculator_1.Model;

namespace Perseverance_Calculator_1.Model.MVVM
{
    public class DataDataCollection_Project_MVVM_Selection
    {
        public static int dataDataCollectionProject_Selection=-1;

    }
    public class DataDataCollection_Project_MVVM
    {
        //dataName, dataValue
        public static Dictionary<string, string> dataValueList = new Dictionary<string, string>();
        public static ObservableCollection<DataDataCollection> dataDataCollectionProject = new ObservableCollection<DataDataCollection>();

    }

    public class DataDataCollection : INotifyPropertyChanged
    {

        //public DataDataCollection(string projectName)
        //{
        //    this.projectName = projectName;
        //}
        private string _projectName;
        private ObservableCollection<DataData> _dataDataCollection = new ObservableCollection<DataData>();

        public string projectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                RaisePropertyChanged("projectName");
            }
        }
        public ObservableCollection<DataData> dataDataCollection
        {
            get { return _dataDataCollection; }
            set
            {
                _dataDataCollection = value;
                RaisePropertyChanged("dataDataCollection");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
