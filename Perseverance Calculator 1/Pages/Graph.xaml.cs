using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Perseverance_Calculator.View.Pages;
using Perseverance_Calculator_1.Controller;
using Perseverance_Calculator_1.Model.MVVM;
using Perseverance_Calculator_1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Dispatching;
using Windows.System;
using System.Globalization;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Perseverance_Calculator_1.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Graph : Page
    {
        //CanvasDrawingSession drawingSession;
        //ApplicationView view;
        DispatcherTimer timer;
        double elapsedTimeMS = 0;
        bool updateElapsedTimeMS = false;
        int currentProject = -1;
        bool finishedGraphing = true;
        bool isPlaying = false;


        double prev_AxisSpacing = 100d;
        double prev_graphStepX = 1d;
        double prev_graphStepY = 1d;
        double prev_GraphScale = .05d;
        //double axisSpacing = 100d;
        //float xtime = 0;
        public Graph()
        {
            this.InitializeComponent();
            //view = ApplicationView.GetForCurrentView();
            //view.Consolidated += View_Consolidated;
            ViewPages.GraphView.Closed += Current_Closed;
            //ApplicationView.GetForCurrentView().Title = "Graph";
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            finishedGraphing = true;
            isPlaying = false;



            //this.random = new Random();
            this.timer = new DispatcherTimer();
            //1000/x = fps
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 200);//30fps = 33
            //this.timer.Interval = new TimeSpan(0, 0, 0, 0, 100);//10fps
            //this.timer.Interval = new TimeSpan(0, 0, 0, 0, 100);//10fps
            this.timer.Tick += timer_Tick;
            this.timer.Start();
        }
        //private async void View_Consolidated(ApplicationView sender, ApplicationViewConsolidatedEventArgs args)
        //{
        //    if (ViewPages.GraphView != null)
        //        await ViewPages.GraphView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //        {
        //            ViewPages.GraphView.CoreWindow.Close();
        //        });
        //}
        private void Current_Closed(object sender, WindowEventArgs e)
        {
            this.timer.Stop();
            this.timer.Tick -= timer_Tick;
            //ViewPages.GraphView.Close();
            ViewPages.GraphView = null;
        }

        private void timer_Tick(object sender, object e)
        {
            //this.timer.Stop();
            if (currentProject != FormulaCollection_MVVM_Selection.selectedProjectIndex)
            {
                if (FormulaCollection_MVVM_Selection.selectedProjectIndex == -1)
                {
                    currentProject = FormulaCollection_MVVM_Selection.selectedProjectIndex;

                    Binding bindFormulaCollection = new Binding();
                    bindFormulaCollection.Mode = BindingMode.OneTime;
                    //bindFormulaCollection.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    bindFormulaCollection.Source = null;

                    GraphList_ItemsControl.SetBinding(ItemsControl.ItemsSourceProperty, bindFormulaCollection);
                    GraphProperties_ItemsControl.SetBinding(ItemsControl.ItemsSourceProperty, bindFormulaCollection);

                    if (string.IsNullOrEmpty(TextBlock_GraphDirections.Text))
                    {
                        TextBlock_GraphDirections.Text = "In the Formula window add a new project and select it to begin graphing.";
                    }

                }
                else
                {
                    if (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties.Count <= 0)
                    {
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties.Add(new GraphFormulaList_Properties());
                    }


                    currentProject = FormulaCollection_MVVM_Selection.selectedProjectIndex;

                    Binding bindFormulaCollection = new Binding();
                    bindFormulaCollection.Mode = BindingMode.TwoWay;
                    bindFormulaCollection.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    bindFormulaCollection.Source = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList;

                    GraphList_ItemsControl.SetBinding(ItemsControl.ItemsSourceProperty, bindFormulaCollection);


                    Binding bindProp = new Binding();
                    bindProp.Mode = BindingMode.TwoWay;
                    bindProp.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    bindProp.Source = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties;
                    GraphProperties_ItemsControl.SetBinding(ItemsControl.ItemsSourceProperty, bindProp);

                    if (!string.IsNullOrEmpty(TextBlock_GraphDirections.Text))
                    {
                        TextBlock_GraphDirections.Text = "";
                    }
                }
            }
            else if (FormulaCollection_MVVM_Selection.selectedProjectIndex == -1)
            {

                if (string.IsNullOrEmpty(TextBlock_GraphDirections.Text))
                {
                    TextBlock_GraphDirections.Text = "In the Formula window add a new project and select it to begin graphing.";
                }

            }
            else if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
            {

                if (!string.IsNullOrEmpty(TextBlock_GraphDirections.Text))
                {
                    TextBlock_GraphDirections.Text = "";
                }

            }

            if (isPlaying)
            {
                try
                {
                    double graphTime = (!string.IsNullOrWhiteSpace(FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphTimeOnPlay))
                        ? double.Parse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphTimeOnPlay, false), System.Globalization.NumberStyles.Any) : 65;

                    this.timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(/*1000d / */graphTime));//30fps = 33
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    double graphTime = (!string.IsNullOrWhiteSpace(FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphTime))
                        ? double.Parse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphTime, false), System.Globalization.NumberStyles.Any) : 200;

                    this.timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(/*1000d /*/ graphTime));//30fps = 33
                }
                catch
                {
                }

            }
            if (updateElapsedTimeMS)
                elapsedTimeMS += (timer.Interval.TotalMilliseconds + timer.Interval.TotalSeconds * 1000) / 1000;
            if (finishedGraphing)
            {
                Win2D_Canvas.Invalidate();

            }

        }



        private void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            finishedGraphing = false;
            double x = (double)Win2D_Canvas.ActualWidth;
            double y = (double)Win2D_Canvas.ActualHeight;
            //string s = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[0].formula;
            if (FormulaCollection_MVVM_Selection.selectedProjectIndex >= 0)
            {
                if (FormulaProject_MVVM.formulaProject.Count >= FormulaCollection_MVVM_Selection.selectedProjectIndex)
                    if (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties.Count > 0)
                    {
                        //try
                        //{
                        double graphStepX = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals("") ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals("-") ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals(double.PositiveInfinity.ToString()) ||
                        !double.TryParse(new MathVue<double>().solveFormula(null, new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX, false), false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out graphStepX) ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals(".")) ?
                            1d : graphStepX;


                        double graphStepY = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals("") ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals("-") ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals(double.PositiveInfinity.ToString()) ||
                        !double.TryParse(new MathVue<double>().solveFormula(null, new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY, false), false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out graphStepY) ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals(".")) ?
                            1d : graphStepY;


                        double graphScale = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale.Equals("") ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale.Equals("-") ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale.Equals(double.PositiveInfinity.ToString()) ||
                        !double.TryParse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out graphScale) ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale.Equals(".")) ?
                            1d : graphScale;


                        double graphResolution = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphResolution.Equals("") ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphResolution.Equals("-") ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphResolution.Equals(double.PositiveInfinity.ToString()) ||
                        !double.TryParse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphResolution, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out graphResolution) ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphResolution.Equals(".")) ?
                            .01d : graphResolution;

                        double graphLocationX = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX.Equals("") ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX.Equals("-") ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX.Equals(double.PositiveInfinity.ToString()) ||
                        !double.TryParse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out graphLocationX) ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX.Equals(".")) ?
                            0d : graphLocationX;

                        double graphLocationY = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY.Equals("") ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY.Equals("-") ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY.Equals(double.PositiveInfinity.ToString()) ||
                        !double.TryParse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out graphLocationY) ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY.Equals(".")) ?
                            0d : graphLocationY;

                        double axisSpacing = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing.Equals("") ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing.Equals("-") ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing.Equals(double.PositiveInfinity.ToString()) ||
                        !double.TryParse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing, false), System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out axisSpacing) ||
                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing.Equals(".")) ?
                            //100d : double.Parse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing, false), System.Globalization.NumberStyles.Any);
                            100d : axisSpacing;

                        graphStepX = (graphStepX <= 0d) ? 1 : graphStepX;
                        graphStepY = (graphStepY <= 0d) ? 1 : graphStepY;
                        graphScale = (graphScale <= 0.05d) ? .05d : graphScale;
                        graphResolution = (graphResolution <= 0.00001) ? .001d : graphResolution;
                        axisSpacing = (axisSpacing <= 20) ? 100 : axisSpacing;

                        //get formula
                        //get var
                        //set customFunction
                        //calc



                        (double[], double[]) axisRange = new Controller.Graphing.Graph.Graph().renderAxis(sender, args, x, y, graphStepX, graphStepY, graphScale, graphLocationX, graphLocationY, axisSpacing);
                        //test

                        //new Controller.Graphing.Graph.Graph().renderDataX(sender, args, "", x, y, graphStepX, graphScale, graphResolution, graphLocationX, graphLocationY, new Color() { A = 100, B = 0, R = 0, G = 100 }, axisRange);


                        foreach (GraphFormulaList graphFormula in FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList)
                        {
                            foreach (Formula formula in FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection)
                            {
                                if (!string.IsNullOrWhiteSpace(graphFormula.formulaName) && !string.IsNullOrWhiteSpace(formula.formulaName))
                                    if (graphFormula.formulaName.Equals(formula.formulaName, StringComparison.CurrentCulture))
                                    {
                                        Color lineColor;
                                        switch (graphFormula.comboBox_Color)
                                        {
                                            case 0:
                                                lineColor = Colors.Green;
                                                break;
                                            case 1:
                                                lineColor = Colors.Yellow;
                                                break;
                                            case 2:
                                                lineColor = Colors.Orange;
                                                break;
                                            case 3:
                                                lineColor = Colors.Brown;
                                                break;
                                            case 4:
                                                lineColor = Colors.Purple;
                                                break;
                                            case 5:
                                                lineColor = Colors.Red;
                                                break;
                                            case 6:
                                                lineColor = Colors.Blue;
                                                break;
                                            default:
                                                lineColor = Colors.Green;
                                                break;
                                        }
                                        StringVue strV = new StringVue();
                                        graphFormula.XYValue_Output = new Controller.Graphing.Graph.Graph().renderDataX_Point(formula, sender, args, strV.replaceData(formula.rearrangedFormula, "time", elapsedTimeMS.ToString()).getNewData.Replace("\n", "").Replace("\r", "").Replace(" ", ""), x, y, graphStepX, graphStepY, graphScale, graphResolution, graphLocationX, graphLocationY, lineColor, axisRange, axisSpacing, graphFormula.XYValue);

                                        //try
                                        //{
                                        new Controller.Graphing.Graph.Graph().renderDataX(formula, sender, args, strV.replaceData(formula.rearrangedFormula, "time", elapsedTimeMS.ToString()).getNewData, x, y, graphStepX, graphStepY, graphScale, graphResolution, graphLocationX, graphLocationY, lineColor, axisRange, axisSpacing);
                                        //}
                                        //catch { }
                                        //new Controller.Graphing.Graph.Graph().renderDataX(sender, args, new Controller.Graphing.Graph.Graph().replaceTime(formula.rearrangedFormula, elapsedTimeMS), x, y, graphStepX, 1, .1f, 0, 0, lineColor, axisRange);
                                        //new Controller.Graphing.Graph.Graph().renderDataX(sender, args, "", x, y, graphStepX, 1, .1f, 0, 0, new Color() { A = 100, B = 0, R = 0, G = 100 }, axisRange);
                                        break;
                                    }
                            }
                        }
                    }
                //}
                //catch { }

            }
            //Win2D_Canvas.Invalidate();

            finishedGraphing = true;


        }

















        //======================================================================================================================
        //======================================================================================================================
        //======================================================================================================================
        //======================================================================================================================
        //======================================================================================================================
        //======================================================================================================================
        //======================================================================================================================
        //======================================================================================================================
        //======================================================================================================================
        //======================================================================================================================
        //======================================================================================================================



        private void Button_AddToGraphList_Click(object sender, RoutedEventArgs e)
        {
            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
                FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList.Add(new Model.GraphFormulaList());
        }

        private void Button_FormulaName_Click(object sender, RoutedEventArgs e)
        {

            GraphFormulaList graphFormulaSelection = (GraphFormulaList)((Button)sender).Tag;
            if (graphFormulaSelection.visibility == Visibility.Visible)
                graphFormulaSelection.visibility = Visibility.Collapsed;
            else
                graphFormulaSelection.visibility = Visibility.Visible;
        }

        private void Button_FormulaNameDelete_Click(object sender, RoutedEventArgs e)
        {

            GraphFormulaList graphFormulaSelection = (GraphFormulaList)((Button)sender).Tag;
            int index = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList.IndexOf(graphFormulaSelection);

            FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList.RemoveAt(index);
        }

        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            string isPlayStop = ((Button)sender).Content.ToString();
            if (isPlayStop.Equals("Play", StringComparison.CurrentCulture))
            {
                ((Button)sender).Content = "Stop";
                isPlaying = true;
                updateElapsedTimeMS = true;
                elapsedTimeMS = 0;
                //this.timer.Interval = new TimeSpan(0, 0, 0, 0, 33);//30fps = 33
            }
            else
            {
                ((Button)sender).Content = "Play";
                isPlaying = false;
                updateElapsedTimeMS = false;
                elapsedTimeMS = 0;
                //this.timer.Interval = new TimeSpan(0, 0, 0, 0, 200);//30fps = 33
                if (pauseBtn != null)
                    pauseBtn.Content = "Pause";
            }
        }
        Button pauseBtn = null;
        private void Button_Pause_Click(object sender, RoutedEventArgs e)
        {
            pauseBtn = (Button)sender;
            string isPauseResume = ((Button)sender).Content.ToString();
            if (isPauseResume.Equals("Pause", StringComparison.CurrentCulture))
            {
                if (elapsedTimeMS != 0)
                {
                    ((Button)sender).Content = "Resume";
                    updateElapsedTimeMS = false;
                    //elapsedTimeMS = 0;
                }
            }
            else
            {
                if (elapsedTimeMS != 0)
                {
                    ((Button)sender).Content = "Pause";
                    updateElapsedTimeMS = true;
                    //elapsedTimeMS = 0;
                }
            }
        }


        PointerPoint currentPoint_Moved { get; set; }

        private void Win2D_Canvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            currentPoint_Moved = e.GetCurrentPoint((UIElement)sender);
        }
        private async void Win2D_Canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint currentPoint = e.GetCurrentPoint((UIElement)sender);
            PointerPointProperties props = currentPoint.Properties;

            //Point pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
            //Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread().
            Point pointerPosition = currentPoint.Position;

            string currentX = "";
            string currentY = "";
            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
            {
                currentX = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX;
                currentY = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY;
            }

            while (InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.RightButton).HasFlag(CoreVirtualKeyStates.Down))
            //while (props.IsRightButtonPressed)
            {
                await Task.Run(() =>
                {
                    Win2D_Canvas.DispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                    {
                        try
                        {
                            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
                            {
                                if (currentX.Equals("") ||
                                currentX.Equals("-") ||
                                currentX.Equals(double.PositiveInfinity.ToString()) ||
                                currentX.Equals("."))
                                    currentX = "0";
                                if (currentY.Equals("") ||
                                currentY.Equals("-") ||
                                currentY.Equals(double.PositiveInfinity.ToString()) ||
                                currentY.Equals("."))
                                    currentY = "0";

                                //double newPositionX = pointerPosition.X - Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition.X;
                                //double newPositionY = pointerPosition.Y - Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition.Y;
                                //PointerPoint currentPoint_New = e.GetCurrentPoint((UIElement)sender);




                                double newPositionX = pointerPosition.X - currentPoint_Moved.Position.X;
                                double newPositionY = pointerPosition.Y - currentPoint_Moved.Position.Y;
                                FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX =
                                (double.Parse(new MathVue<double>().solveFormula(null, currentX, false)) + newPositionX, System.Globalization.NumberStyles.Any).Item1.ToString();
                                //new MathVue<double>().stringMath(currentX, newPositionX.ToString(), MathVue<double>.STRING_MATH_OPTION.add);



                                FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY =
                                (double.Parse(new MathVue<double>().solveFormula(null, currentY, false)) - newPositionY, System.Globalization.NumberStyles.Any).Item1.ToString();
                                //new MathVue<double>().stringMath(currentY, newPositionY.ToString(), MathVue<double>.STRING_MATH_OPTION.subtract);

                            }
                        }
                        catch { }
                        currentPoint = e.GetCurrentPoint((UIElement)sender);
                        props = currentPoint.Properties;
                    });
                });
            }
        }



        private void Win2D_Canvas_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {

            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
            {
                int delta = e.GetCurrentPoint((UIElement)sender).Properties.MouseWheelDelta;

                try
                {

                    double graphScale = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale.Equals("") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale.Equals("-") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale.Equals(".")) ?
                        1d : double.Parse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale, false), System.Globalization.NumberStyles.Any);


                    graphScale = (graphScale <= 0.05d) ? .05d : graphScale;
                    if (delta > 0)
                    {
                        //graphLocationX = graphLocationX / graphScale;
                        //graphLocationX = graphLocationY / graphScale;
                        graphScale = graphScale + graphScale * .1;
                        //graphLocationX = graphLocationX * graphScale;
                        //graphLocationX = graphLocationY * graphScale;
                    }
                    else
                    {
                        //graphLocationX = graphLocationX / graphScale;
                        //graphLocationX = graphLocationY / graphScale;
                        graphScale = graphScale - graphScale * .1;
                        //graphLocationX = graphLocationX * graphScale;
                        //graphLocationX = graphLocationY * graphScale;
                    }
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale = graphScale.ToString();
                }
                catch { }

            }
        }

        private void TextBox_AxisSpacing_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
            {
                TextBox tbox = (TextBox)sender;
                try
                {
                    //double graphStepX = double.Parse(new MathVue<double>().solveFormula (null,tbox.Text,false), System.Globalization.NumberStyles.Any);
                    string axisSpacing = tbox.Text;

                    string currentX = "";
                    string currentY = "";
                    //string _axisSpacing = "";
                    //double axisSpacing = 0d;
                    //double newCurrentY = 0d;
                    //double newCurrentX = 0d;
                    currentX = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX;
                    currentY = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY;
                    //_axisSpacing = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing;

                    if (currentX.Equals("") ||
                    currentX.Equals("-") ||
                    currentX.Equals(double.PositiveInfinity.ToString()) ||
                    currentX.Equals("."))
                        currentX = "0";
                    if (currentY.Equals("") ||
                    currentY.Equals("-") ||
                    currentY.Equals(double.PositiveInfinity.ToString()) ||
                    currentY.Equals("."))
                        currentY = "0";
                    if (axisSpacing.Equals("") ||
                    axisSpacing.Equals("-") ||
                    axisSpacing.Equals(double.PositiveInfinity.ToString()) ||
                    axisSpacing.Equals("."))
                        axisSpacing = "100";

                    axisSpacing = (double.Parse(new MathVue<double>().solveFormula(null, axisSpacing, false), System.Globalization.NumberStyles.Any) <= 20d) ? "100" : axisSpacing;
                    //if (_axisSpacing.Equals("") ||
                    //_axisSpacing.Equals("-") ||
                    //_axisSpacing.Equals("."))
                    //    _axisSpacing = "100";
                    //axisSpacing = double.Parse(new MathVue<double>().solveFormula (null,_axisSpacing,false), System.Globalization.NumberStyles.Any);

                    //axisSpacing = (axisSpacing <= 20) ? 100 : axisSpacing;


                    currentX = (double.Parse(new MathVue<double>().solveFormula(null, currentX, false), System.Globalization.NumberStyles.Any) * double.Parse(new MathVue<double>().solveFormula(null, axisSpacing, false), System.Globalization.NumberStyles.Any) / prev_AxisSpacing).ToString();
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX =
                    (double.Parse(new MathVue<double>().solveFormula(null, currentX, false), System.Globalization.NumberStyles.Any) /*/ double.Parse(new MathVue<double>().solveFormula (null,axisSpacing,false), System.Globalization.NumberStyles.Any)*/).ToString();

                    currentY = (double.Parse(new MathVue<double>().solveFormula(null, currentY, false), System.Globalization.NumberStyles.Any) * double.Parse(new MathVue<double>().solveFormula(null, axisSpacing, false), System.Globalization.NumberStyles.Any) / prev_AxisSpacing).ToString();
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY =
                    (double.Parse(new MathVue<double>().solveFormula(null, currentY, false), System.Globalization.NumberStyles.Any)/* / double.Parse(new MathVue<double>().solveFormula (null,axisSpacing,false), System.Globalization.NumberStyles.Any)*/).ToString();


                }
                catch
                {
                }
            }
        }
        private void TextBox_AxisSpacing_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            //TextBox tbox = (TextBox)sender;
            try
            {
                string axisSpacing = sender.Text;

                if (axisSpacing.Equals("") ||
                axisSpacing.Equals("-") ||
                axisSpacing.Equals(double.PositiveInfinity.ToString()) ||
                axisSpacing.Equals("."))
                    axisSpacing = "100";

                prev_AxisSpacing = (double.Parse(new MathVue<double>().solveFormula(null, axisSpacing, false), System.Globalization.NumberStyles.Any) <= 20d) ? 100d : double.Parse(new MathVue<double>().solveFormula(null, axisSpacing, false), System.Globalization.NumberStyles.Any);
                //prev_graphStepX_Str = sender.Text;
                //prev_graphStepX = double.Parse(new MathVue<double>().solveFormula (null,sender.Text,false), System.Globalization.NumberStyles.Any);
            }
            catch { }

        }

        private void TextBox_GraphStepX_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
            {
                TextBox tbox = (TextBox)sender;
                try
                {
                    //double graphStepX = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals("") ||
                    //FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals("-") ||
                    //FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals(".")) ?
                    //    1d : double.Parse(new MathVue<double>().solveFormula(null, new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX, false), false), System.Globalization.NumberStyles.Any);


                    double graphStepY = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals("") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals("-") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals(".")) ?
                        1d : double.Parse(new MathVue<double>().solveFormula(null, new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY, false), false), System.Globalization.NumberStyles.Any);

                    //graphStepX = (graphStepX <= 0d) ? 1 : graphStepX;
                    graphStepY = (graphStepY <= 0d) ? 1 : graphStepY;

                    //double graphStepX = double.Parse(new MathVue<double>().solveFormula (null,tbox.Text,false), System.Globalization.NumberStyles.Any);
                    string graphStepX = tbox.Text;

                    string currentX = "";
                    string currentY = "";
                    //string _axisSpacing = "";
                    //double axisSpacing = 0d;
                    //double newCurrentY = 0d;
                    //double newCurrentX = 0d;
                    currentX = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX;
                    currentY = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY;
                    //_axisSpacing = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing;

                    if (currentX.Equals(double.NaN.ToString()) || currentX.Equals(double.PositiveInfinity.ToString()) || currentX.Equals(double.NegativeInfinity.ToString()))
                    {
                        currentX = "0";
                    }
                    if (currentY.Equals(double.NaN.ToString()) || currentY.Equals(double.PositiveInfinity.ToString()) || currentY.Equals(double.NegativeInfinity.ToString()))
                    {
                        currentY = "0";
                    }

                    if (currentX.Equals("") ||
                    currentX.Equals("-") ||
                    currentX.Equals(double.PositiveInfinity.ToString()) ||
                    currentX.Equals("."))
                        currentX = "0";
                    if (currentY.Equals("") ||
                    currentY.Equals("-") ||
                    currentY.Equals(double.PositiveInfinity.ToString()) ||
                    currentY.Equals("."))
                        currentY = "0";
                    if (graphStepX.Equals("") ||
                    graphStepX.Equals("-") ||
                    graphStepX.Equals(double.PositiveInfinity.ToString()) ||
                    graphStepX.Equals("."))
                        graphStepX = "1";

                    //////////graphStepX = (double.Parse(new MathVue<double>().solveFormula (null,graphStepX,false), System.Globalization.NumberStyles.Any) <= 0.01d) ? ".1" : graphStepX;



                    currentX = (double.Parse(new MathVue<double>().solveFormula(null, currentX, false), System.Globalization.NumberStyles.Any) * prev_graphStepX).ToString();
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX =
                    (double.Parse(new MathVue<double>().solveFormula(null, currentX, false), System.Globalization.NumberStyles.Any) / double.Parse(new MathVue<double>().solveFormula(null, graphStepX, false), System.Globalization.NumberStyles.Any)).ToString();

                    currentY = (double.Parse(new MathVue<double>().solveFormula(null, currentY, false), System.Globalization.NumberStyles.Any) * graphStepY).ToString();
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY =
                    (double.Parse(new MathVue<double>().solveFormula(null, currentY, false), System.Globalization.NumberStyles.Any) / double.Parse(new MathVue<double>().solveFormula(null, graphStepY.ToString(), false), System.Globalization.NumberStyles.Any)).ToString();


                }
                catch
                {
                }
            }
        }
        //string prev_graphStepX_Str = "";
        private void TextBox_GraphStepX_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            //TextBox tbox = (TextBox)sender;
            try
            {
                string graphStepX = sender.Text;

                if (graphStepX.Equals("") ||
                graphStepX.Equals("-") ||
                graphStepX.Equals(double.PositiveInfinity.ToString()) ||
                graphStepX.Equals("."))
                    graphStepX = "1";

                //////////prev_graphStepX = (double.Parse(new MathVue<double>().solveFormula (null,graphStepX,false), System.Globalization.NumberStyles.Any) <= 0.01d) ? .1 : double.Parse(new MathVue<double>().solveFormula (null,graphStepX,false), System.Globalization.NumberStyles.Any);
                prev_graphStepX = double.Parse(new MathVue<double>().solveFormula(null, graphStepX, false), System.Globalization.NumberStyles.Any);

            }
            catch { }

        }

        private void TextBox_GraphStepY_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
            {
                TextBox tbox = (TextBox)sender;
                try
                {
                    double graphStepX = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals("") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals("-") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals(".")) ?
                        1d : double.Parse(new MathVue<double>().solveFormula(null, new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX, false), false), System.Globalization.NumberStyles.Any);

                    graphStepX = (graphStepX <= 0d) ? 1 : graphStepX;
                    //graphStepY = (graphStepY <= 0d) ? 1 : graphStepY;

                    //double graphStepY = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals("") ||
                    //FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals("-") ||
                    //FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals(".")) ?
                    //    1d : double.Parse(new MathVue<double>().solveFormula(null, new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY, false), false), System.Globalization.NumberStyles.Any);

                    //double graphStepX = double.Parse(new MathVue<double>().solveFormula (null,tbox.Text,false), System.Globalization.NumberStyles.Any);
                    string graphStepY = tbox.Text;

                    string currentX = "";
                    string currentY = "";
                    //string _axisSpacing = "";
                    //double axisSpacing = 0d;
                    //double newCurrentY = 0d;
                    //double newCurrentX = 0d;
                    currentX = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX;
                    currentY = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY;
                    //_axisSpacing = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing;

                    if (currentX.Equals(double.NaN.ToString()) || currentX.Equals(double.PositiveInfinity.ToString()) || currentX.Equals(double.NegativeInfinity.ToString()))
                    {
                        currentX = "0";
                    }
                    if (currentY.Equals(double.NaN.ToString()) || currentY.Equals(double.PositiveInfinity.ToString()) || currentY.Equals(double.NegativeInfinity.ToString()))
                    {
                        currentY = "0";
                    }

                    if (currentX.Equals("") ||
                    currentX.Equals("-") ||
                    currentX.Equals(double.PositiveInfinity.ToString()) ||
                    currentX.Equals("."))
                        currentX = "0";
                    if (currentY.Equals("") ||
                    currentY.Equals("-") ||
                    currentY.Equals(double.PositiveInfinity.ToString()) ||
                    currentY.Equals("."))
                        currentY = "0";
                    if (graphStepY.Equals("") ||
                    graphStepY.Equals("-") ||
                    graphStepY.Equals(double.PositiveInfinity.ToString()) ||
                    graphStepY.Equals("."))
                        graphStepY = "1";

                    //////////graphStepX = (double.Parse(new MathVue<double>().solveFormula (null,graphStepX,false), System.Globalization.NumberStyles.Any) <= 0.01d) ? ".1" : graphStepX;



                    currentX = (double.Parse(new MathVue<double>().solveFormula(null, currentX, false), System.Globalization.NumberStyles.Any) * graphStepX).ToString();
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX =
                    (double.Parse(new MathVue<double>().solveFormula(null, currentX, false), System.Globalization.NumberStyles.Any) / double.Parse(new MathVue<double>().solveFormula(null, graphStepX.ToString(), false), System.Globalization.NumberStyles.Any)).ToString();

                    currentY = (double.Parse(new MathVue<double>().solveFormula(null, currentY, false), System.Globalization.NumberStyles.Any) * prev_graphStepY).ToString();
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY =
                    (double.Parse(new MathVue<double>().solveFormula(null, currentY, false), System.Globalization.NumberStyles.Any) / double.Parse(new MathVue<double>().solveFormula(null, graphStepY, false), System.Globalization.NumberStyles.Any)).ToString();


                }
                catch
                {
                }
            }
        }
        //string prev_graphStepX_Str = "";
        private void TextBox_GraphStepY_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            //TextBox tbox = (TextBox)sender;
            try
            {
                string graphStepY = sender.Text;

                if (graphStepY.Equals("") ||
                graphStepY.Equals("-") ||
                graphStepY.Equals(double.PositiveInfinity.ToString()) ||
                graphStepY.Equals("."))
                    graphStepY = "1";

                //////////prev_graphStepX = (double.Parse(new MathVue<double>().solveFormula (null,graphStepX,false), System.Globalization.NumberStyles.Any) <= 0.01d) ? .1 : double.Parse(new MathVue<double>().solveFormula (null,graphStepX,false), System.Globalization.NumberStyles.Any);
                prev_graphStepY = double.Parse(new MathVue<double>().solveFormula(null, graphStepY, false), System.Globalization.NumberStyles.Any);

            }
            catch { }

        }
        private void TextBox_GraphScale_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
            {
                TextBox tbox = (TextBox)sender;
                try
                {
                    //double graphStepX = double.Parse(new MathVue<double>().solveFormula (null,tbox.Text,false), System.Globalization.NumberStyles.Any);
                    string graphScale = tbox.Text;

                    string currentX = "";
                    string currentY = "";
                    //string _axisSpacing = "";
                    //double axisSpacing = 0d;
                    //double newCurrentY = 0d;
                    //double newCurrentX = 0d;
                    currentX = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX;
                    currentY = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY;
                    //_axisSpacing = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing;

                    if (currentX.Equals("") ||
                    currentX.Equals("-") ||
                    currentX.Equals(double.PositiveInfinity.ToString()) ||
                    currentX.Equals("."))
                        currentX = "0";
                    if (currentY.Equals("") ||
                    currentY.Equals("-") ||
                    currentY.Equals(double.PositiveInfinity.ToString()) ||
                    currentY.Equals("."))
                        currentY = "0";
                    if (graphScale.Equals("") ||
                    graphScale.Equals("-") ||
                    graphScale.Equals(double.PositiveInfinity.ToString()) ||
                    graphScale.Equals("."))
                        graphScale = "1";

                    graphScale = (double.Parse(new MathVue<double>().solveFormula(null, graphScale, false), System.Globalization.NumberStyles.Any) <= 0.05d) ? ".05" : graphScale;
                    //if (_axisSpacing.Equals("") ||
                    //_axisSpacing.Equals("-") ||
                    //_axisSpacing.Equals("."))
                    //    _axisSpacing = "100";
                    //axisSpacing = double.Parse(new MathVue<double>().solveFormula (null,_axisSpacing,false), System.Globalization.NumberStyles.Any);

                    //axisSpacing = (axisSpacing <= 20) ? 100 : axisSpacing;


                    currentX = (double.Parse(new MathVue<double>().solveFormula(null, currentX, false), System.Globalization.NumberStyles.Any) / prev_GraphScale).ToString();
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX =
                    (double.Parse(new MathVue<double>().solveFormula(null, currentX, false), System.Globalization.NumberStyles.Any) * double.Parse(new MathVue<double>().solveFormula(null, graphScale, false), System.Globalization.NumberStyles.Any)).ToString();

                    currentY = (double.Parse(new MathVue<double>().solveFormula(null, currentY, false), System.Globalization.NumberStyles.Any) / prev_GraphScale).ToString();
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY =
                    (double.Parse(new MathVue<double>().solveFormula(null, currentY, false), System.Globalization.NumberStyles.Any) * double.Parse(new MathVue<double>().solveFormula(null, graphScale, false), System.Globalization.NumberStyles.Any)).ToString();


                }
                catch
                {
                }
            }
        }
        //string prev_graphStepX_Str = "";
        private void TextBox_GraphScale_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            //TextBox tbox = (TextBox)sender;
            try
            {
                string graphScale = sender.Text;

                if (graphScale.Equals("") ||
                graphScale.Equals("-") ||
                graphScale.Equals(double.PositiveInfinity.ToString()) ||
                graphScale.Equals("."))
                    graphScale = "1";

                prev_GraphScale = (double.Parse(new MathVue<double>().solveFormula(null, graphScale, false), System.Globalization.NumberStyles.Any) <= 0.05d) ? .05d : double.Parse(new MathVue<double>().solveFormula(null, graphScale, false), System.Globalization.NumberStyles.Any);
                //prev_graphStepX_Str = sender.Text;
                //prev_graphStepX = double.Parse(new MathVue<double>().solveFormula (null,sender.Text,false), System.Globalization.NumberStyles.Any);
            }
            catch { }

        }

        private void TextBox_PointX_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
            {
                try
                {
                    TextBox tbox = (TextBox)sender;
                    double pointX = (!string.IsNullOrWhiteSpace(tbox.Text)) ? double.Parse(new MathVue<double>().solveFormula(null, tbox.Text, false), System.Globalization.NumberStyles.Any) : 0;

                    double graphStepX = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals("") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals("-") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals(".")) ?
                        1d : double.Parse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX, false), System.Globalization.NumberStyles.Any);


                    double graphStepY = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals("") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals("-") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals(".")) ?
                        1d : double.Parse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY, false), System.Globalization.NumberStyles.Any);


                    double graphScale = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale.Equals("") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale.Equals("-") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale.Equals(".")) ?
                        1d : double.Parse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale, false), System.Globalization.NumberStyles.Any);


                    //double graphLocationX = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX.Equals("") ||
                    //FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX.Equals("-") ||
                    //FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX.Equals(".")) ?
                    //    0d : double.Parse(new MathVue<double>().solveFormula (null,FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX,false), System.Globalization.NumberStyles.Any);

                    //double graphLocationY = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY.Equals("") ||
                    //FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY.Equals("-") ||
                    //FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY.Equals(".")) ?
                    //    0d : double.Parse(new MathVue<double>().solveFormula (null,FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY,false), System.Globalization.NumberStyles.Any);

                    double axisSpacing = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing.Equals("") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing.Equals("-") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing.Equals(".")) ?
                        100d : double.Parse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing, false), System.Globalization.NumberStyles.Any);

                    double graphPointY = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphPointY.Equals("") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphPointY.Equals("-") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphPointY.Equals(".")) ?
                        0d : double.Parse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphPointY, false), System.Globalization.NumberStyles.Any);

                    //graphStepX = (graphStepX <= 0.01d) ? .1 : graphStepX;
                    graphScale = (graphScale <= 0.05d) ? .05d : graphScale;
                    //graphResolution = (graphResolution <= 0.00001) ? .001d : graphResolution;
                    axisSpacing = (axisSpacing <= 20) ? 100 : axisSpacing;



                    double newLocationX = pointX / graphStepX * graphScale * axisSpacing;
                    double newLocationY = graphPointY / graphStepY * graphScale * axisSpacing;

                    //FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphPointY = tbox.Text;

                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX = newLocationX.ToString();
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY = newLocationY.ToString();
                }
                catch
                {

                }
            }
        }
        private void TextBox_PointY_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
            {
                try
                {
                    TextBox tbox = (TextBox)sender;
                    double pointY = (!string.IsNullOrWhiteSpace(tbox.Text)) ? double.Parse(new MathVue<double>().solveFormula(null, tbox.Text, false), System.Globalization.NumberStyles.Any) : 0;

                    double graphStepY = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals("") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals("-") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY.Equals(".")) ?
                        1d : double.Parse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepY, false), System.Globalization.NumberStyles.Any);

                    double graphStepX = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals("") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals("-") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX.Equals(".")) ?
                        1d : double.Parse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphStepX, false), System.Globalization.NumberStyles.Any);


                    double graphScale = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale.Equals("") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale.Equals("-") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale.Equals(".")) ?
                        1d : double.Parse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphScale, false), System.Globalization.NumberStyles.Any);


                    //double graphLocationX = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX.Equals("") ||
                    //FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX.Equals("-") ||
                    //FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX.Equals(".")) ?
                    //    0d : double.Parse(new MathVue<double>().solveFormula (null,FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX,false), System.Globalization.NumberStyles.Any);

                    //double graphLocationY = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY.Equals("") ||
                    //FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY.Equals("-") ||
                    //FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY.Equals(".")) ?
                    //    0d : double.Parse(new MathVue<double>().solveFormula (null,FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY,false), System.Globalization.NumberStyles.Any);

                    double axisSpacing = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing.Equals("") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing.Equals("-") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing.Equals(".")) ?
                        100d : double.Parse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].axisSpacing, false), System.Globalization.NumberStyles.Any);

                    double graphPointX = (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphPointX.Equals("") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphPointX.Equals("-") ||
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphPointX.Equals(".")) ?
                        0d : double.Parse(new MathVue<double>().solveFormula(null, FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphPointX, false), System.Globalization.NumberStyles.Any);

                    //graphStepX = (graphStepX <= 0.01d) ? .1 : graphStepX;
                    graphScale = (graphScale <= 0.05d) ? .05d : graphScale;
                    //graphResolution = (graphResolution <= 0.00001) ? .001d : graphResolution;
                    axisSpacing = (axisSpacing <= 20) ? 100 : axisSpacing;


                    double newLocationX = graphPointX / graphStepX * graphScale * axisSpacing;
                    double newLocationY = pointY / graphStepY * graphScale * axisSpacing;

                    //FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphPointY = tbox.Text;

                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationX = newLocationX.ToString();
                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].graphFormulaList_Properties[0].graphLocationY = newLocationY.ToString();


                }
                catch
                {

                }
            }
        }



        private void TextBox_GraphTime_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
            {
                try
                {
                    TextBox tbox = (TextBox)sender;
                    double graphTime = (!string.IsNullOrWhiteSpace(tbox.Text)) ? double.Parse(new MathVue<double>().solveFormula(null, tbox.Text, false), System.Globalization.NumberStyles.Any) : 200;

                    this.timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(/*1000d /*/ graphTime));//30fps = 33
                }
                catch
                {

                }
            }
        }
        private void TextBox_GraphTimeOnPlay_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
            {
                try
                {
                    TextBox tbox = (TextBox)sender;
                    double graphTime = (!string.IsNullOrWhiteSpace(tbox.Text)) ? double.Parse(new MathVue<double>().solveFormula(null, tbox.Text, false), System.Globalization.NumberStyles.Any) : 65;

                    this.timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(/*1000d / */graphTime));//30fps = 33
                }
                catch
                {

                }
            }

        }







        private void Win2D_Canvas_Unloaded(object sender, RoutedEventArgs e)
        {

            Win2D_Canvas.RemoveFromVisualTree();
            Win2D_Canvas = null;
        }

    }
}
