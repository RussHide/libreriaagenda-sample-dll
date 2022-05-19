using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaAgenda
{
    public class Conexion
    {
        public static MySqlConnection conexion()
        {

            String path = System.AppDomain.CurrentDomain.BaseDirectory;
            StreamReader objReader = new StreamReader(path + "host.txt");
            string sLine = "";

            for (int i = 0; i < 1; i++)
            {
                sLine = objReader.ReadLine();
            }

            

            String cadena = sLine;
            // String cadena = "Server=localhost;UserId=root;Password=07598453;Database=dbpinales";
            MySqlConnection con = null;
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = cadena;
                con.Open();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                objReader.Close();
            }
            return con;
        }

        public static void cerrar(MySqlConnection con)
        {
            try
            {
                con.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }
    }
}
