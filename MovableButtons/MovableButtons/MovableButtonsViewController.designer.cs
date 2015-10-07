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

namespace MovableButtons
{
	[Register ("MovableButtonsViewController")]
	partial class MovableButtonsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnFade { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnShake { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnSwell { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch swMovable { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnFade != null) {
				btnFade.Dispose ();
				btnFade = null;
			}
			if (btnShake != null) {
				btnShake.Dispose ();
				btnShake = null;
			}
			if (btnSwell != null) {
				btnSwell.Dispose ();
				btnSwell = null;
			}
			if (swMovable != null) {
				swMovable.Dispose ();
				swMovable = null;
			}
		}
	}
}
