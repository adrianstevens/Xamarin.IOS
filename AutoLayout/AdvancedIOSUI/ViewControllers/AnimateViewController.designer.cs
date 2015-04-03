// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace AdvancedIOSUI
{
	[Register ("AnimateViewController")]
	partial class AnimateViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnAnimate { get; set; }

		[Action ("btnAnimate_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnAnimate_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (btnAnimate != null) {
				btnAnimate.Dispose ();
				btnAnimate = null;
			}
		}
	}
}
