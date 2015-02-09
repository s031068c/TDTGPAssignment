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
	public class Bullet
	{
		private SpriteUV 				sprite;
		private TextureInfo				spriteTex;
		private float					damage, speed;
		private Vector2 				position;
		private Vector2					direction;
		private int						width, height;
		
		public Bullet (Scene scene, Vector2 pos, Vector2 dir)
		{
			spriteTex = new TextureInfo("Application/graphics/BULLET.png");
			sprite = new SpriteUV(spriteTex);
			sprite.Position = pos;
			position = pos;
			sprite.Quad.S 	= spriteTex.TextureSizef;
			
			//Set sprites center to the middle of the sprite
			sprite.CenterSprite(TRS.Local.Center);
			
			//Rotate sprite to face the (already normalized fomr the turret) direction
			sprite.Rotation = dir;
			
			width = sprite.TextureInfo.Texture.Width;
			height = sprite.TextureInfo.Texture.Height;
			direction = dir;
			speed = 3.0f;
			damage = 1.0f;
			scene.AddChild(sprite);
		}
		
		
		public void Update()
		{

			position -= direction * speed;
			sprite.Position = position;

		}
		
		public Vector2 getPos()
		{
			return position;
		}
		
		public void Dispose()
		{
			spriteTex.Dispose();
			sprite = null;
		}
		
		
		
	}
}

