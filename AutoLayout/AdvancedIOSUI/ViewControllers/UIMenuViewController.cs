using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace AdvancedIOSUI
{
	partial class UIMenuViewController : UIViewController
	{
		public UIMenuViewController (IntPtr handle) : base (handle)
		{
		}

		partial void btnCode_TouchUpInside (UIButton sender)
		{
			var vc = new CodeViewController ();

			this.NavigationController.PushViewController(vc, true);
		}
	}
}
