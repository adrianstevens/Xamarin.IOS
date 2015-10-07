using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;

namespace MovableButtons
{
	public partial class MovableButtonsViewController : UIViewController
	{
		UIView movingView = null;

		public MovableButtonsViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			swMovable.ValueChanged += SwMoveable_ValueChanged;

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
			UIView.Animate (0.25, () => btnShake.Transform = CGAffineTransform.MakeTranslation(10, 0),
				() => {
					UIView.Animate (0.25, () => btnShake.Transform = CGAffineTransform.MakeTranslation(-10, 0),
						()=> {  
							UIView.Animate (0.25, () => btnShake.Transform = CGAffineTransform.MakeTranslation(0, 0),
								()=> {  });
						});
				}
			);
		}

		void BtnSwell_TouchUpInside (object sender, EventArgs e)
		{
			UIView.Animate (0.5, () => btnSwell.Transform = CGAffineTransform.MakeScale(1.5f, 1.5f),
				() => {
					UIView.Animate (0.25, () => btnSwell.Transform = CGAffineTransform.MakeScale(1, 1),
						()=> {  });
				}
			);
		}

		void BtnFade_TouchUpInside (object sender, EventArgs e)
		{
			UIView.Animate (1, () => btnFade.Alpha = 0,
				() => {
					UIView.Animate (1, () => btnFade.Alpha = 1,
						()=> {  });
				}
			);
		}		

		void SwMoveable_ValueChanged (object sender, EventArgs e)
		{
			if (swMovable.On == false) {
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

