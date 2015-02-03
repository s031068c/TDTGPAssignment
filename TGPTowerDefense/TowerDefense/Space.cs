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
	public class Space
	{
		private SpriteUV 				selectSp;
		private TextureInfo				selectSpTex;
		private SpriteUV 				tileSp;
		private TextureInfo				tileSpTex;
		private Turret					tr;
		private Waypoint				way;
		private int 					spaceType;
		
		public Space (Scene scene, Vector2 pos, int spT)
		{
			
			selectSpTex = new TextureInfo("Application/graphics/Space.png");
			selectSp = new SpriteUV(selectSpTex);
			selectSp.Position = pos;
			selectSp.Quad.S 	= selectSpTex.TextureSizef;
			selectSp.Color.A = 0.0f;
			spaceType = spT;
			setTile();
			tileSp.Position = pos;
			tileSp.Quad.S 	= tileSpTex.TextureSizef;
			
			
			//add the Space to the scene
			scene.AddChild(tileSp);
			scene.AddChild(selectSp);

		}
		
		public void Update(float t)
		{
			if(tr != null)
			{
				tr.Update(t);
			}
		}
		
		public bool isFull()
		{
			return true;
		}
		
		public turretStats getStats()
		{
			return tr.getStats();
		}
		
		public void setTurret(Turret t)
		{
			tr = t;
		}
		
		public void setSelected()
		{
			//set alpha to 0.5f on press 
			selectSp.Color.A = 0.5f;
			
		}
		
		//0 = Empty/turret space, 1-8 = Wall, 9 = ground, 10-13 waypoints
		public int getType()
		{
			return spaceType;
		}
		
		public int getWayDir()
		{
			if(way != null)
			{
				return way.getDir();
			} else 
			{
				return -1;
			}
		}
		
		public void unSelect()
		{
			//set alpha to 0.0f when not pressed
			selectSp.Color.A = 0.0f;
			
		}
		
		public int getX()
		{
			return (int)selectSp.Position.X;
		}
		
		public int getY()
		{
			return (int)selectSp.Position.Y;
		}
		
		public int getWidth()
		{
			return (int)selectSp.TextureInfo.Texture.Width;
		}
		
		public int getHeight()
		{
			return (int)selectSp.TextureInfo.Texture.Height;
		}
		
		public int getWX()
		{
			return (int)way.getPos().X;
		}
		
		public int getWY()
		{
			return (int)way.getPos().Y;
		}
		
		public int getWWidth()
		{
			return way.getWidth();
		}
		
		public int getWHeight()
		{
			return way.getHeight();
		}
		
		
		
		private void setTile()
		{
			if(spaceType == 10)
			{
				tileSpTex = new TextureInfo("Application/graphics/Empty.png");
				tileSp = new SpriteUV(tileSpTex);
			}else if (spaceType == 11)
			{
				tileSpTex = new TextureInfo("Application/graphics/VertWall.png");
				tileSp = new SpriteUV(tileSpTex);
				
			}else if (spaceType == 12)
			{
				tileSpTex = new TextureInfo("Application/graphics/HozWall.png");
				tileSp = new SpriteUV(tileSpTex);
				
			}else if (spaceType == 13)
			{
				tileSpTex = new TextureInfo("Application/graphics/RCorner.png");
				tileSp = new SpriteUV(tileSpTex);
				
			}else if (spaceType == 14)
			{
				tileSpTex = new TextureInfo("Application/graphics/Lcorner.png");
				tileSp = new SpriteUV(tileSpTex);
				
			}else if (spaceType == 15)
			{
				tileSpTex = new TextureInfo("Application/graphics/IRCorner.png");
				tileSp = new SpriteUV(tileSpTex);
				
			}else if (spaceType == 16)
			{
				tileSpTex = new TextureInfo("Application/graphics/ILCorner.png");
				tileSp = new SpriteUV(tileSpTex);
				
			}else if (spaceType == 19)
			{
				tileSpTex = new TextureInfo("Application/graphics/Ground.png");
				tileSp = new SpriteUV(tileSpTex);
				
			}else if (spaceType == 20)
			{
				tileSpTex = new TextureInfo("Application/graphics/Ground.png");
				tileSp = new SpriteUV(tileSpTex);
				way = new Waypoint(0, selectSp.Position);
				
			}else if (spaceType == 21)
			{
				tileSpTex = new TextureInfo("Application/graphics/Ground.png");
				tileSp = new SpriteUV(tileSpTex);
				way = new Waypoint(1, selectSp.Position);
				
			}else if (spaceType == 22)
			{
				tileSpTex = new TextureInfo("Application/graphics/Ground.png");
				tileSp = new SpriteUV(tileSpTex);
				way = new Waypoint(2, selectSp.Position);
				
			}else if (spaceType == 23)
			{
				tileSpTex = new TextureInfo("Application/graphics/Ground.png");
				tileSp = new SpriteUV(tileSpTex);
				way = new Waypoint(3, selectSp.Position);
			}
		}
		
		private void Dispose()
		{
			selectSpTex.Dispose();
			tileSpTex.Dispose();
		}
		
	}
}

