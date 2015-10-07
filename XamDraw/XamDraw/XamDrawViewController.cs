using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace XamDraw
{
	public partial class XamDrawViewController : UIViewController
	{
		static int MAX_TOUCHES = 12;//I believe max is 11

		UITouch[] fingers = new UITouch[MAX_TOUCHES];
		UIColor[] colors = new UIColor[MAX_TOUCHES];

		public XamDrawViewController (IntPtr handle) : base (handle)
		{
			
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		int AddTouch (UITouch touch)
		{
			for (int i = 0; i < MAX_TOUCHES; i++)
			{
				if (fingers [i] == null)
				{
					fingers [i] = touch;
					return i;
				}
			}

			Debug.WriteLine ("Can't track any additional fingers");

			return -1;
		}

		int GetTouchID(UITouch touch)
		{
			for (int i = 0; i < MAX_TOUCHES; i++) 
			{
				if (fingers [i] == touch)
					return i;
			}
			return -1;
		}

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			foreach (UITouch touch in touches) 
			{
				int id = GetTouchID (touch);

				if (id == -1)
				{
					id = AddTouch (touch);
					colors [id] = GetRandomColor ();
				}
			}
		}

		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			UIGraphics.BeginImageContext (imgDraw.Frame.Size);

			using (var g = UIGraphics.GetCurrentContext ())
			{
				imgDraw.Layer.RenderInContext (UIGraphics.GetCurrentContext ());

				foreach (UITouch touch in touches) 
				{
					var id = GetTouchID (touch);

					if (id == -1)
						continue;//should never happen

					var path = new CGPath ();

					path.AddLines (new PointF[] 
					{ 	
						touch.PreviousLocationInView(imgDraw), 
						touch.LocationInView(imgDraw) 
					});

					g.SetLineWidth (3);
					colors[id].SetStroke ();

					g.AddPath (path);
					g.DrawPath (CGPathDrawingMode.Stroke);

					imgDraw.Image = UIGraphics.GetImageFromCurrentImageContext ();
				}
			}
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			foreach (UITouch touch in touches) {
				int id = GetTouchID (touch);
				fingers [id] = null;
			}
		}

		public override void TouchesCancelled (NSSet touches, UIEvent evt)
		{
			TouchesEnded (touches, evt);
		}


		Random rand = new Random ();
		UIColor GetRandomColor ()
		{
			return new UIColor ((float)rand.NextDouble (), (float)rand.NextDouble (), (float)rand.NextDouble (), 1.0f);
		} 

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			this.View.MultipleTouchEnabled = true;
			this.View.UserInteractionEnabled = true;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			btnClear.TouchUpInside += BtnClear_TouchUpInside;
		}

		void BtnClear_TouchUpInside (object sender, EventArgs e)
		{
			imgDraw.Image = new UIImage ();					
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

