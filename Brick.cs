using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    public class Brick
    {
        public Rectangle Bounds;
        public Texture2D BrickTex;

        public bool IsDestroyed; 


        public Brick(Microsoft.Xna.Framework.Rectangle bounds, Texture2D brickTex) 
        {
            Bounds = bounds;
            BrickTex = brickTex;
            IsDestroyed = false;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsDestroyed == false)
            {
                spriteBatch.Draw(BrickTex, Bounds, Color.Green);
            }
            
        }
    }
}
