using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.Platform.Android;

namespace ABCEnjoy
{
    public class EmptyClass
    {
        public EmptyClass()
        {
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

        }
    }
}
