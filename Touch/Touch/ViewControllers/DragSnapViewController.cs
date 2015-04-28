using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;

namespace Touch
{
	partial class DragSnapViewController : UIViewController
	{
		bool imageIsMoving = false;

		public DragSnapViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			imgLogo.UserInteractionEnabled = true;
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();

			imgLogo.Center = this.View.Center;
		}

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);

			// get the touch
			var touch = touches.AnyObject as UITouch;

			if (touch == null) 
				return;

			if (imgLogo.Frame.Contains (touch.LocationInView (View)))
				imageIsMoving = true;
		}

		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			base.TouchesMoved (touches, evt);

			// get the touch
			var touch = touches.AnyObject as UITouch;

			if (touch == null || imageIsMoving == false) 
				return;

			// get the delta
			nfloat deltaX = touch.PreviousLocationInView(View).X - touch.LocationInView(View).X;
			nfloat deltaY = touch.PreviousLocationInView(View).Y - touch.LocationInView(View).Y;

			var newPoint = new CGPoint (imgLogo.Frame.X - deltaX, imgLogo.Frame.Y - deltaY);

			imgLogo.Frame = new CGRect(newPoint, imgLogo.Frame.Size);
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);

			// get the touch
			var touch = touches.AnyObject as UITouch;

			touch.MajorRadius;

			if (touch == null || imageIsMoving == false) 
				return;

			if (boxOne.Frame.Contains (imgLogo.Center) == true) 
			{
				UIView.Animate (0.5, () => {
					imgLogo.Center = boxOne.Center;
					imgLogo.Transform = CGAffineTransform.MakeScale(0.5f, 0.5f);
				});
			} 
			else if (boxTwo.Frame.Contains (imgLogo.Center) == true) 
			{
				UIView.Animate (0.5, () => {
					imgLogo.Center = boxTwo.Center;
					imgLogo.Transform = CGAffineTransform.MakeScale(0.5f, 0.5f);
				});
			} 
			else 
			{
				//reset the image
				UIView.Animate (0.25, () => {
					imgLogo.Center = this.View.Center;
					imgLogo.Transform = CGAffineTransform.MakeScale (1, 1);
				});
			}
			imageIsMoving = false;
		}

		public override void TouchesCancelled (NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled (touches, evt);

			imageIsMoving = false;
		}
	}
}
