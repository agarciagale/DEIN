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
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDragging;

        public MainWindow()
        {
            InitializeComponent();

            mediaElement.MediaOpened += MediaElement_MediaOpened;
        }

        public void OpenUploadVideoWindow(object sender, RoutedEventArgs e)
        {
            UploadVideo uploadVideo = new UploadVideo();
            Window.GetWindow(this).Close();
            uploadVideo.Show();
        }

        public void OpenAssetsWindow(object sender, RoutedEventArgs e)
        {
            Assets assets = new Assets();
            Window.GetWindow(this).Close();
            assets.Show();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }

        private void PositionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!isDragging)
            {
                mediaElement.Position = TimeSpan.FromSeconds(e.NewValue);
            }
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            positionSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = TimeSpan.Zero;
        }

        private void PositionSlider_DragStarted(object sender, RoutedEventArgs e)
        {
            isDragging = true;
        }

        private void PositionSlider_DragCompleted(object sender, RoutedEventArgs e)
        {
            isDragging = false;
            mediaElement.Position = TimeSpan.FromSeconds(positionSlider.Value);
        }
    }
}
