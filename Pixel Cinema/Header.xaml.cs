using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pixel_Cinema
{
    /// <summary>
    /// Lógica de interacción para Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
        }

        public void OpenCreateWindow(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Window.GetWindow(this).Close();
            mainWindow.Show();
        }

        public void OpenVideosWindow(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("xd");
        }

        public void OpenAssetsWindow(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("xd");
        }

        public void OpenChatWindow(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("xd");
        }

        public void OpenSettingsWindow(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("xd");
        }

        public void OpenContactWindow(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("xd");
        }
    }
}
