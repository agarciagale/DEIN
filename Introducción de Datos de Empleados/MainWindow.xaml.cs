using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Introducción_de_Datos_de_Empleados
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string errorFormulario = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CargarFoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.EndInit();
                UserImage.Source = bitmap;
            }
        }

        private void TextBoxFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
            textBox.BorderBrush = Brushes.Red;
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();
            }

        }

        private void MostrarCampo(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            MessageBox.Show($"Inserta tu {textBox.Tag}", "Información de campo", MessageBoxButton.OK);
        }

        private void Cancelar_Boton(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BotonNombre(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"¿Confirmar el texto '{Nombre.Text}'?",
                                                      "Confirmación",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(Nombre.Text))
                {
                    MessageBox.Show("RELLENA EL CAMPO");
                }
                else
                {
                    MessageBox.Show("ÉXITO AL CONFIRMAR EL CAMPO");
                    Nombre.BorderBrush = Brushes.LimeGreen;
                }
            }
            else
            {
                Nombre.Text = string.Empty;
                Nombre.BorderBrush = Brushes.Red;
            }
        }

        private void BotonApellidos(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"¿Confirmar el texto '{Apellidos.Text}'?",
                                                      "Confirmación",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(Apellidos.Text))
                {
                    MessageBox.Show("RELLENA EL CAMPO");
                }
                else
                {
                    MessageBox.Show("ÉXITO AL CONFIRMAR EL CAMPO");
                    Apellidos.BorderBrush = Brushes.LimeGreen;
                }
            }
            else
            {
                Apellidos.Text = string.Empty;
                Apellidos.BorderBrush = Brushes.Red;
            }
        }

        private void EliminarFoto(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"¿Eliminar la foto?",
                                                      "Confirmación",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                UserImage.Source = null;
            }
        }

        private void BotonEmail(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"¿Confirmar el texto '{Email.Text}'?",
                                                      "Confirmación",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(Email.Text))
                {
                    MessageBox.Show("RELLENA EL CAMPO");
                }
                else
                {
                    MessageBox.Show("ÉXITO AL CONFIRMAR EL CAMPO");
                    Email.BorderBrush = Brushes.LimeGreen;
                }
            }
            else
            {
                Email.Text = string.Empty;
                Email.BorderBrush = Brushes.Red;
            }
        }

        private void BotonTelefono(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"¿Confirmar el texto '{Telefono.Text}'?",
                                                      "Confirmación",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(Telefono.Text))
                {
                    MessageBox.Show("RELLENA EL CAMPO");
                }
                else
                {
                    MessageBox.Show("ÉXITO AL CONFIRMAR EL CAMPO");
                    Telefono.BorderBrush = Brushes.LimeGreen;
                }
            }
            else
            {
                Telefono.Text = string.Empty;
                Telefono.BorderBrush = Brushes.Red;
            }
        }

        private async void Guardar_Click(object sender, RoutedEventArgs e)
        {
            int currentPart = 1;

            while (currentPart <= 4)
            {
                ShowAutoClosingMessageBox($"Validando parte {currentPart} del formulario...", 2);

                if (ValidarForm(currentPart))
                {
                    MessageBoxResult result = MessageBox.Show($"Parte {currentPart} validada", "Validación Exitosa", MessageBoxButton.OK);

                    if (result == MessageBoxResult.OK)
                    {
                        currentPart++;
                    }
                    else
                    {
                        MessageBox.Show("Validación cancelada", "Validación cancelada", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(errorFormulario, "Error de Validación", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            MessageBox.Show("¡Formulario validado completamente!", "Validación Completa", MessageBoxButton.OK);
        }

        private void ShowAutoClosingMessageBox(string message, int durationInSeconds)
        {
            var ventanaDeAviso = new VentanaDeAviso(message, durationInSeconds);
            ventanaDeAviso.ShowDialog();
        }


        private bool ValidarForm(int part)
        {
            switch (part)
            {
                case 1:
                    errorFormulario = "Confirma todos los campos obligatorios con su botón correspondiente porfavor...";
                    return Nombre.BorderBrush != Brushes.Red &&
                   Apellidos.BorderBrush != Brushes.Red &&
                   Email.BorderBrush != Brushes.Red &&
                   Telefono.BorderBrush != Brushes.Red;

                case 2:
                    errorFormulario = "Selecciona una fecha y escribe tu DNI por favor...";
                    return fechaNacimiento.SelectedDate.HasValue && !string.IsNullOrWhiteSpace(DNI.Text);

                case 3:
                    errorFormulario = "Adjunta una foto de perfil porfavor...";
                    return UserImage.Source != null;

                case 4:
                    errorFormulario = "Rellena todos los campos correspondientes al domicilio, inserta un enlace de una red social, selecciona un rol y describe tu rol de tu puesto actual de trabajo.";
                    return !string.IsNullOrWhiteSpace(Direccion.Text) &&
                        !string.IsNullOrWhiteSpace(Ciudad.Text) &&
                        !string.IsNullOrWhiteSpace(Provincia.Text) &&
                        !string.IsNullOrWhiteSpace(CodigoPostal.Text) &&
                        !string.IsNullOrWhiteSpace(Pais.Text) &&
                        !string.IsNullOrWhiteSpace(RedSocial.Text) &&
                        RolActual.SelectedIndex != -1 &&
                        !string.IsNullOrWhiteSpace(DescripcionPuesto.Text);

                default:
                    return true;
                    
            }
        }

    }
}
