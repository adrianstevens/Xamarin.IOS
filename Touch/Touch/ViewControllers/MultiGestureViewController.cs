using Foundation;
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;
using System.Diagnostics;

namespace Touch
{
	partial class MultiGestureViewController : UIViewController
	{
		nfloat rotation = 0;
		nfloat scale = 1;

		public MultiGestureViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//rotation
			var rotationGesture = new UIRotationGestureRecognizer (HandleRotation);
			this.View.AddGestureRecognizer (rotationGesture);

			//scale
			var pinchGesture = new UIPinchGestureRecognizer (HandlePinch);
			this.View.AddGestureRecognizer (pinchGesture);

			//enable simultanious gestures
			rotationGesture.ShouldRecognizeSimultaneously = ShouldRecognizeSimultaneously;
			pinchGesture.ShouldRecognizeSimultaneously = ShouldRecognizeSimultaneously;
		}

		public bool ShouldRecognizeSimultaneously (UIGestureRecognizer gestureRecognizer, UIGestureRecognizer otherGestureRecognizer)
		{
			return true;
		}

		void HandleRotation(UIRotationGestureRecognizer gesture)
		{
			this.rotation = gesture.Rotation;

			UpdateTransform ();
		}

		void HandlePinch (UIPinchGestureRecognizer gesture)
		{
			this.scale *= gesture.Scale;

			//bounds checking
			if (scale > 2.5f)
				scale = 2.5f;
			else if (scale < 0.01)
				scale = 0.1f;

			//Debug.WriteLine ("Scale {0} {1}", gesture.Scale, this.scale);

			// Reset the gesture recognizer's scale for next delta
			gesture.Scale = 1;

			UpdateTransform ();
		}

		void UpdateTransform ()
		{
			var transform = CGAffineTransform.MakeIdentity ();

			transform.Scale (scale, scale);
			transform.Rotate (rotation);

			imgLogo.Transform = transform;

		}
	}
}
