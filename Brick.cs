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
        public int brickValue;

        public Brick(Microsoft.Xna.Framework.Rectangle bounds, Texture2D brickTex, int col, int row) 
        {
            Bounds = bounds;
            BrickTex = brickTex;
            IsDestroyed = false;
            
            if (row < 2)
            {
                color = Color.Green;
                brickValue = 4000;
            }
            else if (row < 4)
            {
                color = Color.GreenYellow;
                brickValue = 2000;
            }
            else if (row < 6)
            {
                color = Color.YellowGreen;
                brickValue = 1000;
            }
            else
            {
                color = Color.Yellow;
                brickValue = 500;
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
