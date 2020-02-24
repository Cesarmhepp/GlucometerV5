using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Biblioteca.BD;
using Microcharts.Droid;
using Microcharts;
using SkiaSharp;
using Biblioteca.Modelo;
using System.Drawing;

namespace Glucometer
{
    public class Reportes : Android.Support.V4.App.Fragment
    {
        string SidUs;
        int idUS;
        CrudGlucosa bd = new CrudGlucosa();
        string[] str1;
        List<Prom_dia> dia;
        List<Microcharts.Entry> entries;
        ChartView chartView;
        ChartView chartViewC;
        ChartView chartViewP;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.Reporte, container, false);// asi se define que layout ocupa
            var btn3 = view.FindViewById<Button>(Resource.Id.btn3);
            var btn7 = view.FindViewById<Button>(Resource.Id.btn7);
            var btn15 = view.FindViewById<Button>(Resource.Id.btn15);
            var btn30 = view.FindViewById<Button>(Resource.Id.btn30);
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

            chartView = view.FindViewById<ChartView>(Resource.Id.chartView);
            chartViewC = view.FindViewById<ChartView>(Resource.Id.chartView2);
            chartViewP = view.FindViewById<ChartView>(Resource.Id.chartView3);

            btn3.Click += Btn3_Click;
            btn7.Click += Btn7_Click;
            btn15.Click += Btn15_Click;
            btn30.Click += Btn30_Click;
            Grafico(7);



            return view;
        }

        private void Btn30_Click(object sender, EventArgs e)
        {
            Grafico(30);
        }

        private void Btn15_Click(object sender, EventArgs e)
        {
            Grafico(15);
        }

        private void Btn7_Click(object sender, EventArgs e)
        {
            Grafico(7);
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            Grafico(3);
        }

        public void Grafico(int dias)
        {
            try
            {
                dia = bd.Prom(idUS, dias);


                entries = new List<Microcharts.Entry> { };



                for (int i = dia.Count - 1; i >= 0; i--)
                {
                    var random = new Random();
                    var color = String.Format("#{0:X6}", random.Next(0x1000000));
                    var x = new Microcharts.Entry(dia[i].Promedio)
                    {


                        Label = dia[i].Fecha.ToString("dd/MM"),
                        ValueLabel = dia[i].Promedio.ToString(),
                        Color = SKColor.Parse(color)
                    };
                    entries.Add(x);

                }



                chartView.Chart = new Microcharts.BarChart { Entries = entries };
                chartView.Chart.LabelTextSize = 30;

                chartViewC.Chart = new Microcharts.LineChart() { Entries = entries };
                chartViewC.Chart.LabelTextSize = 30;

                chartViewP.Chart = new Microcharts.PointChart() { Entries = entries};
                
            }
            catch (Exception ex)
            {
                AlertDialog alerta = new AlertDialog.Builder(Context).Create();
                alerta.SetTitle("Error ");
                alerta.SetMessage(ex.Message + "'asdasd'");
                alerta.SetButton("Aceptar", (a, b) => { });
                alerta.Show();


            }
        }
    }
}