using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Perseverance_Calculator_1.Model
{
    public class DataData : INotifyPropertyChanged
    {
        //Dictionary<string, string> dataValueList = new Dictionary<string, string>();
        private Visibility _searchVisibility = Visibility.Visible;
        private bool _dataChanged = false;
        private string _dataNamePrev;
        private string _valuePrev;
        private string _dataName;
        private string _value;
        private string _description;
        public Visibility searchVisibility
        {
            get { return _searchVisibility; }
            set
            {
                _searchVisibility = value;
                RaisePropertyChanged("searchVisibility");
            }
        }
        public string dataNamePrev
        {
            get { return _dataNamePrev; }
            set
            {
                _dataNamePrev = value;
                RaisePropertyChanged("dataNamePrev");
            }
        }
        public string dataName
        {
            get { return _dataName; }
            set
            {
                _dataName = value;
                RaisePropertyChanged("dataName");
            }
        }
        public string valuePrev
        {
            get { return _valuePrev; }
            set
            {
                _valuePrev = value;
                RaisePropertyChanged("valuePrev");
            }
        }
        public string value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("value");
            }
        }
        public string description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged("description");
            }
        }
        public bool dataChanged
        {
            get { return _dataChanged; }
            set
            {
                _dataChanged = value;
                RaisePropertyChanged("dataChanged");
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
