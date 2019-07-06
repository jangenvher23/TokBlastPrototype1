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

namespace TokBlastPrototype1.Droid
{
	[Activity(Theme = "@style/Theme.Splash",
	 Icon = "@drawable/icon",
	  MainLauncher = true,
	  NoHistory = true)]
	public class SplashActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			StartActivity(typeof(MainActivity));
			// System.Threading.Thread.Sleep(3000);
		}
	}
}