// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MapSnap
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCapture { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imgCapture { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MapKit.MKMapView map { get; set; }

		[Action ("BtnCapture_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void BtnCapture_TouchUpInside (UIButton sender);

		[Action ("BtnEmail_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void BtnEmail_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (btnCapture != null) {
				btnCapture.Dispose ();
				btnCapture = null;
			}
			if (btnEmail != null) {
				btnEmail.Dispose ();
				btnEmail = null;
			}
			if (imgCapture != null) {
				imgCapture.Dispose ();
				imgCapture = null;
			}
			if (map != null) {
				map.Dispose ();
				map = null;
			}
		}
	}
}
