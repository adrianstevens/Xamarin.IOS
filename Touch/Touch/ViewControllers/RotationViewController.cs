using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;
using System.Diagnostics;

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

			var doubleTapGesture = new UITapGestureRecognizer ();
			doubleTapGesture.NumberOfTapsRequired = 2;
			doubleTapGesture.AddTarget (() => {
				Debug.WriteLine("double tap");
			});

			imgLogo.AddGestureRecognizer (doubleTapGesture);
		}

		void HandleRotation(UIRotationGestureRecognizer gesture)
		{
			imgLogo.Transform = CGAffineTransform.MakeRotation (gesture.Rotation);
		}
	}
}
