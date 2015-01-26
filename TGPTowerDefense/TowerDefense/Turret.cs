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
	
	
	public class Turret
	{
		private SpriteUV 				sprite;
		private TextureInfo				spriteTex;
		private turretStats				stats;
		private int						level;
		private int						spentGold;
		
		
		public Turret ()
		{
			spriteTex = new TextureInfo("Application/graphics/FOE.png");
			sprite = new SpriteUV(spriteTex);
			stats = new turretStats();
		}
		
		public void Update()
		{
			
		}
		
		public turretStats getStats()
		{
			return stats;
		}
		
		public void setStats(int level)
		{
			if(level == 0)
			{
				stats.damage = 10;
				stats.fireRate = 50;
			}
		}
		
		public int getLevel()
		{
			return 0;
		}
		
		
		
		
		
	}
}

