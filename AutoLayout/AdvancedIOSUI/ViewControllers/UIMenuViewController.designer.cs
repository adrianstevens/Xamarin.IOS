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
	[Register ("UIMenuViewController")]
	partial class UIMenuViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCode { get; set; }

		[Action ("btnCode_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnCode_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (btnCode != null) {
				btnCode.Dispose ();
				btnCode = null;
			}
		}
	}
}
