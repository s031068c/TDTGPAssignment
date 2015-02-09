using System;
using System.Collections.Generic;


using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace TowerDefense
{
	public class Enemy
	{
		
		private SpriteUV 				sprite;
		private TextureInfo				spriteTex;
		private Vector2 				pos;
		private Vector2 				turnPos;
		private int 					dir, newDir, gold, health, colTimer, colTimerMax;
		private float					speed;
		private bool					turning;
		
		
		public Enemy (Scene scene, Vector2 p, int d, Random rand)
		{
			spriteTex = new TextureInfo("Application/graphics/FOE.png");
			sprite = new SpriteUV(spriteTex);
			sprite.Position = p;
			sprite.Quad.S 	= spriteTex.TextureSizef;
			health = 10;
			speed = 1.0f;
			dir = d;
			gold = 50;
			colTimerMax = rand.Next(32, 64);
			colTimer = colTimerMax;
			turning = false;
			scene.AddChild(sprite);

			
		}
		
		public void Update(float t)
		{
			
			pos = sprite.Position;
			if(health <= 0)
			{
				Dispose();
			}
			if(dir == 0)
			{
				pos = new Vector2(pos.X  -= speed, pos.Y);
			}else if (dir == 1)
			{
				pos = new Vector2(pos.X  += speed, pos.Y);
			}else if (dir == 2)
			{
				pos = new Vector2(pos.X, pos.Y -= speed);
			}else if (dir == 3)
			{
				pos = new Vector2(pos.X, pos.Y+= speed);
			}
			if(turning)
			{
				checkTurn ();
			}
			
			sprite.Position = new Vector2(pos.X, pos.Y);
			
		}
		
		private void checkTurn()
		{
			if(dir == 0)
			{
				if(pos.X <= turnPos.X - colTimerMax)
				{
					dir = newDir;
					turning = false;
				}
			} else if (dir == 1)
			{
				if(pos.X >= turnPos.X + colTimerMax)
				{
					dir = newDir;
					turning = false;
				}
			}else if (dir == 2)
			{
				if(pos.Y <= turnPos.Y - colTimerMax)
				{
					dir = newDir;
					turning = false;
				}
				
			}
		}
		
		public void randDelay(Random rand)
		{
			colTimerMax = rand.Next (32, 64);
			turnPos = pos;
			
		}

		
		public void setDirection(int d)
		{
			turning = true;
			newDir = d;
		}
		
		public void takeDamage(int damage)
		{
			health -= damage;
		}
		
		public Vector2 getPos()
		{
			return pos;
		}
		
		public int getHeight()
		{
			return sprite.TextureInfo.Texture.Width;
		}
		
		public int getWidth()
		{
			return sprite.TextureInfo.Texture.Height;
		}
		
		public Vector2 GetCenter()
		{
			Vector2 ret;
			ret.X = pos.X + getWidth()*0.5f;
			ret.Y = pos.Y + getHeight()*0.5f;
			return ret;
		}
		
		public void Dispose()
		{
			spriteTex.Dispose();
			
		}
		
		
		
	}
}

