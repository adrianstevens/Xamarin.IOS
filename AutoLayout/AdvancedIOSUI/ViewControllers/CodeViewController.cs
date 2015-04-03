using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace AdvancedIOSUI
{
	//http://blog.xamarin.com/autolayout-with-xamarin.mac/
	//http://www.raywenderlich.com/20881/beginning-auto-layout-part-1-of-2

	//http://forums.xamarin.com/discussion/6570/auto-layout-and-view-constraints

	public class CodeViewController : UIViewController
	{
		UIButton myButton;



		public CodeViewController ()
		{

		
		}	

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.View.BackgroundColor = UIColor.White;

			AddViews ();

			CreateButton ();
			AddConstraints ();
		}

		void VisualFormat ()
		{
		/*	this.View.AddConstraint(NSLayoutConstraint.FromVisualFormat(@"V:[view1]-8-[view2]",
															NSLayoutFormatOptions.AlignAllLeading,
				null, NSDictionaryOfVariableBindings(view1, view2));
*/

		}

		void CreateButton ()
		{
			//create a button but don't set the frame 
			myButton = UIButton.FromType (UIButtonType.RoundedRect);
			myButton.BackgroundColor = UIColor.DarkGray;
			myButton.SetTitleColor (UIColor.White, UIControlState.Normal);
			myButton.SetTitle ("Click Me", UIControlState.Normal);

			myButton.Frame = new System.Drawing.RectangleF (10, 100, 100, 100);

			myButton.TranslatesAutoresizingMaskIntoConstraints = false;

			myButton.TouchUpInside += OnMyButtonClick;


			this.Add (myButton);


		}

		void OnMyButtonClick (object sender, EventArgs e)
		{
			//let's add some constraints



		}

		void AddViews ()
		{
			var view1 = new UIView () { BackgroundColor = UIColor.Red, TranslatesAutoresizingMaskIntoConstraints = false };
			var view2 = new UIView () { BackgroundColor = UIColor.Green, TranslatesAutoresizingMaskIntoConstraints = false };

			// View-level constraints to set constant size values
			view1.AddConstraints (new[] {
				NSLayoutConstraint.Create (view1, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 50),
				NSLayoutConstraint.Create (view1, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 80),
			});
			view2.AddConstraints (new[] {
				NSLayoutConstraint.Create (view2, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 50),
				NSLayoutConstraint.Create (view2, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 80),
			});


			this.View.AddSubview (view1);
			this.View.AddSubview (view2);
			// Container view-level constraints to set the position of each subview
			this.View.AddConstraints (new[] {
				NSLayoutConstraint.Create (view1, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 100, 10),
				NSLayoutConstraint.Create (view1, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View, NSLayoutAttribute.Top, 1, 80),
				NSLayoutConstraint.Create (view2, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1010, 10),
				NSLayoutConstraint.Create (view2, NSLayoutAttribute.Top, NSLayoutRelation.Equal, view1, NSLayoutAttribute.Bottom, 1, 5),
			});



		}

	

		void AddConstraints ()
		{
			var constraintCenterX = NSLayoutConstraint.Create (
							                 	myButton,
												NSLayoutAttribute.CenterX, 
												NSLayoutRelation.Equal, 
												View, 
												NSLayoutAttribute.CenterX, 
												1, 0);


			this.View.AddConstraint (constraintCenterX);

			var constraintCenterY = NSLayoutConstraint.Create (
				myButton, 
				NSLayoutAttribute.CenterY, 
				NSLayoutRelation.Equal, 
				View, 
				NSLayoutAttribute.CenterY, 
				1, 0);


			this.View.AddConstraint (constraintCenterY);



			//set the button width 
			var widthConstraint = NSLayoutConstraint.Create (
				                      myButton,
				                      NSLayoutAttribute.Width,
				                      NSLayoutRelation.Equal,
				                      null,
				                      NSLayoutAttribute.NoAttribute,
				                      multiplier: 1,
				                      constant: 100);

			myButton.AddConstraint (widthConstraint);


			//set the button height 
			var heightConstraint = NSLayoutConstraint.Create (
				myButton,
				NSLayoutAttribute.Height,
				NSLayoutRelation.Equal,
				null,
				NSLayoutAttribute.NoAttribute,
				multiplier: 1,
				constant: 40);

			myButton.AddConstraint (heightConstraint);




		}
	}
}
