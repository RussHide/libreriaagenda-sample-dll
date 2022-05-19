using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

using System.Threading.Tasks;

namespace LibreriaAgenda
{
    public class Usuario
    {
        private int id;
        private string name;
        private string user;
        private string password;
        private string add;
        private string edit;
        private string delete;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
        public string Add { get => add; set => add = value; }
        public string Edit { get => edit; set => edit = value; }
        public string Delete { get => delete; set => delete = value; }
    }

    public class ADOUser
    {

        public List<Usuario> datosTabla()
        {
            List<Usuario> lstUsers = new List<Usuario>();
            Usuario usuario;
            MySqlConnection con = LibreriaAgenda.Conexion.conexion();
            try
            {
                string query = "select * from usuario";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader row;
                row = cmd.ExecuteReader();
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        usuario = new Usuario();
                        //DataGridViewRow newRow = new DataGridViewRow();

                        //newRow.CreateCells(dgvUsuarios);
                        usuario.Id = Convert.ToInt32(row["idUsuario"]);
                        usuario.Name = Convert.ToString(row["nombre"]);
                        usuario.User = Convert.ToString(row["usuario"]);
                        usuario.Password = "****"; //row["contraseña"].ToString();
                        usuario.Add = Convert.ToString(row["agregar"]);
                        usuario.Edit = Convert.ToString(row["editar"]);
                        usuario.Delete = Convert.ToString(row["eliminar"]);
                        //dgvUsuarios.Rows.Add(newRow);
                        lstUsers.Add(usuario);
                    }
                }

                con.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();

            }
            return lstUsers;

        }

        public bool addUser(Usuario usuario)
        {
            MySqlConnection con = Conexion.conexion();

            String query;
            try
            {
                query = "Insert into usuario (nombre,usuario,contrasena,agregar,editar,eliminar) values('" + usuario.Name + "','" + usuario.User + "','" + usuario.Password + "','" + usuario.Add + "','" + usuario.Edit + "','" + usuario.Delete + "')";

                MySqlCommand comando = new MySqlCommand(query, con);
                if(comando.ExecuteNonQuery() == 1)
                {
                    return true;

                }else
                {
                    return false;
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
        }

        public bool deleteUser(int usId)
        {
            MySqlConnection con = Conexion.conexion();
            try
            {
                String query = "delete from usuario where idUsuario='" + usId + "'";

                MySqlCommand comando = new MySqlCommand(query, con);
                if (comando.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
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
        }

        public bool editUser(Usuario us, int idUser)
        {
            MySqlConnection con = Conexion.conexion();
            try
            {
                String query = "update usuario set nombre='" + us.Name + "',usuario='" + us.User+ "',contrasena='" + us.Password+ "',agregar='" + us.Add + "',editar='" + us.Edit + "',eliminar='" + us.Delete + "' where idUsuario='" + idUser + "'";
                MySqlCommand comando = new MySqlCommand(query, con);
                if (comando.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
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
        }
    }
}
