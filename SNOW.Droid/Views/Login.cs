using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

namespace SNOW.Droid.Views
{
    [Activity(Label = "View for Login")]
    public class Login : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);
        }
    }
}