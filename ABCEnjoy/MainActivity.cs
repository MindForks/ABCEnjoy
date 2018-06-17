﻿using Android.App;
using Android.Widget;
using Android.OS;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using System.Collections.Generic;
using Android.Views;
using Xamarin.RangeSlider;
using System;

namespace ABCEnjoy
{
    [Activity(Label = "ABCEnjoy", MainLauncher = true, Icon = "@mipmap/icon", Theme="@style/MyTheme")]
    public class MainActivity : ActionBarActivity
    {
        public DateTime DateBefore { get; set; }
        public DateTime DateAfter { get; set; }
        public int minMoney { get; set; }
        public int maxMoney { get; set; }

        private RangeSliderControl _sliderPrice;
        private Button timeButton_before, timeButton_after;
        private SupportToolbar mToolbar;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;
        public Button[] button_menu;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            FindViewById();
            // Get our button from the layout resource,
            // and attach an event to it
            SetSupportActionBar(mToolbar);
            Button b1m;
            b1m = new Button(this);
            mLeftDataSet = new List<string>();
            
            mLeftDataSet.Add("Item 1");
            mLeftDataSet.Add("Item 2");
            mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;          

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);

            var slider = FindViewById<Xamarin.RangeSlider.RangeSliderControl>(Resource.Id.sliderPrice);
            _sliderPrice.SetSelectedMinValue(50);
            _sliderPrice.SetSelectedMaxValue(200);

            _sliderPrice.LowerValueChanged += (s, e) =>
              {
                  minMoney = (int)_sliderPrice.GetSelectedMinValue();
              };

            _sliderPrice.UpperValueChanged += (s, e) =>
             {
                 maxMoney = (int)_sliderPrice.GetSelectedMaxValue();
             };

            timeButton_before.Click += (s, e) =>
              {
                  DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                  {
                      DateBefore = time;
                      Toast.MakeText(this, $"{DateBefore.ToLongDateString()}", ToastLength.Long).Show();
                  });
                  frag.Show(FragmentManager, DatePickerFragment.TAG);
              };
            timeButton_after.Click += delegate
              {
                  DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                  {
                      DateAfter = time;
                      Toast.MakeText(this, $"{DateAfter.ToLongDateString()}", ToastLength.Long).Show();
                  });
                  frag.Show(FragmentManager, DatePickerFragment.TAG);
              };
        }

        private void FindViewById()
        {
            timeButton_before = FindViewById<Button>(Resource.Id.timebefore);
            timeButton_after = FindViewById<Button>(Resource.Id.timeafter);
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

