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
        public Color color;
        public bool IsDestroyed; 


        public Brick(Microsoft.Xna.Framework.Rectangle bounds, Texture2D brickTex, int col, int row) 
        {
            Bounds = bounds;
            BrickTex = brickTex;
            IsDestroyed = false;
            if (row < 2)
            {
                color = Color.Green;
            }
            else if (row < 4)
            {
                color = Color.GreenYellow;
            }
            else if (row < 6)
            {
                color = Color.YellowGreen;
            }
            else
            {
                color = Color.Yellow;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsDestroyed == false)
            {
                spriteBatch.Draw(BrickTex, Bounds, color);
            }
            
        }
    }
}
