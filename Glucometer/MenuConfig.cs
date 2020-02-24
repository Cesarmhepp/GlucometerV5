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

namespace Glucometer
{
    public class MenuConfig : Android.Support.V4.App.Fragment
    {
        string SidUs;
        int idUS;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
             
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.Config, container, false);// asi se define que layout ocupa

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



            var btnConfig = view.FindViewById<Button>(Resource.Id.btnConfigUsuario);
            var btncerrar = view.FindViewById<Button>(Resource.Id.btnCerrarSesion);
            btncerrar.Click += Btncerrar_Click;
            btnConfig.Click += BtnConfig_Click;
            return view;
        }

        private void Btncerrar_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(Context, typeof(Login));
            StartActivity(i);
            Activity.Finish();
        }

        private void BtnConfig_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(Context, typeof(ConfigUs));
            i.PutExtra("idUs", SidUs);
            StartActivity(i);
        }
    }
}