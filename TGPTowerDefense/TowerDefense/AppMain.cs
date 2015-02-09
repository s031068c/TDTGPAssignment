using System;
using System.Collections.Generic;
using System.IO;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.Core.Audio;




namespace TowerDefense
{
	public struct turretStats 
	{
		public float damage;
		public float fireRate;
		
	};
	public class AppMain
	{
		private static Sce.PlayStation.HighLevel.GameEngine2D.Scene 	gameScene;
		private static Sce.PlayStation.HighLevel.UI.Scene 				uiScene;
		
		private static TextureInfo										bgTex;
		private static SpriteUV											bgSprite;
		
		private static Enemy											en;
		
		private static bool												quitGame;
		private static int												screenH, screenW;
		private static int[]											mapData;
		private static List<Space>										grid;
		private static List<Bullet>										bullets;
		private static Random											rand;
		private static Turret											turr;
		
		public static void Main (string[] args)
		{
			quitGame = false;
			Initialize ();
			
			//Game Loop
			while (!quitGame) 
			{
				Update ();
				
				Director.Instance.Update();
				UISystem.Update(Touch.GetData(0));
				Director.Instance.Render();
				UISystem.Render();
				
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}
		}

		public static void Initialize ()
		{
			Director.Initialize ();
			UISystem.Initialize(Director.Instance.GL.Context);
			
			//Set game scene
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			gameScene.Camera.SetViewFromViewport();
			
			//Set the ui scene.
			uiScene = new Sce.PlayStation.HighLevel.UI.Scene();
			
			//Setup Panel
			Panel panel  = new Panel();
			panel.Width  = Director.Instance.GL.Context.GetViewport().Width;
			panel.Height = Director.Instance.GL.Context.GetViewport().Height;
			screenH = (int)panel.Height;
			screenW = (int)panel.Width;
			
			//add things
			load (1);
//			bgTex = new TextureInfo("Application/graphics/TESTBG.png");
//			bgSprite = new SpriteUV(bgTex);
//			bgSprite.Quad.S = bgTex.TextureSizef;
//			bgSprite.Position = new Vector2(10,0);
//			gameScene.AddChild(bgSprite);
			
			Vector2 ePos = new Vector2(0.0f, 0.0f);
			grid = new List<Space>();
			bullets = new List<Bullet>();
			int typeCount = 0;
			bool enemySpawn = false;
			
			
			
			
			for(int i = 17; i > 0; i--)
			{
				Vector2 pos = new Vector2(10,0);
				for(int j = 0; j < 27; j++)
				{
					pos = new Vector2(j*32, i*32);
					Space temp = new Space(gameScene, pos, mapData[typeCount]);
					if(mapData[typeCount] == 19 && enemySpawn == false)
					{
						enemySpawn = true;
						ePos = new Vector2(pos.X + 32.0f, pos.Y - 50.0f);
					}
					typeCount++;
					grid.Add(temp);
				}
			}
			
			
			rand = new Random();
			
			en = new Enemy(gameScene, ePos, 2, rand);
			ePos.X -= 70.0f;
			ePos.Y -= 90.0f;
			turr = new Turret(gameScene, ePos);
			uiScene.RootWidget.AddChildLast(panel);
			
			UISystem.SetScene(uiScene);
			
			
			//Run the scene.
			Director.Instance.RunWithScene(gameScene, true);
		}

		public static void Update ()
		{
			

			en.Update(0);
			turr.RotateToEnemy(en.GetCenter());
			turr.Update (0);
			if(turr.fireCheck() == true)
			{
				Bullet bu = new Bullet(gameScene, turr.getPos(), turr.getDirection());
				bullets.Add (bu);
			}
			BulletUpdate ();
			GridUpdate();
			
			
		}
		
		public static void BulletUpdate()
		{
			foreach(Bullet b in bullets)
			{
				b.Update ();
				Vector2 bPos = b.getPos ();
//				if(bPos.X > screenW || bPos.X < 0 || bPos.Y > screenH || bPos.Y < 0)
//				{
//					b.Dispose ();
//				}
			}
		}
		
		
		public static void GridUpdate()
		{
			var touchT = Touch.GetData(0).ToArray();
			int touchX = -100;
			int touchY = -100;
			if(touchT.Length > 0 && touchT[0].Status == TouchStatus.Up)
			{
				touchX = (int)((touchT[0].X + 0.5f) * screenW);
				touchY = screenH-(int)((touchT[0].Y + 0.5f) * screenH);
				//Grid selection code
				foreach(Space s in grid)
				{
					int sW = s.getWidth();
					int sH = s.getHeight();
					int sX = s.getX ();
					int sY = s.getY ();
					int sT = s.getType();
					if(sT == 10)
					{
						if(touchX <= (sW + sX) && touchX >= sX && touchY <= (sH + sY) && touchY >= sY)
						{
							s.setSelected();
							Vector2 pos = new Vector2(sX, sY);
							foreach(Space h in grid)
							{
								if(h.getType() == 10)
								{
									if(h.getX() != pos.X || h.getY() != pos.Y)
									{
										h.unSelect();	
									}
								}
							}
						}
					}
				}
			}
			
			foreach(Space h in grid)
			{
				if(h.getWayDir() != -1)
				{
					int wayW = h.getWWidth();
					int wayH = h.getWHeight();
					int wayX = h.getWX ();
					int wayY = h.getWY ();
					
					float enX = en.getPos().X;
					float enY = en.getPos().Y;
					int enH = en.getHeight();
					int enW = en.getWidth();
					if(enX <= (wayW + wayX) && (enX + enW) >= wayX && enY <= (wayH + wayY) && (enY + enH) >= wayY)
					{
						en.setDirection(h.getWayDir());
						en.randDelay(rand);
					}
				}
				
			}
		}
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		public static void load(int lv)
		{
			
			
			/*Map editing key
			 * 10 = Empty space for turrets
			 * 11 = Vertical Wall |
			 * 12 = Horizontal wall _
			 * 13 = r corner
			 * 14 = L corner
			 * 15 = inverse R corner -|
			 * 16 = inverse L corner _|
			 * 19 = Standard ground tile for enemies
			 * (20-23 all include 19's graphic)
			 * 20 = Move left waypoint
			 * 21 = Move right waypoint
			 * 22 = move down waypoint
			 * 23 = move up waypoint
			 */
			
			
			mapData = new int[459] 
			{10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,11,19,19,19,11,10,10,10,
			 10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,11,19,19,19,11,10,10,10,
			 10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,11,19,19,19,11,10,10,10,
			 10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,11,19,19,19,11,10,10,10,
			 10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,11,19,19,19,11,10,10,10,
			 10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,11,19,19,19,11,10,10,10,
			 10,10,10,10,10,10,10,10,10,13,12,12,12,12,12,12,12,12,12,16,20,20,20,11,10,10,10,
			 10,10,10,10,10,10,10,10,10,11,19,19,19,22,19,19,19,19,19,19,19,19,19,11,10,10,10,
			 10,10,10,10,10,10,10,10,10,11,19,19,19,22,19,19,19,19,19,19,19,19,19,11,10,10,10,
			 10,10,10,10,10,10,10,10,10,11,19,19,19,22,19,19,19,19,19,19,19,19,19,11,10,10,10,
			 10,10,10,10,10,10,10,10,10,11,19,19,19,13,12,12,12,12,12,12,12,12,12,16,10,10,10,
			 10,10,10,10,10,10,10,10,10,11,19,19,19,11,10,10,10,10,10,10,10,10,10,10,10,10,10,
			 10,10,10,10,10,10,10,10,10,11,21,21,21,14,12,12,12,12,12,12,12,12,12,15,10,10,10,
			 10,10,10,10,10,10,10,10,10,11,19,19,19,19,19,19,19,19,22,19,19,19,19,11,10,10,10,
			 10,10,10,10,10,10,10,10,10,11,19,19,19,19,19,19,19,19,22,19,19,19,19,11,10,10,10,
			 10,10,10,10,10,10,10,10,10,11,19,19,19,19,19,19,19,19,22,19,19,19,19,11,10,10,10,
			 10,10,10,10,10,10,10,10,10,14,12,12,12,12,12,12,12,12,12,15,19,19,19,11,10,10,10};
		
//			string path = "Application/levels/level" + lv.ToString() + ".txt";
//		            using (System.IO.FileStream hStream = System.IO.File.OpenRead(@path))
//					{
//		                if (hStream != null) 
//						{
//		                    long size = hStream.Length;
//			                byte[] buffer = new byte[size];
//			                hStream.Read(buffer, 10, (int)size);
//							int x = sizeof(Int32);
//					//17*27 =459
//							mapData = new int[459];
//			                Int32 sum=0;
//			                for(int i=0; i<459; i++)
//			                {
//			                    Buffer.BlockCopy(buffer, sizeof(Int32)*i, mapData, sizeof(Int32)*i,  sizeof(Int32));
//			                    sum+=mapData[i];
//			                }
//		                }
//						hStream.Close();
//					}
		}
	}
}
