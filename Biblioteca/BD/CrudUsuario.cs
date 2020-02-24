using Biblioteca.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using Android.App;

namespace Biblioteca.BD
{
    public class CrudUsuario
    {
        Conexion con = new Conexion();

        public bool agregarUs(Usuario us)
        {
            bool estado = true;
            try
            {
                estado = false;

                string Query = "insert into Usuario(usuario,pass,fNac,Rut,idFacebook,fk_idComuna,fk_TipoU,fk_IdPrevision,fk_EferGlucosa,fk_tipoSangre,nombre,apellido,fk_factorRh) values('" + us.Uss + "','" + us.Pass + "',@FecNac," + int.Parse(us.Rut) + ",'" + us.IdFacebook + "'," + us.Comuna.IdComuna + "," + 2 + "," + us.Fk_idPrevision + "," + us.Fk_Eferglucosa + "," + us.Fk_TipoSangre + ",'" + us.Nombre + "','" + us.Apellido + "'," + us.Fk_FactorRh + ");";

                con.Open();
                SqlCommand sql = new SqlCommand(Query, con.sqlConnection());
                sql.Parameters.AddWithValue("FecNac", us.FNac);
                sql.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                estado = false;
                throw new Exception("error: " + ex.Message);

            }

            return estado;

        }

        public Usuario mostrarDatosUsuario(int idus)
        {

            try
            {
                Usuario u = new Usuario();
                con.Open();
                string query = "SELECT Usuario.usuario, Usuario.pass, Usuario.fNac, Usuario.Rut, Usuario.fk_idComuna, Usuario.fk_TipoU, Usuario.fk_idPrevision, Usuario.fk_EferGlucosa, Usuario.fk_tipoSangre, Usuario.nombre, Usuario.apellido, Usuario.fk_FactorRh, TipoPrevision.idtipoPrev, Region.idReion, TipoU.idTipoU, Ciudad.idCiudad FROM TipoU INNER JOIN TipoSangre INNER JOIN TipoPrevision INNER JOIN Prevision ON TipoPrevision.idtipoPrev = Prevision.fk_tipoPrev INNER JOIN Usuario ON Prevision.idPrevision = Usuario.fk_idPrevision INNER JOIN EnferGlucosa ON Usuario.fk_EferGlucosa = EnferGlucosa.idEnferGlucosa INNER JOIN Ciudad INNER JOIN Comuna ON Ciudad.idCiudad = Comuna.fk_ciudad INNER JOIN Region ON Ciudad.fk_idRegion = Region.idReion INNER JOIN Pais ON Region.fk_idPais = Pais.idPais ON Usuario.fk_idComuna = Comuna.idComuna INNER JOIN FactorRh ON Usuario.fk_FactorRh = FactorRh.idFactorRh ON TipoSangre.idTiposangre = Usuario.fk_tipoSangre ON TipoU.idTipoU = Usuario.fk_TipoU WHERE (Usuario.idUs = " + idus + ")";
                SqlCommand Query = new SqlCommand(query, con.sqlConnection());
                SqlDataReader reader = Query.ExecuteReader();
                while (reader.Read())
                {
                    u.Uss = reader.GetString(0);
                    u.Pass = reader.GetString(1);
                    u.FNac = reader.GetDateTime(2);
                    u.Rut = reader.GetString(3);
                    Comuna c = new Comuna();
                    c.IdComuna = reader.GetInt32(4);
                    u.Comuna = c;
                    u.Fk_TipoU = reader.GetInt32(5);
                    u.Fk_idPrevision = reader.GetInt32(6);
                    u.Fk_Eferglucosa = reader.GetInt32(7);
                    u.Fk_TipoSangre = reader.GetInt32(8);
                    u.Nombre = reader.GetString(9);
                    u.Apellido = reader.GetString(10);
                    u.Fk_FactorRh = reader.GetInt32(11);
                    u.Fk_tipoPrevision = reader.GetInt32(12);

                    u.Comuna.IdRegion = reader.GetInt32(13);
                    u.Comuna.IdCiudad = reader.GetInt32(15);

                }
                con.Close();
                return u;
            }
            catch (Exception ex)
            {

                throw new Exception("Error: " + ex.Message);
            }

        }

        public bool modificarUsuario(Usuario us, int idus)
        {
            bool estado = false;
            try
            {
                string Query = "";

                estado = false;
                if (us.Pass == "")
                {
                    Query = "update usuario set usuario='" + us.Uss + "',fnac=@FecNac,rut='" + us.Rut + "',fk_idcomuna='" + us.Comuna.IdComuna + "',fk_idPrevision=" + us.Fk_idPrevision + ",fk_eferglucosa=" + us.Fk_Eferglucosa + ",fk_tipoSangre=" + us.Fk_TipoSangre + ",nombre='" + us.Nombre + "',apellido='" + us.Apellido + "',fk_factorRh=" + us.Fk_FactorRh + " where idUs=@idus;";

                }
                else
                {
                    Query = "update usuario set usuario='" + us.Uss + "',pass='" + us.Pass + "',fnac=@FecNac,rut='" + us.Rut + "',fk_idcomuna='" + us.Comuna.IdComuna + "',fk_idPrevision=" + us.Fk_idPrevision + ",fk_eferglucosa=" + us.Fk_Eferglucosa + ",fk_tipoSangre=" + us.Fk_TipoSangre + ",nombre='" + us.Nombre + "',apellido='" + us.Apellido + "' ,fk_factorRh=" + us.Fk_FactorRh + " where idUs=@idus;";

                }

                con.Open();
                SqlCommand sql = new SqlCommand(Query, con.sqlConnection());
                sql.Parameters.AddWithValue("FecNac", us.FNac);
                sql.Parameters.AddWithValue("idus", idus);
                if (sql.ExecuteNonQuery() == 1)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                }

                con.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Error: " + ex.Message);

            }
            return estado;

        }


        public bool BuscaridF(int idus, String idf)
        {
            bool flag = false;
            try
            {
                con.Open();
                String Ssql = "select idUs from Usuario where idUs = @idUS and idFacebook = @idf";
                SqlCommand sql = new SqlCommand(Ssql, con.sqlConnection());
                sql.Parameters.AddWithValue("idUS", idus);
                sql.Parameters.AddWithValue("idf", idf);
                SqlDataReader reader = sql.ExecuteReader();
                if (reader.Read())
                {
                    flag = true;
                }
                con.Close();

            }
            catch (Exception e)
            {


                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                System.Console.WriteLine(e);
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                Console.WriteLine("error");
                Android.Widget.Toast.MakeText(Android.App.Application.Context, "error fb buscar".ToString(), Android.Widget.ToastLength.Short).Show();
                con.Close();
            }




            return flag;
        }

        public bool Eliminarfb(int idus)
        {
            bool flag = false;

            try
            {
                con.Open();
                String Ssql = "update Usuario set idFacebook= Null where idUs = @idus;";
                SqlCommand sql = new SqlCommand(Ssql, con.sqlConnection());
                sql.Parameters.AddWithValue("idus", idus);
                sql.ExecuteNonQuery();
                flag = true;
               

            }
            catch (Exception e)
            {
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                System.Console.WriteLine(e);
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                Console.WriteLine("error");
                Android.Widget.Toast.MakeText(Android.App.Application.Context, "error fb eliminar".ToString(), Android.Widget.ToastLength.Short).Show();


            }

            con.Close();

            return flag;
        }

        public bool Agregarfb(int idus, string idf)
        {
            bool flag = false;

            try
            {
                con.Open();
                String Ssql = "update Usuario set idFacebook= @idf where idUs = @idus;";
                SqlCommand sql = new SqlCommand(Ssql, con.sqlConnection());
                sql.Parameters.AddWithValue("idus", idus);
                sql.Parameters.AddWithValue("idf", idf);
                sql.ExecuteNonQuery();
                flag = true;


            }
            catch (Exception e)
            {
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                System.Console.WriteLine(e);
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                Console.WriteLine("error");
                Android.Widget.Toast.MakeText(Android.App.Application.Context, "error fb eliminar".ToString(), Android.Widget.ToastLength.Short).Show();


            }

            con.Close();

            return flag;
        }


    }
}
