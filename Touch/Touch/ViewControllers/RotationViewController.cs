using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;

namespace Touch
{
	partial class RotationViewController : UIViewController
	{
		public RotationViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var rotationGesture = new UIRotationGestureRecognizer (HandleRotation);
			this.View.AddGestureRecognizer (rotationGesture);
		}

		void HandleRotation(UIRotationGestureRecognizer gesture)
		{
			imgLogo.Transform = CGAffineTransform.MakeRotation (gesture.Rotation);
		}
	}
}
