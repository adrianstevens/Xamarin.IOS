// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace XamDraw
{
	[Register ("XamDrawViewController")]
	partial class XamDrawViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnClear { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imgDraw { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnClear != null) {
				btnClear.Dispose ();
				btnClear = null;
			}
			if (imgDraw != null) {
				imgDraw.Dispose ();
				imgDraw = null;
			}
		}
	}
}
