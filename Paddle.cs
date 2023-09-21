using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Paddle
    {
        int x; 
        int y; 
        int width; 
        int height; 
        Rectangle paddleRec;
        public Texture2D paddleTex;

        public Paddle(int x, int y, int width, int height, Texture2D paddleTex)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.paddleTex = paddleTex;
            paddleRec = new Rectangle(x, y, width,height);

        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(paddleTex, paddleRec, Color.WhiteSmoke);
        }

    }
}
