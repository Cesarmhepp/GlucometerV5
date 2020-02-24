using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Biblioteca.BD;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android.Text.Format;
using Biblioteca.Modelo;
using Xamarin.Essentials;
using System.Threading.Tasks;
using AlertDialog = Android.App.AlertDialog;

namespace Glucometer
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Agregar : AppCompatActivity
    {

        Button btn_agregar;
        Button btnfecha;
        Button btnDesayunoAntes, btnDesayunoDespues;
        EditText txt_des_antes, txt_des_Des;
        Button btnAlmuerzoAntes, btnAlmuerzoDespues;
        EditText txt_alm_antes, txt_alm_des;
        Button btnAntesCena, btnDespuesCena;
        EditText Txt_cen_Antes, Txt_cen_Despues;
        Button btnDormirAntes, btnDormirDespues;
        EditText Txt_Dor_Antes, Txt_Dor_Despues;
        Glucosa g;
        TimeSpan tiempos;
        Button btn_aux;
        EditText txt_obs;
        double latitud, longitud;
        string SidUs;
        int idUS;
        int id_glucosa;
        EditText mnna_rap, mnna_regul, mnna_mezcla, tarde_rap, tarde_regul, tarde_mezcla, noche_rap, noche_regul, noche_mezcla, dormir_rap, dormir_regul, dormir_mezcla;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.agregar);
            btn_agregar = FindViewById<Button>(Resource.Id.button1);
            btn_agregar.Click += Btn_agregar_ClickAsync;
            this.SetTitle(Resource.String.app_name_add);
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

            btnfecha = FindViewById<Button>(Resource.Id.fecha);
            btnfecha.Click += BtnFecha_Click;

            btnDesayunoAntes = FindViewById<Button>(Resource.Id.btnDesayunoAntes);
            btnDesayunoAntes.Click += BtnDesayunoAntes_Click;
            txt_des_antes = FindViewById<EditText>(Resource.Id.txt_des_antes);

            btnDesayunoDespues = FindViewById<Button>(Resource.Id.btnDesayunoDespues);
            btnDesayunoDespues.Click += BtnDesayunoDespues_Click;
            txt_des_Des = FindViewById<EditText>(Resource.Id.txt_des_Des);

            btnAlmuerzoAntes = FindViewById<Button>(Resource.Id.btnAlmuerzoAntes);
            btnAlmuerzoAntes.Click += BtnAlmuerzoAntes_Click;
            txt_alm_antes = FindViewById<EditText>(Resource.Id.txt_alm_antes);

            btnAlmuerzoDespues = FindViewById<Button>(Resource.Id.btnAlmuerzoDespues);
            btnAlmuerzoDespues.Click += BtnAlmuerzoDespues_Click;
            txt_alm_des = FindViewById<EditText>(Resource.Id.txt_alm_des);

            btnAntesCena = FindViewById<Button>(Resource.Id.btnAntesCena);
            btnAntesCena.Click += BtnAntesCena_Click;
            Txt_cen_Antes = FindViewById<EditText>(Resource.Id.Txt_cen_Antes);

            btnDespuesCena = FindViewById<Button>(Resource.Id.btnDespuesCena);
            btnDespuesCena.Click += BtnDespuesCena_Click;
            Txt_cen_Despues = FindViewById<EditText>(Resource.Id.Txt_cen_Despues);

            btnDormirAntes = FindViewById<Button>(Resource.Id.btnDormirAntes);
            btnDormirAntes.Click += BtnDormirAntes_Click;
            Txt_Dor_Antes = FindViewById<EditText>(Resource.Id.Txt_Dor_Antes);

            btnDormirDespues = FindViewById<Button>(Resource.Id.btnDormirDespues);
            btnDormirDespues.Click += BtnDormirDespues_Click;
            Txt_Dor_Despues = FindViewById<EditText>(Resource.Id.Txt_Dor_Despues);
            txt_obs = FindViewById<EditText>(Resource.Id.txt_obs);

            // insulina 
            mnna_rap = FindViewById<EditText>(Resource.Id.mnna_rap);
            mnna_regul = FindViewById<EditText>(Resource.Id.mnna_regul);
            mnna_mezcla = FindViewById<EditText>(Resource.Id.mnna_mezcla);

            tarde_rap = FindViewById<EditText>(Resource.Id.tarde_rap);
            tarde_regul =FindViewById<EditText>(Resource.Id.tarde_regul);
            tarde_mezcla= FindViewById<EditText>(Resource.Id.tarde_mezcla);

            noche_rap = FindViewById<EditText>(Resource.Id.noche_rap);
            noche_regul = FindViewById<EditText>(Resource.Id.noche_regul);
            noche_mezcla = FindViewById<EditText>(Resource.Id.noche_mezcla);

            dormir_rap = FindViewById<EditText>(Resource.Id.dormir_rap);
            dormir_regul = FindViewById<EditText>(Resource.Id.dormir_regul);
            dormir_mezcla = FindViewById<EditText>(Resource.Id.dormir_mezcla);



        }

        private void BtnDormirAntes_Click(object sender, EventArgs e)
        {
            onSomeButtonClick(sender, e, btnDormirAntes);
        }

        private void BtnDespuesCena_Click(object sender, EventArgs e)
        {
            onSomeButtonClick(sender, e, btnDespuesCena);
        }

        private void BtnAlmuerzoDespues_Click(object sender, EventArgs e)
        {
            onSomeButtonClick(sender, e, btnAlmuerzoDespues);
        }

        private void BtnAlmuerzoAntes_Click(object sender, EventArgs e)
        {
            onSomeButtonClick(sender, e, btnAlmuerzoAntes);
        }

        private void BtnFecha_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            DatePickerDialog dialog = new DatePickerDialog(this, OnDateSet1, today.Year, today.Month - 1, today.Day);
            dialog.DatePicker.MinDate = today.Millisecond;
            dialog.Show();

        }

        void OnDateSet1(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            btnfecha.Text = e.Date.ToShortDateString();
           
        }


        private void BtnDormirDespues_Click(object sender, EventArgs e)
        {
            onSomeButtonClick(sender, e, btnDormirDespues); 
        }



        private void BtnAntesCena_Click(object sender, EventArgs e)
        {
            onSomeButtonClick(sender, e, btnAntesCena); 
        }

        private void BtnDesayunoDespues_Click(object sender, EventArgs e)
        {   
            onSomeButtonClick(sender, e, btnDesayunoDespues);
        }

        private void BtnDesayunoAntes_Click(object sender, EventArgs e)
        {
            onSomeButtonClick(sender, e, btnDesayunoAntes);
        }

        
        public void onSomeButtonClick(object sender, EventArgs e,Button b)
        {
            DateTime d = new DateTime();
            d = DateTime.Now;
            new TimePickerDialog(this, data_TimePickerCallback, d.Hour, d.Minute, false).Show();
            R_boton(b);// relaciono el boton de la gui con un boton aux
        }
            
        public void data_TimePickerCallback(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            int hour = e.HourOfDay;
            int minute = e.Minute;
            tiempos = new TimeSpan(hour, minute, 00);
            Texto(btn_aux, tiempos);//
        }

        public void Texto(Button boton, TimeSpan time)// metodos donde escribo en el boton aux y el de la gui
        {
            boton.Text = time.ToString();
        }
        public void R_boton (Button boton)
        {
             btn_aux = boton;
        }



        private async void Btn_agregar_ClickAsync(object sender, EventArgs e)
        {
            await gpsAsync();

            CrudGlucosa bd = new CrudGlucosa();
            try
            {
                g = new Glucosa();
                g.Fk_id_US = idUS;
                                  //desayuno
                if (btnDesayunoAntes.Text != "Hora")
                {
                    g.H_A_Des1 = Convert.ToDateTime(btnDesayunoAntes.Text).TimeOfDay;
                }
                if (txt_des_antes.Text != "")
                {
                    g.N_A_Des1 = Convert.ToInt32(txt_des_antes.Text);
                }

                if (btnDesayunoDespues.Text != "Hora")
                {
                    g.H_D_Des1 = Convert.ToDateTime(btnDesayunoDespues.Text).TimeOfDay;
                }

                if (txt_des_Des.Text != "")
                {
                    g.N_D_Des1 = Convert.ToInt32(txt_des_Des.Text);
                }


                //almuerzo
                if (btnAlmuerzoAntes.Text != "Hora")
                {
                    g.H_A_Alm1 = Convert.ToDateTime(btnAlmuerzoAntes.Text).TimeOfDay;
                }

                if (txt_alm_antes.Text != "")
                {
                    g.N_A_Alm1 = Convert.ToInt32(txt_alm_antes.Text);
                }

                if (btnAlmuerzoDespues.Text != "Hora")
                {
                    g.H_D_Alm1 = Convert.ToDateTime(btnAlmuerzoDespues.Text).TimeOfDay;
                }

                if (txt_alm_des.Text != "")
                {
                    g.N_D_Alm1 = Convert.ToInt32(txt_alm_des.Text);
                }

                //cena
                if (btnAntesCena.Text != "Hora")
                {
                    g.H_A_Cen1 = Convert.ToDateTime(btnAntesCena.Text).TimeOfDay;

                }

                if (Txt_cen_Antes.Text != "")
                {
                    g.N_A_Cen1 = Convert.ToInt32(Txt_cen_Antes.Text);
                }

                if (btnDespuesCena.Text != "Hora")
                {
                    g.H_D_Cen1 = Convert.ToDateTime(btnDespuesCena.Text).TimeOfDay;

                }

                if (Txt_cen_Despues.Text != "")
                {
                    g.N_D_Cen1 = Convert.ToInt32(Txt_cen_Despues.Text);
                }


                //dormir
                if (btnDormirAntes.Text != "Hora")
                {
                    g.H_A_Dor1 = Convert.ToDateTime(btnDormirAntes.Text).TimeOfDay;

                }

                if (Txt_Dor_Antes.Text != "")
                {
                    g.N_A_Dor1 = Convert.ToInt32(Txt_Dor_Antes.Text);
                }

                if (btnDormirDespues.Text != "Hora")
                {
                    g.H_D_Dor1 = Convert.ToDateTime(btnDormirDespues.Text).TimeOfDay;
                }

                if (Txt_Dor_Despues.Text != "")
                {
                    g.N_D_Dor1 = Convert.ToInt32(Txt_Dor_Despues.Text);
                }

                //otros datos
                if (btnfecha.Text != "Selecciónar fecha a ingresar")
                {
                    g.Fecha = Convert.ToDateTime(btnfecha.Text);
                }
                else
                {
                    g.Fecha = DateTime.Now;
                }
                g.Obs = txt_obs.Text;
                g.Latitud = latitud;
                g.Longitud = longitud;

                if (await bd.AgregarGlucosaAsync(g))
                {
                    Toast.MakeText(this, "Dato agregado", ToastLength.Long).Show();
                    id_glucosa = bd.GetID(idUS);
                    DInsulina ins  = new DInsulina();
                    try
                    {
                        ins.Fk_idglu = id_glucosa;

                        if (mnna_rap.Text!="")
                        {
                            ins.Mnna_rap = Convert.ToInt32(mnna_rap.Text);
                        }

                        if (mnna_regul.Text!="")
                        {
                            ins.Mnna_regul = Convert.ToInt32(mnna_regul.Text);
                        }

                        if (mnna_mezcla.Text!="")
                        {
                            ins.Mnna_mezcla = Convert.ToInt32(mnna_mezcla.Text);
                        }

                        if (tarde_rap.Text!="")
                        {
                            ins.Tarde_rap = Convert.ToInt32(tarde_rap.Text);
                        }

                        if (tarde_regul.Text!="")
                        {
                            ins.Tarde_regul = Convert.ToInt32(tarde_regul.Text);
                        }

                        if (tarde_mezcla.Text!="")
                        {
                            ins.Tarde_mezcla = Convert.ToInt32(tarde_mezcla.Text);
                        }

                        if (noche_rap.Text!="")
                        {
                            ins.Noche_rap = Convert.ToInt32(noche_rap.Text);
                        }

                        if (noche_regul.Text!="")
                        {
                            ins.Noche_regul = Convert.ToInt32(noche_regul.Text);
                        }

                        if (noche_mezcla.Text!="")
                        {
                            ins.Noche_mezcla = Convert.ToInt32(noche_mezcla.Text);
                        }

                        if (dormir_rap.Text!="")
                        {
                            ins.Dormir_rap = Convert.ToInt32(dormir_rap.Text);
                        }

                        if (dormir_regul.Text!="")
                        {
                            ins.Dormir_regul = Convert.ToInt32(dormir_regul.Text);
                        }

                        if (dormir_mezcla.Text!="")
                        {
                            ins.Dormir_mezcla = Convert.ToInt32(dormir_mezcla.Text);
                        }



                        CrudInsulina bdinsu = new CrudInsulina();
                        bdinsu.agregar_insulina(ins);
                    }
                    catch (Exception)
                    {

                        System.Console.WriteLine("ERROR");
                        System.Console.WriteLine("exor");
                        System.Console.WriteLine(e);
                        System.Console.WriteLine("ERROR");
                        System.Console.WriteLine("exor");
                        Console.WriteLine("error");
                        Android.Widget.Toast.MakeText(Application.Context, "error agregar isulina".ToString(), Android.Widget.ToastLength.Short).Show();

                    }





                };

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();

            }
            Intent i = new Intent(this, typeof(MainActivity));
            i.PutExtra("idUs", SidUs);
            StartActivity(i);
            Finish();// para volver al master pague 
        }

        
        public async Task gpsAsync()
        {
            //localicacion
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    latitud = location.Latitude;
                    longitud = location.Longitude;

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

        }




    }
}