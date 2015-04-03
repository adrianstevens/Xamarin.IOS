using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

public static class Extensions
{
	public static void debugBorder (this UIView view)
	{
		debugBorder(view, UIColor.Green);
	}

	public static void debugBorder (this UIView view, UIColor color)
	{
#if DEBUG
		view.Layer.BorderColor = color.CGColor;
		view.Layer.BorderWidth = 1;
#endif
	}

	public static void SetLocation(this UIView view, float left, float top)
	{
		if (view != null)
			view.Frame = new RectangleF (new PointF (left, top), view.Frame.Size);
	}

	public static void SetLocation(this UIView view, PointF point)
	{
		if(view != null)
			view.Frame = new RectangleF(point, view.Frame.Size);
	}

	public static void SetSize(this UIView view, float width, float height)
	{
		if(view != null)
			view.Frame = new RectangleF(view.Frame.Location, new SizeF(width, height));
	}

	public static void SetSize(this UIView view, SizeF size)
	{
		if(view != null)
			view.Frame = new RectangleF(view.Frame.Location, size);
	}

	public static void MoveTo(this UIView view, float right, float down)
	{
		if(view != null)
			SetLocation(view, view.Frame.Left + right, view.Frame.Top + down);
	}

	public static void Float(this UIView view, bool bUp, float distance, float duration, float delay)
	{
		float move = ( bUp?distance*(-1) : distance );

		UIView.Animate (duration, delay, UIViewAnimationOptions.CurveEaseInOut, () => view.Frame = new RectangleF (new PointF (view.Frame.Left, view.Frame.Top + move), view.Frame.Size), 
			() => Float (view, !bUp, distance, duration, delay));
	}

	public static void TagView (this UIView view, string tagText)
	{
		var lbl = new UILabel( view.Frame );

		lbl.BackgroundColor = UIColor.FromRGBA(255,255,255, 50);
		lbl.TextColor = UIColor.Green;
		lbl.Font = UIFont.SystemFontOfSize(16);
		lbl.TextAlignment = UITextAlignment.Center;
		lbl.Text = tagText; 
		lbl.ShadowColor = UIColor.FromRGB(70, 70, 70);
		lbl.ShadowOffset = new SizeF(1,1);

		view.Add (lbl);
	}

	public static void FadeIn(this UIView view)
	{
		if(view.Alpha == 1f)
			return;

		UIView.Animate (0.25f, 0, UIViewAnimationOptions.CurveLinear, 
			()=>  { view.Alpha = 1;	},
			()=> {});
	}

	public static void FadeOut(this UIView view, double delay, Action action)
	{
		UIView.Animate (0.25f, delay, UIViewAnimationOptions.CurveLinear, 
			() => view.Alpha = 0,
			() => {
				if (action != null)
					action ();
			});
	}

	public static void JumpIn(this UIView view, float delay = 0, Action action = null)
	{
		view.Transform = CGAffineTransform.MakeScale(0.001f, 0.001f);

		UIView.Animate (0.25f, delay, UIViewAnimationOptions.CurveLinear, 
			() => {
				view.Transform = CGAffineTransform.MakeScale (1.1f, 1.1f);
				view.Alpha = 1.0f;
			}, null);

		/*	() => UIView.Animate (0.2f, 0, UIViewAnimationOptions.CurveLinear, () => view.Transform = CGAffineTransform.MakeIdentity (), 
				() => {
					if (action != null)
					action ();
				}
			));*/
	}

	public static void JumpOut (this UIView view, double delay = 0, Action action = null)
	{
		UIView.Animate (0.25f, delay, UIViewAnimationOptions.CurveLinear, 
			() => {
				view.Transform = CGAffineTransform.MakeScale (0.01f, 0.01f);
				view.Alpha = 0;
			},
			() => {
				if (action != null)
					action ();
			});
	}

	public static void SizeFrameToText(this UILabel label)
	{
		if(label.Frame.IsEmpty || label.Font == null)
			return;

		SizeF size = label.StringSize(label.Text, label.Font, new SizeF(label.Frame.Width, 1000), label.LineBreakMode);

		label.Frame = new RectangleF(label.Frame.X, label.Frame.Y, label.Frame.Width, size.Height);
	}
}
