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
        public bool isPressed;
        public bool isEnabled;

        public float x, y;

        float kx;

        Vector2 forScale;

        public Button(float x, float y, float kx, Vector2 v)
        {
            this.x = x;
            this.y = y;

            isPressed = false;
            isEnabled = false;

            this.kx = kx;

            forScale = v;
        }
        
        public void Process()
        {
            Touches = TouchPanel.GetState();

            if(Touches.Count == 1)
            {
                if(!isPressed)
                {

                }
                if ((Touches[0].Position.X > x - 10f * kx) && (Touches[0].Position.X < x + (float)(forScale.X + 10f) * kx) && (Touches[0].Position.Y > y - 10f * kx) &&(Touches[0].Position.Y < y + (float)(forScale.Y + 10f) * kx))
                {
                    isPressed = true;
                }
                else
                {
                    if (isPressed)
                        isPressed = false;
                }
            }
            if((isPressed) && (Touches.Count == 0))
            {
                isEnabled = true;
            }
        }

        public void Reset()
        {
            isPressed = false;
            isEnabled = false;
        }
    }
}