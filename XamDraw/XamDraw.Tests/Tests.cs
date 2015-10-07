using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;

namespace XamDraw.Tests
{
	[TestFixture]
	public class Tests
	{
		iOSApp app;

		[SetUp]
		public void BeforeEachTest ()
		{
			// TODO: If the iOS app being tested is included in the solution then open
			// the Unit Tests window, right click Test Apps, select Add App Project
			// and select the app projects that should be tested.
			app = ConfigureApp
				.iOS
			// TODO: Update this path to point to your iOS app and uncomment the
			// code if the app is not included in the solution.
			//.AppBundle ("../../../iOS/bin/iPhoneSimulator/Debug/XamDraw.Tests.iOS.app")
				.StartApp ();
		}

		[Ignore]
		public void AppLaunches ()
		{
			app.Screenshot ("First screen.");
		}

		[Ignore]
		public void PullDismiss ()
		{
		//	app.FlickCoordinates (100, 0, 100, 100);

		//	app.FlickCoordinates (100, 665, 100, 600);

			//app.Invoke ("UITestInvoke", "param");

			app.Repl ();
		//	for (int i = 350; i < 700; i += 10) {
		//		app.TapCoordinates (200, i);

		//	}
		}

		[Ignore]
		public void AlertTest ()
		{
			app.Invoke ("UITestInvoke", "param");

			app.Tap ("ok");

		//	app.Repl ();

		}

		[Ignore]
		public void DrawCircle ()
		{
			double x = 0;
			double y = 0;

			double multiple = 100;

			app.SetOrientationLandscape ();

			var myView = app.Query (m => m.Class ("UIView")) [0];

			app.PinchToZoomOut (m => m.Class ("UIView"), null);


			for (int i = 0; i < 72 + 1; i++) 
			{
				double angle = ((double)i) / (9 / Math.PI);

				var newX = Math.Sin (angle) * i * 3 + myView.Rect.CenterX;
				var newY = Math.Cos (angle) * i * 3 + myView.Rect.CenterY;

				if (i != 0)
					app.FlickCoordinates ((int)x, (int)y, (int)newX, (int)newY);

				x = newX;
				y = newY;

			} 

		}


		[Ignore]
		public void DrawTest ()
		{
			app.SwipeLeft ();
			app.SwipeRight ();
			app.ScrollDown ();
		    app.ScrollUp ();

			app.Tap ("Clear");


		//	app.Invoke ("UITestInvoke", "param");




			for (int i = 0; i < 20; i++) {
				app.FlickCoordinates (50, 50 + 5 * i, 
					200, 200 + 5 * i);
			} 

			app.Tap ("Clear");



			double x = 100;
			double y = 100;

			double multiple = 100;

			for (int i = 0; i < 36 + 1; i++) 
			{
				double angle = ((double)i) / (18 / Math.PI);

				var newX = Math.Sin (angle) * multiple + multiple * 2;
				var newY = Math.Cos (angle) * multiple + multiple * 2;

				if (i != 0)
					app.DragCoordinates ((int)x, (int)y, (int)newX, (int)newY);

				x = newX;
				y = newY;
			}  


			//app.SendAppToBackground (new TimeSpan(0, 0, 10));



		}



		[Test]
		public void SwipeText ()
		{
			app.SwipeLeft ();

			app.SwipeRight ();

			app.ScrollDown ();

			app.Tap ("Clear");

			app.Repl ();
		}


	}
}

