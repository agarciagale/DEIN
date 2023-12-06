using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using static GestionEmpleados2023.ListaEmpleados;

namespace GestionEmpleados2023
{
    public partial class BuscarEmpleado : Window
    {
        private List<Empleado> empleadosParaModificar = new List<Empleado>();

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

                    string CadenaDeConexion = "server=localhost;port=3306;uid=root;pwd='';database=gestion-empleados;";
                    MySqlConnection conexionConSql = new MySqlConnection(CadenaDeConexion);

                    DataTable Empleados = new DataTable();

                    List<Empleado> listaEmpleados = new List<Empleado>();

                    MySqlDataAdapter adaptador = new MySqlDataAdapter(consulta, conexionConSql);

                    using (adaptador)
                    {
                        adaptador.Fill(Empleados);
                    }

                    listaEmpleados = Empleados.AsEnumerable().Select(row => new Empleado
                    {
                        Id = row.Field<int>("Id"),
                        Nombre = row.Field<string>("Nombre"),
                        Apellidos = row.Field<string>("Apellidos"),
                        EsUsuario = (row["EsUsuario"] != DBNull.Value) ? Convert.ToBoolean(row["EsUsuario"]) : false,
                        Edad = row.Field<int>("Edad")
                    }).ToList();

                    dataGrid.Visibility = Visibility.Hidden;
                    dataGrid.ItemsSource = listaEmpleados;

                    foreach (var column in dataGrid.Columns)
                    {
                        if (column.Header.ToString() == "Id")
                        {
                            var textColumn = column as DataGridTextColumn;
                            if (textColumn != null)
                            {
                                textColumn.IsReadOnly = true;
                            }
                        }
                    }

                    dataGrid.Visibility = Visibility.Visible;

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

        private void ActualizarEmpleados(object sender, RoutedEventArgs e)
        {
            foreach (var empleadoModificado in empleadosParaModificar)
            {
                ActualizarEmpleadoEnBD(empleadoModificado);
            }

            MessageBox.Show("Cambios actualizados exitosamente.");

            empleadosParaModificar.Clear();
        }


        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs args)
        {
            try
            {
                if (args.EditAction == DataGridEditAction.Commit)
                {
                    var editedItem = args.Row.Item as Empleado;

                    if (editedItem != null)
                    {
                        Empleado existingItem = empleadosParaModificar.FirstOrDefault(e => e.Id == editedItem.Id);

                        if (existingItem != null)
                        {
                            existingItem.Nombre = editedItem.Nombre;
                            existingItem.Apellidos = editedItem.Apellidos;
                            existingItem.EsUsuario = editedItem.EsUsuario;
                            existingItem.Edad = editedItem.Edad;
                        }
                        else
                        {
                            empleadosParaModificar.Add(editedItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void ActualizarEmpleadoEnBD(Empleado empleado)
        {
            try
            {
                string CadenaDeConexion = "server=localhost;port=3306;uid=root;pwd='';database=gestion-empleados;";
                using (MySqlConnection conexionConSql = new MySqlConnection(CadenaDeConexion))
                {
                    string consulta = "UPDATE EMPLEADOS SET Nombre = @Nombre, Apellidos = @Apellidos, EsUsuario = @EsUsuario, Edad = @Edad WHERE Id = @Id";

                    using (MySqlCommand cmd = new MySqlCommand(consulta, conexionConSql))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                        cmd.Parameters.AddWithValue("@Apellidos", empleado.Apellidos);
                        cmd.Parameters.AddWithValue("@EsUsuario", empleado.EsUsuario);
                        cmd.Parameters.AddWithValue("@Edad", empleado.Edad);
                        cmd.Parameters.AddWithValue("@Id", empleado.Id);

                        conexionConSql.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el empleado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
