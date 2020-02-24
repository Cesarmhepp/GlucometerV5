using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Biblioteca.BD;
using Org.Json;
using Xamarin.Facebook;
using Xamarin.Facebook.AppEvents;
using Xamarin.Facebook.Login.Widget;
using static Android.Provider.ContactsContract;
using AlertDialog = Android.App.AlertDialog;
using Profile = Xamarin.Facebook.Profile;

namespace Glucometer
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Login : AppCompatActivity, IFacebookCallback
    {
        EditText correoLogin;
        EditText passLogin;
        LoginButton BtnFBLogin;
        private MyProfileTracker mprofileTracker;
        private ICallbackManager mFBCallManager;
        private JSONObject json_aux;
        Biblioteca.BD.Login bd;
        ImageView img;

       

        protected override void OnCreate(Bundle savedInstanceState)

        {
            
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.Login);
            BtnFBLogin = FindViewById<LoginButton>(Resource.Id.fb_btn);
            var btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            var btnRegistrar = FindViewById<Button>(Resource.Id.btnRegistrarInicio);
            correoLogin = FindViewById<EditText>(Resource.Id.editCorreo);
            passLogin = FindViewById<EditText>(Resource.Id.editContra);
            btnLogin.Click += BtnLogin_Click;
            BtnFBLogin = FindViewById<LoginButton>(Resource.Id.fb_btn);
            btnRegistrar.Click += BtnRegistrar_Click;
            bd = new Biblioteca.BD.Login();

            img = FindViewById<ImageView>(Resource.Id.imageView1);


            img.SetImageResource(Resource.Drawable.icon);

        }

        private void BtnLogin_Click(object sender, EventArgs e)//login normal
        {
            try
            {
                string correo = correoLogin.Text;
                string pass = passLogin.Text;
                int aux = 0;
                aux= bd.LoginNormal(correo, pass).Idusuario;

                if (aux != 0)
                {
                    

                    Intent i = new Intent(this, typeof(MainActivity));
                    i.PutExtra("idUs",Convert.ToString(aux));
                    StartActivity(i);
                }
                else
                {
                    AlertDialog alerta = new AlertDialog.Builder(this).Create();
                    alerta.SetTitle("Error ");
                    alerta.SetMessage("El usuario o la contraseña son incorrectos. Compruebe su conexion a internet");
                    alerta.SetButton("Aceptar", (a, b) => { });
                    alerta.Show();
                }
            }
            catch (Exception ex)
            {
                AlertDialog alerta = new AlertDialog.Builder(this).Create();
                alerta.SetTitle("Error ");
                alerta.SetMessage(ex.Message);
                alerta.SetButton("Aceptar", (a, b) => { });
                alerta.Show();
            }
        }
        public void OnCancel() { }
        public void OnError(FacebookException p0) { }
        public void OnSuccess(Java.Lang.Object p0)
        {


        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            try
            {
                FacebookSdk.SdkInitialize(this);
                // FacebookSdk.ApplicationId = "444917649791842"; //pune el id de la app de facebook
                AppEventsLogger.ActivateApp(Application); //no se que hace esta linea 
                mprofileTracker = new MyProfileTracker();
                mprofileTracker.mOnProfileChanged += MprofileTracker_mOnProfileChanged;
                mprofileTracker.StartTracking();
                // Set our view from the "main" layout resource  
                // SetContentView(Resource.Layout.configCuenta);
                //BtnFBLogin = FindViewById<LoginButton>(Resource.Id.fb_btn);
                BtnFBLogin.SetReadPermissions(new List<string> {
                "public_profile","user_friends","email","user_birthday"
            });
                mFBCallManager = CallbackManagerFactory.Create();
                BtnFBLogin.RegisterCallback(mFBCallManager, this);
                //base.OnActivityResult(requestCode, resultCode, data);
                mFBCallManager.OnActivityResult(requestCode, (int)resultCode, data);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e); Console.WriteLine(e);
                Console.WriteLine(e); Console.WriteLine(e);
                Console.WriteLine(e); Console.WriteLine(e);
                Console.WriteLine(e); Console.WriteLine(e);
                Console.WriteLine(e);
                Console.WriteLine(e);

            }

     

        }

        private void MprofileTracker_mOnProfileChanged(object sender, OnProfileChangedEventArgs e)
        {

            if (e.mProfile != null)
            {
                try
                {
                    Profile p = e.mProfile;
                    int idUS = bd.loginfacebook(p.Id);
                    if (idUS != 0)//consulta para iniciar sesion 
                    {
                        Intent i = new Intent(this, typeof(Inicio));
                        string aux = Convert.ToString(idUS);
                        i.PutExtra("idUs", aux);
                        StartActivity(i);
                    }
                    else
                    {
                        AlertDialog alerta = new AlertDialog.Builder(this).Create();
                        alerta.SetTitle("Error ");
                        alerta.SetMessage("FB no esta enlazado a ninguna cuenta de usuario");
                        alerta.SetButton("Aceptar", (a, b) => { });
                        alerta.Show();

                    }

                    // tengo que crear el layout del  config y pasar los datos a una sesion para almacenarlos entre pestañas
                }
                catch (Java.Lang.Exception ex) { }
            }
            else { }


        }


        private void BtnRegistrar_Click(object sender, EventArgs e)
        {

            Intent i = new Intent(this, typeof(registrarUs));
            StartActivity(i);

        }

        public override void OnBackPressed()
        {

            FinishAffinity();

        }

    }
}