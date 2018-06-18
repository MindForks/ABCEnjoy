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
using Android.Graphics;
using System.Net;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Graphics.Drawables;

namespace ABCEnjoy
{
    [Activity(Label = "ABCEnjoy", MainLauncher = false, Theme = "@style/MyTheme")]
    public class InformationViews : ActionBarActivity
    {
        public int number_of_elements = 0;
        private int minMoney, maxMoney;
        private SupportToolbar mToolbar;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;

        struct ViewElement
        {
            public WebView cardimage;
            public ImageButton[] imgbttn;
            public TextView title;
            public TextView local;
            public TextView price;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.InformationViews);
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
            GetExtraFromMain();
            CreateElements();
            SetSupportActionBar(mToolbar);
            mLeftDataSet = new List<string>();
            mLeftDataSet.Add("Главная страница");
            mLeftDataSet.Add("Аккаунт");
            mLeftDataSet.Add("О нас");
            mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);

            //Toast.MakeText(this, $"{minMoney} {maxMoney}", ToastLength.Long).Show();

            mLeftDrawer.ChoiceMode = ChoiceMode.Single;
            mLeftDrawer.ItemClick += (sender, e) =>
            {
                string s_item = mLeftDrawer.GetItemAtPosition(e.Position).ToString();
                if (s_item == "Главная страница")
                {
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                }
                else
                {
                    if (s_item == "Аккаунт")
                    {
                        Toast.MakeText(this, "Тут будет авторизация!", ToastLength.Short).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "Тут будет информация о нас!", ToastLength.Short).Show();
                    }
                }
            };
        }

        private void GetExtraFromMain()
        {
            minMoney = Intent.GetIntExtra("minMoney", 0);
            maxMoney = Intent.GetIntExtra("maxMoney", 0);
            //Date_before = Convert.ToDateTime(Intent.GetIntExtra("DateBefore", 0));
            //Toast.MakeText(this, $"{Date_before.ToLongDateString()}", ToastLength.Long).Show();
        }

        public void CreateElements()
        {
            ViewElement ve;

            string request = String.Format("select * from Items where price >={0} AND price <={1}", minMoney,maxMoney);
            var res = SQLite_Android.GetDBItems(request);
            number_of_elements = res.Count;
            ve.imgbttn = new ImageButton[number_of_elements];

            ScrollView sw = FindViewById<ScrollView>(Resource.Id.scrollView);
            LinearLayout ll = FindViewById<LinearLayout>(Resource.Id.linearLayout);
            Toast.MakeText(this, "Количество мероприятий: "+res.Count.ToString(), ToastLength.Long).Show();
            for (int index = 0; index < number_of_elements; index++)
            {
                    Android.Net.Uri uri = Android.Net.Uri.Parse($"{res[index].Image}");
                    //var imageBitmap = GetImageBitmapFromUrl($"{res[index].Image}");
                    ve.imgbttn[index] = new ImageButton(this);
                    //ve.imgbttn[index].SetImageBitmap(uri);

                    LinearLayout weblin = new LinearLayout(this);
                    LinearLayout.LayoutParams lp1 = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 500);
                    weblin.LayoutParameters = lp1;
                    CardView cardview = new CardView(this);
                    ve.cardimage = new WebView(this);
                    ve.cardimage.LoadUrl(uri.ToString());
                    weblin.AddView(ve.cardimage);
                    
                    ve.title = new TextView(this);
                    ve.title.Text = res[index].Title;
                    ve.title.TextSize = 20;
                    ve.local = new TextView(this);
                    ve.local.Text = res[index].Location;

                    ve.local.Gravity = GravityFlags.Left;


                    ve.price = new TextView(this);
                if (res[index].Price == 0)
                    ve.price.Text = "бесплатно";
                else
                    ve.price.Text = res[index].Price.ToString() + "грн.";

                    ve.price.Gravity = GravityFlags.Right;
                    /*cardview.AddView(cardimage);
                    cardview.AddView(Title);
                    cardview.AddView(Description);*/
                    LinearLayout llnner = new LinearLayout(this);
                    LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent);
                    lp.SetMargins(40, 40, 40, 10);
                    llnner.LayoutParameters = lp;
                    llnner.SetBackgroundColor(Android.Graphics.Color.ParseColor("#D8D8D8"));

                    llnner.Orientation = Orientation.Vertical;
                    llnner.WeightSum = 1;
                    llnner.AddView(weblin);
                    llnner.AddView(ve.title);
                    llnner.AddView(ve.local);
                    llnner.AddView(ve.price);

                llnner.SetBackgroundResource(Resource.Drawable.abc_btn_default_mtrl_shape);

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
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu_views, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    //this.OnBackPressed();
                    return true;
                case Resource.Id.action_search:
                    return true;
                case Resource.Id.action_sort:
                    return true;
                case Resource.Id.action_filter:
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}