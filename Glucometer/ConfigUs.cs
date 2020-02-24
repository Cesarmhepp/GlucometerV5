using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Biblioteca.BD;
using Biblioteca.Modelo;
using Android.Support.V7.App;
using Org.Json;
using Xamarin.Facebook;
using Xamarin.Facebook.AppEvents;
using Xamarin.Facebook.Login.Widget;
using static Android.Provider.ContactsContract;
using AlertDialog = Android.App.AlertDialog;
using Profile = Xamarin.Facebook.Profile;

namespace Glucometer
{
    [Activity(Label = "ConfigUs")]
    public class ConfigUs :  AppCompatActivity, IFacebookCallback
    {

        Button txtFecNac;
        EditText txtNombre;
        EditText txtApellido;
        EditText txtRut;


        EditText txtContraseña;
        EditText txtConfirmContra;
        EditText txtCorreo;
        EditText txtConfirmCorreo;

        Spinner spinnerTipoPrevision;
        Spinner spinnerPrevision;
        Spinner spinnerTipoSangre;
        Spinner spinnerFactorRh;
        Spinner spinnerRegion;
        Spinner spinnerComuna;
        Spinner spinnerCiudad;
        Spinner spinnerTipoDiabetes;

        string idFacebook;
        int idComunaActivity;
        int idPrevision;
        int idTipoSangre;
        int idtipoDiabetes;
        int idFactorRh;
        string txtTipoPrev;
        CrudUsuario crus = new CrudUsuario();
        Usuario us;
        string SiduS;
        int idUS;
        LoginButton BtnFBLogin;
        private MyProfileTracker mprofileTracker;
        private ICallbackManager mFBCallManager;
        private JSONObject json_aux;
        bool auxF = true;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.configCuenta);
            BtnFBLogin = FindViewById<LoginButton>(Resource.Id.fb_btn2);

            try
            {
                SiduS = (Intent.GetStringExtra("idUs"));
                idUS = Convert.ToInt32(SiduS);
            }
            catch (Exception ex)
            {
                AlertDialog alerta = new AlertDialog.Builder(this).Create();
                alerta.SetTitle("Error ");
                alerta.SetMessage(ex.Message + "'asdasd'");
                alerta.SetButton("Aceptar", (a, b) => { });
                alerta.Show();

            }

            us = new Usuario();
            us = crus.mostrarDatosUsuario(idUS);


            var btnModificar = FindViewById<Button>(Resource.Id.btnModificar);
            txtNombre = FindViewById<EditText>(Resource.Id.txtNombre);
            txtApellido = FindViewById<EditText>(Resource.Id.txtApellido);
            txtRut = FindViewById<EditText>(Resource.Id.txtRut);


            txtContraseña = FindViewById<EditText>(Resource.Id.txtPass);
            txtConfirmContra = FindViewById<EditText>(Resource.Id.txtConfirmPass);
            txtCorreo = FindViewById<EditText>(Resource.Id.txtCorreo);
            txtConfirmCorreo = FindViewById<EditText>(Resource.Id.txtconfirmCorreo);
            txtFecNac = FindViewById<Button>(Resource.Id.btnFecNac);

            spinnerTipoPrevision = FindViewById<Spinner>(Resource.Id.SpinnerTipoPre);
            spinnerPrevision = FindViewById<Spinner>(Resource.Id.SpinnerPre);
            spinnerTipoSangre = FindViewById<Spinner>(Resource.Id.SpinnerTipoSangre);
            spinnerFactorRh = FindViewById<Spinner>(Resource.Id.SpinnerFactorRh);
            spinnerRegion = FindViewById<Spinner>(Resource.Id.SpinnerRegion);
            spinnerCiudad = FindViewById<Spinner>(Resource.Id.SpinnerCiudad);
            spinnerComuna = FindViewById<Spinner>(Resource.Id.SpinnerComuna);
            spinnerTipoDiabetes = FindViewById<Spinner>(Resource.Id.SpinnerTipoDiabetes);


            //carga de datos
            txtNombre.Text = us.Nombre;
            txtApellido.Text = us.Apellido;
            txtRut.Text = us.Rut;
            txtCorreo.Text = us.Uss;
            txtConfirmCorreo.Text = us.Uss;
            txtFecNac.Text = us.FNac.ToShortDateString();



            //Spinners relacionados con isapres y fonasa

            spinnerTipoPrevision.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelectedPrev);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.Dropdown_arraysTipoPrev, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerTipoPrevision.Adapter = adapter;
            spinnerTipoPrevision.SetSelection(us.Fk_tipoPrevision - 1);


            //Spinner relacionados con tipo sangre
            spinnerTipoSangre.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelectedTipoSangre);
            var adapter2 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.Dropdown_arraysTiposangre, Android.Resource.Layout.SimpleSpinnerItem);
            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerTipoSangre.Adapter = adapter2;
            spinnerTipoSangre.SetSelection(us.Fk_TipoSangre - 1);

            //
            //Spinner Relacionado con factor Rh
            spinnerFactorRh.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelectedFactorRh);
            var adapter3 = ArrayAdapter.CreateFromResource(
            this, Resource.Array.Dropdown_arraysfactorRh, Android.Resource.Layout.SimpleSpinnerItem);
            adapter3.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerFactorRh.Adapter = adapter3;
            spinnerFactorRh.SetSelection(us.Fk_FactorRh - 1);

            //Spinner Region
            try
            {
                spinnerRegion.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelectedRegion);
                var adapterRegion = ArrayAdapter.CreateFromResource(
                this, Resource.Array.Dropdown_arraysRegion, Android.Resource.Layout.SimpleSpinnerItem);
                adapterRegion.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerRegion.Adapter = adapterRegion;
                spinnerRegion.SetSelection(us.Comuna.IdRegion - 1);

            }
            catch (Exception)
            {

                throw;
            }



            //Spinner Tipo Diabetes
            spinnerTipoDiabetes.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelectedTipoDiabetes);
            var adapterDiabetes = ArrayAdapter.CreateFromResource(
            this, Resource.Array.Dropdown_arraystipoDiabetes, Android.Resource.Layout.SimpleSpinnerItem);
            adapterDiabetes.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerTipoDiabetes.Adapter = adapterDiabetes;
            spinnerTipoDiabetes.SetSelection(us.Fk_Eferglucosa - 1);



            //Spinner Comuna
            // spinnerComuna.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelectedComuna);
            // var adapterComuna = ArrayAdapter.CreateFromResource(
            // this, Resource.Array.Dropdown_arraysComunasSantiago, Android.Resource.Layout.SimpleSpinnerItem);
            // adapterComuna.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            //spinnerComuna.Adapter = adapterComuna;

           
            btnModificar.Click += BtnModificar_Click;

            //llamada a botones
            txtFecNac.Click += Fecha_Click;

            try
            {
                //carga de datos
                txtNombre.Text = us.Nombre;
                txtApellido.Text = us.Apellido;
                txtRut.Text = us.Rut;
                //spinnerRegion.SetSelection(us.Comuna.IdRegion);
                // spinnerCiudad.SetSelection(us.Comuna.IdCiudad);
                // spinnerComuna.SetSelection(us.Comuna.IdComuna);
            }
            catch (Exception ex)
            {
                string toast = string.Format("Error: {0}", ex.Message);
                Toast.MakeText(this, toast, ToastLength.Long).Show(); ;
            }
        }

        private void spinner_ItemSelectedRegion(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinnerRegion = (Spinner)sender;
            string toast = string.Format("{0}", spinnerRegion.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();

            if (spinnerRegion.GetItemAtPosition(e.Position).ToString().Equals("Región Metropolitana de Santiago"))
            {
                spinnerCiudad.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelectedCiudad);
                var adapterCiudad = ArrayAdapter.CreateFromResource(
                this, Resource.Array.Dropdown_arraysCiudadesMetropo, Android.Resource.Layout.SimpleSpinnerItem);
                adapterCiudad.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerCiudad.Adapter = adapterCiudad;
                spinnerCiudad.SetSelection(us.Comuna.IdCiudad - 1);
            }
        }

        private void spinner_ItemSelectedCiudad(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();

            if (spinnerCiudad.GetItemAtPosition(e.Position).ToString().Equals("Santiago"))
            {
                spinnerComuna.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelectedComuna);
                var adapterComuna = ArrayAdapter.CreateFromResource(
                this, Resource.Array.Dropdown_arraysComunasSantiago, Android.Resource.Layout.SimpleSpinnerItem);
                adapterComuna.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerComuna.Adapter = adapterComuna;
                spinnerComuna.SetSelection(us.Comuna.IdComuna - 1);
            }
        }

        private void spinner_ItemSelectedPrev(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();

            txtTipoPrev = spinner.GetItemAtPosition(e.Position).ToString();

            if (txtTipoPrev.Equals("Isapre"))
            {
                spinnerPrevision.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelectedPrevisionIsapre);

                var adapter1 = ArrayAdapter.CreateFromResource(
                this, Resource.Array.Dropdown_arraysPrevIsapre, Android.Resource.Layout.SimpleSpinnerItem);
                adapter1.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerPrevision.Adapter = adapter1;
                if (us.Fk_idPrevision >= 1 && us.Fk_idPrevision <= 12)
                {
                    spinnerPrevision.SetSelection(us.Fk_idPrevision - 1);

                }



            }
            else if (txtTipoPrev.Equals("Fonasa"))
            {
                spinnerPrevision.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelectedPrevisionFonasa);

                var adapter1 = ArrayAdapter.CreateFromResource(
                this, Resource.Array.Dropdown_arraysPrevFonasa, Android.Resource.Layout.SimpleSpinnerItem);
                adapter1.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerPrevision.Adapter = adapter1;
                if (us.Fk_idPrevision >= 13 && us.Fk_idPrevision <= 16)
                {
                    spinnerPrevision.SetSelection(us.Fk_idPrevision - 13);

                }

            }
        }



        private void spinner_ItemSelectedTipoSangre(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("La id de TipoSangre seleccionada es:{0}", spinner.GetItemIdAtPosition(e.Position + 1));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
            idTipoSangre = int.Parse(spinner.GetItemIdAtPosition(e.Position + 1).ToString());
        }
        private void spinner_ItemSelectedFactorRh(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("La id de FactorRh seleccionada es:{0}", spinner.GetItemIdAtPosition(e.Position + 1));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
            idFactorRh = int.Parse(spinner.GetItemIdAtPosition(e.Position + 1).ToString());
        }

        private void spinner_ItemSelectedComuna(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("La id de comuna seleccionada es:{0}", spinner.GetItemIdAtPosition(e.Position + 1));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
            idComunaActivity = int.Parse(spinner.GetItemIdAtPosition(e.Position + 1).ToString());
        }

        private void spinner_ItemSelectedPrevisionIsapre(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("La id de prevision seleccionada es:{0}", spinner.GetItemIdAtPosition(e.Position + 1));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
            idPrevision = int.Parse(spinner.GetItemIdAtPosition(e.Position + 1).ToString());
        }

        private void spinner_ItemSelectedPrevisionFonasa(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("La id de prevision seleccionada es:{0}", spinner.GetItemIdAtPosition(e.Position + 13));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
            idPrevision = int.Parse(spinner.GetItemIdAtPosition(e.Position + 13).ToString());
        }

        private void spinner_ItemSelectedTipoDiabetes(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("La id de Diabetes seleccionada es:{0}", spinner.GetItemIdAtPosition(e.Position + 1));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
            idtipoDiabetes = int.Parse(spinner.GetItemIdAtPosition(e.Position + 1).ToString());
        }


        public override void OnBackPressed()
        {
            Intent i = new Intent(this, typeof(MainActivity));
            i.PutExtra("idUs", SiduS);
            StartActivity(i);
            Finish();

        }


        private void BtnModificar_Click(object sender, EventArgs e)
        {

            try
            {
                Usuario us = new Usuario();

                //inicio validacion correo
                if (txtCorreo.Text == txtConfirmCorreo.Text)
                {
                    us.Uss = txtCorreo.Text;
                    //if validacion contraseña
                    if (txtContraseña.Text == txtConfirmContra.Text && txtContraseña.Text.Length > 1 && txtConfirmContra.Text.Length > 1)
                    {
                        us.Pass = txtContraseña.Text;

                        //if validacion fecha nac
                        if (txtFecNac.Text != null)
                        {
                            us.FNac = Convert.ToDateTime(txtFecNac.Text);

                            //ingreso restos de datos
                            //validacion rut
                            us.Rut = txtRut.Text;
                            //validacionfb
                            us.IdFacebook = idFacebook;
                            //validacion Comuna
                            Comuna c = new Comuna();
                            c.IdComuna = idComunaActivity;
                            us.Comuna = c;
                            //validacion tipo usuario normal
                            us.Fk_TipoU = 1;
                            //validacion tipo prevision.
                            us.Fk_idPrevision = idPrevision;
                            //validacion tipo de diabetes
                            us.Fk_Eferglucosa = idtipoDiabetes;
                            //fk tipo de sangre
                            us.Fk_TipoSangre = idTipoSangre;
                            //fk factor Rh
                            us.Fk_FactorRh = idFactorRh;
                            //nombre y apellido
                            us.Nombre = txtNombre.Text;
                            us.Apellido = txtApellido.Text;

                            CrudUsuario cu = new CrudUsuario();

                            if (cu.modificarUsuario(us, idUS))
                            {
                                AlertDialog alerta = new AlertDialog.Builder(this).Create();
                                alerta.SetTitle("Registro ");
                                alerta.SetMessage("Cuenta Modificada correctamente!");
                                alerta.SetButton("Aceptar", (a, b) =>
                                {

                                    Intent i = new Intent(this, typeof(Login));
                                    StartActivity(i);
                                });
                                alerta.Show();
                            }
                            else
                            {
                                AlertDialog alerta = new AlertDialog.Builder(this).Create();
                                alerta.SetTitle("Registro ");
                                alerta.SetMessage("A ocurrido un error al modificar");
                                alerta.SetButton("Aceptar", (a, b) =>
                                {

                                });
                                alerta.Show();
                            }


                        }
                        else
                        {
                            AlertDialog alerta = new AlertDialog.Builder(this).Create();
                            alerta.SetTitle("Registro ");
                            alerta.SetMessage("Debe ingresar alguna fecha de nacimiento");
                            alerta.SetButton("Aceptar", (a, b) =>
                            {

                            });
                            alerta.Show();


                        }
                    }
                    else if (txtContraseña.Text.Length == 0 && txtConfirmContra.Text.Length == 0)
                    {
                        us.Pass = "";
                        //if validacion fecha nac
                        if (txtFecNac.Text != null)
                        {
                            us.FNac = Convert.ToDateTime(txtFecNac.Text);

                            //ingreso restos de datos
                            //validacion rut
                            us.Rut = txtRut.Text;
                            //validacionfb
                            us.IdFacebook = idFacebook;
                            //validacion Comuna
                            Comuna c = new Comuna();
                            c.IdComuna = idComunaActivity;
                            us.Comuna = c;
                            //validacion tipo usuario normal
                            us.Fk_TipoU = 1;
                            //validacion tipo prevision.
                            us.Fk_idPrevision = idPrevision;
                            //validacion tipo de diabetes
                            us.Fk_Eferglucosa = idtipoDiabetes; //problemas aca
                            //fk tipo de sangre
                            us.Fk_TipoSangre = idTipoSangre;
                            //fk factor Rh
                            us.Fk_FactorRh = idFactorRh;
                            //nombre y apellido
                            us.Nombre = txtNombre.Text;
                            us.Apellido = txtApellido.Text;

                            CrudUsuario cu = new CrudUsuario();

                            if (cu.modificarUsuario(us, idUS))
                            {
                                AlertDialog alerta = new AlertDialog.Builder(this).Create();
                                alerta.SetTitle("Registro ");
                                alerta.SetMessage("Cuenta Modificada correctamente!");
                                alerta.SetButton("Aceptar", (a, b) =>
                                {

                                    Intent i = new Intent(this, typeof(MainActivity));
                                    i.PutExtra("idUs", SiduS);
                                    StartActivity(i);
                                    Finish();
                                });
                                alerta.Show();
                            }
                            else
                            {
                                AlertDialog alerta = new AlertDialog.Builder(this).Create();
                                alerta.SetTitle("Registro ");
                                alerta.SetMessage("A ocurrido un error al modificar");
                                alerta.SetButton("Aceptar", (a, b) =>
                                {

                                });
                                alerta.Show();
                            }
                        }
                        
                        
                    }
                    else
                    {
                        AlertDialog alerta = new AlertDialog.Builder(this).Create();
                        alerta.SetTitle("Registro ");
                        alerta.SetMessage("Los campos “Correo” y “Confirmar correo” deben ser iguales.");
                        alerta.SetButton("Aceptar", (a, b) =>
                        {

                        });
                        alerta.Show();
                    }

                }
                else
                {
                    AlertDialog alerta = new AlertDialog.Builder(this).Create();
                    alerta.SetTitle("Registro ");
                    alerta.SetMessage("Los campos “Correo” y “Confirmar correo” deben ser iguales.");
                    alerta.SetButton("Aceptar", (a, b) =>
                    {

                    });
                    alerta.Show();

                }
            }
            catch (Exception ex)
            {

                AlertDialog alerta = new AlertDialog.Builder(this).Create();
                alerta.SetTitle("A ocurrido un error al momento de crear la cuenta. ");
                alerta.SetMessage(ex.Message);
                alerta.SetButton("Aceptar", (a, b) => { });
                alerta.Show(); ;
            }




        }

        private void Fecha_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            DatePickerDialog dialog = new DatePickerDialog(this, OnDateSet1, today.Year, today.Month - 1, today.Day);
            dialog.DatePicker.MinDate = today.Millisecond;
            dialog.Show();
        }

        void OnDateSet1(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            txtFecNac.Text = e.Date.ToShortDateString();
        }

        public void OnCancel() { }
        public void OnError(FacebookException p0) { }
        public void OnSuccess(Java.Lang.Object p0)
        {


        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            FacebookSdk.SdkInitialize(this);
            // FacebookSdk.ApplicationId = "444917649791842"; //pune el id de la app de facebook
            AppEventsLogger.ActivateApp(Application); //no se que hace esta linea 
            

            mprofileTracker = new MyProfileTracker();
            mprofileTracker.mOnProfileChanged += MprofileTracker_mOnProfileChangedconfig;
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

        private void MprofileTracker_mOnProfileChangedconfig(object sender, OnProfileChangedEventArgs e)
        {
            
            if (e.mProfile != null)
            {
                try
                {
                    CrudUsuario bd = new CrudUsuario();
                    Profile p = e.mProfile;
                    bool encontrado = bd.BuscaridF(idUS, e.mProfile.Id);
                    if (auxF) // si ya paso una vez hay que reiniciar la actividad para que se pueda volver a asociar 
                    {
                        if (encontrado)//si encuentra el usuario y fb entra al metodo que elimina el id de fb del usuario
                        {
                            bd.Eliminarfb(idUS);
                        }
                        else
                        {
                            bd.Agregarfb(idUS, e.mProfile.Id);

                        }
                    }

                    // tengo que crear el layout del  config y pasar los datos a una sesion para almacenarlos entre pestañas
                }
                catch (Java.Lang.Exception ex) { }
            }
            else { }

            auxF = false;
        }



    }
}