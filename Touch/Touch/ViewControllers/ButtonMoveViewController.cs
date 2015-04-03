using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;

namespace Touch
{
	partial class ButtonMoveViewController : UIViewController
	{
		UIView movingView = null;

		public ButtonMoveViewController (IntPtr handle) : base (handle)
		{




		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			swMoveable.ValueChanged += SwMoveable_ValueChanged;

			btnFade.TouchUpInside += BtnFade_TouchUpInside;
			btnShake.TouchUpInside += BtnShake_TouchUpInside;
			btnSwell.TouchUpInside += BtnSwell_TouchUpInside;
		}

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);

			// get the touch
			var touch = touches.AnyObject as UITouch;
			if (touch == null) 
				return;

			if (btnFade.Frame.Contains (touch.LocationInView (View)))
				movingView = btnFade;
			else if (btnShake.Frame.Contains (touch.LocationInView (View)))
				movingView = btnShake;
			else if (btnSwell.Frame.Contains (touch.LocationInView (View)))
				movingView = btnSwell;
			else
				movingView = null;
		}

		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			base.TouchesMoved (touches, evt);

			// get the touch
			var touch = touches.AnyObject as UITouch;

			if (touch == null || movingView == null) 
				return;

			// get the delta
			nfloat deltaX = touch.PreviousLocationInView(View).X - touch.LocationInView(View).X;
			nfloat deltaY = touch.PreviousLocationInView(View).Y - touch.LocationInView(View).Y;

			var newPoint = new CGPoint (movingView.Frame.X - deltaX, movingView.Frame.Y - deltaY);

			movingView.Frame = new CGRect(newPoint, movingView.Frame.Size);
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);

			movingView = null;
		}

		public override void TouchesCancelled (NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled (touches, evt);

			movingView = null;
		}

		void BtnShake_TouchUpInside (object sender, EventArgs e)
		{
			btnShake.Enabled = false;

			UIView.Animate (0.25, () => btnShake.Transform = CGAffineTransform.MakeTranslation(10, 0),
				() => {
					UIView.Animate (0.25, () => btnShake.Transform = CGAffineTransform.MakeTranslation(-10, 0),
						()=> {  
							UIView.Animate (0.25, () => btnShake.Transform = CGAffineTransform.MakeTranslation(0, 0),
								()=> { btnShake.Enabled = true; });
						});
				}
			);
		}

		void BtnSwell_TouchUpInside (object sender, EventArgs e)
		{
			btnSwell.Enabled = false;

			UIView.Animate (0.5, () => btnSwell.Transform = CGAffineTransform.MakeScale(1.5f, 1.5f),
				() => {
					UIView.Animate (0.25, () => btnSwell.Transform = CGAffineTransform.MakeScale(1, 1),
						()=> { btnSwell.Enabled = true; });
				}
			);
		}

		void BtnFade_TouchUpInside (object sender, EventArgs e)
		{
			btnFade.Enabled = false;

			UIView.Animate (1, () => btnFade.Alpha = 0,
				() => {
					UIView.Animate (1, () => btnFade.Alpha = 1,
						()=> { btnFade.Enabled = true; });
				}
			);
		}		

		void SwMoveable_ValueChanged (object sender, EventArgs e)
		{
			if (swMoveable.On == false) {
				btnFade.UserInteractionEnabled = false;
				btnShake.UserInteractionEnabled = false;
				btnSwell.UserInteractionEnabled = false;

			} else {
				btnFade.UserInteractionEnabled = true;
				btnShake.UserInteractionEnabled = true;
				btnSwell.UserInteractionEnabled = true;
			}
		}
	}
}
