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

namespace ABCEnjoy
{
    [Activity(Label = "ABCEnjoy", MainLauncher = false, Icon = "@mipmap/icon", Theme = "@style/MyTheme")]
    public class InformationViews : Activity
    {
        public int number_of_elements=0;
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

            string s = ""

            ScrollView sw = FindViewById<ScrollView>(Resource.Id.scrollView);
            LinearLayout ll = FindViewById<LinearLayout>(Resource.Id.linearLayout);
            for (int index = 0; index < number_of_elements; index++)
            {
                LinearLayout llInner = new LinearLayout(this);
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
                llInner.AddView(someDescr);
                ll.AddView(llInner);
            }
        }
    }
}