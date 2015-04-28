using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Diagnostics;
using System.Collections.Generic;

namespace FingerTracker
{
	public partial class FingerTrackerViewController : UIViewController
	{
		static int MAX_TOUCHES = 12;//I believe max is 11
		static int BOX_SIZE = 70;

		UITouch[] fingers = new UITouch[MAX_TOUCHES];

		Dictionary<int, UIView> views = new Dictionary<int, UIView>();

		Random rand = new Random ();

		public FingerTrackerViewController (IntPtr handle) : base (handle)
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


		UIColor GetRandomColor ()
		{
			return new UIColor ((float)rand.NextDouble (), (float)rand.NextDouble (), (float)rand.NextDouble (), 1.0f);
		} 

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			foreach (UITouch touch in touches) 
			{
				int id = GetTouchID (touch);

				if (id == -1)
				{
					id = AddTouch (touch);

					if (id != -1) 
					{
						views.Add(id, new UIView (new RectangleF (0, 0, BOX_SIZE, BOX_SIZE)) 
							{
								BackgroundColor = GetRandomColor (),
								Center = touch.LocationInView (this.View),
							}
						);

						this.View.Add (views [id]);
					}
				}
			}
		}

		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			foreach (UITouch touch in touches) 
			{
				int id = GetTouchID (touch);

				if (id != -1) 
				{
					UIView view = null;
					views.TryGetValue (id, out view);

					if(view != null)
						view.Center = touch.LocationInView (this.View);
				}
			}
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			foreach (UITouch touch in touches) 
			{
				int id = GetTouchID (touch);

				if (id != -1 && 
					(touch.Phase == UITouchPhase.Ended || touch.Phase == UITouchPhase.Cancelled))
				{
					fingers [id] = null;

					UIView view = null;
					views.TryGetValue (id, out view);
					views.Remove (id);

					UIView.Animate (2.0, () => view.Alpha = 0, 
						() => view.RemoveFromSuperview ());
				}
			}
		}

		public override void TouchesCancelled (NSSet touches, UIEvent evt)
		{
			TouchesEnded (touches, evt);
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			this.View.MultipleTouchEnabled = true;
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

