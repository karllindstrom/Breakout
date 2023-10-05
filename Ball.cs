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
    public class Ball
    {
        Rectangle ballRect;
        public Vector2 velocity;
        public Vector2 initialVelocity;
        Texture2D ballTex;
        int screenWidth;
        int screenHeight;
       
        public Ball(Rectangle ballRect, Vector2 velocity, Texture2D ballTex, int screenWidth, int screenHeight)
        {
            this.ballRect = ballRect;
            this.velocity = velocity;
            this.ballTex = ballTex;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }
        public void Update()
        {
            ballRect.X += (int)velocity.X;
            ballRect.Y += (int)velocity.Y;

            if (ballRect.Left < 0 || ballRect.Right > screenWidth)
            {
                velocity.X *= -1;
            }
            if (ballRect.Top < 0)
            {
                velocity.Y *= -1;
            }
        }
        public void Reset()
        {
            
            ballRect.X = 850;
            ballRect.Y = 600;
            Vector2 initialVelocity = new Vector2(0, 0);
            
        }
        public void Start()
        {
            
            velocity = new Vector2 (0, 8);
            
        }
        public void InvertYDirection()
        {
            velocity.Y = -velocity.Y;
        }
        public Rectangle Bounds
        {
            get { return ballRect; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ballTex, ballRect, Color.AntiqueWhite);
        }
    }
}

