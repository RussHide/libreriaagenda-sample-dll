using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaAgenda
{
   public class Telefono
    {
        private string numTelefono;
        private string lada;

        public string NumTelefono { get => numTelefono; set => numTelefono = value; }
        public string Lada { get => lada; set => lada = value; }
    }
    public class ADOTelefonos
    {
        public bool insertarTelefonos(Persona persona, MySqlConnection con)
        {
            bool bandera = false;
            String query;
            int registro = 0;
            try
            {
                foreach (Telefono tel in persona.LstTelefonos)
                {
                    query = "Insert into telefonos (idPersona,lada,telefono) values({0},'{1}','{2}');";
                    query = string.Format(query, persona.Id, tel.Lada, tel.NumTelefono);
                    MySqlCommand comando = new MySqlCommand(query, con);
                    registro = registro + (comando.ExecuteNonQuery());
                }

                // keyAuto = Convert.ToInt32(comando.ExecuteScalar());

                if (registro == persona.LstTelefonos.Count)
                {
                    bandera = true;

                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            return bandera;
        }

        public List<Telefono> buscaTelefonosByIDPersona(int id)
        {
            List<Telefono> lstTelefonos = new List<Telefono>();
            DataTable dt = new DataTable();
            Telefono telefono;
            MySqlConnection con = Conexion.conexion();
            string Query = "SELECT * FROM telefonos where idPersona = {0};";
            Query = String.Format(Query,id);

            try
            {

                MySqlDataAdapter da = new MySqlDataAdapter(Query, con);
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    telefono = new Telefono();
                    telefono.Lada = dt.Rows[i][2].ToString();
                    telefono.NumTelefono = dt.Rows[i][3].ToString();
                    lstTelefonos.Add(telefono);
                }


            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return lstTelefonos;

        }
    }
}
