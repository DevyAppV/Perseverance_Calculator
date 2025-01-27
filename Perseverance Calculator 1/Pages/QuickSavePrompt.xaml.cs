using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Perseverance_Calculator.View.Pages;
using Perseverance_Calculator_1.Controller.SaveLoad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Perseverance_Calculator_1.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuickSavePrompt : Page
    {
        //ApplicationView view;
        bool save = false;
        MainWindow page;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            page = (MainWindow)e.Parameter;
            //await Task.Run(async () =>
            //{
            //    //await ((MainPage)e.Parameter).Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            //    //    {
            //    //        ((MainPage)e.Parameter).Textblock_LoadedButtonsFile.Text = "aaaaaaa";
            //    //    });

            //});
        }
        public QuickSavePrompt()
        {

            this.InitializeComponent();


            //ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(10, 10));
            //Window.Current.Activate();

            //Window.Current.SizeChanged += Current_SizeChanged;
            //view = ApplicationView.GetForCurrentView();

            ViewPages.quickSavePromptView.AppWindow.Resize(new SizeInt32(500, 300));
            //view.Consolidated += View_Consolidated;
            //Window.Current.Activated += Current_Activated;
            ViewPages.quickSavePromptView.Closed += Current_Closed;
        }

        //private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //}

        //private void Current_Activated(object sender, Microsoft.UI.Xaml.WindowActivatedEventArgs e)
        //{
        //    view.SetPreferredMinSize(new Size(10, 10));
        //    view.TryResizeView(new Size(500, 250));
        //    //view.TryResizeView(new Size(500, 250));
        //}

        private async void Current_Closed(object sender, WindowEventArgs e)
        {
            //view.TryResizeView(new Size(1500, 800));
            //ViewPages.quickSavePromptView = null;
            if (save)
            {
                await Task.Delay(500);
                Task t = SaveLoad.quickSave_OtherData();
                //while (!t.IsCompleted)
                //{
                await t.AsAsyncAction();
                //t.Wait(500);
                if (t.IsCompleted)
                {
                    //await Task.Run(async () =>
                    //{
                    //await ViewPages.loadingScreenView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,() =>
                    //{
                    //    ViewPages.loadingScreenView.CoreWindow.Close();
                    //    //ViewPages.loadingScreenView = null;
                    //    //Window.Current.Close();
                    //});
                    await Task.Delay(500);
                    Task t2 = ViewPages.close_loadingScreen();

                    await t2.AsAsyncAction();
                    //});
                    //await Task.Run(async () =>
                    //{

                    if (t2.IsCompleted)
                    {
                        page.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                        {
                            page.Textblock_LoadedButtonsFile.Text = SaveLoad.fileSavedLoadFormula_Name;
                        });

                        //});
                        if (ViewPages.dataView != null)
                        {
                            //ViewPages.dataView.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                            //{
                                ViewPages.dataView.Close();
                            //});
                            ViewPages.dataView = null;
                        }
                        if (ViewPages.quickSavePromptView != null)
                        {
                            //ViewPages.quickSavePromptView.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                            //{
                            ViewPages.quickSavePromptView = null;
                            //ViewPages.quickSavePromptView.Close();
                            //});
                        }
                    }
                    //break;
                    //await Task.Run(async () =>
                    //{
                    //await 
                    //await CoreApplication.Views[0].Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                    //Textblock_LoadedButtonsFile.
                    //});

                    //});
                }
                //}
            }
            else
            {
                //ViewPages.quickSavePromptView = null;
                if (ViewPages.quickSavePromptView != null)
                {
                    //ViewPages.quickSavePromptView.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                    //{
                    //ViewPages.quickSavePromptView.Close();
                    ViewPages.quickSavePromptView = null;
                    //});
                }
            }
        }

        //private async void View_Consolidated(ApplicationView sender, ApplicationViewConsolidatedEventArgs args)
        //{
        //    if (save)
        //    {
        //        await Task.Delay(500);
        //        Task t = await SaveLoad.quickSave_OtherData();
        //        //while (!t.IsCompleted)
        //        //{
        //        await t.AsAsyncAction();
        //        //t.Wait(500);
        //        if (t.IsCompleted)
        //        {
        //            //await Task.Run(async () =>
        //            //{
        //            //await ViewPages.loadingScreenView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,() =>
        //            //{
        //            //    ViewPages.loadingScreenView.CoreWindow.Close();
        //            //    //ViewPages.loadingScreenView = null;
        //            //    //Window.Current.Close();
        //            //});
        //            await Task.Delay(500);
        //            Task t2 = ViewPages.close_loadingScreen();

        //            await t2.AsAsyncAction();
        //            //});
        //            //await Task.Run(async () =>
        //            //{

        //            if (t2.IsCompleted)
        //            {
        //                page.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
        //                {
        //                    page.Textblock_LoadedButtonsFile.Text = SaveLoad.fileSavedLoadFormula_Name;
        //                });

        //                //});
        //                if (ViewPages.dataView != null)
        //                {
        //                    ViewPages.dataView.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
        //                    {
        //                        ViewPages.dataView.Close();
        //                    });
        //                    ViewPages.dataView = null;
        //                }
        //                if (ViewPages.quickSavePromptView != null)
        //                {
        //                    ViewPages.quickSavePromptView.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
        //                    {
        //                        ViewPages.quickSavePromptView.Close();
        //                    });
        //                }
        //            }
        //            //break;
        //            //await Task.Run(async () =>
        //            //{
        //            //await 
        //            //await CoreApplication.Views[0].Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
        //            //Textblock_LoadedButtonsFile.
        //            //});

        //            //});
        //        }
        //        //}
        //    }
        //    else
        //    {
        //        //ViewPages.quickSavePromptView = null;
        //        if (ViewPages.quickSavePromptView != null)
        //        {
        //            ViewPages.quickSavePromptView.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
        //            {
        //                ViewPages.quickSavePromptView.Close();
        //            });
        //        }
        //    }
        //}

        private async void Button_Yes_Click(object sender, RoutedEventArgs e)
        {
            save = true;
            if (ViewPages.loadingScreenView == null)
            {
                Task t = ViewPages.open_loadingScreen();
                await t.AsAsyncAction();
                if (t.IsCompleted)
                    if (ViewPages.quickSavePromptView != null)
                    {
                        //await view.TryConsolidateAsync();
                        ViewPages.quickSavePromptView.Close();
                    }
            }
            else
            {
                if (ViewPages.quickSavePromptView != null)
                {
                    //await view.TryConsolidateAsync();
                    ViewPages.quickSavePromptView.Close();
                }
                //if (view != null)
                //{
                //    await view.TryConsolidateAsync();
                //}
            }
        }
        private void Button_No_Click(object sender, RoutedEventArgs e)
        {
            save = false;
            if (ViewPages.quickSavePromptView != null)
            {
                //await view.TryConsolidateAsync();
                ViewPages.quickSavePromptView.Close();
            }
            //if (view != null)
            //{
            //    await view.TryConsolidateAsync();
            //}
        }
    }
}
