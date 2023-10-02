using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Security.Principal;

namespace Breakout
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        public Paddle paddle;
        Texture2D paddleTex;
        Ball ball;
        Texture2D ballTex;
        Brick[,] bricks;
        Texture2D brickTex;
        Texture2D backTex;
        Rectangle backRect;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
         
          

            base.Initialize();
        }
       

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            paddleTex = Content.Load<Texture2D>("padle");

            backTex = Content.Load<Texture2D>("114740");

            backRect = new Rectangle(0, 0, 1920, 1080);

            ballTex = Content.Load<Texture2D>("ball_breakout");

            Vector2 ballStartPos = new Vector2(750, 600);
            Vector2 ballStartVelocity = new Vector2(4, 4);


            brickTex = Content.Load<Texture2D>("block_breakout");
            int width = _graphics.PreferredBackBufferWidth = 14 * brickTex.Width;
            int height = _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            paddle = new Paddle(140, 20, paddleTex, 10, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            ball = new Ball(new Rectangle((int)ballStartPos.X, (int)ballStartPos.Y, ballTex.Width, ballTex.Height), ballStartVelocity, ballTex, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);


            bricks = new Brick[14, 8];

            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int x = i * (brickTex.Width);
                    int y = j * (brickTex.Height);
                    Rectangle bounds = new Rectangle(x, y, brickTex.Width, brickTex.Height);
                    bricks[i, j] = new Brick(bounds, brickTex);
                }
            }

            //bricks = new List<Brick>();
            //int numberOfBricks = 10;
            //int brickWidth = 80;
            //int brickHeight = 20;
            //int brickSpacing = 10;

            //for (int i = 0; i < numberOfBricks; i++)
            //{
            //    int x = i * (brickWidth + brickSpacing);
            //    Rectangle bounds = new Rectangle(x, 5, brickWidth, brickHeight);
            //    Brick brick = new Brick(bounds, brickTex);
            //    bricks.Add(brick);
            //}



        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ball.Update();

            paddle.Update();

            if (ball.Bounds.Intersects(paddle.Bounds))
            {
                ball.InvertYDirection();
            }

            foreach (var brick in bricks)
            {
                if (ball.Bounds.Intersects(brick.Bounds) && brick.IsDestroyed == false)
                {
                    brick.IsDestroyed = true;

                    ball.InvertYDirection();
                }
            }

            //bricks.RemoveAll(brick => brick.IsDestroyed);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(backTex, backRect, Color.White);

            // TODO: Add your drawing code here
            ball.Draw(spriteBatch);
            
            paddle.Draw(spriteBatch);

            for (int i = 0; i < bricks.GetLength(0); i++)
            {
                for (int j = 0; j < bricks.GetLength(1); j++)
                {
                    bricks[i, j].Draw(spriteBatch);
                }
            }

            foreach (var brick in bricks)
            {
                if (!brick.IsDestroyed)
                {
                    spriteBatch.Draw(brick.BrickTex, brick.Bounds, Color.Green);
                }

            }



            spriteBatch.End();

            base.Draw(gameTime);
        }
        
    }
}