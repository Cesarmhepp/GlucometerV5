using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Biblioteca.BD;
using Biblioteca.Modelo;

namespace Biblioteca.BD
{
    public class Login
    {
        Conexion con = new Conexion();



        public Usuario LoginNormal(string correo, string pass)
        {
            Usuario us = new Usuario();
            us.Idusuario = 0;
            try
            {
                con.Open();

                SqlCommand Query = new SqlCommand("select idUs,nombre,apellido,fk_TipoU from Usuario where usuario = '" + correo + "' and pass='" + pass + "';", con.sqlConnection());
                SqlDataReader reader = Query.ExecuteReader();
                if (reader.Read())
                {
                    us.Idusuario = reader.GetInt32(0);
                    us.Nombre = reader.GetString(1);
                    us.Apellido = reader.GetString(2);
                    us.Fk_TipoU = reader.GetInt32(3);

                }

                if (us.Idusuario != 0)
                {
                    con.Close();
                    con.Open();
                    SqlCommand sql2 = new SqlCommand("insert into us_iniciarSec(InicioSesion,fk_idus)values('" + DateTime.Now + "'," + us.Idusuario + ");", con.sqlConnection());
                    sql2.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                con.Close();
            }




            con.Close();
            return us;
        }




        public int loginfacebook(String idFacebook)//podria devolver el id del usuario para que sea mas facil agregar la glucosa
        {// y el if se cambiaria if(!=0) e inicia el intent 
            int idUS = 0;
            try
            {
                con.Open();
            }
            catch (Exception)
            {

                throw;
            }

            SqlCommand Query = new SqlCommand("select idUs from Usuario where idFacebook = '" + idFacebook + "';", con.sqlConnection());
            SqlDataReader reader = Query.ExecuteReader();
            if (reader.Read())
            {
                idUS = reader.GetInt32(0);

            }
            if (idUS != 0)
            {
                con.Close();
                con.Open();
                SqlCommand sql2 = new SqlCommand("insert into us_iniciarSec(InicioSesion,fk_idus)values('" + DateTime.Now + "'," + idUS + ");", con.sqlConnection());
                sql2.ExecuteNonQuery();
            }
            con.Close();
            return idUS;
        }


    }
 }
