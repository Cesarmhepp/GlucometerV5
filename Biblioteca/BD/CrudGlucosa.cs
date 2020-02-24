using Android.App;
using Biblioteca.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Biblioteca.BD
{
    public class CrudGlucosa
    {
        Conexion con = new Conexion();
        public async Task<bool> AgregarGlucosaAsync(Glucosa g)
        {
            bool estado = true;
            try
            {
                con.Open();
                SqlCommand sql = new SqlCommand("insert into Glucosa" +
                   "(H_A_Des, N_A_Des, H_D_Des, N_D_Des, " + //desayuno
                    "H_A_Alm, N_A_Alm, H_D_Alm, N_D_Alm, " + //almuerzo
                    "H_A_Cen, N_A_Cen, H_D_Cen, N_D_Cen, " + //cena
                    "H_A_Dor, N_A_Dor, H_D_Dor, N_D_Dor, " + //dormir 
                    "FK_idUs, Fecha, Longitud, Latitud, Obs)" +
                    "values (" +
                    "@H_A_Des, @N_A_Des, @H_D_Des, @N_D_Des, " +  //desayuno
                    "@H_A_Alm, @N_A_Alm, @H_D_Alm, @N_D_Alm," +// almuerzo
                    "@H_A_Cen, @N_A_Cen, @H_D_Cen, @N_D_Cen," + //cena
                    "@H_A_Dor, @N_A_Dor, @H_D_Dor, @N_D_Dor," + // dormir 
                    "@FK_idUs, @Fecha, @Longitud, @Latitud, @Obs);", con.sqlConnection());

                // desayuno 
                sql.Parameters.AddWithValue("H_A_Des", g.H_A_Des1);
                sql.Parameters.AddWithValue("N_A_Des", g.N_A_Des1);
                sql.Parameters.AddWithValue("H_D_Des", g.H_D_Des1);
                sql.Parameters.AddWithValue("N_D_Des", g.N_D_Des1);
                // almuerzo
                sql.Parameters.AddWithValue("H_A_Alm", g.H_A_Alm1);
                sql.Parameters.AddWithValue("N_A_Alm", g.N_A_Alm1);
                sql.Parameters.AddWithValue("H_D_Alm", g.H_D_Alm1);
                sql.Parameters.AddWithValue("N_D_Alm", g.N_D_Alm1);
                // cena 
                sql.Parameters.AddWithValue("H_A_Cen", g.H_A_Cen1);
                sql.Parameters.AddWithValue("N_A_Cen", g.N_A_Cen1);
                sql.Parameters.AddWithValue("H_D_Cen", g.H_D_Cen1);
                sql.Parameters.AddWithValue("N_D_Cen", g.N_D_Cen1);
                //dormir
                sql.Parameters.AddWithValue("H_A_Dor", g.H_A_Dor1);
                sql.Parameters.AddWithValue("N_A_Dor", g.N_A_Dor1);
                sql.Parameters.AddWithValue("H_D_Dor", g.H_D_Dor1);
                sql.Parameters.AddWithValue("N_D_Dor", g.N_D_Dor1);
                // otros datos
                sql.Parameters.AddWithValue("Fecha", g.Fecha);
                sql.Parameters.AddWithValue("FK_idUs", g.Fk_id_US);
                sql.Parameters.AddWithValue("Latitud", g.Latitud);
                sql.Parameters.AddWithValue("Longitud", g.Longitud);
                sql.Parameters.AddWithValue("Obs", g.Obs);

                System.Console.WriteLine("sadsadaslkdnmsajksdasdsadsad");
                System.Console.WriteLine("sadsadaslkdnmsajksdasdsadsad");
                System.Console.WriteLine("sadsadaslkdnmsajksdasdsadsad");
                System.Console.WriteLine("sadsadaslkdnmsajksdasdsadsad");

                //await sql.ExecuteNonQueryAsync();

                // sql.ExecuteNonQuery();

                await ExecuteCommandAsync(sql);

                System.Console.WriteLine("sadsadaslkdnmsajksdasdsadsad");
                System.Console.WriteLine("sadsadaslkdnmsajksdasdsadsad");
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
                Android.Widget.Toast.MakeText(Application.Context, "error glucosa".ToString(), Android.Widget.ToastLength.Short).Show();
                estado = false;
                con.Close();
            }


            return estado;

        }

        public async Task<int> ExecuteCommandAsync(SqlCommand sql)
        {
            {
                return await sql.ExecuteNonQueryAsync();
            }
        }

        public int GetID(int idus)
        {
            int id = 0;
            try
            {

                con.Open();
                string Ssql = "select top 1 * from Glucosa  where FK_idUs = @FK_idUs order by IDGlucosa desc;;";
                SqlCommand sql = new SqlCommand(Ssql, con.sqlConnection());
                sql.Parameters.AddWithValue("FK_idUs", idus);
                SqlDataReader reader = sql.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                con.Close();
            }
            catch (Exception e)
            {
                con.Close();
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                System.Console.WriteLine(e);
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                Console.WriteLine("error");
                Android.Widget.Toast.MakeText(Application.Context, "error glucosa".ToString(), Android.Widget.ToastLength.Short).Show();
            }
            

            return id;
        }



        public List<Glucosa> ListaGlucosa(int idUs)
        {
            con.Open();

            List<Glucosa> lista = new List<Glucosa>();

            string query = "select " +
                "H_A_Des, N_A_Des, H_D_Des, N_D_Des, " + //3
                "H_A_Alm, N_A_Alm, H_D_Alm, N_D_Alm, " + //7
                "H_A_Cen, N_A_Cen, H_D_Cen, N_D_Cen, " + //11
                "H_A_Dor, N_A_Dor, H_D_Dor, N_D_Dor, 	" + //15
                "FK_idUs, Fecha, Longitud, Latitud, Obs, IDGlucosa	" + //21
                "from Glucosa where fk_idUs = @idus;";
            SqlCommand sql = new SqlCommand(query, con.sqlConnection());
            sql.Parameters.AddWithValue("idus", idUs);

            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                Glucosa g = new Glucosa();
                g.H_A_Des1 = reader.GetTimeSpan(0);
                g.N_A_Des1 = reader.GetInt32(1);
                g.H_D_Des1 = reader.GetTimeSpan(2);
                g.N_D_Des1 = reader.GetInt32(3);

                g.H_A_Alm1 = reader.GetTimeSpan(4);
                g.N_A_Alm1 = reader.GetInt32(5);
                g.H_D_Alm1 = reader.GetTimeSpan(6);
                g.N_D_Alm1 = reader.GetInt32(7);

                g.H_A_Cen1 = reader.GetTimeSpan(8);
                g.N_A_Cen1 = reader.GetInt32(9);
                g.H_D_Cen1 = reader.GetTimeSpan(10);
                g.N_D_Cen1 = reader.GetInt32(11);

                g.H_A_Dor1 = reader.GetTimeSpan(12);
                g.N_A_Dor1 = reader.GetInt32(13);
                g.H_D_Dor1 = reader.GetTimeSpan(14);
                g.N_D_Dor1 = reader.GetInt32(15);

                g.Fk_id_US = reader.GetInt32(16);
                g.Fecha = reader.GetDateTime(17);
                g.Longitud =reader.GetDouble(18);
                g.Latitud =reader.GetDouble(19);
                g.Obs = reader.GetString(20);
                g.IdGlucosa = reader.GetInt32(21);
                lista.Add(g);
            }
            con.Close();

            return lista;
        }

        public Glucosa buscar(int idG)
        {
            Glucosa g = new Glucosa();
            try
            {
                con.Open();
                String squery = "select " +
               "H_A_Des, N_A_Des, H_D_Des, N_D_Des, " +//3
               "H_A_Alm, N_A_Alm, H_D_Alm, N_D_Alm, " +//7
               "H_A_Cen, N_A_Cen, H_D_Cen, N_D_Cen, " +//11
               "H_A_Dor, N_A_Dor, H_D_Dor, N_D_Dor, " +//15
               "Fecha, Longitud, Latitud, Obs " + //19
               "from Glucosa where IDGlucosa = @idG;";
                SqlCommand sql = new SqlCommand(squery, con.sqlConnection());
                sql.Parameters.AddWithValue("idG", idG);

                SqlDataReader reader = sql.ExecuteReader();
                while (reader.Read())
                {
                    g.H_A_Des1 = reader.GetTimeSpan(0);
                    g.N_A_Des1 = reader.GetInt32(1);
                    g.H_D_Des1 = reader.GetTimeSpan(2);
                    g.N_D_Des1 = reader.GetInt32(3);

                    g.H_A_Alm1 = reader.GetTimeSpan(4);
                    g.N_A_Alm1 = reader.GetInt32(5);
                    g.H_D_Alm1 = reader.GetTimeSpan(6);
                    g.N_D_Alm1 = reader.GetInt32(7);

                    g.H_A_Cen1 = reader.GetTimeSpan(8);
                    g.N_A_Cen1 = reader.GetInt32(9);
                    g.H_D_Cen1 = reader.GetTimeSpan(10);
                    g.N_D_Cen1 = reader.GetInt32(11);

                    g.H_A_Dor1 = reader.GetTimeSpan(12);
                    g.N_A_Dor1 = reader.GetInt32(13);
                    g.H_D_Dor1 = reader.GetTimeSpan(14);
                    g.N_D_Dor1 = reader.GetInt32(15); //

                    g.Fecha = reader.GetDateTime(16);
                    g.Longitud = reader.GetDouble(17);
                    g.Latitud = reader.GetDouble(18);
                    g.Obs = reader.GetString(19);
                }
                con.Close();
            }

            catch (Exception e)
            {
                con.Close();
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                System.Console.WriteLine(e);
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                Console.WriteLine("error");
                Android.Widget.Toast.MakeText(Application.Context, "error glucosa buscar".ToString(), Android.Widget.ToastLength.Short).Show();
            }


            return g;

        }


        public async Task<bool> Modificar (Glucosa g)
        {
            bool flag = false;
            

            try
            {
               con.Open();
                string Sslq = "update Glucosa  Set  " +
                   "H_A_Des=@H_A_Des, N_A_Des=@N_A_Des, H_D_Des=@H_D_Des, N_D_Des=@N_D_Des, " + //desayuno
                   "H_A_Alm=@H_A_Alm, N_A_Alm=@N_A_Alm, H_D_Alm=@H_D_Alm, N_D_Alm=@N_D_Alm, " + //almuerzo
                   "H_A_Cen=@H_A_Cen, N_A_Cen=@N_A_Cen, H_D_Cen=@H_D_Cen, N_D_Cen=@N_D_Cen, " + //cena
                   "H_A_Dor=@H_A_Dor, N_A_Dor=@N_A_Dor, H_D_Dor=@H_D_Dor, N_D_Dor=@N_D_Dor, " + //dormir 
                   "Fecha=@Fecha, Obs=@Obs where IDGlucosa = @idG; ";
                SqlCommand sql = new SqlCommand(Sslq, con.sqlConnection());
                sql.Parameters.AddWithValue("idG", g.IdGlucosa);
                // desayuno 
                sql.Parameters.AddWithValue("H_A_Des", g.H_A_Des1);
                sql.Parameters.AddWithValue("N_A_Des", g.N_A_Des1);
                sql.Parameters.AddWithValue("H_D_Des", g.H_D_Des1);
                sql.Parameters.AddWithValue("N_D_Des", g.N_D_Des1);
                // almuerzo
                sql.Parameters.AddWithValue("H_A_Alm", g.H_A_Alm1);
                sql.Parameters.AddWithValue("N_A_Alm", g.N_A_Alm1);
                sql.Parameters.AddWithValue("H_D_Alm", g.H_D_Alm1);
                sql.Parameters.AddWithValue("N_D_Alm", g.N_D_Alm1);
                // cena 
                sql.Parameters.AddWithValue("H_A_Cen", g.H_A_Cen1);
                sql.Parameters.AddWithValue("N_A_Cen", g.N_A_Cen1);
                sql.Parameters.AddWithValue("H_D_Cen", g.H_D_Cen1);
                sql.Parameters.AddWithValue("N_D_Cen", g.N_D_Cen1);
                //dormir
                sql.Parameters.AddWithValue("H_A_Dor", g.H_A_Dor1);
                sql.Parameters.AddWithValue("N_A_Dor", g.N_A_Dor1);
                sql.Parameters.AddWithValue("H_D_Dor", g.H_D_Dor1);
                sql.Parameters.AddWithValue("N_D_Dor", g.N_D_Dor1);
                // otros datos
                sql.Parameters.AddWithValue("Fecha", g.Fecha);
                sql.Parameters.AddWithValue("Obs", g.Obs);



                ///sql.ExecuteNonQuery();
                ///

                await ExecuteCommandAsync(sql);
                
                    flag = true;
                

                con.Close();

            }
            catch (Exception e)
            {
                con.Close();
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                System.Console.WriteLine(e);
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                Console.WriteLine("error");
                Android.Widget.Toast.MakeText(Application.Context, "error glucosa".ToString(), Android.Widget.ToastLength.Short).Show();
                flag = false;
            }

            return flag;
        }

        public bool Eliminar(int idG)
        {
            bool flag = false;

            try
            {
                con.Open();
                String Ssql1 = "delete from Insulina where FK_idglu =@FK_idglu ";             
                SqlCommand sql1 = new SqlCommand(Ssql1, con.sqlConnection());
                sql1.Parameters.AddWithValue("FK_idglu", idG);
                sql1.ExecuteNonQuery();

                String Ssql2 = "delete from Glucosa where IDGlucosa = @FK_idglu";
                SqlCommand sql2 = new SqlCommand(Ssql2, con.sqlConnection());
                sql2.Parameters.AddWithValue("FK_idglu", idG);
                sql2.ExecuteNonQuery();
                con.Close();



            }
            catch (Exception e)
            {

                con.Close();
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                System.Console.WriteLine("exor eliminar glucosa");
                System.Console.WriteLine(e);
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                Console.WriteLine("error");
                Android.Widget.Toast.MakeText(Application.Context, "error glucosa eliminar".ToString(), Android.Widget.ToastLength.Short).Show();
            }


            return flag;
        }


        public List<Glucosa> ListaGlucosa_fecha(int idUs, String fecha1 , String fecha2)
        {
            List<Glucosa> lista = new List<Glucosa>();

            try
            {
                con.Open();
                string query = "select " +
                    "H_A_Des, N_A_Des, H_D_Des, N_D_Des, " + //3
                    "H_A_Alm, N_A_Alm, H_D_Alm, N_D_Alm, " + //7
                    "H_A_Cen, N_A_Cen, H_D_Cen, N_D_Cen, " + //11
                    "H_A_Dor, N_A_Dor, H_D_Dor, N_D_Dor, 	" + //15
                    "FK_idUs, Fecha, Longitud, Latitud, Obs, IDGlucosa	" + //21
                    "from Glucosa where FK_idUs = "+ idUs + " and Fecha BETWEEN  '"+fecha1+"' AND '"+fecha2+"';";
                SqlCommand sql = new SqlCommand(query, con.sqlConnection());
               // sql.Parameters.AddWithValue("idus", idUs);


              //  sql.Parameters.AddWithValue("fecha1", fecha1);
              //  sql.Parameters.AddWithValue("fecha2", fecha2);
                System.Console.WriteLine(sql.ToString());
                System.Console.WriteLine(sql.CommandText);
                SqlDataReader reader = sql.ExecuteReader();

                while (reader.Read())
                {
                    Glucosa g = new Glucosa();
                    g.H_A_Des1 = reader.GetTimeSpan(0);
                    g.N_A_Des1 = reader.GetInt32(1);
                    g.H_D_Des1 = reader.GetTimeSpan(2);
                    g.N_D_Des1 = reader.GetInt32(3);

                    g.H_A_Alm1 = reader.GetTimeSpan(4);
                    g.N_A_Alm1 = reader.GetInt32(5);
                    g.H_D_Alm1 = reader.GetTimeSpan(6);
                    g.N_D_Alm1 = reader.GetInt32(7);

                    g.H_A_Cen1 = reader.GetTimeSpan(8);
                    g.N_A_Cen1 = reader.GetInt32(9);
                    g.H_D_Cen1 = reader.GetTimeSpan(10);
                    g.N_D_Cen1 = reader.GetInt32(11);

                    g.H_A_Dor1 = reader.GetTimeSpan(12);
                    g.N_A_Dor1 = reader.GetInt32(13);
                    g.H_D_Dor1 = reader.GetTimeSpan(14);
                    g.N_D_Dor1 = reader.GetInt32(15);

                    g.Fk_id_US = reader.GetInt32(16);
                    g.Fecha = reader.GetDateTime(17);
                    g.Longitud = reader.GetDouble(18);
                    g.Latitud = reader.GetDouble(19);
                    g.Obs = reader.GetString(20);
                    g.IdGlucosa = reader.GetInt32(21);
                    lista.Add(g);
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
                Android.Widget.Toast.MakeText(Application.Context, "error glucosa filtra".ToString(), Android.Widget.ToastLength.Short).Show();
                con.Close();
            }
            return lista;
        }

        public int Promedio15Dias(int idus)
        {
            int promedio15dias = 0;
            try
            {
                con.Open();
                string Squery = "select  top 1 ( N_A_Des + N_D_Des +N_A_Alm + N_D_Alm + N_A_Cen + N_D_Cen + N_A_Dor + N_D_Dor )/8 from glucosa where  Fecha BETWEEN (CURRENT_TIMESTAMP -15) and (CURRENT_TIMESTAMP) and FK_idUs = @idus ;";
                SqlCommand query = new SqlCommand(Squery, con.sqlConnection());
                query.Parameters.AddWithValue("idus", idus);

                SqlDataReader reader = query.ExecuteReader();
                while (reader.Read())
                {
                    promedio15dias = reader.GetInt32(0);
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
                Android.Widget.Toast.MakeText(Application.Context, "error glucosa 15 dias".ToString(), Android.Widget.ToastLength.Short).Show();
                con.Close();
            }

            return promedio15dias;
        }

        public List<Prom_dia> Prom (int idus, int dias)
        {
            List<Prom_dia> lista = new List<Prom_dia>();
            
            try
            {
                DateTime fecha = new DateTime();
                fecha = DateTime.Now.Date;
              
                
                for (int i = 0; i < dias; i++)
                {
                    bool flag = true;
                    con.Open();
                    String aux = fecha.AddDays(-i).ToShortDateString();
                    string Squery = "select  top 1 ( N_A_Des + N_D_Des +N_A_Alm + N_D_Alm + N_A_Cen + N_D_Cen + N_A_Dor + N_D_Dor )/8  " +
                        "from glucosa where Fecha = @fecha and FK_idUs = @idus";
                    SqlCommand query = new SqlCommand(Squery, con.sqlConnection());
                    query.Parameters.AddWithValue("idus", idus);
                    query.Parameters.AddWithValue("fecha",aux);
                    SqlDataReader reader = query.ExecuteReader();
                    while (reader.Read())
                    {
                        Prom_dia pm = new Prom_dia();
                        pm.Promedio = reader.GetInt32(0);
                        pm.Fecha = fecha.AddDays(-i);
                        lista.Add(pm);
                        flag = false;
                    }
                    /*if (flag)
                    {
                        pm.Promedio = 0;
                        pm.Fecha = DateTime.MinValue;
                        lista.Add(pm);
                    }*/

                    con.Close();

                }

                
            }
            catch (Exception e)
            {


                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                System.Console.WriteLine(e);
                System.Console.WriteLine("ERROR");
                System.Console.WriteLine("exor");
                Console.WriteLine("error");
                Android.Widget.Toast.MakeText(Application.Context, "error glucosa 7 dias".ToString(), Android.Widget.ToastLength.Short).Show();
                con.Close();
            }

            return lista;
        }

        






        public int Promedio30Dias(int idus)
        {
            int promedio30dias = 0;
            try
            {
               
                con.Open();
                string Squery = "select  top 1 ( N_A_Des + N_D_Des +N_A_Alm + N_D_Alm + N_A_Cen + N_D_Cen + N_A_Dor + N_D_Dor )/8 from glucosa where  Fecha BETWEEN (CURRENT_TIMESTAMP -30) and (CURRENT_TIMESTAMP) and FK_idUs = @idus ;";
                SqlCommand query = new SqlCommand(Squery, con.sqlConnection());
                query.Parameters.AddWithValue("idus", idus);

                SqlDataReader reader = query.ExecuteReader();
                while (reader.Read())
                {
                    promedio30dias = reader.GetInt32(0);
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
                Android.Widget.Toast.MakeText(Application.Context, "error glucosa 30 dias".ToString(), Android.Widget.ToastLength.Short).Show();
                con.Close();
            }



            return promedio30dias;
        }

    }
}
