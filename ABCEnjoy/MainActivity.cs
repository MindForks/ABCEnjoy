using Android.App;
using Android.Widget;
using Android.OS;

namespace ABCEnjoy
{
    [Activity(Label = "ABCEnjoy", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
<<<<<<< HEAD
=======
        int b = 0;
        int c = 0;
>>>>>>> 6620e0593da1cc98125a15557a465a62e3a6260a
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate { button.Text = $"{count++} clicks!"; };
        }
    }
}

