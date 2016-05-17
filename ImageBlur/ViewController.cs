using System;

using UIKit;

namespace ImageBlur
{
	public partial class ViewController : UIViewController
	{
		UIVisualEffectView blurView;

		UIImageView imgBackground;

		UIImageView[] images = new UIImageView[5]; 

		protected ViewController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			InitImages();

		}

		void InitImages()
		{
			imgBackground = new UIImageView(UIImage.FromFile("pic1.jpg"));
			imgBackground.Frame = this.View.Bounds;
			imgBackground.ContentMode = UIViewContentMode.ScaleAspectFill;

			//blur effect
			var blur = UIBlurEffect.FromStyle(UIBlurEffectStyle.Light);
			blurView = new UIVisualEffectView(blur);
			blurView.Frame = imgBackground.Bounds;
			imgBackground.Add(blurView);

			this.Add(imgBackground);

			int yStart = 50;

			//create clickable images
			for (int i = 0; i < images.Length; i++)
			{
				images[i] = new UIImageView();
				images[i].ContentMode = UIViewContentMode.ScaleAspectFill;
				images[i].ClipsToBounds = true;
				images[i].Frame = new CoreGraphics.CGRect(20, yStart + i * 100, 80, 80);
				images[i].UserInteractionEnabled = true;

				var imgName = "pic" + (i + 1) + ".jpg";
				images[i].Image = UIImage.FromFile(imgName);

				this.View.Add(images[i]);

				images[i].AddGestureRecognizer(new UITapGestureRecognizer(OnImageTap));
			}
		}

		void OnImageTap(UIGestureRecognizer gesture)
		{
			var imgView = gesture.View as UIImageView;

			if (imgView == null)
				return;

			for (int i = 0; i < images.Length; i++)
			{
				if (images[i] == imgView)
				{
					var imgName = "pic" + (i + 1) + ".jpg";
					imgBackground.Image.Dispose();
					imgBackground.Image = UIImage.FromFile(imgName);
					break;
				}
			}
		}

		public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillRotate(toInterfaceOrientation, duration);
		}

		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate(fromInterfaceOrientation);

			imgBackground.Frame = blurView.Frame = View.Bounds;
		}
	}
}

