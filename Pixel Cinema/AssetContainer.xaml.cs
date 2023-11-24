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
using System.Windows.Threading;

namespace Pixel_Cinema
{
    /// <summary>
    /// Lógica de interacción para AssetContainer.xaml
    /// </summary>
    public partial class AssetContainer : UserControl
    {
        private DispatcherTimer timer;

        public AssetContainer()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;

            MouseEnter += StartTimer;
            MouseLeave += StopTimer;
        }

        public void AssetAdded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Asset añadido");
        }

        public void StartTimer(object sender, MouseEventArgs e)
        {
            timer.Start();
        }

        public void StopTimer(object sender, MouseEventArgs e)
        {
            timer.Stop();

            WrapPanel parentGrid = FindParent<WrapPanel>(this);
            ShowOtherComponents(parentGrid);
            AssetInfo.Visibility = Visibility.Collapsed;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            WrapPanel parentGrid = FindParent<WrapPanel>(this);
            HideOtherComponents(parentGrid, this);
            AssetInfo.Visibility = Visibility.Visible;
        }

        private void HideOtherComponents(WrapPanel parentGrid, UIElement currentElement)
        {
            foreach (UIElement child in parentGrid.Children)
            {
                if (!ReferenceEquals(child, currentElement))
                {
                    child.Visibility = Visibility.Hidden;
                }
            }
        }

        private void ShowOtherComponents(WrapPanel parentGrid)
        {
            foreach (UIElement child in parentGrid.Children)
            {
                child.Visibility = Visibility.Visible;
            }
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            while (parentObject != null && !(parentObject is T))
            {
                parentObject = VisualTreeHelper.GetParent(parentObject);
            }

            return (T)parentObject;
        }
    }
}
