using System;
using System.Drawing;

using Foundation;
using UIKit;
using System.Timers;
using CoreGraphics;

namespace PongIOS
{
	public partial class PongIOSViewController : UIViewController
	{
		UIView paddleUser;

		UIView ball;

		static int paddleIndent = 30;

		static int paddleHeight = 24;
		static int paddleWidth = 80;

		static int ballSize = 5;

		//ball parameters
		float ballXVel = 2;
		float ballYVel = 5;

		bool bPaddleTouched = false;

		Random rand = new Random();

		//Timer
		Timer gameLoop;

		public PongIOSViewController (IntPtr handle) : base (handle)
		{
			gameLoop = new Timer (20);//50fps
			gameLoop.Start ();
			gameLoop.Elapsed += GameLoopTimer;
		}

		void ResetBall ()
		{
			//ball.Frame = new CoreGraphics.CGRect (this.View.Center, new CoreGraphics.CGSize (ballSize, ballSize));
			ball.Center = new CGPoint(this.View.Center.X, 0);

			ballYVel = rand.Next () % 5 + 2;
			ballXVel = 9 - ballYVel;

		}

		void Explode ()
		{
			paddleUser.Alpha = 0;
			paddleUser.Transform = CGAffineTransform.MakeScale(0.001f, 0.001f);

			ball.Alpha = 0;
			ball.Transform = CGAffineTransform.MakeScale(0.001f, 0.001f);

			UIView.Animate (1, () => {
				paddleUser.Alpha = 1;
				paddleUser.Transform = CGAffineTransform.MakeScale(1, 1);

				ball.Alpha = 1;
				ball.Transform = CGAffineTransform.MakeScale(1, 1);
			});
		}



		//update our positions
		void GameLoopTimer (object sender, ElapsedEventArgs e)
		{
			InvokeOnMainThread (() => {

				//update our ball
				var xNewPos = ballXVel + ball.Frame.X;
				var yNewPos = ballYVel + ball.Frame.Y;

				//X direction walls
				if (xNewPos + ball.Frame.Width >= this.View.Frame.Width) {
					xNewPos -= ballXVel;
					ballXVel *= -1;
				} else if (xNewPos < 0) { //we're assuming the velocity is negative
					xNewPos -= ballXVel;
					ballXVel *= -1;
				}

				//Y direction
				if (yNewPos + ball.Frame.Height > paddleUser.Frame.Y &&
					yNewPos + ball.Frame.Height < paddleUser.Frame.Y + ballYVel &&
					ball.Center.X > paddleUser.Frame.Left && ball.Center.X < paddleUser.Frame.Right)
				{
					yNewPos -= ballYVel;
					ballYVel *= -1;
				}

				else if (yNewPos + ball.Frame.Height >= this.View.Frame.Height) 
				{
					Explode ();
					ResetBall ();
					return;
				} else if (yNewPos < 0) { //we're assuming the velocity is negative
					yNewPos -= ballYVel;
					ballYVel *= -1;
				}

				//finally update the frame
				ball.Frame = new CoreGraphics.CGRect (xNewPos, yNewPos, ballSize, ballSize);
			});
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			CreateControls ();
			AlignControls ();
		}

		void CreateControls ()
		{
			paddleUser = new UIView (new RectangleF (0, 0, paddleWidth, paddleHeight));
			paddleUser.BackgroundColor = UIColor.Blue;

			ball = new UIView (new RectangleF(0, 0, ballSize, ballSize));
			ball.BackgroundColor = UIColor.Red;


			//add controls
			this.Add (paddleUser);
			this.Add (ball);
		}

		void AlignControls ()
		{
			//set our paddle location
			paddleUser.Frame = new CoreGraphics.CGRect (this.View.Frame.Width / 2 - paddleUser.Frame.Width / 2, 
				this.View.Frame.Height - paddleIndent - paddleUser.Frame.Height,
				paddleUser.Frame.Width, paddleUser.Frame.Height);

			//the ball
			ball.Center = View.Center;

		}

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);

			var touch = touches.AnyObject as UITouch;
			if (touch == null)
				return;

			if (paddleUser.Frame.Contains (touch.LocationInView (View))) {
				bPaddleTouched = true;
			}
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);

			bPaddleTouched = false;
		}

		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			base.TouchesMoved (touches, evt);

			var touch = touches.AnyObject as UITouch;
			if (touch == null)
				return;

			if (bPaddleTouched == false)
				return;

			var xOffset = touch.LocationInView (View).X - touch.PreviousLocationInView (View).X;
		//	float yOffset = touch.LocationInView (View).Y - touch.PreviousLocationInView (View).Y;

			var xNew = Math.Max (0, xOffset + paddleUser.Frame.Left);
			xNew = (Math.Min (this.View.Frame.Width - paddleWidth, xNew));

			paddleUser.Frame = new CoreGraphics.CGRect ((nfloat)xNew, paddleUser.Frame.Top,
				paddleUser.Frame.Width, paddleUser.Frame.Height);
		}

		public override void TouchesCancelled (NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled (touches, evt);

			bPaddleTouched = false;
		}









	

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion
	}
}

