using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using static GestionEmpleados2023.ListaEmpleados;

namespace GestionEmpleados2023
{
    /// <summary>
    /// Lógica de interacción para BuscarEmpleado.xaml
    /// </summary>
    public partial class BuscarEmpleado : Window
    {
        public BuscarEmpleado()
        {
            InitializeComponent();
        }

        private void BuscarEmpleadoClick(object sender, RoutedEventArgs e)
        {
            string consulta = "SELECT * FROM EMPLEADOS WHERE ";
            bool consultaCorrecta = true;
            int edad;
            bool usuario = (bool)EsUsuario.IsChecked;

            if (!string.IsNullOrWhiteSpace(Nombre.Text))
            {
                consulta += "Nombre = '" + Nombre.Text + "' AND ";
            }

            if (!string.IsNullOrWhiteSpace(Apellidos.Text))
            {
                consulta += "Apellidos = '" + Apellidos.Text + "' AND ";
            }

            if (usuario)
            {
                consulta += "EsUsuario = '1' AND ";
            }
            else
            {
                consulta += "EsUsuario = '0' AND ";
            }

            if (!string.IsNullOrWhiteSpace(Edad.Text))
            {
                if (int.TryParse(Edad.Text, out edad))
                {
                    consulta += "Edad = '" + Edad.Text + "' AND ";
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese una edad válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    consultaCorrecta = false;
                }
            }

            if (consultaCorrecta)
            {
                try
                {
                    consulta = consulta.Trim();
                    consulta = consulta.Substring(0, consulta.Length - 3);
                    consulta += ";";
                    string CadenaDeConexion = ConfigurationManager.ConnectionStrings["GestionEmpleados2023.Properties.Settings.GestionEmpleadosConnectionString"].ConnectionString;
                    SqlConnection conexionConSql = new SqlConnection(CadenaDeConexion);

                    DataTable Empleados = new DataTable();

                    List<Empleado> listaEmpleados = new List<Empleado>();

                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexionConSql);

                    using (adaptador)
                    {
                        adaptador.Fill(Empleados);
                    }

                    listaEmpleados = Empleados.AsEnumerable().Select(row => new Empleado
                    {
                        Id = row.Field<int>("Id"),
                        Nombre = row.Field<string>("Nombre"),
                        Apellidos = row.Field<string>("Apellidos"),
                        EsUsuario = (row["EsUsuario"] != DBNull.Value) ? row.Field<bool>("EsUsuario") : false,
                        Edad = row.Field<int>("Edad")
                    }).ToList();

                    dataGrid.ItemsSource = listaEmpleados;

                    if (Empleados.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay resultados con la consulta especificada");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
