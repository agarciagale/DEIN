using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pixel_Cinema
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        public class NavigationHelper
        {
            public void NavigateToURL(string url)
            {
                try
                {
                    Process.Start(url);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions, such as if the URL is invalid or there's no associated application to open the URL
                    Console.WriteLine("Error navigating to URL: " + ex.Message);
                }
            }
        }

        public void OpenMainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Window.GetWindow(this).Close();
            mainWindow.Show();
        }

        public void TerminosYCondiciones(object sender, RoutedEventArgs e)
        {
            NavigationHelper navigationHelper = new NavigationHelper();

            // Call the method with the desired URL
            string desiredURL = "https://www.youtube.com/watch?v=5992zvskJz4";
            navigationHelper.NavigateToURL(desiredURL);
        }

        public void PasswordOlvidada(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("F", "xd", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
