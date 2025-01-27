using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Perseverance_Calculator.View.Pages;
using Perseverance_Calculator_1.Controller.SaveLoad;
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
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Microsoft.UI.Dispatching;
using Windows.ApplicationModel.Background;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Perseverance_Calculator_1.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Data_Spreadsheet : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DataDataCollection_Project_MVVM.dataDataCollectionProject = (ObservableCollection<DataDataCollection>)e.Parameter;

            if (!SaveLoad.fileSavedLoadData_Name.Equals("", StringComparison.CurrentCulture))
                Textblock_LoadedSpreadsheet.Text = SaveLoad.fileSavedLoadData_Name;

            // parameters.Name
            // parameters.Text
            // ...
        }
        //ApplicationView view;
        public Data_Spreadsheet()
        {
            this.InitializeComponent();
            //view = ApplicationView.GetForCurrentView();
            //view.Consolidated += View_Consolidated;
            ViewPages.dataView.Closed += Current_Closed;
            //ApplicationView.GetForCurrentView().Title = "Data Spreadsheet";

        }

        //private async void View_Consolidated(ApplicationView sender, ApplicationViewConsolidatedEventArgs args)
        //{
        //    if (ViewPages.dataView != null)
        //        await ViewPages.dataView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //        {
        //            ViewPages.dataView.CoreWindow.Close();
        //        });
        //}

        private void Current_Closed(object sender, WindowEventArgs e)
        {
            //ViewPages.dataView.CoreWindow.Close();
            //ViewPages.dataView.Close();
            ViewPages.dataView = null;
        }

        private async void Button_AddSpreadsheet_Click(object sender, RoutedEventArgs e)
        {
            bool add = true;
            string mainString = Textbox_DataSpreadsheetName.Text;
            if (!string.IsNullOrWhiteSpace(mainString))
                foreach (var v in DataDataCollection_Project_MVVM.dataDataCollectionProject)
                {
                    bool breakOut = false;
                    await Task.Run(() =>
                    {
                        if (v.projectName == mainString)
                        {
                            ((Button)sender).DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                            {
                                add = false;
                                breakOut = true;
                            });
                            //break;
                        }
                    });
                    if (breakOut) break;
                }
            else
                add = false;
            if (add)
            {
                DataDataCollection_Project_MVVM.dataDataCollectionProject.Add(new DataDataCollection());
                DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM.dataDataCollectionProject.Count - 1].projectName = Textbox_DataSpreadsheetName.Text;
            }
        }
        private void Button_ProjectSelection_Click(object sender, RoutedEventArgs e)
        {
            DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection = DataDataCollection_Project_MVVM.dataDataCollectionProject.IndexOf((DataDataCollection)((Button)sender).Tag);

            Binding bindDataCollection = new Binding();
            bindDataCollection.Mode = BindingMode.TwoWay;
            bindDataCollection.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            bindDataCollection.Source = ((DataDataCollection)((Button)sender).Tag).dataDataCollection;
            //TextBlock MyText = new TextBlock();
            ItemsControl_DataDataList.SetBinding(ItemsControl.ItemsSourceProperty, bindDataCollection);
        }
        private void Button_AddData_Click(object sender, RoutedEventArgs e)
        {
            if (DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection != -1)
            {
                DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection.Add(new DataData());
            }
        }

        private void Button_RemoveDataType_Click(object sender, RoutedEventArgs e)
        {
            if (DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection != -1)
            {
                DataData dataToRem = ((DataData)((Button)sender).Tag);

                if (!string.IsNullOrWhiteSpace(dataToRem.dataNamePrev))
                    ((Button)sender).DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                    {
                        DataDataCollection_Project_MVVM.dataValueList.Remove(dataToRem.dataNamePrev);
                    });

                int indexOfData = DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection.IndexOf(dataToRem);
                if (indexOfData != -1)
                    DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection.RemoveAt(indexOfData);
                //if (!string.IsNullOrWhiteSpace(dataToRem.dataName))
                //    await this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                //    {
                //        DataDataCollection_Project_MVVM.dataValueList.Remove(dataToRem.dataName);
                //    });
            }
        }
        private async void Button_RemoveSelectedProject_Click(object sender, RoutedEventArgs e)
        {
            DataDataCollection dataToRem = ((DataDataCollection)((Button)sender).Tag);

            int count = dataToRem.dataDataCollection.Count;
            for (int i = 0; i < count;)
            {
                await Task.Run(() =>
                {
                    //foreach (var v in dataToRem.dataDataCollection)
                    if (!string.IsNullOrWhiteSpace(dataToRem.dataDataCollection[i].dataName))
                        App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                        {
                            DataDataCollection_Project_MVVM.dataValueList.Remove(dataToRem.dataDataCollection[i].dataName);

                        });
                    if (!string.IsNullOrWhiteSpace(dataToRem.dataDataCollection[i].dataNamePrev))
                        App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                        {
                            DataDataCollection_Project_MVVM.dataValueList.Remove(dataToRem.dataDataCollection[i].dataNamePrev);

                        });
                    ((Button)sender).DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                    {
                        dataToRem.dataDataCollection.RemoveAt(i);
                    });
                });
                count = dataToRem.dataDataCollection.Count;
            }
            DataDataCollection_Project_MVVM.dataDataCollectionProject.Remove(dataToRem);
            DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection = -1;
        }

        private async void Button_SearchData_Click(object sender, RoutedEventArgs e)
        {
            if (DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection != -1)
            {
                string mainString = Textbox_SearchData.Text;
                int count = DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection.Count;


                if (string.IsNullOrWhiteSpace(mainString))
                {
                    for (int i = 0; i < count; i++)
                    {
                        await Task.Run(() =>
                        {
                            ((Button)sender).DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                            {
                                int asd = i;
                                DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection[i].searchVisibility = Visibility.Visible;
                            });
                        });
                    }
                }
                else
                    for (int i = 0; i < count; i++)
                    {
                        await Task.Run(() =>
                        {
                            bool continu = false;
                            //foreach (var v in DataDataCollection_Project_MVVM.dataDataCollectionProject)
                            if (!string.IsNullOrWhiteSpace(DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection[i].dataName) &&
                            DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection[i].dataName.Contains(mainString, StringComparison.CurrentCultureIgnoreCase))
                            {
                                ((Button)sender).DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                {
                                    DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection[i].searchVisibility = Visibility.Visible;
                                });
                                continu = true;
                                //continue;
                            }
                            else if (!string.IsNullOrWhiteSpace(DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection[i].value) &&
                            DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection[i].value.Contains(mainString, StringComparison.CurrentCultureIgnoreCase))
                            {
                                ((Button)sender).DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                {
                                    DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection[i].searchVisibility = Visibility.Visible;
                                });
                                continu = true;
                                //continue;
                            }
                            else if (!string.IsNullOrWhiteSpace(DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection[i].description) &&
                            DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection[i].description.Contains(mainString, StringComparison.CurrentCultureIgnoreCase))
                            {
                                ((Button)sender).DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                {
                                    DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection[i].searchVisibility = Visibility.Visible;
                                });
                                continu = true;
                                //continue;
                            }
                            if (!continu)
                            {
                                ((Button)sender).DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                {
                                    DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection[i].searchVisibility = Visibility.Collapsed;
                                });
                            }
                        });
                    }

            }
        }

        private void Button_SetData_Click(object sender, RoutedEventArgs e)
        {
            DataData dataData = ((DataData)((Button)sender).Tag);
            bool changed = false;

            if (!string.IsNullOrWhiteSpace(dataData.dataName))
            {
                if (!string.IsNullOrWhiteSpace(dataData.dataNamePrev))
                {
                    //await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    //{
                    if (DataDataCollection_Project_MVVM.dataValueList.ContainsKey(dataData.dataNamePrev))
                    {
                        if (!DataDataCollection_Project_MVVM.dataValueList.ContainsKey(dataData.dataName))
                        {
                            //if (!dataData.dataNamePrev.Equals(dataData.dataName, StringComparison.CurrentCulture))
                            //{
                            if (!string.IsNullOrWhiteSpace(dataData.value))
                            {
                                DataDataCollection_Project_MVVM.dataValueList.Remove(dataData.dataNamePrev);
                                DataDataCollection_Project_MVVM.dataValueList[dataData.dataName] = dataData.value;
                                dataData.valuePrev = dataData.value;
                                changed = true;
                            }
                            //}
                        }
                        else if (dataData.dataNamePrev.Equals(dataData.dataName, StringComparison.CurrentCulture))
                        {
                            if (!string.IsNullOrWhiteSpace(dataData.value))
                            {
                                DataDataCollection_Project_MVVM.dataValueList.Remove(dataData.dataNamePrev);
                                DataDataCollection_Project_MVVM.dataValueList[dataData.dataName] = dataData.value;
                                dataData.valuePrev = dataData.value;
                                changed = true;
                            }
                        }
                    }
                    else
                    {
                        //await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                        //{
                        if (!DataDataCollection_Project_MVVM.dataValueList.ContainsKey(dataData.dataName))
                        {
                            if (!string.IsNullOrWhiteSpace(dataData.value))
                            {
                                //if (!dataData.dataNamePrev.Equals(dataData.dataName, StringComparison.CurrentCulture))
                                //{
                                //DataDataCollection_Project_MVVM.dataValueList.Remove(dataData.dataNamePrev);
                                DataDataCollection_Project_MVVM.dataValueList[dataData.dataName] = dataData.value;
                                dataData.valuePrev = dataData.value;
                                changed = true;
                            }
                            //}
                        }
                        //});
                    }

                    //});
                }
                else
                {
                    //await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    //{
                    if (!DataDataCollection_Project_MVVM.dataValueList.ContainsKey(dataData.dataName))
                    {
                        if (!string.IsNullOrWhiteSpace(dataData.value))
                        {
                            //if (!dataData.dataNamePrev.Equals(dataData.dataName, StringComparison.CurrentCulture))
                            //{
                            //DataDataCollection_Project_MVVM.dataValueList.Remove(dataData.dataNamePrev);
                            DataDataCollection_Project_MVVM.dataValueList[dataData.dataName] = dataData.value;
                            dataData.valuePrev = dataData.value;
                            changed = true;
                        }
                        //}
                    }
                    //});
                }

            }
            if (changed)
            {
                dataData.dataNamePrev = dataData.dataName;
                dataData.dataChanged = false;

            }

        }

        private void Textbox_dataNameType_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((DataData)((TextBox)sender).Tag).dataChanged = true;
        }


        private void TextBox_dataNameType_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection != -1)
            {
                if (((DataData)((TextBox)sender).Tag) != null)
                    if (!((DataData)((TextBox)sender).Tag).dataChanged)
                    {
                        //int indexOfData = DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection.IndexOf(dataToRem);
                        //DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection);
                        DataDataCollection_Project_MVVM.dataValueList.Remove(((TextBox)sender).Text);
                        ((DataData)((TextBox)sender).Tag).dataNamePrev = "";


                    }
            }
        }


        private void Textbox_dataValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((DataData)((TextBox)sender).Tag).dataChanged = true;
        }
        private async void Button_SaveData_Click(object sender, RoutedEventArgs e)
        {
            Task t = await SaveLoad.saveFormulaPicker("data", Textblock_LoadedSpreadsheet);
            await t.AsAsyncAction();
            if (t.IsCompleted)
            {

                await Task.Delay(1000);
                Task t2 = ViewPages.close_loadingScreen();
                await t2.AsAsyncAction();
                //    if (ViewPages.loadingScreenView != null)
                //    {
                //        Task t2 = ViewPages.close_loadingScreen();
                //        await t2.AsAsyncAction();
                //    }
            }
            //if (!SaveLoad.fileSavedLoadData_Name.Equals("", StringComparison.CurrentCulture))
            //    Textblock_LoadedSpreadsheet.Text = SaveLoad.fileSavedLoadData_Name;
        }
        private async void Button_LoadData_Click(object sender, RoutedEventArgs e)
        {
            Task t = SaveLoad.loadFormulaPicker("data", Textblock_LoadedSpreadsheet);
            await t.AsAsyncAction();
            //if (!SaveLoad.fileSavedLoadData_Name.Equals("", StringComparison.CurrentCulture))
            //    Textblock_LoadedSpreadsheet.Text = SaveLoad.fileSavedLoadData_Name;
        }

        private void TextBox_DataName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            tbox.SelectAll();
        }

        private void TextBox_DataValue_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            tbox.SelectAll();
        }

        private void TextBox_DataDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            tbox.SelectAll();
        }




        private void Button_HideShow_ProjectExplorer_Click(object sender, RoutedEventArgs e)
        {
            if (Grid_Main.ColumnDefinitions[0].Width.Value > 0d)
                Grid_Main.ColumnDefinitions[0].Width = new GridLength(0d);
            else
                Grid_Main.ColumnDefinitions[0].Width = new GridLength(500d);
        }



    }
}
