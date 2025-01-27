using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Perseverance_Calculator.View.Pages;
using Perseverance_Calculator_1.Controller.DefaultData;
using Perseverance_Calculator_1.Controller.SaveLoad;
using Perseverance_Calculator_1.Controller;
using Perseverance_Calculator_1.Model.MVVM;
using Perseverance_Calculator_1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Microsoft.UI.Input;
//using CommunityToolkit.WinUI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Perseverance_Calculator_1
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {//public static Contacts Contact { get; set; }
        //public class Contacts
        //{
        //}

        //class Person
        //{
        //    public string Name { get; set; }
        //    public string PhoneNumber { get; set; }
        //}

        //public static int asd = 0;


        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    Debug.WriteLine("asdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasd");
        //}
        TextBox formula_GotFocus = null;
        Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        public MainWindow()
        {
            //ApplicationView.PreferredLaunchViewSize = new Size(1500, 800);
            //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            //ApplicationView.GetForCurrentView().TryResizeView(new Size { Width = 1500, Height = 1000 });
            //Window.Current.Activate();
            DefaultCustomButtons.populateButtonWith_Default();

            this.InitializeComponent();
            Textbox_DefaultSaveLocation.Text = ApplicationData.Current.LocalFolder.Path;
            DefaultCustomButtons.populateButtonWith_DefaultGraph();
            DefaultCustomButtons.populateButtonWith_DefaultProgram();


            //ApplicationView.GetForCurrentView().Consolidated += MainPage_Consolidated;
            //ApplicationView.GetForCurrentView().Title = "Formula";


        }

        //private void MainPage_Consolidated(ApplicationView sender, ApplicationViewConsolidatedEventArgs args)
        //{
        //    Application.Current.Exit();
        //}

        private async void Button_AddProject_Click(object sender, RoutedEventArgs e)
        {
            bool add = true;
            string projectName = Textbox_ProjectName.Text;
            if (!projectName.Equals(""))
            {
                foreach (var v in FormulaProject_MVVM.formulaProject)
                {
                    bool breakOut = false;
                    await Task.Run(() =>
                    {
                        if (v.projectName.Equals(projectName, StringComparison.CurrentCulture))
                        {
                            dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                            {
                                add = false;
                            });
                        }
                        if (!add)
                            dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                            {
                                breakOut = true;
                            });
                    });
                    if (breakOut) break;

                }
                if (add)
                {
                    FormulaProject_MVVM.formulaProject.Add(new FormulaProjectExplorer() { projectName = Textbox_ProjectName.Text, formulaCollection = new ObservableCollection<Formula>() });
                }
            }
        }

        private void Button_ProjectSelection_Click(object sender, RoutedEventArgs e)
        {

            // Create the source string.
            //ObservableCollection<Formula> s = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection;

            // Create the binding description.
            FormulaCollection_MVVM_Selection.selectedProjectIndex = FormulaProject_MVVM.formulaProject.IndexOf((FormulaProjectExplorer)((Button)sender).Tag);
            Binding bindFormulaCollection = new Binding();
            bindFormulaCollection.Mode = BindingMode.TwoWay;
            bindFormulaCollection.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            bindFormulaCollection.Source = ((FormulaProjectExplorer)((Button)sender).Tag).formulaCollection;
            //TextBlock MyText = new TextBlock();
            ItemsControl_FormulaList.SetBinding(ItemsControl.ItemsSourceProperty, bindFormulaCollection);


            TextBlock_SelectedProjectName.Text = ((Button)sender).Content.ToString();

            //TODO: Graph
            //GraphPage_Button.Visibility = Visibility.Visible;
            Graph2DPage_Button.Visibility = Visibility.Visible;

        }
        private void Button_DeleteProject_Click(object sender, RoutedEventArgs e)
        {
            int indexOfProject = FormulaProject_MVVM.formulaProject.IndexOf((FormulaProjectExplorer)((Button)sender).Tag);
            if (indexOfProject != -1)
            {
                if (FormulaCollection_MVVM_Selection.selectedProjectIndex == indexOfProject)
                {
                    TextBlock_SelectedProjectName.Text = "Project Name";
                    FormulaCollection_MVVM_Selection.selectedProjectIndex = -1;
                    FormulaProject_MVVM.formulaProject[indexOfProject].formulaCollection.Clear();
                    FormulaProject_MVVM.formulaProject.RemoveAt(indexOfProject);
                }
                else
                {
                    FormulaProject_MVVM.formulaProject.RemoveAt(indexOfProject);
                }
            }
        }
        private void Button_AddFormula_Click(object sender, RoutedEventArgs e)
        {
            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
            {
                FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection.Add(new Formula());
            }
        }
        private void Button_ClearFormulaList_Click(object sender, RoutedEventArgs e)
        {
            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
            {
                FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection.Clear();
            }
        }
        private async void Button_CreateTab_Click(object sender, RoutedEventArgs e)
        {

            string newTabName = TextBox_NewTabName.Text;
            bool add = true;
            if (!newTabName.Equals(""))
            {
                foreach (var v in FunctionCollectionTab_MVVM.functionCollectionTab)
                {
                    bool breakOut = false;
                    await Task.Run(() =>
                    {
                        if (v.functionTabName.Equals(newTabName, StringComparison.CurrentCulture))
                        {
                            dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                            {
                                add = false;
                            });
                        }
                        if (!add)
                            dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                            {
                                breakOut = true;
                            });
                    });
                    if (breakOut) break;

                }
                if (add)
                {
                    FunctionCollectionTab_MVVM.functionCollectionTab.Add(new FunctionCollection());
                    FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollectionTab_MVVM.functionCollectionTab.Count - 1].functionTabName = newTabName;
                }
            }
        }

        private async void Button_CustomFunctionTab_Click(object sender, RoutedEventArgs e)
        {

            int indexOfSelection = FunctionCollectionTab_MVVM.functionCollectionTab.IndexOf((FunctionCollection)((Button)sender).Tag);
            //var test =  Window.Current.CoreWindow.GetKeyState(VirtualKey.Control);
            if (InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Control).HasFlag(CoreVirtualKeyStates.Down))
            {
                //Ctrl is pressed
                if (!FunctionCollectionTab_MVVM.functionCollectionTab[indexOfSelection].functionTabName.Equals("Default", StringComparison.CurrentCulture)
                    && !FunctionCollectionTab_MVVM.functionCollectionTab[indexOfSelection].functionTabName.Equals("Graph", StringComparison.CurrentCulture)
                    && !FunctionCollectionTab_MVVM.functionCollectionTab[indexOfSelection].functionTabName.Equals("Program", StringComparison.CurrentCulture))
                {

                    foreach (var v in FunctionCollectionTab_MVVM.functionCollectionTab[indexOfSelection].customFunctionCollection_Bind)
                    {
                        await Task.Run(() =>
                        {
                            if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(v.formulaObj.formulaName))
                            {
                                dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                                {
                                    FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Remove(v.formulaObj.formulaName);
                                });
                            }
                        });
                    }
                    FunctionCollectionTab_MVVM.functionCollectionTab[indexOfSelection].customFunctionCollection_Bind.Clear();
                    FunctionCollectionTab_MVVM.functionCollectionTab.RemoveAt(indexOfSelection);
                    if (indexOfSelection == FunctionCollection_MVVM_Selection.functionCollection_Selection)
                    {
                        FunctionCollection_MVVM_Selection.functionCollection_Selection = -1;
                    }
                }

            }
            else
            {
                FunctionCollection_MVVM_Selection.functionCollection_Selection = FunctionCollectionTab_MVVM.functionCollectionTab.IndexOf((FunctionCollection)((Button)sender).Tag);

                Binding bindFunctionCollection = new Binding();
                bindFunctionCollection.Mode = BindingMode.TwoWay;
                bindFunctionCollection.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                bindFunctionCollection.Source = ((FunctionCollection)((Button)sender).Tag).customFunctionCollection_Bind;
                //TextBlock MyText = new TextBlock();
                ItemsControl_FuncitonList.SetBinding(ItemsControl.ItemsSourceProperty, bindFunctionCollection);
            }

        }
        private void Button_FormulaName_Click(object sender, RoutedEventArgs e)
        {
            if (((Formula)(((Button)sender).Tag)).visibility == Visibility.Collapsed)
                ((Formula)(((Button)sender).Tag)).visibility = Visibility.Visible;
            else
                ((Formula)(((Button)sender).Tag)).visibility = Visibility.Collapsed;

        }
        private async void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            string textToSearch = TextBox_SearchFormula.Text;

            //await Task.Run(() =>
            //{

            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
                if (string.IsNullOrWhiteSpace(textToSearch))
                    for (int i = 0; i < FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection.Count; i++)
                    {
                        await Task.Run(() =>
                        {
                            if (FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].visibilitySearch == Visibility.Collapsed)

                                dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                                    {
                                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].visibilitySearch = Visibility.Visible;

                                    });
                        });
                    }
                else
                    //bool found = false;
                    for (int i = 0; i < FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection.Count; i++)
                    {
                        bool breakOut = false;
                        await Task.Run(() =>
                        {
                            bool continu = false;
                            if (!string.IsNullOrWhiteSpace(FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].formulaName) &&
                                FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].formulaName.Contains(textToSearch, StringComparison.CurrentCultureIgnoreCase))
                            {
                                dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                                {
                                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].visibilitySearch = Visibility.Visible;

                                });
                                continu = true;
                                //continue;
                            }
                            if (!string.IsNullOrWhiteSpace(FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].formula) &&
                                FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].formula.Contains(textToSearch, StringComparison.CurrentCultureIgnoreCase))
                            {
                                dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                                {
                                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].visibilitySearch = Visibility.Visible;

                                });
                                continu = true;
                            }
                            if (!string.IsNullOrWhiteSpace(FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].description) &&
                                FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].description.Contains(textToSearch, StringComparison.CurrentCultureIgnoreCase))
                            {
                                dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                                {
                                    FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].visibilitySearch = Visibility.Visible;

                                });
                                continu = true;
                            }
                            if (!continu)
                            {
                                bool found = false;
                                for (int q = 0; q < FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].variableList_Bind.Count; q++)
                                {
                                    if (!string.IsNullOrWhiteSpace(FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].variableList_Bind[q].description) &&
                                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].variableList_Bind[q].description.Contains(textToSearch, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                                        {
                                            FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].visibilitySearch = Visibility.Visible;
                                            breakOut = true;

                                        });
                                        found = true;
                                        //break;
                                    }
                                }

                                if (!found)
                                    dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                                    {
                                        FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection[i].visibilitySearch = Visibility.Collapsed;
                                    });
                            }
                        });
                        if (breakOut) { break; }
                    }
            //});

        }

        private void Button_DataSpreadsheet_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(Measurement_Spreadsheet));
            ViewPages.open_DataSpreadSheet();
        }

        private void Button_Graph_Click(object sender, RoutedEventArgs e)
        {
            ViewPages.open_Graph();
        }

        private void Button_2DVisualGraph_Click(object sender, RoutedEventArgs e)
        {
            ViewPages.open_2DVisualGraph();
        }



        private void Button_HideShow_ProjectExplorer_Click(object sender, RoutedEventArgs e)
        {
            if (Grid_Main.ColumnDefinitions[0].Width.Value > 0d)
                Grid_Main.ColumnDefinitions[0].Width = new GridLength(0d);
            else
                Grid_Main.ColumnDefinitions[0].Width = new GridLength(500d);
        }














        private async void Button_SaveFormula_Click(object sender, RoutedEventArgs e)
        {


            Task t = await SaveLoad.saveFormulaPicker("formula", Textblock_LoadedFormulaFile);
            await t.AsAsyncAction();
            if (t.IsCompleted)
            {

                await Task.Delay(1000);
                Task t2 = ViewPages.close_loadingScreen();
                await t2.AsAsyncAction();

                if (t2.IsCompleted)
                {
                    if (SaveLoad.isPickerSaved)
                        if (!string.IsNullOrWhiteSpace(Textblock_LoadedFormulaFile.Text) &&
                            !Textblock_LoadedFormulaFile.Text.Equals("", StringComparison.CurrentCulture))
                        {
                            await Task.Delay(1000);
                            ViewPages.open_QuickSavePrompt(this);
                        }
                }

            }



        }

        private async void Button_LoadFormula_Click(object sender, RoutedEventArgs e)
        {
            Task t = SaveLoad.loadFormulaPicker("formula", Textblock_LoadedFormulaFile, Textblock_LoadedButtonsFile, TextBlock_SelectedProjectName);

            await t.AsAsyncAction();
        }
        private async void Button_SaveButtons_Click(object sender, RoutedEventArgs e)
        {
            Task t = await SaveLoad.saveFormulaPicker("button", Textblock_LoadedButtonsFile);
            await t.AsAsyncAction();

            if (t.IsCompleted)
            {
                await Task.Delay(1000);
                Task t2 = ViewPages.close_loadingScreen();
                await t2.AsAsyncAction();

            }

        }
        private async void Button_LoadButtons_Click(object sender, RoutedEventArgs e)
        {
            Task t = SaveLoad.loadFormulaPicker("button", Textblock_LoadedButtonsFile);
            await t.AsAsyncAction();

        }










        private void Button_Fromula_GotFocus(object sender, RoutedEventArgs e)
        {
            formula_GotFocus = ((TextBox)sender);
        }


        private void Button_CustomButton_RightTapped(object sender, RoutedEventArgs e)
        {
            ViewPages.open_ButtonDescription((CustomFunction)((Button)sender).Tag);
        }


        private void Button_CustomButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Control).HasFlag(CoreVirtualKeyStates.Down))
            {
                if (FunctionCollection_MVVM_Selection.functionCollection_Selection != -1)
                {
                    if (!FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].functionTabName.Equals("Default", StringComparison.CurrentCulture)
                        && !FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].functionTabName.Equals("Graph", StringComparison.CurrentCulture)
                        && !FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].functionTabName.Equals("Program", StringComparison.CurrentCulture))
                    {
                        if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(((CustomFunction)((Button)sender).Tag).formulaObj.formulaName))
                        {
                            FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Remove(((CustomFunction)((Button)sender).Tag).formulaObj.formulaName);
                        }
                        FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].customFunctionCollection_Bind.RemoveAt(
                            FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].customFunctionCollection_Bind.IndexOf((CustomFunction)((Button)sender).Tag));
                    }
                }

            }
            else
            {
                if (formula_GotFocus != null)
                {
                    formula_GotFocus.SelectedText = ((CustomFunction)((Button)sender).Tag).use;
                }
            }
        }









        private void Button_DeleteFormula_Click(object sender, RoutedEventArgs e)
        {
            if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
            {
                int index = FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection.IndexOf(((Formula)((Button)sender).Tag));
                FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection.RemoveAt(index);
            }

        }
        private void Button_CreateFunction_Click(object sender, RoutedEventArgs e)
        {
            Formula formula = ((Formula)((Button)sender).Tag);
            FunctionCollection collection = null;
            bool willInfinityLoop = false;
            if (!string.IsNullOrWhiteSpace(formula.formulaName) && !string.IsNullOrWhiteSpace(formula.formula))
            {
                if (!string.IsNullOrWhiteSpace(formula.formula))
                {
                    int index = formula.formula.Length - 1;
                    while (formula.formula.LastIndexOf(formula.formulaName + "(", index) != -1)
                    {
                        if (formula.formula.LastIndexOf(formula.formulaName + "(", index) == 0)
                        {
                            willInfinityLoop = true;
                            break;
                        }
                        index = formula.formula.LastIndexOf(formula.formulaName + "(", index) - 1;
                        if (new MathVue<bool>().isOperator(formula.formula[index])
                            || new MathVue<bool>().isParenthesis(formula.formula[index]))
                        {
                            willInfinityLoop = true;
                            break;
                        }
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(formula.formulaName) && !willInfinityLoop)
            {



                if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(formula.formulaName))
                {
                    if (FunctionCollection_MVVM_Selection.functionCollection_Selection != -1)
                    {
                        if (!FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].functionTabName.Equals("Default", StringComparison.CurrentCulture)
                            && !FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].functionTabName.Equals("Graph", StringComparison.CurrentCulture)
                            && !FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].functionTabName.Equals("Program", StringComparison.CurrentCulture))
                        {
                            collection = FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection];
                            collection.customFunctionCollection_Bind.Add(
                                new CustomFunction(new Formula(formula),
                                    //formula.formulaName,
                                    //formula.formula,
                                    //formula.rearrangedFormula,
                                    use: formula.formulaName + "("
                                    //formula.description,
                                    //null,
                                    //functionTab_IndexLocation: FunctionCollection_MVVM_Selection.functionCollection_Selection.ToString()
                                    )
                                );
                        }
                        else
                        {
                            //same as below else
                            int i = 0;
                            for (; i < FunctionCollectionTab_MVVM.functionCollectionTab.Count; i++)
                            {
                                if (FunctionCollectionTab_MVVM.functionCollectionTab[i].functionTabName.Equals("Custom", StringComparison.CurrentCulture))
                                {
                                    collection = FunctionCollectionTab_MVVM.functionCollectionTab[i];
                                    break;
                                }
                            }
                            if (collection != null)
                            {
                                collection.customFunctionCollection_Bind.Add(
                                new CustomFunction(new Formula(formula),
                                    //formula.formulaName,
                                    //formula.formula,
                                    //formula.rearrangedFormula,
                                    use: formula.formulaName + "("
                                    //formula.description,
                                    //null,
                                    //functionTab_IndexLocation: i.ToString()
                                    )
                                );
                            }
                            else
                            {
                                FunctionCollectionTab_MVVM.functionCollectionTab.Add(new FunctionCollection());
                                collection = FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollectionTab_MVVM.functionCollectionTab.Count - 1];
                                collection.functionTabName = "Custom";
                                collection.customFunctionCollection_Bind.Add(
                                new CustomFunction(new Formula(formula),
                                    //formula.formulaName,
                                    //formula.formula,
                                    //formula.rearrangedFormula,
                                    use: formula.formulaName + "("
                                    //formula.description,
                                    //null,
                                    //functionTab_IndexLocation: (FunctionCollectionTab_MVVM.functionCollectionTab.Count - 1).ToString()
                                    )
                                );
                            }
                        }
                    }
                    else
                    {
                        //same as above else
                        int i = 0;
                        for (; i < FunctionCollectionTab_MVVM.functionCollectionTab.Count; i++)
                        {
                            if (FunctionCollectionTab_MVVM.functionCollectionTab[i].functionTabName.Equals("Custom", StringComparison.CurrentCulture))
                            {
                                collection = FunctionCollectionTab_MVVM.functionCollectionTab[i];
                                break;
                            }
                        }
                        if (collection != null)
                        {
                            collection.customFunctionCollection_Bind.Add(
                            new CustomFunction(new Formula(formula),
                                //formula.formulaName,
                                //formula.formula,
                                //formula.rearrangedFormula,
                                use: formula.formulaName + "("
                                //formula.description,
                                //null,
                                //functionTab_IndexLocation: i.ToString()
                                )
                            );
                        }
                        else
                        {
                            FunctionCollectionTab_MVVM.functionCollectionTab.Add(new FunctionCollection());
                            collection = FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollectionTab_MVVM.functionCollectionTab.Count - 1];
                            collection.functionTabName = "Custom";
                            collection.customFunctionCollection_Bind.Add(
                            new CustomFunction(new Formula(formula),
                                //formula.formulaName,
                                //formula.formula,
                                //formula.rearrangedFormula,
                                use: formula.formulaName + "("
                                //formula.description,
                                //null,
                                //functionTab_IndexLocation: (FunctionCollectionTab_MVVM.functionCollectionTab.Count - 1).ToString()
                                )
                            );
                        }
                    }

                    bool hasVar = false;
                    foreach (var v in formula.variableList_Bind)
                    {
                        if (!new MathVue<object>().isNumber(v.value))
                        {
                            if (!hasVar)
                                hasVar = true;
                            if (!string.IsNullOrWhiteSpace(v.value))
                                collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].use += v.value + ",";
                            else
                                collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].use += v.name + ",";
                            //collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].formulaObj.variableList_Bind.Add(new VariableData() { name = v.name, value = v.value, description = v.description });
                        }
                    }
                    if (hasVar)
                        collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].use =
                            collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].use.Remove(
                                collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].use.LastIndexOf(","), 1) + ")";
                    else
                        collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].use =
                            collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].use += ")";

                    FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[formula.formulaName] =
                        collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1];
                }
            }
        }
        private void Button_CreateConstant_Click(object sender, RoutedEventArgs e)
        {
            Formula formula = ((Formula)((Button)sender).Tag);
            FunctionCollection collection = null;
            bool willInfinityLoop = false;
            //if (!string.IsNullOrWhiteSpace(formula.formulaName) && !string.IsNullOrWhiteSpace(formula.formula))
            //{
            //    if (!string.IsNullOrWhiteSpace(formula.formula))
            //    {
            //        int index = formula.formula.Length - 1;
            //        while (formula.formula.LastIndexOf(formula.formulaName + "(", index) != -1)
            //        {
            //            if (formula.formula.LastIndexOf(formula.formulaName + "(", index) == 0)
            //            {
            //                willInfinityLoop = true;
            //                break;
            //            }
            //            index = formula.formula.LastIndexOf(formula.formulaName + "(", index) - 1;
            //            if (new MathVue<bool>().isOperator(formula.formula[index])
            //                || new MathVue<bool>().isParenthesis(formula.formula[index]))
            //            {
            //                willInfinityLoop = true;
            //                break;
            //            }
            //        }
            //    }
            //}
            if (!string.IsNullOrWhiteSpace(formula.formulaName) && !willInfinityLoop)
            {



                if (!FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.ContainsKey(formula.formulaName))
                {
                    if (FunctionCollection_MVVM_Selection.functionCollection_Selection != -1)
                    {
                        if (!FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].functionTabName.Equals("Default", StringComparison.CurrentCulture)
                            && !FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].functionTabName.Equals("Graph", StringComparison.CurrentCulture)
                            && !FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].functionTabName.Equals("Program", StringComparison.CurrentCulture))
                        {
                            collection = FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection];
                            collection.customFunctionCollection_Bind.Add(
                                new CustomFunction(new Formula(formula),
                                    //formula.formulaName,
                                    //formula.formula,
                                    //formula.rearrangedFormula,
                                    use: formula.formulaName
                                    //formula.description,
                                    //null,
                                    //functionTab_IndexLocation: FunctionCollection_MVVM_Selection.functionCollection_Selection.ToString()
                                    )
                                );
                        }
                        else
                        {
                            int i = 0;
                            //same as below else
                            for (; i < FunctionCollectionTab_MVVM.functionCollectionTab.Count; i++)
                            {
                                if (FunctionCollectionTab_MVVM.functionCollectionTab[i].functionTabName.Equals("Custom", StringComparison.CurrentCulture))
                                {
                                    collection = FunctionCollectionTab_MVVM.functionCollectionTab[i];
                                    break;
                                }
                            }
                            if (collection != null)
                            {
                                collection.customFunctionCollection_Bind.Add(
                                new CustomFunction(new Formula(formula),
                                    //formula.formulaName,
                                    //formula.formula,
                                    //formula.rearrangedFormula,
                                    use: formula.formulaName
                                    //formula.description,
                                    //null,
                                    //functionTab_IndexLocation: i.ToString()
                                    )
                                );
                            }
                            else
                            {
                                FunctionCollectionTab_MVVM.functionCollectionTab.Add(new FunctionCollection());
                                collection = FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollectionTab_MVVM.functionCollectionTab.Count - 1];
                                collection.functionTabName = "Custom";
                                collection.customFunctionCollection_Bind.Add(
                                new CustomFunction(new Formula(formula),
                                    //formula.formulaName,
                                    //formula.formula,
                                    //formula.rearrangedFormula,
                                    use: formula.formulaName
                                    //formula.description,
                                    //null,
                                    //functionTab_IndexLocation: (FunctionCollectionTab_MVVM.functionCollectionTab.Count - 1).ToString()
                                    )
                                );
                            }
                        }
                    }
                    else
                    {
                        int i = 0;
                        //same as above else
                        for (; i < FunctionCollectionTab_MVVM.functionCollectionTab.Count; i++)
                        {
                            if (FunctionCollectionTab_MVVM.functionCollectionTab[i].functionTabName.Equals("Custom", StringComparison.CurrentCulture))
                            {
                                collection = FunctionCollectionTab_MVVM.functionCollectionTab[i];
                                break;
                            }
                        }
                        if (collection != null)
                        {
                            collection.customFunctionCollection_Bind.Add(
                            new CustomFunction(new Formula(formula),
                                //formula.formulaName,
                                //formula.formula,
                                //formula.rearrangedFormula,
                                use: formula.formulaName
                                //formula.description,
                                //null,
                                //functionTab_IndexLocation: i.ToString()
                                )
                            );
                        }
                        else
                        {
                            FunctionCollectionTab_MVVM.functionCollectionTab.Add(new FunctionCollection());
                            collection = FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollectionTab_MVVM.functionCollectionTab.Count - 1];
                            collection.functionTabName = "Custom";
                            collection.customFunctionCollection_Bind.Add(
                            new CustomFunction(new Formula(formula),
                                //formula.formulaName,
                                //formula.formula,
                                //formula.rearrangedFormula,
                                use: formula.formulaName
                                //formula.description,
                                //null,
                                //functionTab_IndexLocation: (FunctionCollectionTab_MVVM.functionCollectionTab.Count - 1).ToString()
                                )
                            );
                        }
                    }

                    //bool hasVar = false;
                    //foreach (var v in formula.variableList_Bind)
                    //{
                    //    if (!hasVar)
                    //        hasVar = true;
                    //    collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].use += v.name + ",";
                    //    collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].variableList_Bind.Add(v);
                    //}
                    //if (hasVar)
                    //    collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].use =
                    //        collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].use.Remove(
                    //            collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].use.LastIndexOf(","), 1) + ")";
                    //else
                    //    collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].use =
                    //        collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1].use += ")";

                    FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[formula.formulaName] =
                        collection.customFunctionCollection_Bind[collection.customFunctionCollection_Bind.Count - 1];
                }
            }
        }

        private void Button_UpdateDefaultTab_Click(object sender, RoutedEventArgs e)
        {
            DefaultCustomButtons.populateButtonWith_Default();
            DefaultCustomButtons.populateButtonWith_DefaultGraph();
            DefaultCustomButtons.populateButtonWith_DefaultProgram();
        }

        private async void Button_SearchSelectedTab_Click(object sender, RoutedEventArgs e)
        {
            if (FunctionCollection_MVVM_Selection.functionCollection_Selection != -1)
            {
                string searchString = TextBox_SearchSelectedTab.Text;
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    for (int i = 0; i < FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].customFunctionCollection_Bind.Count; i++)
                    {
                        await Task.Run(() =>
                        {
                            if (FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].customFunctionCollection_Bind[i].formulaObj.formulaName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                            {
                                dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                                {
                                    FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].customFunctionCollection_Bind[i].visibility = Visibility.Visible;
                                });
                            }
                            else
                            {
                                dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                                {
                                    FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].customFunctionCollection_Bind[i].visibility = Visibility.Collapsed;
                                });
                            }
                        });
                    }
                }
                else
                {
                    for (int i = 0; i < FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].customFunctionCollection_Bind.Count; i++)
                    {
                        await Task.Run(() =>
                        {
                            dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                            {
                                FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].customFunctionCollection_Bind[i].visibility = Visibility.Visible;
                            });
                        });
                    }
                }
            }
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
                            //if (string.IsNullOrWhiteSpace(data.value))
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

            if (formula.comboBox_SolutionType == 0)
            {
                Task t = Task.Run(() =>
                {
                    //await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    //{
                    dispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                    {
                        if (formula.comboBox_Double_OR_AutoDecimal == 0)
                        {
                            formula.solution = new MathVue<double>().solveFormula(formula);
                        }
                        else
                            formula.solution = new MathVue<decimal>().solveFormula(formula);
                    });
                });

                await t.AsAsyncAction();

            }
        }










        private void Button_GetVariables_Click(object sender, RoutedEventArgs e)
        {
            Formula formula = ((Formula)((Button)sender).Tag);

            //formula.variableList_Dictionary.Clear();
            //formula.variableList_Bind.Clear();

            Dictionary<string, VariableData> variableDataDictionary = new System.Collections.Generic.Dictionary<string, VariableData>();

            foreach (VariableData data in formula.variableList_Bind)
            {
                variableDataDictionary.Add(data.name, data);
            }
            formula.variableList_Bind.Clear();
            getVariables(formula, variableDataDictionary);
        }

        private void Button_Solve_Click(object sender, RoutedEventArgs e)
        {
            Formula formula = ((Formula)((Button)sender).Tag);
            solveFormula(formula);
        }

        private void Textbox_Formula_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.Equals(VirtualKey.Menu))
            {
                Formula formula = ((Formula)((TextBox)sender).Tag);

                //formula.variableList_Dictionary.Clear();
                //formula.variableList_Bind.Clear();

                Dictionary<string, VariableData> variableDataDictionary = new System.Collections.Generic.Dictionary<string, VariableData>();

                foreach (VariableData data in formula.variableList_Bind)
                {
                    variableDataDictionary.Add(data.name, data);
                }
                formula.variableList_Bind.Clear();
                getVariables(formula, variableDataDictionary);
            }
        }

        private void Textbox_VariableValue_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.Equals(VirtualKey.Menu))
            {
                Formula formula = ((Formula)((TextBox)sender).Tag);
                solveFormula(formula);
            }
        }

        private void Window_SizeChanged(object sender, Microsoft.UI.Xaml.WindowSizeChangedEventArgs args)
        {
            double maxHeight = args.Size.Height - 155d;

            if (maxHeight > 1)
            {
                Scrollviewer_CustomFunctionList.MaxHeight = maxHeight;
                ScrollViewer_FormulaList.MaxHeight = maxHeight;
                Scrollviewer_FormulaProjectList.MaxHeight = maxHeight;
            }
            else
            {
                Scrollviewer_CustomFunctionList.MaxHeight = 100d;
                ScrollViewer_FormulaList.MaxHeight = 100d;
                Scrollviewer_FormulaProjectList.MaxHeight = 100;
            }

            //Scrollviewer_Main.MaxWidth = args.Size.Width;
            //ScrollViewer_CustomFunctionList.MaxWidth = args.Size.Width-1055d;
        }

        private void TextBox_VariableValue_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            tbox.SelectAll();
        }
        private void TextBox_VariableDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            tbox.SelectAll();
        }
    }
}
