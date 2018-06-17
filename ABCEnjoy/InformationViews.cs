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
using Android.Webkit;
using Android.Support.V7.Widget;

namespace ABCEnjoy
{
    [Activity(Label = "ABCEnjoy", MainLauncher = false, Icon = "@mipmap/icon", Theme = "@style/MyTheme")]
    public class InformationViews : Activity
    {
        public int number_of_elements=0;
        public WebView cardimage;
        public TextView Title;
        public TextView Description;
        
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.InformationViews);
            CreateElements();
        }

        public void CreateElements()
        {

            var res = SQLite_Android.GetDBItems("select * from Itm");
<<<<<<< HEAD
            string s = ""
=======
            number_of_elements = res.Count;

            var uri = Android.Net.Uri.Parse(@"data:image/jpeg;base64,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");
>>>>>>> 9d5d859f1cd01653b9f29a554479e2a0839b9969

            ScrollView sw = FindViewById<ScrollView>(Resource.Id.scrollView);
            LinearLayout ll = FindViewById<LinearLayout>(Resource.Id.linearLayout);
            Toast.MakeText(this, res.Count.ToString(), ToastLength.Long).Show();
            for (int index = 0; index < 5; index++)
            {
                CardView cardview = new CardView(this);
                cardimage = new WebView(this);
                cardimage.LoadUrl(uri.ToString());
                Title = new TextView(this);
                Title.Text = "тайтл текс #" + index.ToString();
                Description = new TextView(this);
                Description.Text = "Описание тайтлов";

                /*cardview.AddView(cardimage);
                cardview.AddView(Title);
                cardview.AddView(Description);*/
                LinearLayout llnner = new LinearLayout(this);
                llnner.Orientation = Orientation.Vertical;
                llnner.WeightSum = 1;
                llnner.AddView(cardimage);
                llnner.AddView(Title);
                llnner.AddView(Description);
                //llnner.AddView(cardview);
                ll.AddView(llnner);

                /*LinearLayout llInner = new LinearLayout(this);
                LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.WrapContent);
                llInner.Orientation = Orientation.Horizontal;
                llInner.WeightSum = 1;

                ImageView iv = new ImageView(this);
                llInner.AddView(iv);

                TextView title = new TextView(this);
                title.Text = "Some title";

                llInner.AddView(title);
                
                TextView someDescr = new TextView(this);
                someDescr.Text = "some Description";
                llInner.AddView(someDescr);*/

            }
                //sw.AddView(ll);
            //ll.AddView(cardview);

        }
    }
}