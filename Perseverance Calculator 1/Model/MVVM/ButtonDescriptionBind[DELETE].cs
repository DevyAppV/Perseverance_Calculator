using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.UI.Xaml;

namespace Perseverance_Calculator_1.Model.MVVM
{
    public class ButtonDescriptionBind
    {
        //public static ObservableCollection<ButtonDescription> _buttonsx = new ObservableCollection<ButtonDescription>();
        public static ButtonDescription_Bind buttonDescriptionBind = new ButtonDescription_Bind();
    }
    public class ButtonDescription_Bind : INotifyPropertyChanged
    {

        private string _use;

        private string _formulaName;
        private string _formula;
        private string _rearrangedFormula;
        private string _solution;
        private string _description;
        private string _functionTab_IndexLocation;

        private ObservableCollection<VariableData> _variableList_Bind = new ObservableCollection<VariableData>();
        public ObservableCollection<VariableData> variableList_Bind
        {
            get { return _variableList_Bind; }
            set
            {
                _variableList_Bind = value;
                RaisePropertyChanged("variableList_Bind");
            }
        }

        public string functionTab_IndexLocation
        {
            get { return _functionTab_IndexLocation; }
            set
            {
                _functionTab_IndexLocation = value;
                RaisePropertyChanged("functionTab_IndexLocation");
            }
        }

        public string use
        {
            get { return _use; }
            set
            {
                _use = value;
                RaisePropertyChanged("use");
            }
        }
        public string formulaName
        {
            get { return _formulaName; }
            set
            {
                _formulaName = value;
                RaisePropertyChanged("formulaName");
            }
        }
        public string formula
        {
            get { return _formula; }
            set
            {
                _formula = value;
                RaisePropertyChanged("formula");
            }
        }
        public string rearrangedFormula
        {
            get { return _rearrangedFormula; }
            set
            {
                _rearrangedFormula = value;
                RaisePropertyChanged("rearrangedFormula");
            }
        }
        public string solution
        {
            get { return _solution; }
            set
            {
                _solution = value;
                RaisePropertyChanged("solution");
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
