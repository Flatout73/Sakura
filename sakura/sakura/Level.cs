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
        Vertex[][] flowers;

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

        public Level(Texture2D t, Vector2 pos, float kx)
        {
            texture = t;
            position = pos;
            this.kx = kx;
            button = new Button(position.X, position.Y, kx, new Vector2(100 * kx, 100 * kx), texture);
        }

        public void Initilize(Vertex[][] fl)
        {
            flowers = fl;
        }

        public void Draw(Texture2D closedFlower, Texture2D Leaf, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (flowers[i][j] != null)
                        flowers[i][j].Draw(closedFlower, Leaf, spriteBatch);
                }
            }
        }

    }
}