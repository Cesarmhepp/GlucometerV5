using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Biblioteca.BD
{
    public class Conexion
    {
        static string cadenaConex = @"data source=192.168.137.1;initial catalog=DB_GM2;user id=sa ; password=hola123;Connect Timeout=10";
        SqlConnection con;

        public Conexion()
        {
            con = new SqlConnection(cadenaConex); 
        }

        public void Open()
        {
            con.Open();
        }
        public void Close()
        {
            con.Close();
        }

        public SqlConnection sqlConnection()
        {
            return con;
        }


    }

    

}
