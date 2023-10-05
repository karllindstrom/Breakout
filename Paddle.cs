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
        Vector2 position;
        Rectangle paddleRec;
        Texture2D paddleTex;
        int speed;
        int screenWidth;
        int screenHeight;
        public GraphicsDevice graphicsDevice;

        public Paddle(int width, int height, Texture2D paddleTex, int speed, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice)
        {
            this.paddleTex = paddleTex;
            this.speed = speed;
            this.screenWidth = screenWidth;
            this.graphicsDevice = graphicsDevice; 

            // Startposition för paddeln
            position = new Vector2(screenWidth / 2 - paddleTex.Width / 2, screenHeight - height - 40);

            // Skapa rektangeln baserad på position och storlek
            paddleRec = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public void Update()
        {
            MouseState mouseState = Mouse.GetState();

            position.X = mouseState.X - paddleTex.Width / 2;

            position.X = MathHelper.Clamp(position.X, 0, screenWidth - paddleTex.Width);

            paddleRec.X = (int)position.X;

            //KeyboardState keyboardState = new KeyboardState();

            //float speed = 5.0f;

            //if (keyboardState.IsKeyDown(Keys.Left))
            //{
            //    position.X -= speed;
            //}
            //else if (keyboardState.IsKeyDown(Keys.Right))
            //{
            //    position.X += speed;
            //}

            //position.X = MathHelper.Clamp(position.X, 0, screenWidth - paddleTex.Width);

            //paddleRec.X = (int)position.X;
        }
        public Rectangle Bounds
        {
            get { return paddleRec; }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(paddleTex, paddleRec, Color.SkyBlue);
        }
        public void Reset()
        {
            position.X = (screenWidth - paddleTex.Width / 2);
        }
        
    }
}





 

