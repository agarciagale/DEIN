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
            if (dataGrid.SelectedItem != null)
            {
                // Obtener la fila seleccionada
                DataRowView filaSeleccionada = (DataRowView)dataGrid.SelectedItem;

                // Obtener el valor de la clave primaria (suponiendo que se llama 'Id')
                int idEmpleado = Convert.ToInt32(filaSeleccionada["Id"]);

                // Remover la fila del DataGrid
                ((DataTable)dataGrid.ItemsSource).Rows.Remove(filaSeleccionada.Row);
            }
            else
            {
                MessageBox.Show("Selecciona una fila para borrar.");
            }
        }

        public class Empleado
        {
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
