using System;
using UIKit;

namespace CustomCell
{
	public class CustomCell : UITableViewCell
	{
		UIView background = new UIView();
		UILabel title = new UILabel();

		public CustomCell (IntPtr ptr) : base (ptr)
		{
			background.BackgroundColor = UIColor.Orange;

			title.TranslatesAutoresizingMaskIntoConstraints = false;
			background.TranslatesAutoresizingMaskIntoConstraints = false;

			this.ContentView.Add (background);
			this.ContentView.Add (title);

			SetConstraints();
		}

		void SetConstraints ()
		{
			//constrain the label to the background 
			var leftConstLbl = NSLayoutConstraint.Create (title, NSLayoutAttribute.Left,
				NSLayoutRelation.Equal,
				background, NSLayoutAttribute.Left,
				1, 10);

			var topConstLbl = NSLayoutConstraint.Create (title, NSLayoutAttribute.Top,
				NSLayoutRelation.Equal,
				background, NSLayoutAttribute.Top,
				1, 5);


			//let's add some constraints for the background view
			var leftConst = NSLayoutConstraint.Create (background, NSLayoutAttribute.Left,
				NSLayoutRelation.Equal,
				this.ContentView, NSLayoutAttribute.Left,
				1, 10);

			var rightConst = NSLayoutConstraint.Create (background, NSLayoutAttribute.Right,
				NSLayoutRelation.Equal,
				this.ContentView, NSLayoutAttribute.Right,
				1, -10);

			var topConst = NSLayoutConstraint.Create (background, NSLayoutAttribute.Top,
				NSLayoutRelation.Equal,
				this.ContentView, NSLayoutAttribute.Top,
				1, 10);

			var bottomConst = NSLayoutConstraint.Create (background, NSLayoutAttribute.Bottom,
				NSLayoutRelation.Equal,
				this.ContentView, NSLayoutAttribute.Bottom,
				1, -10);

			this.ContentView.AddConstraint (leftConstLbl);
			this.ContentView.AddConstraint (topConstLbl);

			this.ContentView.AddConstraints (new NSLayoutConstraint[] {
				leftConst, rightConst, topConst, bottomConst
			});
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			Console.WriteLine (background.Frame);
		}

		public void SetTitle (string text)
		{
			title.Text = text;
		}
	}
}

