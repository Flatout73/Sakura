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
		public bool edgeUp;
        public bool edgeRight;
        public bool edgeDown;
        public bool edgeLeft;

        bool edgeUpNew;
        bool edgeRightNew;
        bool edgeDownNew;
        bool edgeLeftNew;


        public bool _edgeUp
        {
           get
            {
                return edgeUp;
            }
        }
        public bool _edgeDown
        {
            get
            {
                return edgeDown;
            }
        }

        public bool _edgeLeft
        {
            get
            {
                return edgeLeft;
            }
        }
        public bool _edgeRight
        {
            get
            {
                return edgeRight;
            }
        }

        float kx, ky;

        const int flowerWidth = 50, flowerHeight = 50, leafWidth = 15, leafHeight = 35;
    

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

            turn = new Button(p.X, p.Y, kx, new Vector2(flowerWidth, flowerHeight), null);
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

        public void Draw(Texture2D textureFlower, Texture2D textureLeaf, SpriteBatch spriteBatch)
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

            //		spriteBatch.Draw(textureFlower, position + new Vector2(textureFlower.Width / 2.0f + ((float)flowerWidth * kx / 2.0f - (float)flowerWidth/2.0f), textureFlower.Height / 2.0f + ((float)flowerHeight * kx / 2.0f - flowerWidth/2.0f)),
            //           null, Color.White, (float)(Math.PI / 2.0f) * (float)touches, new Vector2(textureFlower.Width / 2.0f, textureFlower.Height / 2.0f), (float)flowerWidth / (float)textureFlower.Width * kx, SpriteEffects.None, 0f);

            Rectangle flrect = new Rectangle((int)(position.X + flowerWidth * kx / 2f), (int)(position.Y + flowerHeight * kx / 2f), (int)((float)flowerWidth * kx), (int)((float)flowerHeight * kx));
            spriteBatch.Draw(textureFlower, flrect, null, Color.White, (float)(Math.PI / 2f) * (float)touches, new Vector2(textureFlower.Width / 2, textureFlower.Height / 2), SpriteEffects.None, 1f);

            Rectangle rect = new Rectangle((int)(position.X + (float)flowerWidth * kx / 2.0f - (float)leafWidth * kx / 2.0f), (int)(position.Y - (float)leafHeight * kx), (int)((float)(leafWidth * kx)), (int)((float)leafHeight * kx));
             if (edgeUp)
               spriteBatch.Draw(textureLeaf, /*position + new Vector2(textureFlower.Width/2 - textureLeaf.Width/2, -textureLeaf.Height)*/ rect, 
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            rect = new Rectangle((int)(position.X + (float)flowerWidth * kx + (float)leafHeight * kx / 2.0f), (int)(position.Y + (float)flowerHeight * kx / 2.0f), (int)((float)leafWidth * kx), (int)((float)leafHeight * kx));
                if (edgeRight)
                    spriteBatch.Draw(textureLeaf, /*position + new Vector2(-textureLeaf.Height/2, textureFlower.Height / 2)*/ rect,
                      null, Color.White, (float)(Math.PI / 2.0f), new Vector2(textureLeaf.Width / 2.0f, textureLeaf.Height / 2.0f), SpriteEffects.None, 0f);

            rect = new Rectangle((int)(position.X + flowerWidth * kx / 2 - leafWidth * kx / 2), (int)(position.Y + flowerHeight * kx), (int)((float)(leafWidth * kx)), (int)((float)(leafHeight * kx)));
            if (edgeDown)
              spriteBatch.Draw(textureLeaf, /*position + new Vector2(textureFlower.Width / 2 - textureLeaf.Width / 2, textureFlower.Height)*/ rect,
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipVertically, 0f);

          //  rect = new Rectangle((int)Math.Round(position.X - leafHeight * kx), (int)Math.Round(position.Y + 44 * kx / 2 - (float)leafWidth * kx / 2), (int)((float)leafHeight * kx) + 1, (int)((float)leafWidth * kx) + 1);
          //   if (edgeLeft)
           //     spriteBatch.Draw(textureLeafHor, /*position + new Vector2(-textureLeaf.Height/2, textureFlower.Height / 2)*/ rect,
           //       null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0f);

           rect = new Rectangle((int)(position.X - (float)leafHeight * kx / 2.0f), (int)(position.Y + (float)flowerHeight * kx / 2.0f), (int)(leafWidth * kx) + 1, (int)(leafHeight * kx) + 1);     
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