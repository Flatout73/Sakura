using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace sakura
{
    [Activity(Label = "Sakura"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Portrait
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        Game g;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            g = new Game();
            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }
        
    }

}

