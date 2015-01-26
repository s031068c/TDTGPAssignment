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
	public class Waypoint
	{
		private int direction;
		//0 = Left, 1 = Right, 2 = Down, 3 = Up
		
		public Waypoint (int dir)
		{
			direction = dir;
		}
		
		
		public int getDir()
		{
			return direction;
		}
	}
}

