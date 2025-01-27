using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Perseverance_Calculator.View.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Perseverance_Calculator_1.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoadingScreen : Page
    {
        public LoadingScreen()
        {
            this.InitializeComponent();
            ViewPages.loadingScreenView.Closed += Current_Closed;
        }

        private void Current_Closed(object sender, WindowEventArgs e)
        {
            ViewPages.loadingScreenView = null;
            //throw new NotImplementedException();
        }
    }
}
