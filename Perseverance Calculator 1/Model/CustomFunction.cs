using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perseverance_Calculator_1.Model.MVVM;
using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml;
using System.Runtime.CompilerServices;

namespace Perseverance_Calculator_1.Model
{
    public class CustomFunction : INotifyPropertyChanged
    {
        public CustomFunction() { }
        public CustomFunction(
            Formula formulaObj,
            string use,
            //string functionTab_IndexLocation,
            bool ignoreFunctionForGraph = false,
            string foregroundColor = null,

            string formulaName=null,
            string formula = null,
            string rearrangedFormula = null,
            string description = null,
            ObservableCollection<VariableData> varData = null)
        {
            this.formulaObj = new Formula(formulaObj);
            this.formulaName = formulaName;
            this.formula = formula;
            this.rearrangedFormula = rearrangedFormula;
            this.use = use;
            this.description = description;
            this.ignoreFunctionForGraph = ignoreFunctionForGraph;
            //this.functionTab_IndexLocation = functionTab_IndexLocation;
            if (foregroundColor != null)
            {
                this.foregroundColor = foregroundColor;
            }
            if (varData != null)
                foreach (VariableData v in varData)
                {
                    this.variableList_Bind.Add(v);
                    this.variableList.Add(v.name, v);
                }
        }
        private Visibility _visibility = Visibility.Visible;

        private Formula _formulaObj;
        private string _formulaName;//used for loading previous version file
        private string _formula;//used for loading previous version file
        private string _rearrangedFormula;//used for loading previous version file
        private string _use;
        private string _description;//used for loading previous version file
        //private string _functionTab_IndexLocation;
        private string _previousFunctionName_OnEdit;

        private bool _rearrange_UseAnotherFunction = false;
        private string _rearrange_UseAnotherFunction_Function;
        private bool _rearrange_ChangeOtherSide_ToPlusMinus = false;
        private bool _rearrange_ReverseInequalitySign = false;

        //all graphs = blue //null
        //2d graphs = green //#FF0EFF0E
        //3d graphs = orange //#FFFF6900
        //private Brush _foregroundColor = new SolidColorBrush(Windows.UI.Color.FromArgb(
        //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FF0090FF")).A,
        //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FF0090FF")).R,
        //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FF0090FF")).G,
        //    ((System.Drawing.Color)new ColorConverter().ConvertFromString("#FF0090FF")).B

        //    ));//hex is blue

        private string _foregroundColor = "#FF0090FF";


        //use for graphs
        private bool _ignoreFunctionForGraph = false;
        //variablename, obj
        private Dictionary<string, VariableData> variableList = new Dictionary<string, VariableData>();//used for loading previous version file
        private ObservableCollection<VariableData> _variableList_Bind = new ObservableCollection<VariableData>();//used for loading previous version file
        public ObservableCollection<VariableData> variableList_Bind
        {
            get { return _variableList_Bind; }
            set
            {
                _variableList_Bind = value;
                RaisePropertyChanged("variableList_Bind");
            }
        }

        public Formula formulaObj
        {
            get { return _formulaObj; }
            set
            {
                _formulaObj = value;
                RaisePropertyChanged("formulaObj");
            }
        }
        public bool rearrange_UseAnotherFunction
        {
            get { return _rearrange_UseAnotherFunction; }
            set
            {
                _rearrange_UseAnotherFunction = value;
                RaisePropertyChanged("rearrange_UseAnotherFunction");
            }
        }
        public string rearrange_UseAnotherFunction_Function
        {
            get { return _rearrange_UseAnotherFunction_Function; }
            set
            {
                _rearrange_UseAnotherFunction_Function = value;
                RaisePropertyChanged("rearrange_UseAnotherFunction_Function");
            }
        }
        public bool rearrange_ChangeOtherSide_ToPlusMinus
        {
            get { return _rearrange_ChangeOtherSide_ToPlusMinus; }
            set
            {
                _rearrange_ChangeOtherSide_ToPlusMinus = value;
                RaisePropertyChanged("rearrange_ChangeOtherSide_ToPlusMinus");
            }
        }
        public bool rearrange_ReverseInequalitySign
        {
            get { return _rearrange_ReverseInequalitySign; }
            set
            {
                _rearrange_ReverseInequalitySign = value;
                RaisePropertyChanged("rearrange_ReverseInequalitySign");
            }
        }

        public string previousFunctionName_OnEdit
        {
            get { return _previousFunctionName_OnEdit; }
            set
            {
                _previousFunctionName_OnEdit = value;
                RaisePropertyChanged("previousFunctionName_OnEdit");
            }
        }
        //public string functionTab_IndexLocation
        //{
        //    get { return _functionTab_IndexLocation; }
        //    set
        //    {
        //        _functionTab_IndexLocation = value;
        //        RaisePropertyChanged("functionTab_IndexLocation");
        //    }
        //}

        public bool ignoreFunctionForGraph
        {
            get { return _ignoreFunctionForGraph; }
            set
            {
                _ignoreFunctionForGraph = value;
                RaisePropertyChanged("ignoreFunctionForGraph");
            }
        }
        public string foregroundColor
        {
            get { return _foregroundColor; }
            set
            {
                _foregroundColor = value;
                RaisePropertyChanged("foregroundColor");
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
        public string use
        {
            get { return _use; }
            set
            {
                _use = value;
                RaisePropertyChanged("use");
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
