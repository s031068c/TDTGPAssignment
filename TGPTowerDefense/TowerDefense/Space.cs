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
		
		//0 = Empty/turret space, 1-8 = Wall, 9 = ground
		public int getType()
		{
			return spaceType;
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
		
		
		
		
		private void setTile()
		{
			if(spaceType == 0)
			{
				tileSpTex = new TextureInfo("Application/graphics/Empty.png");
				tileSp = new SpriteUV(tileSpTex);
			}else if (spaceType == 1)
			{
				tileSpTex = new TextureInfo("Application/graphics/VertWall.png");
				tileSp = new SpriteUV(tileSpTex);
				
			}else if (spaceType == 2)
			{
				tileSpTex = new TextureInfo("Application/graphics/HozWall.png");
				tileSp = new SpriteUV(tileSpTex);
				
			}else if (spaceType == 3)
			{
				tileSpTex = new TextureInfo("Application/graphics/RCorner.png");
				tileSp = new SpriteUV(tileSpTex);
				
			}else if (spaceType == 4)
			{
				tileSpTex = new TextureInfo("Application/graphics/Lcorner.png");
				tileSp = new SpriteUV(tileSpTex);
				
			}else if (spaceType == 5)
			{
				tileSpTex = new TextureInfo("Application/graphics/IRCorner.png");
				tileSp = new SpriteUV(tileSpTex);
				
			}else if (spaceType == 6)
			{
				tileSpTex = new TextureInfo("Application/graphics/ILCorner.png");
				tileSp = new SpriteUV(tileSpTex);
				
			}else if (spaceType == 9)
			{
				tileSpTex = new TextureInfo("Application/graphics/Ground.png");
				tileSp = new SpriteUV(tileSpTex);
				
			}
		}
		
	}
}

