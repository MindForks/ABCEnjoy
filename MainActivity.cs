using Android.App;
using Android.Widget;
using Android.OS;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using System.Collections.Generic;
using Android.Views;
using Xamarin.RangeSlider;
using System;
using Android.Content;
using Java.IO;

namespace ABCEnjoy
{
    [Activity(Label = "ABCEnjoy", MainLauncher = true, Icon = "@mipmap/icon", Theme="@style/MyTheme")]
    public class MainActivity : ActionBarActivity
    {
        public DateTime Date { get; set; }
        private DateTime Time_b, Time_a;
        public int minMoney { get; set; }
        public int maxMoney { get; set; }

        private RangeSliderControl _sliderPrice;
        private Button toEnt;
        private Button DateButton, time_before, time_after;
        private SupportToolbar mToolbar;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            FindViewById();
            var slider = FindViewById<Xamarin.RangeSlider.RangeSliderControl>(Resource.Id.sliderPrice);
            _sliderPrice.SetSelectedMinValue(50);
            _sliderPrice.SetSelectedMaxValue(200);
            FSlider();

            SetSupportActionBar(mToolbar);
            mLeftDataSet = new List<string>();
            mLeftDataSet.Add("Развлечения");
            mLeftDataSet.Add("Аккаунт");
            mLeftDataSet.Add("О нас");
            mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;   

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            

            mLeftDrawer.ChoiceMode = ChoiceMode.Single;
            mLeftDrawer.ItemClick += (sender, e) =>
              {
                  string s_item = mLeftDrawer.GetItemAtPosition(e.Position).ToString();
                  if (s_item == "Развлечения")
                  {
                      var intent = new Intent(this, typeof(InformationViews));
                      StartActivity(intent);
                    //intent.PutExtra("DateBefore", Convert.ToInt32(DateBefore));
                    //intent.PutExtra("DateAfter", Convert.ToInt32(DateAfter));
                      intent.PutExtra("minMoney", minMoney);
                      intent.PutExtra("maxMoney", maxMoney);
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

            DateButton.Click += (s, e) =>
              {
                  DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                  {
                      Date = time;
                      Toast.MakeText(this, $"{Date.ToLongDateString()}", ToastLength.Long).Show();
                      DateButton.Text = Date.Date.ToString();
                  });
                  frag.Show(FragmentManager, DatePickerFragment.TAG);
              };
            time_before.Click += delegate
              {
                  TimePickerFragment frag = TimePickerFragment.NewInstance(
                    delegate (DateTime time)
                {
                    Time_b = time;
                    Toast.MakeText(this, $"{Time_b.Hour}", ToastLength.Long).Show();
                });

                  frag.Show(FragmentManager, TimePickerFragment.TAG);
              };

            time_after.Click += delegate
            {
                TimePickerFragment frag = TimePickerFragment.NewInstance(
                  delegate (DateTime time)
                  {
                      Time_a = time;
                      Toast.MakeText(this, $"{Time_a.Hour}", ToastLength.Long).Show();
                  });

                frag.Show(FragmentManager, TimePickerFragment.TAG);
            };
            toEnt.Click += (e, s) =>
              {
                  var intent = new Intent(this, typeof(InformationViews));
                  StartActivity(intent);
                //intent.PutExtra("DateBefore", Convert.ToInt32(DateBefore));
                //intent.PutExtra("DateAfter", Convert.ToInt32(DateAfter));
                intent.PutExtra("minMoney", minMoney);
                  intent.PutExtra("maxMoney", maxMoney);
              };
        }

        private void FSlider()
        {
            _sliderPrice.LowerValueChanged += (s, e) =>
            {
                minMoney = (int)_sliderPrice.GetSelectedMinValue();
                Toast.MakeText(this, "Минимальная цена: " + minMoney.ToString() + "грн", ToastLength.Short).Show();
            };

            _sliderPrice.UpperValueChanged += (s, e) =>
            {
                maxMoney = (int)_sliderPrice.GetSelectedMaxValue();
                Toast.MakeText(this, "Максимальная цена: " + maxMoney.ToString() + "грн", ToastLength.Short).Show();
            };
        }

        public override File GetDatabasePath(string name)
        {
            return base.GetDatabasePath(name);
        }

        private void FindViewById()
        {
            toEnt = FindViewById<Button>(Resource.Id.toEntert);
            DateButton = FindViewById<Button>(Resource.Id.datebefore);
            time_before = FindViewById<Button>(Resource.Id.timebefore);
            time_after = FindViewById<Button>(Resource.Id.timeafter);
            _sliderPrice = FindViewById<RangeSliderControl>(Resource.Id.sliderPrice);
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
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
                    this.OnBackPressed();
                    return true;

                case Resource.Id.action_menu:
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}

