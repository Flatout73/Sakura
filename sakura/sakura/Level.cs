using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace sakura
{
    class Level
    {
        Texture2D texture;
        Vector2 position;
        public Button button;
        float kx;

        public Vertex[][] flowers;
        GameProcess gameProcess;

        List<int> graph;
        int[][] graphsum;

        bool[] used;

        List<int> comp;

		Vector2 flower0Position;
		Vector2[][] flowersPosition;

		Random rnd;

        public const int countFlowersWidth = 4;
        public const int countFlowersHeight = 7;

        public bool isPrevEnd { get; set; }

        public Vector2 _position
        {
            get
            {
                return position;
            }
        }

        public Level(Texture2D t, Vector2 pos, float kx, GameProcess gm)
        {
            texture = t;
            position = pos;
            this.kx = kx;
            button = new Button(position.X, position.Y, kx, new Vector2(100 * kx, 100 * kx));
            gameProcess = gm;
			flower0Position = new Vector2((int)(35 * kx + 30*kx), (int)(35 * kx + 30*kx));

			flowersPosition = new Vector2[7][];
			for (int i = 0; i < 7; i++) 
			{
				flowersPosition [i] = new Vector2[4];
			}
			for (int i = 0; i < 7; i++) 
			{
				for (int j = 0; j < 4; j++)
				{
					flowersPosition [i] [j] = (flower0Position + new Vector2((70 + 50) * kx * j, (70 + 50) * kx * i));
				}
			}

			graphsum = new int[28][];
			for (int i = 0; i < 28; i++)
			{
				graphsum[i] = new int[4];
			}

			used = new bool[28];

			comp = new List<int>(28);

        }

        public void Initilize(Vertex[][] fl)
        {
            flowers = fl;

            int k = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (flowers[i][j] != null)
                        k++;
                }
            }
			   graph = new List<int>(10);
            graph.Add(k);

        }

		public void Initilize(List<int> g) 
		{
			flowers = new Vertex[7][]; 

			for (int i = 0; i < 7; i++) {
				flowers [i] = new Vertex[4];
			}

			for (int i = 0; i < g.Count; i++) 
			{
				flowers [g[i] / 4] [g[i] % 4] = new Vertex(flowersPosition[g[i]/4][g[i]%4], kx, false, false, false, false);
			}

            for (int i = 0; i < g.Count; i++)
            {
                for (int j = 0; j < g.Count; j++)
                {

                    if (g[i] - 4 == g[j])
                    {
                        flowers[g[i] / 4][g[i] % 4]._edgeUp = true;
                        flowers[g[j] / 4][g[j] % 4]._edgeDown = true;
                    }

                    if (g[i] + 1 == g[j])
                    {
                        flowers[g[i] / 4][g[i] % 4]._edgeRight = true;
                        flowers[g[j] / 4][g[j] % 4]._edgeLeft = true;
                    }

                    if (g[i] + 4 == g[j])
                    {
                        flowers[g[i] / 4][g[i] % 4]._edgeDown = true;
                        flowers[g[j] / 4][g[j] % 4]._edgeUp = true;
                    }
                    if (g[i] - 1 == g[j])
                    {
                        flowers[g[i] / 4][g[i] % 4]._edgeLeft = true;
                        flowers[g[j] / 4][g[j] % 4]._edgeRight = true;
                    }
                }
            }
		}


        public void Update()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int k = 0;
                    if (flowers[i][j] != null)
                    {

                        graphsum[4 * i + j][0] = -1;
                        graphsum[4 * i + j][1] = -1;
                        graphsum[4 * i + j][2] = -1;
                        graphsum[4 * i + j][3] = -1;

                        if (i - 1 > -1 && flowers[i - 1][j] != null)
                            if (flowers[i][j]._edgeUp && flowers[i - 1][j]._edgeDown)
                            {
                                graphsum[4 * i + j][k] = 4 * i + j - 4;
                                k++;
                            }

                        if (j - 1 > -1 && flowers[i][j - 1] != null)
                            if (flowers[i][j]._edgeLeft && flowers[i][j - 1]._edgeRight)
                            {
                                graphsum[4*i + j][k] = i * 4 + j - 1;
                                k++;
                            }

                        if (j + 1 < 4 && flowers[i][j + 1] != null)
                            if (flowers[i][j]._edgeRight && flowers[i][j + 1]._edgeLeft)
                            {
                                graphsum[4*i + j][k] = i * 4 + j + 1;
                                k++;
                            }

                        if (i + 1 < 7 && flowers[i + 1][j] != null)
                            if (flowers[i][j]._edgeDown && flowers[i + 1][j]._edgeUp)
                            {
                                graphsum[4*i + j][k] = 4 * i + j + 4;
                                k++;

                            }
                    }
                    else
                    {
                        graphsum[4 * i + j] = null;
                    }
                }
            }

            Search(0);
            bool flag = true;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if(graphsum[4*i+j] != null)
                    {
                        if(!comp.Contains(4*i+j))
                        {
                            flag = false;                     
                        }
                    }
                }
            }
            comp.Clear();
            for (int i = 0; i < 28; i++)
            {
                used[i] = false;
            }
            if (flag)
            {
				gameProcess.WinGame();
            }

        }

        public void Draw(Texture2D closedFlower, Texture2D Leaf, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (flowers[i][j] != null)
                        flowers[i][j].Draw(closedFlower, Leaf, spriteBatch);
                }
            }
        }

        void Search(int v)
		{
				int to = -1;
				used [v] = true;
				comp.Add (v);
			if (graphsum [v] != null) {
				for (int i = 0; i < graphsum [v].Length; i++) {
					if (graphsum [v] [i] != -1) {
						to = graphsum [v] [i];
						if (!used [to]) {
							Search (to);
						}
					}
				} 
			} else {
				Search (v + 1);
			}
		}

		public void Mix() 
		{
			rnd = new Random (4);
			for (int i = 0; i < 7; i++) {
				for (int j = 0; j < 4; j++) {
			
					int r = rnd.Next (1, 4);
					for (int l = 0; l < r; l++) {
                        if(flowers[i][j] != null)
						flowers [i] [j].Tap ();
					}

				}
			}
		}

    }
}