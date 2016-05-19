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
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace sakura
{
    class Button
    {
        private TouchCollection Touches;
        private bool IsPressed;
        public bool IsEnabled;

        public float x, y;

        float kx;

        Vector2 forScale;

        Texture2D texture;

        public Button(float x, float y, float kx, Vector2 v, Texture2D t)
        {
            this.x = x;
            this.y = y;

            IsPressed = false;
            IsEnabled = false;

            this.kx = kx;

            forScale = v;

            texture = t;
        }
        
        public void Process()
        {
            Touches = TouchPanel.GetState();

            if(Touches.Count == 1)
            {
                if(!IsPressed)
                {

                }
                if ((Touches[0].Position.X > x - 20f * kx) && (Touches[0].Position.X < x + (float)(forScale.X + 20f) * kx) && (Touches[0].Position.Y > y - 20f * kx) &&(Touches[0].Position.Y < y + (float)(forScale.Y + 20f) * kx))
                {
                    IsPressed = true;
                }
                else
                {
                    if (IsPressed)
                        IsPressed = false;
                }
            }
            if((IsPressed) && (Touches.Count == 0))
            {
                IsEnabled = true;
            }
        }

        public void Reset()
        {
            IsPressed = false;
            IsEnabled = false;
        }
    }
}