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
    /// Lógica de interacción para VideoContainer.xaml
    /// </summary>
    public partial class VideoContainer : UserControl
    {
        public VideoContainer()
        {
            InitializeComponent();
        }

        public void ShowVideoInfo(object sender, RoutedEventArgs e)
        {
            playButtonImage.Visibility = Visibility.Collapsed;
            videoInfo.Visibility = Visibility.Visible;
            videoBorder.Background = new SolidColorBrush(Colors.White);
        }

        public void HideVideoInfo(object sender, RoutedEventArgs e)
        {
            videoInfo.Visibility = Visibility.Collapsed;
            playButtonImage.Visibility = Visibility.Visible;
            Color color = (Color)ColorConverter.ConvertFromString("#9395d3");
            videoBorder.Background = new SolidColorBrush(color);
        }
    }
}
