using Android.App;
using Android.Widget;
using Android.OS;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using System.Collections.Generic;
using Android.Views;

namespace ABCEnjoy
{
    [Activity(Label = "ABCEnjoy", MainLauncher = true, Icon = "@mipmap/icon", Theme="@style/MyTheme")]
    public class MainActivity : ActionBarActivity
    {
        private SupportToolbar mToolbar;
        //private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;

        int count = 1;
        int b = 0;
        int c = 0;
        int Eugene = 0;
        int taaj;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.myButton);

           // button.Click += delegate { button.Text = $"{count++} clicks!"; };

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);

            SetSupportActionBar(mToolbar);

            mLeftDataSet = new List<string>();
            mLeftDataSet.Add("Item 1");
            mLeftDataSet.Add("Item 2");
            mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);

            var slider = FindViewById<Xamarin.RangeSlider.RangeSliderControl>(Resource.Id.slider);
            slider.SetSelectedMinValue(11);
            slider.SetSelectedMaxValue(16);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    //The hamburger icon was clicked which means the drawer toggle will handle the event
                    //all we need to do is ensure the right drawer is closed so the don't overlap
                    return true;

                case Resource.Id.action_menu:
                    //Refresh
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}

