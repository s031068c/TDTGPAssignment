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
		
		
		
		public Enemy (Scene scene, System.Random rand)
		{
			spriteTex = new TextureInfo("Application/graphics/Enemy.png");
			sprite = new SpriteUV(spriteTex);
		}
	}
}

