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
		
		private static bool												quitGame;
		private static int												screenH, screenW;
		private static int[]											mapData;
		private static List<Space>										grid;
		
		
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
//			bgSprite.Position = new Vector2(0,0);
//			gameScene.AddChild(bgSprite);
			
			
			grid = new List<Space>();
			int typeCount = 0;
			for(int i = 17; i > 1; i--)
			{
				Vector2 pos = new Vector2(0,0);
				for(int j = 0; j < 27; j++)
				{
					pos = new Vector2(j*32, i*32);
					Space temp = new Space(gameScene, pos, mapData[typeCount]);
					typeCount++;
					grid.Add(temp);
				}
			}
			

			uiScene.RootWidget.AddChildLast(panel);
			
			UISystem.SetScene(uiScene);
			
			
			//Run the scene.
			Director.Instance.RunWithScene(gameScene, true);
		}

		public static void Update ()
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
					if(sT == 0)
					{
						if(touchX <= (sW + sX) && touchX >= sX && touchY <= (sH + sY) && touchY >= sY)
						{
							s.setSelected();
							Vector2 pos = new Vector2(sX, sY);
							foreach(Space h in grid)
							{
								if(h.getType() == 0)
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
		}
		
		
		
		
		public static void load(int lv)
		{
			mapData = new int[459] 
			{0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0,
			0,0,0,0,0,0,0,0,0,1, 9, 9, 9, 1, 0,0,0,0,0,0,0,0,0,0,0,0,0};
		
//			string path = "Application/levels/level" + lv.ToString() + ".txt";
//		            using (System.IO.FileStream hStream = System.IO.File.OpenRead(@path))
//					{
//		                if (hStream != null) 
//						{
//		                    long size = hStream.Length;
//			                byte[] buffer = new byte[size];
//			                hStream.Read(buffer, 0, (int)size);
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
