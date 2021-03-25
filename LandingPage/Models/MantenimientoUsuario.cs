using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LandingPage.Models
{
    public class MantenimientoUsuario
    {
        private SqlConnection con;

        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["admin"].ToString();
            con = new SqlConnection(constr);
        }
        public int Insertar(Usuario usu)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("insert into usuarios(nombre,correo,comentario) values (@nombre,@correo,@comentario)", con);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar);
            comando.Parameters.Add("@correo", SqlDbType.VarChar);
            comando.Parameters.Add("@comentario", SqlDbType.VarChar);

            comando.Parameters["@nombre"].Value = usu.Nombre;
            comando.Parameters["@correo"].Value = usu.Correo;
            comando.Parameters["@comentario"].Value = usu.Comentario;

            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();

            return i;
        }
        public List<Usuario> RecuperarTodos()
        {
            Conectar();
            List<Usuario> usuarios = new List<Usuario>();
            SqlCommand comando = new SqlCommand("select id, nombre, correo, comentario from usuarios order by id asc", con);
            con.Open();
            SqlDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                Usuario usu = new Usuario
                {
                    Id = int.Parse(registros["id"].ToString()),
                    Nombre = registros["nombre"].ToString(),
                    Correo = registros["correo"].ToString(),
                    Comentario = registros["comentario"].ToString()
                };
                usuarios.Add(usu);
            }
            con.Close();
            return usuarios;
        }
        public Usuario Recuperar(int id)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("select id, nombre, correo, comentario from usuarios where id = @id", con);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters["@id"].Value = id;
            con.Open();

            SqlDataReader registros = comando.ExecuteReader();
            Usuario usuario = new Usuario();

            if (registros.Read())
            {
                usuario.Id = int.Parse(registros["id"].ToString());
                usuario.Nombre = registros["nombre"].ToString();
                usuario.Correo = registros["correo"].ToString();
                usuario.Comentario = registros["comentario"].ToString();
            }
            con.Close();
            return usuario;
        }
        public int Borrar(int id)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("delete from usuarios where id=@id", con);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters["@id"].Value = id;

            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}