using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Perseverance_Calculator_1.Model.MVVM
{
    //dont save
    public class FormulaCollection_MVVM_Selection
    {
        public static int selectedProjectIndex = -1;
    }
    //MVVM - SAVE
    public class FormulaProject_MVVM
    {
        public static ObservableCollection<FormulaProjectExplorer> formulaProject = new ObservableCollection<FormulaProjectExplorer>();
    }

    //MVVM
    public class FormulaProjectExplorer : INotifyPropertyChanged
    {
        private string _projectName;

        private ObservableCollection<Formula> _formulaCollection = new ObservableCollection<Formula>();
        private ObservableCollection<GraphFormulaList> _graphFormulaList = new ObservableCollection<GraphFormulaList>();
        private ObservableCollection<GraphFormulaList_Properties> _graphFormulaList_Properties = new ObservableCollection<GraphFormulaList_Properties>() { /*new GraphFormulaList_Properties()*/ };
        private ObservableCollection<Graph2DFormulaList> _graph2DFormulaList = new ObservableCollection<Graph2DFormulaList>();
        private ObservableCollection<Graph2DFormulaList_Properties> _graph2DFormulaList_Properties = new ObservableCollection<Graph2DFormulaList_Properties>() { /*new Graph2DFormulaList_Properties()*/ };
        private ObservableCollection<Graph3DFormulaList> _graph3DFormulaList = new ObservableCollection<Graph3DFormulaList>();
        private ObservableCollection<Graph3DFormulaList_Properties> _graph3DFormulaList_Properties = new ObservableCollection<Graph3DFormulaList_Properties>() { /*new Graph2DFormulaList_Properties()*/ };


        public ObservableCollection<Graph3DFormulaList_Properties> graph3DFormulaList_Properties
        {
            get { return _graph3DFormulaList_Properties; }
            set
            {
                _graph3DFormulaList_Properties = value;
                RaisePropertyChanged("graph3DFormulaList_Properties");
            }
        }

        public ObservableCollection<Graph3DFormulaList> graph3DFormulaList
        {
            get { return _graph3DFormulaList; }
            set
            {
                _graph3DFormulaList = value;
                RaisePropertyChanged("graph3DFormulaList");
            }
        }

        public ObservableCollection<Graph2DFormulaList_Properties> graph2DFormulaList_Properties
        {
            get { return _graph2DFormulaList_Properties; }
            set
            {
                _graph2DFormulaList_Properties = value;
                RaisePropertyChanged("graph2DFormulaList_Properties");
            }
        }
        public ObservableCollection<Graph2DFormulaList> graph2DFormulaList
        {
            get { return _graph2DFormulaList; }
            set
            {
                _graph2DFormulaList = value;
                RaisePropertyChanged("graph2DFormulaList");
            }
        }
        
        public ObservableCollection<GraphFormulaList_Properties> graphFormulaList_Properties
        {
            get { return _graphFormulaList_Properties; }
            set
            {
                _graphFormulaList_Properties = value;
                RaisePropertyChanged("graphFormulaList_Properties");
            }
        }
        
        public ObservableCollection<GraphFormulaList> graphFormulaList
        {
            get { return _graphFormulaList; }
            set
            {
                _graphFormulaList = value;
                RaisePropertyChanged("graphFormulaList");
            }
        }
        
        public ObservableCollection<Formula> formulaCollection
        {
            get { return _formulaCollection; }
            set
            {
                _formulaCollection = value;
                RaisePropertyChanged("formulaCollection");
            }
        }
        public string projectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                RaisePropertyChanged("projectName");
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
