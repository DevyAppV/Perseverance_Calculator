using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Perseverance_Calculator_1;
using Perseverance_Calculator_1.Controller.SaveLoad;
using Perseverance_Calculator_1.Model;
using Perseverance_Calculator_1.Model.MVVM;
using Perseverance_Calculator_1.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace Perseverance_Calculator.View.Pages
{
    public class ViewPages
    {
        //public static int dataViewId = 0;
        public static Window GraphView = null;
        public static Window Visual2DGraphView = null;
        public static Window dataView = null;
        public static Window buttonDescriptionView = null;
        public static Window quickSavePromptView = null;
        public static Window loadingScreenView = null;


        public static Frame frameVisual2DGraph;
        public static Frame frameGraph;
        public static Frame frame2DGraph;
        public static Frame frameLoadingScreen;
        public static Frame frameQuickSavePrompt;
        public static Frame frameData;
        public static Frame frameButtonDescription;
        public static void open_2DVisualGraph()
        {
            if (Visual2DGraphView == null)
            {
                //GraphView = CoreApplication.CreateNewView();
                Visual2DGraphView = new Window();
                frame2DGraph = new Frame();

                frame2DGraph.Navigate(typeof(Visual2D_Graph));
                Visual2DGraphView.Content = frame2DGraph;


                Visual2DGraphView.Title = "2D Graph";

                Visual2DGraphView.Activate();
            }

            //var viewId = 0;

            //if (Visual2DGraphView == null)
            //{
            //    Visual2DGraphView = CoreApplication.CreateNewView();
            //    await Visual2DGraphView.Dispatcher.RunAsync(
            //        CoreDispatcherPriority.Normal,
            //        () =>
            //        {
            //            frameVisual2DGraph = new Frame();
            //            frameVisual2DGraph.Navigate(typeof(Visual2D_Graph));
            //            Microsoft.UI.Xaml.Window.Current.Content = frameVisual2DGraph;

            //            viewId = ApplicationView.GetForCurrentView().Id;

            //            //ApplicationView.GetForCurrentView().Consolidated += App.ViewConsolidated;

            //            Microsoft.UI.Xaml.Window.Current.Activate();
            //        });

            //    var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId);
            //}
            //else
            //{
            //    Task t = Task.Run(async () =>
            //    {
            //        await Visual2DGraphView.Dispatcher.RunAsync(
            //        CoreDispatcherPriority.Normal,
            //        async () =>
            //        {
            //            Visual2DGraphView.CoreWindow.Activate();
            //            var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(ApplicationView.GetForCurrentView().Id);
            //        });
            //    });
            //    await t.AsAsyncAction();
            //}

        }
        public static void open_Graph()
        {
            //var viewId = 0;

            if (GraphView == null)
            {
                //GraphView = CoreApplication.CreateNewView();
                GraphView = new Window();
                frameGraph = new Frame();

                frameGraph.Navigate(typeof(Graph));
                GraphView.Content = frameGraph;


                GraphView.Title = "Graph";

                GraphView.Activate();
            }

        }
        public static void open_DataSpreadSheet()
        {
            if (dataView == null)
            {
                //GraphView = CoreApplication.CreateNewView();
                dataView = new Window();
                frameData = new Frame();

                frameData.Navigate(typeof(Data_Spreadsheet), DataDataCollection_Project_MVVM.dataDataCollectionProject);
                dataView.Content = frameData;


                dataView.Title = "Data Spreadsheet";

                dataView.Activate();
            }
            //dataViewId = 0;

            //if (dataView == null)
            //{
            //    dataView = CoreApplication.CreateNewView();
            //    await dataView.Dispatcher.RunAsync(
            //        CoreDispatcherPriority.Normal,
            //        () =>
            //        {
            //            frameData = new Frame();
            //            frameData.Navigate(typeof(Data_Spreadsheet), DataDataCollection_Project_MVVM.dataDataCollectionProject);
            //            Microsoft.UI.Xaml.Window.Current.Content = frameData;

            //            dataViewId = ApplicationView.GetForCurrentView().Id;

            //            //ApplicationView.GetForCurrentView().Consolidated += App.ViewConsolidated;

            //            Microsoft.UI.Xaml.Window.Current.Activate();
            //        });
            //    var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(dataViewId);
            //}
            //else
            //{
            //    Task t = Task.Run(async () =>
            //    {
            //        await dataView.Dispatcher.RunAsync(
            //        CoreDispatcherPriority.Normal,
            //        async () =>
            //        {
            //            dataView.CoreWindow.Activate();
            //            var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(ApplicationView.GetForCurrentView().Id);
            //        });
            //    });
            //    await t.AsAsyncAction();
            //}

        }
        public static void open_ButtonDescription(CustomFunction function)
        {
            if (buttonDescriptionView == null)
            {
                //GraphView = CoreApplication.CreateNewView();
                buttonDescriptionView = new Window();
                frameButtonDescription = new Frame();

                frameButtonDescription.Navigate(typeof(ButtonDescription), function);
                buttonDescriptionView.Content = frameButtonDescription;


                buttonDescriptionView.Title = "Button Description";

                buttonDescriptionView.Activate();
            }
            else
            {
                frameButtonDescription.Navigate(typeof(ButtonDescription), function);
            }
            //var viewId = 0;

            //if (buttonDescriptionView == null)
            //    //{
            //    buttonDescriptionView = CoreApplication.CreateNewView();
            //await buttonDescriptionView.Dispatcher.RunAsync(
            //    CoreDispatcherPriority.Normal,
            //    () =>
            //    {
            //        var frame = new Frame();
            //        frame.Navigate(typeof(ButtonDescription), function);
            //        Microsoft.UI.Xaml.Window.Current.Content = frame;

            //        viewId = ApplicationView.GetForCurrentView().Id;

            //        //ApplicationView.GetForCurrentView().Consolidated += App.ViewConsolidated;

            //        Microsoft.UI.Xaml.Window.Current.Activate();
            //    });

            //var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId);
            ////}
            ////else
            ////{
            ////    Task t = Task.Run(async () =>
            ////    {
            ////        await buttonDescriptionView.Dispatcher.RunAsync(
            ////        CoreDispatcherPriority.Normal,
            ////        async () =>
            ////        {
            ////            buttonDescriptionView.CoreWindow.Activate();
            ////            var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(ApplicationView.GetForCurrentView().Id);
            ////        });
            ////    });
            ////    await t.AsAsyncAction();
            ////}
        }

        //public static ApplicationView mainView_QuickSavePrompt;
        public static void open_QuickSavePrompt(MainWindow page)
        {
            if (quickSavePromptView == null)
            {
                //GraphView = CoreApplication.CreateNewView();
                quickSavePromptView = new Window();
                frameQuickSavePrompt = new Frame();

                frameQuickSavePrompt.Navigate(typeof(QuickSavePrompt), page);
                quickSavePromptView.Content = frameQuickSavePrompt;


                quickSavePromptView.Title = "Quick Save Prompt";

                quickSavePromptView.Activate();
            }
            //var viewId = 0;
            ////ApplicationView appView = null;
            //if (quickSavePromptView == null)
            //{
            //    quickSavePromptView = CoreApplication.CreateNewView();

            //    //return Task.Run(async () =>
            //    //{
            //    Task t = Task.Run(async () =>
            //    {
            //        await quickSavePromptView.Dispatcher.RunAsync(
            //        CoreDispatcherPriority.Normal,
            //        async () =>
            //        {
            //            var frame = new Frame();
            //            frame.Navigate(typeof(QuickSavePrompt), page);
            //            Microsoft.UI.Xaml.Window.Current.Content = frame;

            //            viewId = ApplicationView.GetForCurrentView().Id;
            //            //mainView_QuickSavePrompt = ApplicationView.GetForCurrentView();


            //            quickSavePromptView.CoreWindow.Activate();
            //            //Windows.UI.Xaml.Window.Current.Activate();
            //        });
            //    });
            //    await t.AsAsyncAction();
            //    if (t.IsCompleted)
            //    {
            //        //await quickSavePromptView.Dispatcher.RunAsync(
            //        //CoreDispatcherPriority.High,
            //        //async () =>
            //        //{
            //        var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId);
            //        //});
            //    }
            //}
            //else
            //{
            //    Task t = Task.Run(async () =>
            //    {
            //        await quickSavePromptView.Dispatcher.RunAsync(
            //        CoreDispatcherPriority.Normal,
            //        async () =>
            //        {
            //            quickSavePromptView.CoreWindow.Activate();
            //            var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(ApplicationView.GetForCurrentView().Id);
            //        });
            //    });
            //    await t.AsAsyncAction();
            //}

            //});
        }
        public  static Task open_loadingScreen()
        {
            if (loadingScreenView == null)
            {
                //GraphView = CoreApplication.CreateNewView();

                loadingScreenView = new Window();
                return Task.Run(() =>
                {

                    loadingScreenView.DispatcherQueue.TryEnqueue(
                        DispatcherQueuePriority.Normal,
                         () =>
                        {
                            frameLoadingScreen = new Frame();

                            frameLoadingScreen.Navigate(typeof(LoadingScreen));
                            loadingScreenView.Content = frameLoadingScreen;


                            loadingScreenView.Title = "Loading Screen";
                            loadingScreenView.Activate();
                        });
                });
            }
            else return null;


            //var viewId = 0;
            ////ApplicationView appView = null;
            //if (loadingScreenView == null)
            //{
            //    loadingScreenView = CoreApplication.CreateNewView();
            //    return Task.Run(async () =>
            //    {
            //        await loadingScreenView.Dispatcher.RunAsync(
            //            CoreDispatcherPriority.Normal,
            //            async () =>
            //            {
            //                var frame = new Frame();
            //                frame.Navigate(typeof(LoadingScreen));
            //                Microsoft.UI.Xaml.Window.Current.Content = frame;

            //                viewId = ApplicationView.GetForCurrentView().Id;


            //                Microsoft.UI.Xaml.Window.Current.Activate();
            //                var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId);
            //            });

            //    });
            //}
            //else
            //{
            //    return Task.Run(async () =>
            //    {
            //        await loadingScreenView.Dispatcher.RunAsync(
            //        CoreDispatcherPriority.Normal,
            //        async () =>
            //        {
            //            loadingScreenView.CoreWindow.Activate();
            //            var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(ApplicationView.GetForCurrentView().Id);
            //        });
            //    });
            //}
        }


        public static Task close_loadingScreen()
        {
            return Task.Run(() =>
            {
                if (loadingScreenView != null)
                    loadingScreenView.DispatcherQueue.TryEnqueue(
                    DispatcherQueuePriority.Normal,
                     () =>
                    {
                        if(loadingScreenView!=null)
                            loadingScreenView.Close();
                    });
            });

        }
    }
}
