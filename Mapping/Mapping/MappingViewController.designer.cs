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

namespace Mapping
{
	[Register ("MappingViewController")]
	partial class MappingViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnPointToPoint { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnRoute { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MapKit.MKMapView map { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnPointToPoint != null) {
				btnPointToPoint.Dispose ();
				btnPointToPoint = null;
			}
			if (btnRoute != null) {
				btnRoute.Dispose ();
				btnRoute = null;
			}
			if (map != null) {
				map.Dispose ();
				map = null;
			}
		}
	}
}
