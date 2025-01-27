using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Perseverance_Calculator_1.Model
{

    public class GraphFormulaList_Properties : INotifyPropertyChanged
    {

        private string _graphTime="";
        private string _graphTimeOnPlay="";
        private string _graphStepX="";
        private string _graphStepY="";
        private string _graphScale="";
        private string _graphResolution="";
        private string _graphLocationX = "";
        private string _graphLocationY = "";
        
        private string _graphPointX = "";
        private string _graphPointY = "";

        private string _axisSpacing = "100";

        public string graphTime
        {
            get { return _graphTime; }
            set
            {
                _graphTime = value;
                RaisePropertyChanged("graphTime");
            }
        }
        public string graphTimeOnPlay
        {
            get { return _graphTimeOnPlay; }
            set
            {
                _graphTimeOnPlay = value;
                RaisePropertyChanged("graphTimeOnPlay");
            }
        }
        public string graphPointY
        {
            get { return _graphPointY; }
            set
            {
                _graphPointY = value;
                RaisePropertyChanged("graphPointY");
            }
        }
        

        public string graphPointX
        {
            get { return _graphPointX; }
            set
            {
                _graphPointX = value;
                RaisePropertyChanged("graphPointX");
            }
        }
        

        public string axisSpacing
        {
            get { return _axisSpacing; }
            set
            {
                _axisSpacing = value;
                RaisePropertyChanged("axisSpacing");
            }
        }
        
        public string graphStepX
        {
            get { return _graphStepX; }
            set
            {
                _graphStepX = value;
                RaisePropertyChanged("graphStepX");
            }
        }
        public string graphStepY
        {
            get { return _graphStepY; }
            set
            {
                _graphStepY = value;
                RaisePropertyChanged("graphStepY");
            }
        }
        

        public string graphScale
        {
            get { return _graphScale; }
            set
            {
                _graphScale = value;
                RaisePropertyChanged("graphScale");
            }
        }

        public string graphResolution
        {
            get { return _graphResolution; }
            set
            {
                _graphResolution = value;
                RaisePropertyChanged("graphResolution");
            }
        }

        public string graphLocationX
        {
            get { return _graphLocationX; }
            set
            {
                _graphLocationX = value;
                RaisePropertyChanged("graphLocationX");
            }
        }

        public string graphLocationY
        {
            get { return _graphLocationY; }
            set
            {
                _graphLocationY = value;
                RaisePropertyChanged("graphLocationY");
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

    public class GraphFormulaList : INotifyPropertyChanged
    {

        private string _formulaName;
        private int _comboBox_Color = 0;

        private string _XYValue;
        private string _XYValue_Output;

        public string XYValue
        {
            get { return _XYValue; }
            set
            {
                try
                {
                    _XYValue = value;
                    RaisePropertyChanged("XYValue");
                }
                catch { }
            }
        }
        public string XYValue_Output
        {
            get { return _XYValue_Output; }
            set
            {
                try
                {
                    _XYValue_Output = value;
                    RaisePropertyChanged("XYValue_Output");
                }
                catch { }
            }
        }
        public int comboBox_Color
        {
            get { return _comboBox_Color; }
            set
            {
                try
                {
                    _comboBox_Color = value;
                    RaisePropertyChanged("comboBox_Color");
                }
                catch { }
            }
        }
        private Visibility _visibility = Visibility.Visible;
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
