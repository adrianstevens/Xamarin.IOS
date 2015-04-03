using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;
using System.Diagnostics;

namespace AdvancedIOSUI
{
	public class SimpleParticleGen
	{
		UIImage imgParticle;
		UIView parent;
		PointF location;

		public float durrationMin { get; set; }
		public float durrationMax { get; set; }
		public float delayMin { get; set; }
		public float delayMax { get; set; }
		public float immortalDelay { get; set; }

		public float scaleMin { get; set; }
		public float scaleMax { get; set; }
		public float scaleStart { get; set; }

		public float alphaStart { get; set; }
		public float alphaFinal { get; set; }

		public int numberOfParticles { get; set; }

		public int distanceMin { get; set; }
		public int distanceMax { get; set; }

		public bool destroyParticles { get; set; }
		public bool immortalParticles { get; set; }

		private bool isRunning;

		Random random = new Random( System.DateTime.Now.Second );

		public SimpleParticleGen (UIImage imageParticle, UIView parent, PointF position)
		{
			this.imgParticle = imageParticle;
			this.parent = parent;
			this.location = position;

			SetDefaults ();
		}

		public void SetDefaults ()
		{
			numberOfParticles = 250;
			durrationMin = 1;
			durrationMax = 3;
			delayMin = 0;
			delayMax = 15;
			immortalDelay = 0.5f; //time between immortal particles

			scaleMin = 0.25f;
			scaleMax = 1.0f;
			scaleStart = 0.01f;

			distanceMin = 50;
			distanceMax = 140;

			alphaStart = 1;
			alphaFinal = 0;

			destroyParticles = true;
			immortalParticles = false;
		}

		PointF GetRandomPosition(float scale)
		{
			//get random angle (radians) and distance
			double radius = random.Next(distanceMin, distanceMax);
			double angle = random.NextDouble () * 2 * Math.PI;  

			//convert to cartisian
			float xPos = (float)(radius * Math.Cos(angle));
			float yPos = (float)(radius * Math.Sin(angle));

			//offset for upper left origin
			xPos -= (scaleStart)*imgParticle.Size.Width / 2;
			yPos -= (scaleStart)*imgParticle.Size.Height / 2;

			return new PointF (xPos + location.X, yPos + location.Y); 
		}

		float GetRandomScale()
		{
			if(scaleMin == scaleMax)
				return scaleMax;

			return (float)random.NextDouble() * (scaleMax - scaleMin) + scaleMin;
		}

		void NewParticle (PointF startPosition, Action complete )
		{
			float scale = 1;

			var particle = new UIImageView (imgParticle) 
			{
				Alpha = alphaStart,
			};

			if(scaleStart != 1.0f)
			{
				particle.Transform = CGAffineTransform.MakeScale(scaleStart, scaleStart);
			}

			particle.SetLocation ( startPosition );

			parent.Add (particle);

			UIView.Animate (random.NextDouble () * (durrationMax - durrationMin) + durrationMin, random.NextDouble () * (delayMax - delayMin) + delayMin, 
				UIViewAnimationOptions.CurveEaseOut, 
				() => 
				{
					scale = GetRandomScale ();

					particle.SetLocation (GetRandomPosition (scale));
					if (scaleStart != scale)
					{
						particle.Transform = CGAffineTransform.MakeScale (scale, scale);
					}
					if (alphaFinal != alphaStart) 
					{
						particle.Alpha = alphaFinal;
					}
				}, 
				() => {
					if (destroyParticles == true) 
					{
						particle.RemoveFromSuperview ();
						particle = null;
					}
					if (complete != null)
						complete ();
				});

		}

		void StartImmortalParticle ()
		{
			isRunning = true;

			float xOffSet = imgParticle.Size.Width*scaleStart/2;
			float yOffSet = imgParticle.Size.Height*scaleStart/2;

			var startLoc = new PointF(location.X - xOffSet, location.Y - yOffSet);

			for(int i = 0; i < numberOfParticles; i++)
				GetImmortalParticle(startLoc);
		}

		void GetImmortalParticle (PointF startLoc)
		{
			NewParticle (startLoc, () => 
			{
				if (isRunning == true)
				{
					GetImmortalParticle (startLoc);
				}
			});
		}

		public void Start ()
		{
			if(immortalParticles == true)
			{
				StartImmortalParticle();
				return;
			}

			float xOffSet = imgParticle.Size.Width*scaleStart/2;
			float yOffSet = imgParticle.Size.Height*scaleStart/2;

			var startLoc = new PointF(location.X - xOffSet, location.Y - yOffSet);

			for (int i = 0; i < numberOfParticles; i++)
			{
				NewParticle(startLoc, null);
			}
		}

		public void Stop ()
		{
			isRunning = false;
		}
	}
}