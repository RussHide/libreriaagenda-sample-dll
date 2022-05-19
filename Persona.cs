using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaAgenda
{
    public class Persona
    {
        private string nombre;
        private string apePaterno;
        private string apeMaterno;
        private int edad;
        private string sexo;
        private List<Telefono> lstTelefonos;
        private Direccion direccion;
        private int id;

        public string Nombre { get => nombre; set => nombre = value; }
        public string ApePaterno { get => apePaterno; set => apePaterno = value; }
        public string ApeMaterno { get => apeMaterno; set => apeMaterno = value; }
        public int Edad { get => edad; set => edad = value; }
        public string Sexo { get => sexo; set => sexo = value; }
        public int Id { get => id; set => id = value; }
        public List<Telefono> LstTelefonos { get => lstTelefonos; set => lstTelefonos = value; }
        public Direccion Direccion { get => direccion; set => direccion = value; }
    }
     public class ADOPersona
    {
        public Persona buscarPersonaByID(int id)
        {
            Persona persona = new Persona();
            return persona;
        }
        public List<Persona> buscarPersonas()
        {
            List<Persona> lstPersonas = new List<Persona>();
            DataTable dt = new DataTable();
            Persona persona;
            MySqlConnection con = Conexion.conexion();
            string Query = "SELECT * FROM personas;";
            try
            {

                MySqlDataAdapter da = new MySqlDataAdapter(Query, con);
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    persona = new Persona();
                    persona.Id = int.Parse(dt.Rows[i][0].ToString());
                    persona.Nombre = dt.Rows[i][1].ToString();
                    persona.ApePaterno = dt.Rows[i][2].ToString();
                    persona.ApeMaterno = dt.Rows[i][3].ToString();
                    persona.Edad = int.Parse(dt.Rows[i][4].ToString());
                    persona.Sexo = dt.Rows[i][5].ToString();
                    ADODireccion adoDireccion = new ADODireccion();
                    persona.Direccion = adoDireccion.buscarDireccionByIdPersona(persona.Id);
                    ADOTelefonos adoTel = new ADOTelefonos();
                    persona.LstTelefonos = adoTel.buscaTelefonosByIDPersona(persona.Id);
                    lstPersonas.Add(persona);
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


            return lstPersonas;
        }
        public List<Persona> buscarByNombreLike(string nombre)
        {
            List<Persona> lstPersonas = new List<Persona>();

            return lstPersonas;
        }
        public bool actualizarPersona(Persona persona)
        {
            //Le vamos a pegar a las clases ADO de Teléfono y Dirección
            return true;
        }
        public bool eliminarPersona(int id)
        {
            return true;
        }

        public bool insertarPersona(Persona persona)
        {
            bool bandera = false;
            MySqlConnection con = Conexion.conexion();
            var conexionSql = con.BeginTransaction();
            String query;
            int keyAuto;
            try
            {
                query = "Insert into personas (nombre,apePaterno,apeMaterno,edad,sexo) values('{0}','{1}','{2}',{3},'{4}');SELECT LAST_INSERT_ID()";
                query = string.Format(query, persona.Nombre, persona.ApePaterno, persona.ApeMaterno, persona.Edad, persona.Sexo);
                MySqlCommand comando = new MySqlCommand(query, conexionSql.Connection);
                keyAuto = Convert.ToInt32(comando.ExecuteScalar());
       
                if (keyAuto > 0)
                {
                    persona.Id = keyAuto;
                    ADODireccion direccion = new ADODireccion();
                    if (direccion.insertarDireccion(persona, conexionSql.Connection))
                    {
                        conexionSql.Commit();
                        bandera = true;
                    }
                    else
                    {
                        conexionSql.Rollback();
                       
                    }
                    
                   
                }
               
                
            }catch(MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
                return bandera;
        }
    }
}
