using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace AnalizadorLexico
{
    class EjecutorQuerys
    {
        SqlConnection cn;

        public SqlConnection Conectar()
        {
            try
            {
                cn = new SqlConnection("Data Source=DESKTOP-ANTONIO\\SQLEXPRESS; Initial Catalog = AUTOMATAS; User ID=automata; Password=123;Integrated Security=False");
                //cn = new SqlConnection("Data Source=DESKTOP-NJIFA2O\\SQLEXPRESS; Initial Catalog = AUTOMATAS; User ID=automata; Password=12345;Integrated Security=False");
                //cn = new SqlConnection("Data Source= LAPTOP-99KGA844\\SQLEXPRESS; Initial Catalog = AUTOMATAS; User ID=sa; Password=02Kpedo_.;Integrated Security=False");
                cn.Open();
                return cn;
                //MessageBox.Show("Conectado");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se conecto con la base de datos: " + ex.ToString());
                return cn;
            }
        }
        public void Desconectar()
        {
            cn.Close();
        }
        DataTable dt;
        SqlDataAdapter da;
        public void CargarSimbolos(DataGridView dgvSimbolos)
        {
            try
            {
                Conectar();
                da = new SqlDataAdapter("Select * from Matriz", cn); //se llena con la consulta.
                dt = new DataTable();
                da.Fill(dt); //se llena el dt.
                dgvSimbolos.DataSource = (dt); //el data table llena el data grid.
                Desconectar();

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puedo llenar el data grid", ex.ToString());
            }
        }
    }            
}