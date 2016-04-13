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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace sakura
{
    class Vertex
    {
        Vector2 position;
        bool edgeUp;
        bool edgeRight;
        bool edgeDown;
        bool edgeLeft;

        bool edgeUpNew;
        bool edgeRightNew;
        bool edgeDownNew;
        bool edgeLeftNew;

        float kx, ky;

        Button turn;
        private int touches = 0;

        public Vertex(Vector2 p, float kx, float ky, bool eU, bool eR, bool eD, bool eL)
        {
            position = p;

            edgeUp = eU;
            edgeRight = eR;
            edgeDown = eD;
            edgeLeft = eL;

            edgeUpNew = false;
            edgeRightNew = false;
            edgeDownNew = false;
            edgeLeftNew = false;

            this.kx = kx;
            this.ky = ky;

            turn = new Button(p.X, p.Y, kx);
        }

        public void Tap()
        {
            edgeUpNew = false;
            edgeRightNew = false;
            edgeDownNew = false;
            edgeLeftNew = false;

            if (edgeLeft)
            {
                edgeUpNew = true;
            }

            if (edgeDown)
            {
                edgeLeftNew = true;
            }

            if (edgeRight)
            {
                edgeDownNew = true;
            }

            if (edgeUp)
            {
                edgeRightNew = true;
            }

            edgeUp = edgeUpNew;
            edgeRight = edgeRightNew;
            edgeDown = edgeDownNew;
            edgeLeft = edgeLeftNew;
        }

        public void Draw(Texture2D textureFlower, Texture2D textureLeaf, Texture2D textureLeafHor, SpriteBatch spriteBatch)
        {
            turn.Process();

            if(turn.IsEnabled)
            {
                Tap();
                touches++;
                turn.Reset();
            }

            //  Rectangle posrect = new Rectangle((int)(position.X), (int)(position.Y), (int)(44 * kx), (int)(44 * kx));
            //   spriteBatch.Draw(textureFlower, position + new Vector2(textureFlower.Width/2, textureFlower.Height/2), 
            //     null, Color.White, (float)(Math.PI/ 2.0f) * (float)touches, new Vector2(textureFlower.Width / 2, textureFlower.Height / 2), 1 , SpriteEffects.None, 0f);

            spriteBatch.Draw(textureFlower, position + new Vector2(textureFlower.Width / 2.0f + 44.0f * kx / 4.0f, textureFlower.Height / 2.0f + 44.0f * kx / 4.0f),
                null, Color.White, (float)(Math.PI / 2.0f) * (float)touches, new Vector2(textureFlower.Width / 2.0f, textureFlower.Height / 2.0f), 44.0f / (float)textureFlower.Width * kx, SpriteEffects.None, 0f);

            Rectangle rect = new Rectangle((int)(position.X + 44.0f * kx / 2.0f - 10.0f * kx / 2.0f), (int)(position.Y - 32.0f * kx), (int)(10.0f * kx), (int)(32.0f * kx));
             if (edgeUp)
               spriteBatch.Draw(textureLeaf, /*position + new Vector2(textureFlower.Width/2 - textureLeaf.Width/2, -textureLeaf.Height)*/ rect, 
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            rect = new Rectangle((int)(position.X + 44.0f * kx + 32.0f * kx / 2.0f), (int)(position.Y + 44.0f * kx / 2.0f), (int)(10.0f * kx), (int)(32.0f * kx));
                if (edgeRight)
                    spriteBatch.Draw(textureLeaf, /*position + new Vector2(-textureLeaf.Height/2, textureFlower.Height / 2)*/ rect,
                      null, Color.White, (float)(Math.PI / 2.0f), new Vector2(textureLeaf.Width / 2.0f, textureLeaf.Height / 2.0f), SpriteEffects.None, 0f);

            rect = new Rectangle((int)(position.X + 44 * kx / 2 - 10 * kx / 2), (int)(position.Y + 44 * kx), (int)(10 * kx), (int)(32 * kx));
            if (edgeDown)
              spriteBatch.Draw(textureLeaf, /*position + new Vector2(textureFlower.Width / 2 - textureLeaf.Width / 2, textureFlower.Height)*/ rect,
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);

          //  rect = new Rectangle((int)Math.Round(position.X - 32 * kx), (int)Math.Round(position.Y + 44 * kx / 2 - 10.0f * kx / 2), (int)(32.0f * kx) + 1, (int)(10.0f * kx) + 1);
          //   if (edgeLeft)
           //     spriteBatch.Draw(textureLeafHor, /*position + new Vector2(-textureLeaf.Height/2, textureFlower.Height / 2)*/ rect,
           //       null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0f);

           rect = new Rectangle((int)(position.X - 32.0f * kx / 2.0f), (int)(position.Y + 44.0f * kx / 2.0f), (int)(10 * kx) + 1, (int)(32 * kx) + 1);     
            if (edgeLeft)
                spriteBatch.Draw(textureLeaf, /*position + new Vector2(-textureLeaf.Height/2, textureFlower.Height / 2)*/ rect,
                  null, Color.White, (float)(-Math.PI / 2.0f), new Vector2(textureLeaf.Width / 2.0f, textureLeaf.Height / 2.0f), SpriteEffects.None, 0f);

           
                
            /*       if (edgeUp)
                      spriteBatch.Draw(textureLeaf, position + new Vector2(textureFlower.Width/2 - textureLeaf.Width/2, -textureLeaf.Height), 
                        null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                   if (edgeRight)
                      spriteBatch.Draw(textureLeaf, position + new Vector2((textureFlower.Width + textureLeaf.Height/2), textureFlower.Height/2),
                        null, Color.White, (float)(Math.PI / 2.0f), new Vector2(textureLeaf.Width/ 2, textureLeaf.Height/ 2), 1, SpriteEffects.None, 0f);
                   if (edgeDown)
                     spriteBatch.Draw(textureLeaf, position + new Vector2(textureFlower.Width / 2 - textureLeaf.Width / 2, textureFlower.Height),
                       null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.FlipVertically, 0f);
                   if (edgeLeft)
                       spriteBatch.Draw(textureLeaf, position + new Vector2(-textureLeaf.Height/2, textureFlower.Height / 2),
                         null, Color.White, (float)(-Math.PI / 2.0f), new Vector2(textureLeaf.Width / 2, textureLeaf.Height / 2), 1, SpriteEffects.None, 0f);*/
        }
    }
}