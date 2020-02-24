using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Biblioteca.Modelo;

namespace Glucometer
{
    public class GlAdapter : BaseAdapter<Glucosa>
    {
        private List<Glucosa> mItem = new List<Glucosa>();
        private Context context;
        Button btnfecha;
        Button btnDesayunoAntes, btnDesayunoDespues;
        EditText txt_des_antes, txt_des_Des;
        Button btnAlmuerzoAntes, btnAlmuerzoDespues;
        EditText txt_alm_antes, txt_alm_des;
        Button btnAntesCena, btnDespuesCena;
        EditText Txt_cen_Antes, Txt_cen_Despues;
        Button btnDormirAntes, btnDormirDespues;
        EditText Txt_Dor_Antes, Txt_Dor_Despues;
        TableLayout tabla_click;


        public override int Count
        {
            get
            {
                return mItem.Count;
            }
        }

        public override Glucosa this[int position] => throw new NotImplementedException();

        public GlAdapter(Context mcontext, List<Glucosa> mItems)
        {
            mItem.Clear();
            mItem = mItems;
            context = mcontext;


        }



        public override long GetItemId(int position)
        {
            int idGlucosa = 0; 
            idGlucosa=mItem[position].IdGlucosa;
            return idGlucosa;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)//metodo para llenar la cosaq creo
        {
            View view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListAdapter, null);

            }
            btnfecha = view.FindViewById<Button>(Resource.Id.fecha);
            btnfecha.Text =mItem[position].Fecha.ToShortDateString();
            tabla_click = view.FindViewById<TableLayout>(Resource.Id.tabla_click);


            // desayuno
            btnDesayunoAntes = view.FindViewById<Button>(Resource.Id.btnDesayunoAntes);
            btnDesayunoAntes.Text = Convert.ToString(mItem[position].H_A_Des1);
            txt_des_antes = view.FindViewById<EditText>(Resource.Id.txt_des_antes);
            txt_des_antes.Text = Convert.ToString(mItem[position].N_A_Des1);

            btnDesayunoDespues = view.FindViewById<Button>(Resource.Id.btnDesayunoDespues);
            btnDesayunoDespues.Text =  Convert.ToString(mItem[position].H_D_Des1);
            txt_des_Des = view.FindViewById<EditText>(Resource.Id.txt_des_Des);
            txt_des_Des.Text = Convert.ToString(mItem[position].N_D_Des1);

            //almuerzo
            btnAlmuerzoAntes = view.FindViewById<Button>(Resource.Id.btnAlmuerzoAntes);
            btnAlmuerzoAntes.Text = Convert.ToString(mItem[position].H_A_Alm1);
            txt_alm_antes = view.FindViewById<EditText>(Resource.Id.txt_alm_antes);
            txt_alm_antes.Text = Convert.ToString(mItem[position].N_A_Alm1);

            btnAlmuerzoDespues = view.FindViewById<Button>(Resource.Id.btnAlmuerzoDespues);
            btnAlmuerzoDespues.Text = Convert.ToString(mItem[position].H_D_Alm1);
            txt_alm_des = view.FindViewById<EditText>(Resource.Id.txt_alm_des);
            txt_alm_des.Text = Convert.ToString(mItem[position].N_D_Alm1);

            //cena
            btnAntesCena = view.FindViewById<Button>(Resource.Id.btnAntesCena);
            btnAntesCena.Text = Convert.ToString(mItem[position].H_A_Cen1);
            Txt_cen_Antes = view.FindViewById<EditText>(Resource.Id.Txt_cen_Antes);
            Txt_cen_Antes.Text = Convert.ToString(mItem[position].N_A_Cen1);

            btnDespuesCena = view.FindViewById<Button>(Resource.Id.btnDespuesCena);
            btnDespuesCena.Text = Convert.ToString(mItem[position].H_D_Cen1);
            Txt_cen_Despues = view.FindViewById<EditText>(Resource.Id.Txt_cen_Despues);
            Txt_cen_Despues.Text = Convert.ToString(mItem[position].N_D_Cen1);

            //dormir
            btnDormirAntes = view.FindViewById<Button>(Resource.Id.btnDormirAntes);
            btnDormirAntes.Text = Convert.ToString(mItem[position].H_A_Dor1);
            Txt_Dor_Antes = view.FindViewById<EditText>(Resource.Id.Txt_Dor_Antes);
            Txt_Dor_Antes.Text = Convert.ToString(mItem[position].N_A_Dor1);

            btnDormirDespues = view.FindViewById<Button>(Resource.Id.btnDormirDespues);
            btnDormirDespues.Text = Convert.ToString(mItem[position].H_D_Dor1);
            Txt_Dor_Despues = view.FindViewById<EditText>(Resource.Id.Txt_Dor_Despues);
            Txt_Dor_Despues.Text = Convert.ToString(mItem[position].N_D_Dor1);
            view.Click += (object sender, EventArgs e) =>
            {

                    Android.Widget.Toast.MakeText(parent.Context, "Clicked " + mItem[position].IdGlucosa, ToastLength.Short).Show();
                    Intent i = new Intent(context, typeof(Detalles));
                    string idG = Convert.ToString(mItem[position].IdGlucosa);
                    string idUS = Convert.ToString(mItem[position].Fk_id_US);
                    i.PutExtra("idG", idG);
                    i.PutExtra("idUS", idUS);
                    context.StartActivity(i);

                

                    
                // aca deveria ir el codigo para abrir  una ventana detyalles y pasar el id a la ventana detalles para la consulta

                


            };

            




            return view;
        }


        
    }
}