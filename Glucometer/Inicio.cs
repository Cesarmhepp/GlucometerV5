using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Biblioteca.BD;
using Biblioteca.Modelo;
using DocumentFormat.OpenXml.Wordprocessing;
using static Android.Widget.AdapterView;
using View = Android.Views.View;

namespace Glucometer
{
    public class Inicio : Android.Support.V4.App.Fragment 
    {
        CrudGlucosa bd;
        public ListView lista;
        private List<Glucosa> datos;
        EditText fecha1, fecha2;
        DateTime fecha_axu1, fecha_axu2;
        string SidUs;
        int idUS;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            bd = new CrudGlucosa();
            fecha_axu1 = new DateTime();
            fecha_axu2 = new DateTime();
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.Inicio2, container, false);// asi se define que layout ocupa

            try
            {
                SidUs = Arguments.GetString("idUs");
                idUS = Convert.ToInt32(SidUs);
            }
            catch (Exception ex)
            {
                AlertDialog alerta = new AlertDialog.Builder(Context).Create();
                alerta.SetTitle("Error ");
                alerta.SetMessage(ex.Message + "'asdasd'");
                alerta.SetButton("Aceptar", (a, b) => { });
                alerta.Show();

            }

            FloatingActionButton fab = (FloatingActionButton)view.FindViewById(Resource.Id.fab);// asi solo se declaran los botones flantes
            fab.Click += Fab_Click;
            var btn_buscar = view.FindViewById<Button>(Resource.Id.button1);// declaracion de un boton normal "view....."
            var btn_listar = view.FindViewById<Button>(Resource.Id.button2);
            fecha1 = view.FindViewById<EditText>(Resource.Id.txtFecha1);
            fecha2 = view.FindViewById<EditText>(Resource.Id.txtFecha2);
            lista = view.FindViewById<ListView>(Resource.Id.lista);
            Listar();
            btn_buscar.Click += Btn_buscar_Click;
            fecha1.Click += Fecha1_Click;
            fecha2.Click += Fecha2_Click;
            btn_listar.Click += Btn_listar_Click;


            return view;
        }

        private void Btn_listar_Click(object sender, EventArgs e)
        {
            Listar();
            fecha1.Text = "";
            fecha2.Text = "";
        }

        public void Listar()
        {
            datos = bd.ListaGlucosa(idUS);// se tiene que re mplazar por el id us esto es a modo de prueba
            GlAdapter adapter = new GlAdapter(Context, datos);

            lista.Adapter = adapter;
        }



        private void Fecha2_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            DatePickerDialog dialog = new DatePickerDialog(Context, OnDateSet2, today.Year, today.Month - 1, today.Day);
            dialog.DatePicker.MinDate = today.Millisecond;
            dialog.Show();
        }

        private void Fecha1_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            DatePickerDialog dialog = new DatePickerDialog(Context, OnDateSet1, today.Year, today.Month - 1, today.Day);
            dialog.DatePicker.MinDate = today.Millisecond;
            dialog.Show();

        }

        void OnDateSet1(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            fecha1.Text = e.Date.ToShortDateString();
            fecha_axu1 = e.Date;
        }

        void OnDateSet2(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            fecha2.Text = e.Date.ToShortDateString();
            fecha_axu2 = e.Date;
        }




        private void Btn_buscar_Click(object sender, EventArgs e)
        {
            datos = bd.ListaGlucosa_fecha(idUS,fecha1.Text, fecha2.Text);// se tiene que re mplazar por el id us esto es a modo de prueba
            GlAdapter adapter = new GlAdapter(Context, datos);
            lista.Adapter = adapter;
            Toast.MakeText(Application.Context, "lista", ToastLength.Short).Show();
        }

        private void Fab_Click(object sender, EventArgs e)//boton para agregar
        {
            Intent i = new Intent(Context,typeof(Agregar));
            i.PutExtra("idUs", SidUs);
            StartActivity(i);

        }


       
    }
}