using System;
using System.Collections.Generic;


using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;

namespace TowerDefense
{
	public class Waypoint
	{
		private int direction;
		private Vector2 pos;
		private int height;
		private int width;
		//0 = Left, 1 = Right, 2 = Down, 3 = Up
		
		public Waypoint (int dir, Vector2 p)
		{
			direction = dir;
			pos = new Vector2(p.X, p.Y);
			if(dir == 0 || dir == 1)
			{
				width = 32;
				height = 32;
			} else if (dir == 2 || dir == 3)
			{
				width = 32;
				height = 32;
			}
			
		}
		
		
		public int getDir()
		{
			return direction;
		}
		
		public int getHeight()
		{
			return height;
		}
		
		public int getWidth()
		{
			return width;	
		}
		
		public Vector2 getPos()
		{
			return pos;
		}
		
		
	}
}

