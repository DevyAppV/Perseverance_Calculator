﻿using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Perseverance_Calculator.View.Pages;
using Perseverance_Calculator_1.Controller.DefaultData;
using Perseverance_Calculator_1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Perseverance_Calculator_1
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Title = "Formula";
            m_window.AppWindow.MoveAndResize(new Windows.Graphics.RectInt32(0,0,1750,700));
            m_window.Closed += M_window_Closed;
            m_window.Activate();

            //Task t = Task.Run(() =>
            //{
            //    m_window.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
            //    {
            //        DefaultCustomButtons.populateButtonWith_Default();
            //    });

            //});
            //await t.AsAsyncAction();
            //if (t.IsCompleted)
            //{
            //    DefaultCustomButtons.populateButtonWith_DefaultGraph();
            //}
            //ApplicationView.GetForCurrentView().Consolidated += App_Consolidated;

        }


        private void M_window_Closed(object sender, WindowEventArgs args)
        {

            App.Current.Exit();
            if (ViewPages.buttonDescriptionView != null)
            {
                ViewPages.buttonDescriptionView.Close();
                ViewPages.buttonDescriptionView = null;
            }
            if (ViewPages.dataView != null)
                ViewPages.dataView.Close();
            if (ViewPages.GraphView != null)
                ViewPages.GraphView.Close();
            if (ViewPages.loadingScreenView != null)
                ViewPages.loadingScreenView.Close();
            if (ViewPages.quickSavePromptView != null)
                ViewPages.quickSavePromptView.Close();
            if (ViewPages.Visual2DGraphView != null)
                ViewPages.Visual2DGraphView.Close();
            App.Current.Exit();
        }

        public static Window m_window;
    }
}
