using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace AdvancedIOSUI
{
	partial class ViewAnimationsVC : UIViewController
	{
		SimpleParticleGen particles;

		public ViewAnimationsVC (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.View.BackgroundColor = UIColor.Black;

 			particles = new SimpleParticleGen (UIImage.FromFile ("Icon-29.png"), this.View, this.View.Center);

			particles.Start ();
		}
	}
}
