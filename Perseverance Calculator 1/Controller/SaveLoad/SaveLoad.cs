using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using Perseverance_Calculator_1.Model.MVVM;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Collections.ObjectModel;
using Perseverance_Calculator_1.Model;
using System.Diagnostics;
using Microsoft.UI.Xaml.Controls;
using System.Xml;
using Windows.UI.Core;
using Windows.Storage.AccessCache;
using Windows.UI.ViewManagement;
using Perseverance_Calculator.View.Pages;
using Windows.UI.Xaml;
using Microsoft.Win32.SafeHandles;
using System.Threading;
using Microsoft.Graphics.Canvas.Text;
using Windows.Storage.Pickers;
using WinRT.Interop;
using Microsoft.UI.Dispatching;

namespace Perseverance_Calculator_1.Controller.SaveLoad
{

    public class SaveLoad
    {
        public static bool isPickerSaved = true;
        public static string fileSavedLoadFormula_Name = "";
        public static string fileSavedLoadButtons_Name = "";
        public static string fileSavedLoadData_Name = "";
        public static string fileSavedLoadMeasure_Name = "";
        public static async Task<Task> saveFormulaPicker(string is_formula_button_data, TextBlock tbox)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            if (is_formula_button_data.Equals("formula", StringComparison.CurrentCulture))
            {
                picker.FileTypeChoices.Add("Formula List", new List<string>() { ".formula" });
                picker.SuggestedFileName = fileSavedLoadFormula_Name;
            }
            else if (is_formula_button_data.Equals("button", StringComparison.CurrentCulture))
            {
                picker.FileTypeChoices.Add("Custom Buttons", new List<string>() { ".button" });
                picker.SuggestedFileName = fileSavedLoadButtons_Name;
            }
            else if (is_formula_button_data.Equals("data", StringComparison.CurrentCulture))
            {
                picker.FileTypeChoices.Add("Data Spreadsheet", new List<string>() { ".data" });
                picker.SuggestedFileName = fileSavedLoadData_Name;
            }
            //else if (is_formula_button_data.Equals("measure", StringComparison.CurrentCulture))
            //{
            //    picker.FileTypeChoices.Add("Measurement Spreadsheet", new List<string>() { ".measure" });
            //    picker.SuggestedFileName = fileSavedLoadMeasure_Name;
            //}

            nint windowHandle = WindowNative.GetWindowHandle(App.m_window);
            InitializeWithWindow.Initialize(picker, windowHandle);

            Windows.Storage.StorageFile file = await picker.PickSaveFileAsync();
            Task t = Task.Run(() => { });

            if (file != null && !string.IsNullOrWhiteSpace(file.DisplayName) && !file.DisplayName.Equals("", StringComparison.CurrentCulture))
            {
                if (ViewPages.quickSavePromptView != null)
                {

                    Task closePrompt = Task.Run(() =>
                    {
                        ViewPages.quickSavePromptView.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                        {
                            ViewPages.quickSavePromptView.Close();
                        });
                    });
                    await closePrompt.AsAsyncAction();
                }

                t = ViewPages.open_loadingScreen();
                await t.AsAsyncAction();
                await Task.Delay(500);

                if (t.IsCompleted)
                {
                    lock (file)
                    {
                        return Task.Run(async () =>
                        {
                            if (file != null && !string.IsNullOrWhiteSpace(file.DisplayName) && !file.DisplayName.Equals("", StringComparison.CurrentCulture))
                            {

                                AsyncReaderWriterLock m_lock = new AsyncReaderWriterLock();

                                using (var releaser = await m_lock.WriterLockAsync())
                                {
                                    // code #1
                                    //t = SomethingAsync();
                                    //await t;
                                    //using (var releaser = await m_lock.WriterLockAsync())
                                    //{
                                    //    // code #2
                                    //}
                                    //CachedFileManager.DeferUpdates(file);
                                    //StorageApplicationPermissions.MostRecentlyUsedList;

                                    XmlWriterSettings settings = new XmlWriterSettings();
                                    settings.Async = true;
                                    settings.Encoding = Encoding.UTF8;
                                    settings.OmitXmlDeclaration = true;
                                    StringBuilder builder = new StringBuilder();

                                    if (is_formula_button_data.Equals("formula", StringComparison.CurrentCulture))
                                    {
                                        XmlSerializer serializer1 = new XmlSerializer(typeof(ObservableCollection<FormulaProjectExplorer>));
                                        //using (Stream stream = await file.OpenStreamForWriteAsync())
                                        //{
                                        using (XmlWriter writer = XmlWriter.Create(builder, settings))
                                            serializer1.Serialize(writer, FormulaProject_MVVM.formulaProject);
                                        //stream.Close();
                                        //}
                                        fileSavedLoadFormula_Name = file.DisplayName;
                                    }
                                    else if (is_formula_button_data.Equals("button", StringComparison.CurrentCulture))
                                    {
                                        XmlSerializer serializer1 = new XmlSerializer(typeof(ObservableCollection<FunctionCollection>));
                                        //using (Stream stream = await file.OpenStreamForWriteAsync())
                                        //{
                                        using (XmlWriter writer = XmlWriter.Create(builder, settings))
                                            serializer1.Serialize(writer, FunctionCollectionTab_MVVM.functionCollectionTab);
                                        //    stream.Close();
                                        //}
                                        fileSavedLoadButtons_Name = file.DisplayName;
                                    }
                                    else if (is_formula_button_data.Equals("data", StringComparison.CurrentCulture))
                                    {
                                        XmlSerializer serializer1 = new XmlSerializer(typeof(ObservableCollection<DataDataCollection>));
                                        //using (Stream stream = await file.OpenStreamForWriteAsync())
                                        //{
                                        using (XmlWriter writer = XmlWriter.Create(builder, settings))
                                            serializer1.Serialize(writer, DataDataCollection_Project_MVVM.dataDataCollectionProject);
                                        //    stream.Close();
                                        //}
                                        fileSavedLoadData_Name = file.DisplayName;
                                    }
                                    //await Windows.Storage.FileIO.WriteTextAsync(file, builder.ToString());
                                    //using (Stream stream = await file.OpenStreamForWriteAsync())
                                    //{
                                    //    XmlSerializer serializer1 = new XmlSerializer(typeof(ObservableCollection<FormulaProjectExplorer>));

                                    //    System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
                                    //    settings.Indent = true;
                                    //    settings.Async = true;

                                    //    CachedFileManager.DeferUpdates(file);
                                    //    using (XmlWriter writer = System.Xml.XmlWriter.Create(stream, settings))
                                    //    {
                                    //        serializer1.Serialize(stream, FormulaProject_MVVM.formulaProject);
                                    //    }
                                    //    stream.Close();
                                    //}
                                    //while (await CachedFileManager.CompleteUpdatesAsync(file) == Windows.Storage.Provider.FileUpdateStatus.Incomplete)

                                    //using (SafeFileHandle fileHandle = file.CreateSafeFileHandle(FileAccess.Write, FileShare.Read, FileOptions.None))
                                    //{
                                    //await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
                                    //{
                                    //await Task.Factory.StartNew(async () =>
                                    //{
                                    while (true)
                                    {
                                        try
                                        {
                                            //await Task.Run(async() => {
                                            CachedFileManager.DeferUpdates(file);
                                            await Windows.Storage.FileIO.WriteTextAsync(file, builder.ToString(), encoding: Windows.Storage.Streams.UnicodeEncoding.Utf8);
                                            //});
                                            break;
                                        }
                                        catch
                                        {
                                            await Task.Delay(1000);
                                        }
                                    }
                                    //}, CancellationToken.None, TaskCreationOptions.None, null);
                                    //});
                                    //    fileHandle.Close();
                                    //    fileHandle.Dispose();
                                    //}
                                    if (await CachedFileManager.CompleteUpdatesAsync(file) == Windows.Storage.Provider.FileUpdateStatus.Complete)
                                    {
                                        isPickerSaved = true;
                                        //file = null;
                                        //if (await CachedFileManager.CompleteUpdatesAsync(file) == Windows.Storage.Provider.FileUpdateStatus.Complete)
                                        ////{
                                        //if (ViewPages.loadingScreenView != null)
                                        //{
                                        //    Task t2 = ViewPages.close_loadingScreen();
                                        //    await t2.AsAsyncAction();
                                        //    //if(t2.IsCompleted) { }
                                        //    //await ViewPages.loadingScreenView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                        //    //{
                                        //    //    ViewPages.loadingScreenView.CoreWindow.Close();
                                        //    //    //ViewPages.loadingScreenView = null;
                                        //    //    //Window.Current.Close();
                                        //    //});
                                        //}
                                        //if (t.Status == TaskStatus.RanToCompletion)
                                        //{

                                        //if (ViewPages.loadingScreenView != null)
                                        //    await ViewPages.loadingScreenView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                        //    {
                                        //        ViewPages.loadingScreenView.CoreWindow.Close();
                                        //        ViewPages.loadingScreenView = null;
                                        //        //Window.Current.Close();
                                        //    });
                                        //}

                                        if (is_formula_button_data.Equals("formula", StringComparison.CurrentCulture))
                                        {
                                            if (!SaveLoad.fileSavedLoadFormula_Name.Equals("", StringComparison.CurrentCulture))
                                                if (tbox != null)
                                                    tbox.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                                                    {
                                                        tbox.Text = fileSavedLoadFormula_Name;
                                                    });
                                        }
                                        else if (is_formula_button_data.Equals("button", StringComparison.CurrentCulture))
                                        {
                                            if (!SaveLoad.fileSavedLoadButtons_Name.Equals("", StringComparison.CurrentCulture))
                                                if (tbox != null)
                                                    tbox.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                                                    {
                                                        tbox.Text = fileSavedLoadButtons_Name;
                                                    });
                                        }
                                        else if (is_formula_button_data.Equals("data", StringComparison.CurrentCulture))
                                        {
                                            if (!SaveLoad.fileSavedLoadData_Name.Equals("", StringComparison.CurrentCulture))
                                                if (tbox != null)
                                                    tbox.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                                                    {
                                                        tbox.Text = fileSavedLoadData_Name;
                                                    });
                                        }
                                    }
                                }

                            }
                            else
                            {
                                isPickerSaved = false;
                            }

                            if (is_formula_button_data.Equals("formula", StringComparison.CurrentCulture))
                            {
                                if (tbox != null)
                                    tbox.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                                        {
                                            tbox.Text = fileSavedLoadFormula_Name;
                                        });
                            }
                            else if (is_formula_button_data.Equals("button", StringComparison.CurrentCulture))
                            {
                                if (tbox != null)
                                    tbox.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                                        {
                                            tbox.Text = fileSavedLoadButtons_Name;
                                        });
                            }
                            else if (is_formula_button_data.Equals("data", StringComparison.CurrentCulture))
                            {
                                if (tbox != null)
                                    tbox.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                                        {
                                            tbox.Text = fileSavedLoadData_Name;
                                        });
                            }
                            //return "";
                        });
                    }
                }
            }
            return Task.Run(() =>
            {
                isPickerSaved = false;
            });

        }














        public static async Task<Task> loadFormulaPicker(string is_formula_button_data, TextBlock tbox_DisplayFile = null, TextBlock tbox_DisplayFile_Btn = null, TextBlock Tbox_SelectedProjectName = null)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            //picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
            //picker.SuggestedStartLocation = ApplicationData.Current.LocalFolder;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;

            if (is_formula_button_data.Equals("formula", StringComparison.CurrentCulture))
            {
                picker.FileTypeFilter.Add(".formula");
            }
            else if (is_formula_button_data.Equals("button", StringComparison.CurrentCulture))
            {
                picker.FileTypeFilter.Add(".button");
            }
            else if (is_formula_button_data.Equals("data", StringComparison.CurrentCulture))
            {
                picker.FileTypeFilter.Add(".data");
            }

            nint windowHandle = WindowNative.GetWindowHandle(App.m_window);
            InitializeWithWindow.Initialize(picker, windowHandle);

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            Task t = Task.Run(() => { });
            if (file != null && !string.IsNullOrWhiteSpace(file.DisplayName) && !file.DisplayName.Equals("", StringComparison.CurrentCulture))
            {
                t = ViewPages.open_loadingScreen();
                await t.AsAsyncAction();
                await Task.Delay(500);

                lock (file)
                {
                    return Task.Run(async () =>
                    {
                        if (t.IsCompleted)
                        {
                            AsyncReaderWriterLock m_lock = new AsyncReaderWriterLock();

                            using (var releaser = await m_lock.ReaderLockAsync())
                            {
                                string data = "";
                                while (true)
                                {
                                    try
                                    {
                                        //await Task.Run(async () =>
                                        //{
                                        CachedFileManager.DeferUpdates(file);
                                        data = await FileIO.ReadTextAsync(file);
                                        //});
                                        break;
                                    }
                                    catch { await Task.Delay(1000); }
                                }
                                if (is_formula_button_data.Equals("formula", StringComparison.CurrentCulture))
                                {
                                    if (FormulaCollection_MVVM_Selection.selectedProjectIndex != -1)
                                    {
                                        App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                        {
                                            FormulaProject_MVVM.formulaProject[FormulaCollection_MVVM_Selection.selectedProjectIndex].formulaCollection.Clear();

                                            FormulaCollection_MVVM_Selection.selectedProjectIndex = -1;
                                        });
                                    }
                                    if (FormulaProject_MVVM.formulaProject.Count > 0)
                                        App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                        {
                                            FormulaProject_MVVM.formulaProject.Clear();
                                        });
                                    XmlSerializer serializer1 = new XmlSerializer(typeof(ObservableCollection<FormulaProjectExplorer>));

                                    using (StringReader reader = new StringReader(data))
                                    {
                                        await Task.Run(() =>
                                        {
                                            foreach (var v in (ObservableCollection<FormulaProjectExplorer>)serializer1.Deserialize(reader))
                                            {
                                                App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                                {
                                                    FormulaProject_MVVM.formulaProject.Add(v);
                                                });
                                            }
                                        });
                                    }
                                    fileSavedLoadFormula_Name = file.DisplayName;
                                }
                                else if (is_formula_button_data.Equals("button", StringComparison.CurrentCulture))
                                {
                                    if (FunctionCollection_MVVM_Selection.functionCollection_Selection != -1)
                                    {
                                        App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                        {
                                            //if(!FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].functionTabName.Equals("Default", StringComparison.CurrentCulture))
                                            FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].customFunctionCollection_Bind.Clear();

                                            FunctionCollection_MVVM_Selection.functionCollection_Selection = -1;
                                        });
                                    }
                                    if (FunctionCollectionTab_MVVM.functionCollectionTab.Count > 0)
                                    {
                                        App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                        {
                                            FunctionCollectionTab_MVVM.functionCollectionTab.Clear();
                                        });
                                        //await Task.Run(async () =>
                                        //{
                                        //    int count = FunctionCollectionTab_MVVM.functionCollectionTab.Count;
                                        //    for (int i = 0; i < count;)
                                        //    {
                                        //        if (!FunctionCollectionTab_MVVM.functionCollectionTab[i].functionTabName.Equals("Default", StringComparison.CurrentCulture))
                                        //        {
                                        //            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                                        //            {
                                        //                FunctionCollectionTab_MVVM.functionCollectionTab.RemoveAt(i);
                                        //            });
                                        //        }
                                        //        else
                                        //        {
                                        //            i++;
                                        //        }
                                        //        count = FunctionCollectionTab_MVVM.functionCollectionTab.Count;
                                        //    }
                                        //});
                                    }
                                    if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Count > 0)
                                        App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                        {
                                            FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Clear();
                                        });

                                    //foreach (var v in FunctionCollectionTab_MVVM.functionCollectionTab)
                                    //{
                                    //    if (v.functionTabName.Equals("Default", StringComparison.CurrentCulture))
                                    //    {
                                    //        foreach (var v2 in v.customFunctionCollection_Bind)
                                    //        {
                                    //            FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[v2.formulaName] = v2;
                                    //        }
                                    //        break;
                                    //    }
                                    //}


                                    XmlSerializer serializer1 = new XmlSerializer(typeof(ObservableCollection<FunctionCollection>));

                                    using (StringReader reader = new StringReader(data))
                                    {
                                        await Task.Run(() =>
                                        {
                                            foreach (var v in (ObservableCollection<FunctionCollection>)serializer1.Deserialize(reader))
                                            {
                                                //if (!v.functionTabName.Equals("Default", StringComparison.CurrentCulture))
                                                //{
                                                tbox_DisplayFile.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                                {
                                                    FunctionCollectionTab_MVVM.functionCollectionTab.Add(v);
                                                });
                                                foreach (var v2 in v.customFunctionCollection_Bind)
                                                {

                                                    App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                                    {
                                                        FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[v2.formulaObj.formulaName] = v2;
                                                    });
                                                }
                                                //}
                                            }
                                        });
                                    }
                                    fileSavedLoadButtons_Name = file.DisplayName;
                                }
                                else if (is_formula_button_data.Equals("data", StringComparison.CurrentCulture))
                                {
                                    if (DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection != -1)
                                    {
                                        tbox_DisplayFile.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                        {
                                            DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection.Clear();

                                            DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection = -1;
                                        });
                                    }
                                    if (DataDataCollection_Project_MVVM.dataDataCollectionProject.Count > 0)
                                        tbox_DisplayFile.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                        {
                                            DataDataCollection_Project_MVVM.dataDataCollectionProject.Clear();
                                        });

                                    if (DataDataCollection_Project_MVVM.dataValueList.Count > 0)
                                        App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                        {
                                            DataDataCollection_Project_MVVM.dataValueList.Clear();
                                        });
                                    XmlSerializer serializer1 = new XmlSerializer(typeof(ObservableCollection<DataDataCollection>));

                                    using (StringReader reader = new StringReader(data))
                                    {
                                        await Task.Run(() =>
                                        {
                                            foreach (var v in (ObservableCollection<DataDataCollection>)serializer1.Deserialize(reader))
                                            {
                                                tbox_DisplayFile.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                                {
                                                    DataDataCollection_Project_MVVM.dataDataCollectionProject.Add(v);
                                                });
                                                foreach (var v2 in v.dataDataCollection)
                                                {

                                                    if (v2.dataChanged == false)
                                                    {
                                                        if (!string.IsNullOrWhiteSpace(v2.value))
                                                            App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                                            {
                                                                //TODO: if error uncomment and comment below
                                                                //DataDataCollection_Project_MVVM.dataValueList[v2.dataNamePrev] = v2.valuePrev;
                                                                DataDataCollection_Project_MVVM.dataValueList[v2.dataNamePrev] = v2.value;
                                                            });
                                                    }
                                                    //else if (!string.IsNullOrWhiteSpace(v2.dataNamePrev))
                                                    //{
                                                    //    App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                                    //    {
                                                    //        DataDataCollection_Project_MVVM.dataValueList[v2.dataNamePrev] = v2.valuePrev;
                                                    //    });
                                                    //}
                                                }
                                            }
                                        });
                                    }
                                    fileSavedLoadData_Name = file.DisplayName;
                                }



                                if (await CachedFileManager.CompleteUpdatesAsync(file) == Windows.Storage.Provider.FileUpdateStatus.Complete)
                                {
                                    if (is_formula_button_data.Equals("formula", StringComparison.CurrentCulture))
                                    {
                                        //bool reopenData = false;
                                        if (ViewPages.dataView != null)
                                            await Task.Run(() =>
                                            {
                                                ViewPages.dataView.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                                                {
                                                    ViewPages.dataView.Close();
                                                    //reopenData = true;
                                                    //CoreWindow.GetForCurrentThread().Close();
                                                });
                                            });
                                        Task tl = quickLoadOther();
                                        await tl.AsAsyncAction();
                                        if (tl.IsCompleted)
                                        {
                                            await tl.AsAsyncAction();
                                            //t.Wait(500);
                                            if (tl.IsCompleted)
                                            {
                                                Tbox_SelectedProjectName.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                                                {
                                                    Tbox_SelectedProjectName.Text = "Selected Project";
                                                });
                                                if (!SaveLoad.fileSavedLoadFormula_Name.Equals("", StringComparison.CurrentCulture))
                                                {
                                                    tbox_DisplayFile.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                                                    {
                                                        tbox_DisplayFile.Text = SaveLoad.fileSavedLoadFormula_Name;
                                                    });
                                                    if (!string.IsNullOrWhiteSpace(fileSavedLoadButtons_Name) &&
                                                        !fileSavedLoadButtons_Name.Equals("", StringComparison.CurrentCulture))

                                                        tbox_DisplayFile_Btn.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                                                        {
                                                            tbox_DisplayFile_Btn.Text = fileSavedLoadButtons_Name;
                                                        });

                                                    //fileSavedLoadButtons_Name = fileSavedLoadFormula_Name;
                                                    //fileSavedLoadData_Name = fileSavedLoadFormula_Name;
                                                }
                                            }
                                        }

                                    }
                                    else if (is_formula_button_data.Equals("button", StringComparison.CurrentCulture))
                                    {
                                        if (!SaveLoad.fileSavedLoadButtons_Name.Equals("", StringComparison.CurrentCulture))
                                            tbox_DisplayFile.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                                            {
                                                tbox_DisplayFile.Text = SaveLoad.fileSavedLoadButtons_Name;
                                            });
                                    }
                                    else if (is_formula_button_data.Equals("data", StringComparison.CurrentCulture))
                                    {
                                        if (!SaveLoad.fileSavedLoadData_Name.Equals("", StringComparison.CurrentCulture))
                                            tbox_DisplayFile.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                                            {
                                                tbox_DisplayFile.Text = SaveLoad.fileSavedLoadData_Name;
                                            });
                                    }


                                    await Task.Delay(1000);
                                    Task t2 = ViewPages.close_loadingScreen();
                                    await t2.AsAsyncAction();
                                }
                            }
                        }
                    });
                }
            }
            else
            {
                return Task.Run(() => { });
                //this.textBlock.Text = "Operation cancelled.";
            }
        }



























        public static async Task<Task> quickLoadButton()
        {

            Windows.Storage.StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            //Windows.Storage.StorageFolder Main_SaveDataFolder = await localFolder.CreateFolderAsync("Perseverance Calculator", CreationCollisionOption.OpenIfExists);
            Windows.Storage.StorageFolder otherFolder = await localFolder.CreateFolderAsync("ButtonMeasureData", Windows.Storage.CreationCollisionOption.OpenIfExists);
            try
            {
                StorageFile file_Function = await otherFolder.GetFileAsync(fileSavedLoadFormula_Name + ".button");
                lock (file_Function)
                {
                    return Task.Run(async () =>
                    {
                        AsyncReaderWriterLock m_lock = new AsyncReaderWriterLock();

                        using (var releaser = await m_lock.ReaderLockAsync())
                        {
                            fileSavedLoadButtons_Name = file_Function.DisplayName.Replace(".button", "");
                            string data = "";
                            while (true)
                            {
                                try
                                {
                                    //await Task.Run(async () =>
                                    //{
                                    CachedFileManager.DeferUpdates(file_Function);

                                    data = await FileIO.ReadTextAsync(file_Function);
                                    //});
                                    break;
                                }
                                catch { await Task.Delay(1000); }
                            }

                            if (FunctionCollection_MVVM_Selection.functionCollection_Selection != -1)
                            {
                                App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                {
                                    //if (!FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].functionTabName.Equals("Default", StringComparison.CurrentCulture))
                                    FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].customFunctionCollection_Bind.Clear();
                                    FunctionCollection_MVVM_Selection.functionCollection_Selection = -1;

                                });
                                //FunctionCollectionTab_MVVM.functionCollectionTab[FunctionCollection_MVVM_Selection.functionCollection_Selection].customFunctionCollection_Bind.Clear();
                            }
                            if (FunctionCollectionTab_MVVM.functionCollectionTab.Count > 0)
                            {
                                App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                {
                                    FunctionCollectionTab_MVVM.functionCollectionTab.Clear();
                                });
                                //await Task.Run(async () =>
                                //{
                                //    int count = FunctionCollectionTab_MVVM.functionCollectionTab.Count;
                                //    for (int i = 0; i < count;)
                                //    {
                                //        if (!FunctionCollectionTab_MVVM.functionCollectionTab[i].functionTabName.Equals("Default", StringComparison.CurrentCulture))
                                //        {
                                //            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                                //            {
                                //                FunctionCollectionTab_MVVM.functionCollectionTab.RemoveAt(i);
                                //            });
                                //        }
                                //        else
                                //        {
                                //            i++;
                                //        }
                                //        count = FunctionCollectionTab_MVVM.functionCollectionTab.Count;
                                //    }
                                //});
                            }
                            //FunctionCollectionTab_MVVM.functionCollectionTab.Clear();

                            if (FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Count > 0)
                                App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                {
                                    FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary.Clear();
                                });

                            //foreach (var v in FunctionCollectionTab_MVVM.functionCollectionTab)
                            //{
                            //    if (v.functionTabName.Equals("Default", StringComparison.CurrentCulture))
                            //    {
                            //        foreach (var v2 in v.customFunctionCollection_Bind)
                            //        {
                            //            FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[v2.formulaName] = v2;
                            //        }
                            //        break;
                            //    }
                            //}

                            XmlSerializer serializer1 = new XmlSerializer(typeof(ObservableCollection<FunctionCollection>));

                            using (StringReader reader = new StringReader(data))
                            {
                                await Task.Run(() =>
                                {
                                    foreach (var v in (ObservableCollection<FunctionCollection>)serializer1.Deserialize(reader))
                                    {
                                        //if (!v.functionTabName.Equals("Default", StringComparison.CurrentCulture))
                                        //{
                                        App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                        {
                                            FunctionCollectionTab_MVVM.functionCollectionTab.Add(v);
                                        });
                                        foreach (var v2 in v.customFunctionCollection_Bind)
                                        {
                                            if(v2.formulaObj == null)
                                            {
                                                v2.formulaObj = new Formula();
                                            }
                                            if (string.IsNullOrWhiteSpace(v2.formulaObj.formulaName))
                                            {
                                                v2.formulaObj.formulaName = v2.formulaName;
                                                v2.formulaObj.formula = v2.formula;
                                                v2.formulaObj.rearrangedFormula = v2.rearrangedFormula;
                                                v2.formulaObj.description = v2.description;
                                                v2.formulaObj.variableList_Bind = v2.variableList_Bind;

                                            }
                                            App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                            {
                                                //if (v2.formula != null && !string.IsNullOrEmpty(v2.formula.formulaName))
                                                    FunctionCollectionTab_MVVM.customFunctionCollection_Dictionary[v2.formulaObj.formulaName] = v2;
                                            });
                                        }
                                        //}
                                    }
                                });
                            }
                        }
                    });
                }
            }
            catch
            {
                return Task.Run(() => { });
            }

        }


        public static async Task<Task> quickLoadData()
        {

            Windows.Storage.StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            //Windows.Storage.StorageFolder Main_SaveDataFolder = await localFolder.CreateFolderAsync("Perseverance Calculator", CreationCollisionOption.OpenIfExists);
            Windows.Storage.StorageFolder otherFolder = await localFolder.CreateFolderAsync("ButtonMeasureData", Windows.Storage.CreationCollisionOption.OpenIfExists);
            try
            {
                StorageFile file_Data = await otherFolder.GetFileAsync(fileSavedLoadFormula_Name + ".data");
                lock (file_Data)
                {
                    return Task.Run(async () =>
                    {
                        AsyncReaderWriterLock m_lock = new AsyncReaderWriterLock();

                        using (var releaser = await m_lock.ReaderLockAsync())
                        {
                            fileSavedLoadData_Name = file_Data.DisplayName.Replace(".data", "");
                            string data = "";
                            while (true)
                            {
                                try
                                {
                                    //await Task.Run(async () =>
                                    //{
                                    CachedFileManager.DeferUpdates(file_Data);
                                    //---------------
                                    data = await FileIO.ReadTextAsync(file_Data);
                                    //});
                                    break;
                                }
                                catch { await Task.Delay(1000); }
                            }

                            if (DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection != -1)
                            {
                                App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                {
                                    DataDataCollection_Project_MVVM.dataDataCollectionProject[DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection].dataDataCollection.Clear();
                                    DataDataCollection_Project_MVVM_Selection.dataDataCollectionProject_Selection = -1;

                                });
                            }
                            if (DataDataCollection_Project_MVVM.dataDataCollectionProject.Count > 0)
                                App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                {
                                    DataDataCollection_Project_MVVM.dataDataCollectionProject.Clear();
                                });

                            if (DataDataCollection_Project_MVVM.dataValueList.Count > 0)
                                App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                {
                                    DataDataCollection_Project_MVVM.dataValueList.Clear();
                                });
                            XmlSerializer serializer12 = new XmlSerializer(typeof(ObservableCollection<DataDataCollection>));

                            using (StringReader reader = new StringReader(data))
                            {
                                await Task.Run(() =>
                                {
                                    foreach (var v in (ObservableCollection<DataDataCollection>)serializer12.Deserialize(reader))
                                    {
                                        App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                        {
                                            DataDataCollection_Project_MVVM.dataDataCollectionProject.Add(v);
                                        });
                                        foreach (var v2 in v.dataDataCollection)
                                        {

                                            if (v2.dataChanged == false)
                                            {
                                                if (!string.IsNullOrWhiteSpace(v2.value))
                                                    App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                                    {
                                                        //TODO: if error uncomment and comment below
                                                        //DataDataCollection_Project_MVVM.dataValueList[v2.dataNamePrev] = v2.valuePrev;
                                                        DataDataCollection_Project_MVVM.dataValueList[v2.dataNamePrev] = v2.value;
                                                    });
                                            }
                                            //else if (!string.IsNullOrWhiteSpace(v2.dataNamePrev))
                                            //{
                                            //    App.m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.High, () =>
                                            //    {
                                            //        DataDataCollection_Project_MVVM.dataValueList[v2.dataNamePrev] = v2.valuePrev;
                                            //    });
                                            //}
                                        }
                                    }
                                });
                            }
                        }
                    });
                }
            }
            catch
            {
                return Task.Run(() => { });
            }

        }
















        public static Task quickLoadOther()
        {
            return Task.Run(async () =>
            {
                Task t1 = await quickLoadButton();

                await t1.AsAsyncAction();
                if (t1.IsCompleted)
                {
                    Task t2 = await quickLoadData();
                    await t2.AsAsyncAction();
                    //if (t2.IsCompleted)
                    //{
                    //    Task t3 = await quickLoadMeasure();
                    //    await t3.AsAsyncAction();
                    //}
                }
            });
        }













        // <summary>
        // if use quicksave must use lock(file)
        // </summary>
        // <returns></returns>

        //public static async void quickSave()
        //{

        //    if (string.IsNullOrWhiteSpace(fileSavedLoadFormula_Name) && fileSavedLoadFormula_Name.Equals("", StringComparison.CurrentCulture))
        //        fileSavedLoadFormula_Name = "DefaultFormula";

        //    //Windows.Storage.StorageFolder localFolder = Windows.Storage.KnownFolders.DocumentsLibrary;
        //    Windows.Storage.StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        //    //Windows.Storage.StorageFolder Main_SaveDataFolder = await localFolder.CreateFolderAsync("Perseverance Calculator", CreationCollisionOption.OpenIfExists);
        //    Windows.Storage.StorageFile file = await localFolder.CreateFileAsync(fileSavedLoadFormula_Name + ".formula");

        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    settings.Async = true;
        //    settings.Encoding = Encoding.UTF8;
        //    settings.Indent = true;
        //    settings.OmitXmlDeclaration = true;


        //    XmlSerializer serializer1 = new XmlSerializer(typeof(ObservableCollection<FormulaProjectExplorer>));
        //    //using (Stream stream = await file.OpenStreamForWriteAsync())
        //    //{
        //    //    serializer1.Serialize(stream, FormulaProject_MVVM.formulaProject);
        //    //    stream.Close();
        //    //}



        //    StringBuilder builder = new StringBuilder();
        //    using (XmlWriter writer = XmlWriter.Create(builder, settings))
        //        serializer1.Serialize(writer, FormulaProject_MVVM.formulaProject);
        //    using (SafeFileHandle fileHandle = file.CreateSafeFileHandle(FileAccess.Write, FileShare.Read, FileOptions.None))
        //    {
        //        await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
        //        {
        //            await Windows.Storage.FileIO.WriteTextAsync(file, builder.ToString(), encoding: Windows.Storage.Streams.UnicodeEncoding.Utf8);
        //        });
        //        fileHandle.Close();
        //        fileHandle.Dispose();
        //    }


        //}






        public static async Task<Task> quickSaveButton()
        {

            Windows.Storage.StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            //Windows.Storage.StorageFolder Main_SaveDataFolder = await localFolder.CreateFolderAsync("Perseverance Calculator", CreationCollisionOption.OpenIfExists);
            Windows.Storage.StorageFolder otherFolder = await localFolder.CreateFolderAsync("ButtonMeasureData", Windows.Storage.CreationCollisionOption.OpenIfExists);

            Windows.Storage.StorageFile customButtonsFile = await otherFolder.CreateFileAsync(fileSavedLoadFormula_Name + ".button", CreationCollisionOption.OpenIfExists);
            lock (customButtonsFile)
            {
                return Task.Run(async () =>
                {
                    //await CoreWindow.GetForCurrentThread().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    //{

                    AsyncReaderWriterLock m_lock = new AsyncReaderWriterLock();

                    using (var releaser = await m_lock.WriterLockAsync())
                    {
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Async = true;
                        settings.Encoding = Encoding.UTF8;
                        settings.Indent = true;
                        settings.OmitXmlDeclaration = true;

                        XmlSerializer serializer11 = new XmlSerializer(typeof(ObservableCollection<FunctionCollection>));


                        StringBuilder builder = new StringBuilder();
                        using (XmlWriter writer = XmlWriter.Create(builder, settings))
                        {
                            serializer11.Serialize(writer, FunctionCollectionTab_MVVM.functionCollectionTab);

                            //using (SafeFileHandle fileHandle = customButtonsFile.CreateSafeFileHandle(FileAccess.Write, FileShare.Read, FileOptions.None))
                            //{
                            //await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
                            //{
                            while (true)
                            {
                                try
                                {
                                    //await Task.Run(async () =>
                                    //{
                                    CachedFileManager.DeferUpdates(customButtonsFile);
                                    await Windows.Storage.FileIO.WriteTextAsync(customButtonsFile, builder.ToString(), encoding: Windows.Storage.Streams.UnicodeEncoding.Utf8);
                                    //});
                                    break;
                                }
                                catch
                                {
                                    await Task.Delay(1000);
                                }
                            }
                            //});
                            //fileHandle.Close();
                            //fileHandle.Dispose();
                            //}
                        }

                        if (await CachedFileManager.CompleteUpdatesAsync(customButtonsFile) == Windows.Storage.Provider.FileUpdateStatus.Complete)
                        {
                            //if (await CachedFileManager.CompleteUpdatesAsync(customButtonsFile) == Windows.Storage.Provider.FileUpdateStatus.Complete)
                            //{
                            //}
                            fileSavedLoadButtons_Name = customButtonsFile.DisplayName;
                            //customButtonsFile = null;
                        }
                    }
                });
            }
        }
        public static async Task<Task> quickSaveData()
        {

            Windows.Storage.StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            //Windows.Storage.StorageFolder Main_SaveDataFolder = await localFolder.CreateFolderAsync("Perseverance Calculator", CreationCollisionOption.OpenIfExists);
            Windows.Storage.StorageFolder otherFolder = await localFolder.CreateFolderAsync("ButtonMeasureData", Windows.Storage.CreationCollisionOption.OpenIfExists);

            Windows.Storage.StorageFile customDataFile = await otherFolder.CreateFileAsync(fileSavedLoadFormula_Name + ".data", CreationCollisionOption.OpenIfExists);
            lock (customDataFile)
            {
                return Task.Run(async () =>
                {
                    AsyncReaderWriterLock m_lock = new AsyncReaderWriterLock();

                    using (var releaser = await m_lock.WriterLockAsync())
                    {
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Async = true;
                        settings.Encoding = Encoding.UTF8;
                        settings.Indent = true;
                        settings.OmitXmlDeclaration = true;

                        XmlSerializer serializer31 = new XmlSerializer(typeof(ObservableCollection<DataDataCollection>));

                        StringBuilder builder = new StringBuilder();
                        using (XmlWriter writer = XmlWriter.Create(builder, settings))
                        {
                            serializer31.Serialize(writer, DataDataCollection_Project_MVVM.dataDataCollectionProject);

                            //using (SafeFileHandle fileHandle = customDataFile.CreateSafeFileHandle(FileAccess.Write, FileShare.Read, FileOptions.None))
                            //{
                            //await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
                            //{
                            while (true)
                            {
                                try
                                {
                                    //await Task.Run(async () =>
                                    //{
                                    CachedFileManager.DeferUpdates(customDataFile);
                                    await Windows.Storage.FileIO.WriteTextAsync(customDataFile, builder.ToString(), encoding: Windows.Storage.Streams.UnicodeEncoding.Utf8);
                                    //});
                                    break;
                                }
                                catch
                                {
                                    await Task.Delay(1000);

                                }
                            }
                            //});
                            //    fileHandle.Close();
                            //    fileHandle.Dispose();
                            //}
                        }

                        if (await CachedFileManager.CompleteUpdatesAsync(customDataFile) == Windows.Storage.Provider.FileUpdateStatus.Complete)
                        {
                            //if (await CachedFileManager.CompleteUpdatesAsync(customDataFile) == Windows.Storage.Provider.FileUpdateStatus.Complete)
                            //{
                            fileSavedLoadData_Name = customDataFile.DisplayName;
                            //}
                            //customDataFile = null;

                        }
                    }
                });
            }
        }




        public static Task quickSave_OtherData()
        {
            return Task.Run(async () =>
            {
                Task t1 = await quickSaveButton();

                await t1.AsAsyncAction();
                if (t1.IsCompleted)
                {
                    Task t2 = await quickSaveData();
                    await t2.AsAsyncAction();
                    //if (t2.IsCompleted)
                    //{
                    //    Task t3 = await quickSaveMeasure();
                    //    await t3.AsAsyncAction();
                    //}
                }
            });
        }




    }
}
