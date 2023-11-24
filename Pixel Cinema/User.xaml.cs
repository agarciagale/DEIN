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
using System.Windows.Shapes;

namespace Pixel_Cinema
{
    /// <summary>
    /// Lógica de interacción para User.xaml
    /// </summary>
    public partial class User : Window
    {
        public User()
        {
            InitializeComponent();
        }

        public void OpenAssetsWindow(object sender, RoutedEventArgs e)
        {
            Assets assets = new Assets();
            Window.GetWindow(this).Close();
            assets.Show();
        }
    }
}
