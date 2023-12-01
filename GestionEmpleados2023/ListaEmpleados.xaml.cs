using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GestionEmpleados2023
{
    public partial class ListaEmpleados : Window
    {
        private GestionEmpleados2023 gestionEmpleados;

        public ListaEmpleados()
        {
            InitializeComponent();
            gestionEmpleados = new GestionEmpleados2023();
            CargarEmpleadosEnDataGrid();
        }

        private void CargarEmpleadosEnDataGrid()
        {
            List<Empleado> empleados = gestionEmpleados.ObtenerEmpleados();
            dataGrid.ItemsSource = empleados;
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem != null)
                {
                    var selectedObject = (Empleado)dataGrid.SelectedItem;

                    gestionEmpleados.BorrarEmpleadoDeBD(selectedObject.Id);

                    CargarEmpleadosEnDataGrid();
                }
                else
                {
                    MessageBox.Show("Selecciona una fila para borrar.");
                }
            }catch(Exception edsad)
            {
                MessageBox.Show(edsad.Message);
            }
        }

        public class Empleado 
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public bool EsUsuario { get; set; }
            public int Edad { get; set; }
        }

        public partial class GestionEmpleados2023
        {
            private SqlConnection conexionConSql;

            public GestionEmpleados2023()
            {
                EstablecerConexion();
            }

            private void EstablecerConexion()
            {
                string CadenaDeConexion = ConfigurationManager.ConnectionStrings["GestionEmpleados2023.Properties.Settings.GestionEmpleadosConnectionString"].ConnectionString;
                conexionConSql = new SqlConnection(CadenaDeConexion);
            }

            public void BorrarEmpleadoDeBD(int id)
            {
                EstablecerConexion();

                string consulta = "DELETE FROM EMPLEADOS WHERE id = " + id + ";";

                using (SqlCommand cmd = new SqlCommand(consulta, conexionConSql))
                {
                    try
                    {
                        conexionConSql.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al borrar el empleado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

            public List<Empleado> ObtenerEmpleados()
            {
                EstablecerConexion();

                string consulta = "SELECT * FROM EMPLEADOS";
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

                return listaEmpleados;
            }
        }
    }
}
