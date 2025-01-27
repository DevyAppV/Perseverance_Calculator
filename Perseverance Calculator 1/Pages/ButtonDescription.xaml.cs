using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
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
using Perseverance_Calculator.View.Pages;
using Windows.Graphics;
using Microsoft.UI.Dispatching;
using System.Globalization;
using Perseverance_Calculator_1.Controller;
using System.Collections.ObjectModel;
using Windows.System;
using Microsoft.VisualBasic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Perseverance_Calculator_1.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ButtonDescription : Page
    {
        //ApplicationView view = ApplicationView.GetForCurrentView();
        public static CustomFunction buttonDescription_CustomFunction = new CustomFunction();
        Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //CustomFunction function = (CustomFunction)e.Parameter;
            buttonDescription_CustomFunction = (CustomFunction)e.Parameter;

            if (string.IsNullOrWhiteSpace(buttonDescription_CustomFunction.previousFunctionName_OnEdit))
                buttonDescription_CustomFunction.previousFunctionName_OnEdit = buttonDescription_CustomFunction.formulaObj.formulaName;



            //////////ButtonDescriptionBind.buttonDescriptionBind.formulaName = function.formulaName;
            //////////ButtonDescriptionBind.buttonDescriptionBind.formula = function.formula;
            //////////ButtonDescriptionBind.buttonDescriptionBind.rearrangedFormula = function.rearrangedFormula;
            //////////ButtonDescriptionBind.buttonDescriptionBind.use = function.use;
            //////////ButtonDescriptionBind.buttonDescriptionBind.description = function.description;
            //////////ButtonDescriptionBind.buttonDescriptionBind.functionTab_IndexLocation = function.functionTab_IndexLocation;

            //////////if (ButtonDescriptionBind.buttonDescriptionBind.variableList_Bind != null)
            //////////    ButtonDescriptionBind.buttonDescriptionBind.variableList_Bind.Clear();


            //////////await Task.Run( () =>
            //////////{
            //////////    foreach (var v in function.variableList_Bind)
            //////////    {
            //////////        ViewPages.buttonDescriptionView.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () => {
            //////////                ButtonDescriptionBind.buttonDescriptionBind.variableList_Bind.Add(v);
            //////////        });
            //////////    }

            //////////});


        }
        public ButtonDescription()
        {
            this.InitializeComponent();
            //ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(10, 10));
            //Window.Current.Activate();
            //////////ViewPages.buttonDescriptionView.AppWindow.Resize(new SizeInt32(800, 300));
            //MaxWidth = "800" MaxHeight = "300"
            //ViewPages.buttonDescriptionView.Activated += Current_Activated;
            ViewPages.buttonDescriptionView.Closed += Current_Closed;
            //view.Consolidated += View_Consolidated;
            //ApplicationView.GetForCurrentView().Title = "Button Description";
        }

        //private void View_Consolidated(ApplicationView sender, ApplicationViewConsolidatedEventArgs args)
        //{
        //    //view.TryResizeView(new Size(1500, 800));
        //}

        private void Current_Closed(object sender, WindowEventArgs e)
        {
            ViewPages.buttonDescriptionView = null;
            //buttonDescription_CustomFunction.formulaObj.formulaName = formulaName_Tbox.Text;
            //ViewPages.buttonDescriptionView.Close();
            //view.TryResizeView(new Size(1500, 800));

        }

        private async void getVariables(Formula formula, Dictionary<string, VariableData> variableDataDictionary)
        {
            await Task.Run(() =>
            {
                string formulaToSolve = formula.formula;
                if (!string.IsNullOrWhiteSpace(formulaToSolve))
                {
                    formulaToSolve = new StringVue().replaceFormulaFunction(formula.formula.Replace("\n", "").Replace("\r", "").Replace(" ", ""));
                    ObservableCollection<VariableData> dat = new MathVue<object>().getVariables(formulaToSolve, formula, variableDataDictionary);

                    dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, async () =>
                    {
                        foreach (VariableData data in dat)
                        {
                            //await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                            //{

                            //formula.variableList_Dictionary.Add(data.name, data);
                            //if(string.IsNullOrWhiteSpace(data.value))
                            //    data.value = data.name;
                            formula.variableList_Bind.Add(data);
                            await Task.Delay(1);
                        }

                        if (formula.variableList_Bind.Count <= 0)
                        {
                            solveFormula(formula);
                        }
                    });
                }
            });
        }


        private async void solveFormula(Formula formula)
        {

            //if (formula.comboBox_SolutionType == 0)
            //{
                Task t = Task.Run(() =>
                {
                    //await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    //{
                    dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                    {
                        //if (formula.comboBox_Double_OR_AutoDecimal == 0)
                        //{
                        formula.solution = new MathVue<double>().solveFormula(formula);
                        //}
                        //else
                        //    formula.solution = new MathVue<decimal>().solveFormula(formula);
                    });
                });

                await t.AsAsyncAction();

            //}
        }
        private void SolveAndSet_CustomButton_Button_Click(object sender, RoutedEventArgs e)
        {
            //int index = -1;
            //if (int.TryParse(ButtonDescriptionBind.buttonDescriptionBind.functionTab_IndexLocation,CultureInfo.CurrentCulture,out index)) {

            if (!string.IsNullOrEmpty(buttonDescription_CustomFunction.formulaObj.formulaName))
            {
                //Formula formula = ;
                solveFormula(buttonDescription_CustomFunction.formulaObj);

                if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(buttonDescription_CustomFunction.previousFunctionName_OnEdit))
                    FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Remove(buttonDescription_CustomFunction.previousFunctionName_OnEdit);

                bool hasVar = false;
                buttonDescription_CustomFunction.use = buttonDescription_CustomFunction.formulaObj.formulaName;
                //buttonDescription_CustomFunction.use = buttonDescription_CustomFunction.formulaName;


                foreach (var v in buttonDescription_CustomFunction.formulaObj.variableList_Bind)
                {
                    if (!new MathVue<object>().isNumber(v.value))
                    {
                        if (!hasVar)
                        {
                            buttonDescription_CustomFunction.use += "(";
                            hasVar = true;
                        }
                        if (!string.IsNullOrWhiteSpace(v.value))
                            buttonDescription_CustomFunction.use += v.value + ",";
                        else
                            buttonDescription_CustomFunction.use += v.name + ",";
                        //buttonDescription_CustomFunction.formulaObj.variableList_Bind.Add(new VariableData() { name = v.name, value = v.value, description = v.description });
                    }
                }
                if (hasVar)
                    buttonDescription_CustomFunction.use =
                    buttonDescription_CustomFunction.use.Remove(
                            buttonDescription_CustomFunction.use.LastIndexOf(","), 1) + ")";
                //else
                //    buttonDescription_CustomFunction.use =
                //        buttonDescription_CustomFunction.use += ")";
                //}
                
                FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Add(buttonDescription_CustomFunction.formulaObj.formulaName, buttonDescription_CustomFunction);
                buttonDescription_CustomFunction.previousFunctionName_OnEdit = buttonDescription_CustomFunction.formulaObj.formulaName;
            }

        }
        private void GetVariables_CustomButton_Button_Click(object sender, RoutedEventArgs e)
        {
            //int index = -1;
            //if (int.TryParse(ButtonDescriptionBind.buttonDescriptionBind.functionTab_IndexLocation, CultureInfo.CurrentCulture, out index))
            //{

            //Formula formula = FunctionCollectionTab_MVVM.functionCollectionTab[index].customFunctionCollection_Bind.First(x => x.formulaObj.formulaName.Equals(ButtonDescriptionBind.buttonDescriptionBind.formulaName)).formulaObj;

            Formula formula = buttonDescription_CustomFunction.formulaObj;
            //Formula formula = ((Formula)((Button)sender).Tag);

            //formula.variableList_Dictionary.Clear();
            //formula.variableList_Bind.Clear();

            Dictionary<string, VariableData> variableDataDictionary = new System.Collections.Generic.Dictionary<string, VariableData>();

            foreach (VariableData data in formula.variableList_Bind)
            {
                variableDataDictionary.Add(data.name, data);
            }
            formula.variableList_Bind.Clear();
            getVariables(formula, variableDataDictionary);

            //}

        }

        //private void Current_Activated(object sender, Microsoft.UI.Xaml.WindowActivatedEventArgs e)
        //{
        //    //view.TryResizeView(new Size(800, 300));

        //}
    }
}
