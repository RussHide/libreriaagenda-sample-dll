using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaAgenda
{
   public class Direccion
    {
        private string calle;
        private string numCasa;
        private string colonia;
        private int codigoPostal;
        private string ciudad;
        private string estado;

        public string Calle { get => calle; set => calle = value; }
        public string NumCasa { get => numCasa; set => numCasa = value; }
        public string Colonia { get => colonia; set => colonia = value; }
        public int CodigoPostal { get => codigoPostal; set => codigoPostal = value; }
        public string Ciudad { get => ciudad; set => ciudad = value; }
        public string Estado { get => estado; set => estado = value; }
    }
    public class ADODireccion
    {
        public bool insertarDireccion(Persona persona, MySqlConnection con)
        {
            bool bandera = false;
            String query;
            int registro;
            try
            {
                query = "Insert into direcciones (idPersona,numCasa,colonia,ciudad,estado) values({0},'{1}','{2}','{3}','{4}');";
                query = string.Format(query, persona.Id, persona.Direccion.NumCasa, persona.Direccion.Colonia, persona.Direccion.Ciudad, persona.Direccion.Estado);
                MySqlCommand comando = new MySqlCommand(query, con);
                registro=comando.ExecuteNonQuery();
               // keyAuto = Convert.ToInt32(comando.ExecuteScalar());

                if (registro > 0)
                {
                    ADOTelefonos tels = new ADOTelefonos();
                    if (tels.insertarTelefonos(persona, con))
                    {
                        bandera = true;
                    }
                  
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            return bandera;
        }
        public Direccion buscarDireccionByIdPersona(int id)
        {
            DataTable dt = new DataTable();
            Direccion direccion = new Direccion();
            MySqlConnection con = Conexion.conexion();
            string Query = "SELECT * FROM direcciones where idPersona={0};";
            Query = String.Format(Query,id);

            try
            {

                MySqlDataAdapter da = new MySqlDataAdapter(Query, con);
                da.Fill(dt);
                if (dt.Rows.Count!=0)
                {
                    
                    direccion.NumCasa = dt.Rows[0][0].ToString();
                    direccion.Colonia = dt.Rows[0][1].ToString();
                    direccion.Ciudad = dt.Rows[0][2].ToString();
                    direccion.Estado = dt.Rows[0][3].ToString();
                   
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


            return direccion;
        }
    }
}
