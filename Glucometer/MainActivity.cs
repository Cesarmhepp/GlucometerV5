using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Util.Zip;
using System;
using AlertDialog = Android.App.AlertDialog;

namespace Glucometer
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        FrameLayout frame;
        int idUS;
        string SidUs;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            try
            {
                SidUs = (Intent.GetStringExtra("idUs"));
                idUS = Convert.ToInt32(SidUs);
            }
            catch (Exception ex)
            {
                AlertDialog alerta = new AlertDialog.Builder(this).Create();
                alerta.SetTitle("Error ");
                alerta.SetMessage(ex.Message + "'asdasd'");
                alerta.SetButton("Aceptar", (a, b) => { });
                alerta.Show();

            }



            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            frame = FindViewById<FrameLayout>(Resource.Id.frameLayout);
            this.SetTitle(Resource.String.app_name);//nombre por defecto

            /// tengo que pasar el id

            Bundle bundle = new Bundle();
            bundle.PutString("idUs", SidUs);
            Inicio fragInfo = new Inicio();
            fragInfo.Arguments=bundle;
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Replace(Resource.Id.frameLayout, fragInfo);
            trans.Commit();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            var trans = SupportFragmentManager.BeginTransaction();
            Bundle bundle = new Bundle();
            bundle.PutString("idUs", SidUs);
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    Inicio inicio = new Inicio();
                    inicio.Arguments = bundle;
                    trans.Replace(Resource.Id.frameLayout, inicio);
                    trans.Commit();
                    this.SetTitle(Resource.String.app_name);//nombre por defecto 
                    
                    return true;
                case Resource.Id.navigation_report:
                    Reportes reportes = new Reportes();
                    reportes.Arguments = bundle;
                    trans.Replace(Resource.Id.frameLayout, reportes);
                    trans.Commit();
                    this.SetTitle(Resource.String.app_name_repor);
                    return true;
                case Resource.Id.navigation_config:
                    MenuConfig config = new MenuConfig();
                    config.Arguments = bundle;
                    trans.Replace(Resource.Id.frameLayout, config);
                    trans.Commit();
                    this.SetTitle(Resource.String.app_name_config);
                    return true;
            }
            return false;
        }





    }
}

