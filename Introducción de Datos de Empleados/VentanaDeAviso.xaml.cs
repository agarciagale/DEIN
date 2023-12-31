using System;
using System.Threading.Tasks;
using System.Windows;

namespace Introducción_de_Datos_de_Empleados
{
    public partial class VentanaDeAviso : Window
    {
        private readonly int durationInSeconds;

        public VentanaDeAviso(string message, int durationInSeconds)
        {
            InitializeComponent();
            MessageTextBlock.Text = message;
            this.durationInSeconds = durationInSeconds;

            Loaded += AutoClosingMessageBox_Loaded;
        }

        private async void AutoClosingMessageBox_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(TimeSpan.FromSeconds(durationInSeconds));
            Close();
        }
    }
}
