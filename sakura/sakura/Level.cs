using System;
using System.Collections.Generic;
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
        int[][][] graph2;
        int[][] graphsum;

        bool[] used;

        List<int> comp;


        public Vector2 _position
        {
            get
            {
                return position;
            }
        }

        public Texture2D _texture
        {
            get
            {
                return texture;
            }
        }

        public Level(Texture2D t, Vector2 pos, float kx, GameProcess gm)
        {
            texture = t;
            position = pos;
            this.kx = kx;
            button = new Button(position.X, position.Y, kx, new Vector2(100 * kx, 100 * kx), texture);
            gameProcess = gm;
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

            graph2 = new int[7][][];
            for (int i = 0; i < 7; i++)
            {
                graph2[i] = new int[4][];
                for (int j = 0; j < 4; j++)
                {
                    graph2[i][j] = new int[4];
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
                                graph.Add(4 * i + j - 4);
                                graph2[i][j][k] = 4 * i + j - 4;
                                graphsum[4 * i + j][k] = 4 * i + j - 4;
                                k++;
                            }

                        if (j - 1 > -1 && flowers[i][j - 1] != null)
                            if (flowers[i][j]._edgeLeft && flowers[i][j - 1]._edgeRight)
                            {
                                graph.Add(i * 4 + j - 1);
                                graph2[i][j][k] = i * 4 + j - 1;
                                graphsum[4*i + j][k] = i * 4 + j - 1;
                                k++;
                            }

                        if (j + 1 < 4 && flowers[i][j + 1] != null)
                            if (flowers[i][j]._edgeRight && flowers[i][j + 1]._edgeLeft)
                            {
                                graph.Add(i * 4 + j + 1);
                                graph2[i][j][k] = i * 4 + j + 1;
                                graphsum[4*i + j][k] = i * 4 + j + 1;
                                k++;
                            }

                        if (i + 1 < 4 && flowers[i + 1][j] != null)
                            if (flowers[i][j]._edgeDown && flowers[i + 1][j]._edgeUp)
                            {
                                graph.Add(4 * i + j + 4);
                                graph2[i][j][k] = 4 * i + j + 4;
                                graphsum[4*i + j][k] = 4 * i + j + 4;
                                k++;

                            }

                        graph.Add(-1);
                    }
                    else
                    {
                        graphsum[4 * i + j] = null;
                    }
                }
            }

            dfs(0);
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
                gameProcess.NewGame();
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

        void dfs(int v)
        {
            int to = -1;
            used[v] = true;
            comp.Add(v);
            for (int i = 0; i < graphsum[v].Length; i++)
            {
                if (graphsum[v][i] != -1)
                {
                    to = graphsum[v][i];
                    if (!used[to])
                    {
                        dfs(to);
                    }
                }
            } 
        }

    }
}