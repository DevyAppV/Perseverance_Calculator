using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.ConversationalAgent;
using static System.Net.Mime.MediaTypeNames;

namespace Perseverance_Calculator_1.Model.MVVM
{
    public class FunctionCollection_MVVM_Selection
    {
        public static int functionCollection_Selection = -1;
    }
    public class FunctionCollectionTab_MVVM
    {
        public static ObservableCollection <FunctionCollection> functionCollectionTab = new ObservableCollection<FunctionCollection>();

        //function name + "(", customFunction
        public static Dictionary<string, CustomFunction> customFunctionCollection_Dictionary = new Dictionary<string, CustomFunction>();
    }

    public class FunctionCollection : INotifyPropertyChanged
    {
        private string _functionTabName;
        private ObservableCollection<CustomFunction> _customFunctionCollection_Bind = new ObservableCollection<CustomFunction>();
        //tab

        public string functionTabName
        {
            get { return _functionTabName; }
            set
            {
                _functionTabName = value;
                RaisePropertyChanged("functionTabName");
            }
        }

        public ObservableCollection<CustomFunction> customFunctionCollection_Bind
        {
            get { return _customFunctionCollection_Bind; }
            set
            {
                _customFunctionCollection_Bind = value;
                RaisePropertyChanged("customFunctionCollection_Bind");
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
