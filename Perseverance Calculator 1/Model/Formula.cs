using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace Perseverance_Calculator_1.Model
{



    //============================================================== Formula data
    public class VariableData : INotifyPropertyChanged
    {
        public VariableData() { }
        public VariableData(
        //string variable,
        string name,
        string value = "",
        string description = "")
        {
            //this.variable = variable;
            //this.name = name;
            this.name = name;
            this.value = value;
            this.description = description;
        }
        //private string _variable;
        private string _name;
        private string _value;
        private string _description;

        //public string variable
        //{
        //    get { return _variable; }
        //    set
        //    {
        //        _variable = value;
        //        RaisePropertyChanged("variable");
        //    }
        //}
        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("name");
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


        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    };
    public class Formula : INotifyPropertyChanged
    {
        public Formula() { }
        public Formula(Formula formula)
        {
            this.formulaName = formula.formulaName;
            this.formula = formula.formula;
            this.rearrangedFormula = formula.rearrangedFormula;
            this.solution = formula.solution;
            this.description = formula.description;

            if (formula.variableList_Bind != null)
                foreach (VariableData v in formula.variableList_Bind)
                {
                    this.variableList_Bind.Add(v);
                    this.variableList_Dictionary.Add(v.name, v);
                }
        }
        public Formula(string formulaName,
        string formula,
        string rearrangedFormula,
        string solution,
        string description,
        //variablename, obj
        ObservableCollection<VariableData> variableList)
        {
            this.formulaName = formulaName;
            this.formula = formula;
            this.rearrangedFormula = rearrangedFormula;
            this.solution = solution;
            this.description = description;

            foreach (VariableData v in variableList)
            {
                this.variableList_Bind.Add(v);
                this.variableList_Dictionary.Add(v.name, v);
            }
        }
        private Visibility _visibility = Visibility.Collapsed;
        private Visibility _visibilitySearch = Visibility.Visible;
        //private int _comboBox_SolveType;
        private int _comboBox_SolutionType;
        private int _comboBox_Double_OR_AutoDecimal = 0;
        private int _comboBox_AsymptoteError_NoAsymptoteError = 0;
        private string _use;

        private string _formulaName;
        private string _formula;
        private string _rearrangedFormula;
        private string _solution;
        private string _description;
        //variablename, obj
        [XmlIgnoreAttribute]
        public Dictionary<string, VariableData> variableList_Dictionary = new Dictionary<string, VariableData>();

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

        public Visibility visibilitySearch
        {
            get { return _visibilitySearch; }
            set
            {
                _visibilitySearch = value;
                RaisePropertyChanged("visibilitySearch");
            }
        }
        public Visibility visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                RaisePropertyChanged("visibility");
            }
        }

        //public int comboBox_SolveType
        //{
        //    get { return _comboBox_SolveType; }
        //    set
        //    {
        //        try
        //        {
        //            _comboBox_SolveType = value;
        //            RaisePropertyChanged("comboBox_SolveType");
        //        }
        //        catch { }
        //    }
        //}
        public int comboBox_SolutionType
        {
            get { return _comboBox_SolutionType; }
            set
            {
                try
                {
                    _comboBox_SolutionType = value;
                    RaisePropertyChanged("comboBox_SolutionType");
                }
                catch { }
            }
        }
        public int comboBox_Double_OR_AutoDecimal
        {
            get { return _comboBox_Double_OR_AutoDecimal; }
            set
            {
                try
                {
                    _comboBox_Double_OR_AutoDecimal = value;
                    RaisePropertyChanged("comboBox_Double_OR_AutoDecimal");
                }
                catch { }
            }
        }
        public int comboBox_AsymptoteError_NoAsymptoteError
        {
            get { return _comboBox_AsymptoteError_NoAsymptoteError; }
            set
            {
                try
                {
                    _comboBox_AsymptoteError_NoAsymptoteError = value;
                    RaisePropertyChanged("comboBox_AsymptoteError_NoAsymptoteError");
                }
                catch { }
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
