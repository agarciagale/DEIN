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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GuardarEmpleado(object sender, RoutedEventArgs e)
        {
            
            Empleado empleado = new Empleado(Nombre.Text, Apellidos.Text, Email.Text, Telefono.Text);
            tablaEmpleados.Items.Add(empleado);
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
    }


    public class Empleado
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        public Empleado(string nombre, string apellidos, string email, string telefono)
        {
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.Email = email;
            this.Telefono = telefono;
        }

        private void comprobarDatos(Empleado empleado)
        {
            foreach (PropertyInfo campo in empleado.GetType().GetProperties())
            {
                
            }
        }
    }
}
