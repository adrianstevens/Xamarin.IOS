using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

//http://kingscocoa.com/tutorials/autolayout-animations/
using MonoTouch.CoreGraphics;

namespace AdvancedIOSUI
{
	partial class AnimateViewController : UIViewController
	{
		UIImageView imgXamEvolve;

		bool isOpen = false;

		public AnimateViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();



			imgXamEvolve = new UIImageView (UIImage.FromFile ("evolve-logo.png").Scale(new System.Drawing.SizeF(170, 81)));
			imgXamEvolve.SetSize (imgXamEvolve.Image.Size);
			imgXamEvolve.Center = this.View.Center;

			this.View.Add (imgXamEvolve);
		}

		partial void btnAnimate_TouchUpInside (UIButton sender)
		{

			SetTopConstraintConstant(isOpen?-420:0);

			AnimateConstaints ();

			AnimateLogo ();

			isOpen = !isOpen;

			btnAnimate.SetTitle(isOpen?"Open":"Close", UIControlState.Normal);
		}

		void SetTopConstraintConstant (int constant)
		{
			//update the constraint
			foreach (NSLayoutConstraint c in this.View.Constraints) {
				if (c.SecondItem == this.View &&
				   c.FirstAttribute == NSLayoutAttribute.Top)
					c.Constant = constant;
			}
		}

		void AnimateConstaints ()
		{
			UIView.Animate (0.5, () => 
			{
				this.View.LayoutIfNeeded();
			});
		}

		void AnimateLogo ()
		{
			if (isOpen) 
			{
				imgXamEvolve.JumpIn ();

			} 
			else 
			{
				imgXamEvolve.JumpOut ();
			}


		}
	}
}
