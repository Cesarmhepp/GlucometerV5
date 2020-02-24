using Android.App;
using Biblioteca.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Biblioteca.BD
{
    public class CrudInsulina
    {
        Conexion con = new Conexion();

        public bool agregar_insulina(DInsulina i)
        {
            bool flag = false;
            try
            {
                con.Open();
                string Ssql = "insert into Insulina " +
                  "(FK_idglu, " +
                  "mnna_rap,      mnna_regul,     mnna_mezcla, " +
                  "tarde_rap,     tarde_regul,    tarde_mezcla, " +
                  "noche_rap,     noche_regul,    noche_mezcla, " +
                  "dormir_rap,    dormir_regul,   dormir_mezcla) " +
                  "values " +
                  "(@FK_idglu, " +
                  "@mnna_rap,     @mnna_regul,    @mnna_mezcla, " +
                  "@tarde_rap,    @tarde_regul,   @tarde_mezcla, " +
                  "@noche_rap,    @noche_regul,   @noche_mezcla, " +
                  "@dormir_rap,   @dormir_regul,  @dormir_mezcla) ;";


                SqlCommand sql = new SqlCommand(Ssql, con.sqlConnection());
                sql.Parameters.AddWithValue("FK_idglu", i.Fk_idglu);
                sql.Parameters.AddWithValue("mnna_rap", i.Mnna_rap);
                sql.Parameters.AddWithValue("mnna_regul", i.Mnna_regul);
                sql.Parameters.AddWithValue("mnna_mezcla", i.Mnna_mezcla);

                sql.Parameters.AddWithValue("tarde_rap", i.Tarde_rap);
                sql.Parameters.AddWithValue("tarde_regul", i.Tarde_regul);
                sql.Parameters.AddWithValue("tarde_mezcla", i.Tarde_mezcla);

                sql.Parameters.AddWithValue("noche_rap", i.Noche_rap);
                sql.Parameters.AddWithValue("noche_regul", i.Noche_regul);
                sql.Parameters.AddWithValue("noche_mezcla", i.Noche_mezcla);

                sql.Parameters.AddWithValue("dormir_rap", i.Dormir_rap);
                sql.Parameters.AddWithValue("dormir_regul", i.Dormir_regul);
                sql.Parameters.AddWithValue("dormir_mezcla", i.Dormir_mezcla);

                if (sql.ExecuteNonQuery() == 1)
                {
                    flag = true;
                }
                con.Close();


            }
            catch (Exception e)
            {
                con.Close();
                flag = false;
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                System.Console.WriteLine(e);
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                Console.WriteLine("error");
                Android.Widget.Toast.MakeText(Application.Context, "error glucosa".ToString(), Android.Widget.ToastLength.Short).Show();

            }

            return flag;

        }

        public bool Modificar(DInsulina i)
        {
            bool flag = false;

            try
            {
                con.Open();

                String Ssql = "update Insulina  Set   " +
                                  "mnna_rap=@mnna_rap ,  mnna_regul=@mnna_regul,   mnna_mezcla=@mnna_mezcla, " +
                                  "tarde_rap=@tarde_rap, tarde_regul=@tarde_regul, tarde_mezcla=@tarde_mezcla, " +
                                  "noche_rap=@noche_rap,     noche_regul=@noche_regul,    noche_mezcla=@noche_mezcla, " +
                                  "dormir_rap=@dormir_rap,    dormir_regul=@dormir_regul,   dormir_mezcla=@dormir_mezcla";


                SqlCommand sql = new SqlCommand(Ssql, con.sqlConnection());
                sql.Parameters.AddWithValue("mnna_rap", i.Mnna_rap);
                sql.Parameters.AddWithValue("mnna_regul", i.Mnna_regul);
                sql.Parameters.AddWithValue("mnna_mezcla", i.Mnna_mezcla);

                sql.Parameters.AddWithValue("tarde_rap", i.Tarde_rap);
                sql.Parameters.AddWithValue("tarde_regul", i.Tarde_regul);
                sql.Parameters.AddWithValue("tarde_mezcla", i.Tarde_mezcla);

                sql.Parameters.AddWithValue("noche_rap", i.Noche_rap);
                sql.Parameters.AddWithValue("noche_regul", i.Noche_regul);
                sql.Parameters.AddWithValue("noche_mezcla", i.Noche_mezcla);

                sql.Parameters.AddWithValue("dormir_rap", i.Dormir_rap);
                sql.Parameters.AddWithValue("dormir_regul", i.Dormir_regul);
                sql.Parameters.AddWithValue("dormir_mezcla", i.Dormir_mezcla);

                sql.ExecuteNonQuery();

                flag = true;
                
                con.Close();
            }
            catch (Exception e)
            {
                con.Close ();
                flag = false;
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                System.Console.WriteLine(e);
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                Console.WriteLine("error");
                Android.Widget.Toast.MakeText(Application.Context, "error glucosa modificar".ToString(), Android.Widget.ToastLength.Short).Show();
            }





            return flag;
        }

        public DInsulina buscarin(int idG)
        {
            DInsulina i = new DInsulina();
            try
            {
                con.Open();
                String Ssql = "select IdInsulina, " +
                    "mnna_rap, mnna_regul, mnna_mezcla, " +//3
                    "tarde_rap, tarde_regul, tarde_mezcla, " +//6
                    "noche_rap, noche_regul, noche_mezcla, " +//9
                    "dormir_rap, dormir_regul, dormir_mezcla " +//12
                    "from Insulina where FK_idglu = @idG ;";
                SqlCommand sql = new SqlCommand(Ssql, con.sqlConnection());
                sql.Parameters.AddWithValue("idG", idG);

                SqlDataReader reader = sql.ExecuteReader();
                while (reader.Read())
                {
                    i.Idinsulina = reader.GetInt32(0);
                    i.Mnna_rap = reader.GetInt32(1);
                    i.Mnna_regul = reader.GetInt32(2);
                    i.Mnna_mezcla = reader.GetInt32(3);

                    i.Tarde_rap = reader.GetInt32(4);
                    i.Tarde_regul = reader.GetInt32(5);
                    i.Tarde_mezcla = reader.GetInt32(6);

                    i.Noche_rap = reader.GetInt32(7);
                    i.Noche_regul = reader.GetInt32(8);
                    i.Noche_mezcla = reader.GetInt32(9);

                    i.Dormir_rap = reader.GetInt32(10);
                    i.Dormir_regul = reader.GetInt32(11);
                    i.Dormir_mezcla = reader.GetInt32(12);
                    
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
                Android.Widget.Toast.MakeText(Application.Context, "error insulina buscar".ToString(), Android.Widget.ToastLength.Short).Show();
                con.Close();
            }
            
            return i;

        }


    }
}
