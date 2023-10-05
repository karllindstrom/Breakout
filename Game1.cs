using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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

        public Ball ball;
        Texture2D ballTex;

        Brick[,] bricks;
        Texture2D brickTex;

        Texture2D backTex;
        Rectangle backRect;

        GameState currentGameState;
        enum GameState {Start, Play, GameOver}
        int lives;
        public bool ballInPlay = false;
        SpriteFont font;
        Random random = new Random();

        public int score;

        Texture2D mageSprite;
        double timeSinceLastFrame;
        double timeBetweenFrames;
        Point sheetSize;
        Point frameSize;
        Point currentFrame;

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

            Vector2 ballStartPos = new Vector2(850, 600);
            Vector2 ballStartVelocity = new Vector2(0, 8);

            font = Content.Load<SpriteFont>("nu");


            brickTex = Content.Load<Texture2D>("block_breakout");
            int width = _graphics.PreferredBackBufferWidth = 14 * brickTex.Width;
            int height = _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            paddle = new Paddle(140, 20, paddleTex, 10, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, GraphicsDevice);

            ball = new Ball(new Rectangle((int)ballStartPos.X, (int)ballStartPos.Y, ballTex.Width, ballTex.Height), ballStartVelocity, ballTex, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);


            bricks = new Brick[14, 8];

            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int x = i * (brickTex.Width);
                    int y = j * (brickTex.Height);
                    Rectangle bounds = new Rectangle(x, y, brickTex.Width, brickTex.Height);
                    
                    bricks[i, j] = new Brick(bounds, brickTex, i, j);
                }
            }

            lives = 3;

            mageSprite = Content.Load<Texture2D>("mage sprite");

            timeBetweenFrames = 0.1;
            timeSinceLastFrame = 0;
            
            sheetSize = new Point(3, 2);
            frameSize = new Point(204, 136);
            currentFrame = new Point(0, 0);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (ballInPlay)
            {
                ball.Update();
                paddle.Update();

                if (ball.Bounds.Y + ball.Bounds.Height > GraphicsDevice.Viewport.Height)
                {
                    lives--;

                    ball.Reset();
                    paddle.Reset();

                    if (lives <= 0)
                    {
                        currentGameState = GameState.GameOver;
                    }
                    else
                    {
                        ballInPlay = false;
                    }
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    ball.Start();
                    ballInPlay = true;
                }
            }

            if (ball.Bounds.Intersects(paddle.Bounds) && ball.velocity.Y > 0)
            {
                int randomXVelocityChange = random.Next(-3, 4);
                ball.velocity.X += randomXVelocityChange;

                
                if (ball.velocity.X < -3)
                {
                    ball.velocity.X = -3;
                }
                else if (ball.velocity.X > 3)
                {
                    ball.velocity.X = 3;
                }

                ball.InvertYDirection();
            }

            foreach (var brick in bricks)
            {
                if (ball.Bounds.Intersects(brick.Bounds) && brick.IsDestroyed == false)
                {
                    brick.IsDestroyed = true;

                    score += brick.brickValue;

                    ball.InvertYDirection();
                }
            }

            switch (currentGameState)
            {
                case GameState.Start:
                    

                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        currentGameState = GameState.Play;
                    }
                    break;
                case GameState.Play:
                    

                    if (lives <= 0)
                    {
                        currentGameState = GameState.GameOver;
                    }
                    
                    break;
                case GameState.GameOver:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        currentGameState = GameState.Start;
                        lives = 3;
                        score = 0;
                    }
                    break;
                   
            }

            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastFrame >= timeBetweenFrames)
            {
                timeSinceLastFrame -= timeBetweenFrames;

                currentFrame.X++;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    currentFrame.Y++;
                    if (currentFrame.Y >= sheetSize.Y)
                    {
                        currentFrame.Y = 0;

                    }

                }
            }


           
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            if (currentGameState == GameState.Start)
            {
                spriteBatch.Draw(backTex, backRect, Color.White);
                spriteBatch.DrawString(font, $"Press Enter To Start!", new Vector2(550, 450), Color.Yellow);
            }
            else if (currentGameState == GameState.Play) 
            {

                spriteBatch.Draw(backTex, backRect, Color.White);

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
                        brick.Draw(spriteBatch);
                    }

                }
                spriteBatch.DrawString(font, $"Lives: {lives}", new Vector2(50, 650), Color.Yellow);
                spriteBatch.DrawString(font, $"Score: {score}", new Vector2(50, 750), Color.Yellow);
            }
            else if (currentGameState == GameState.GameOver)
            {
                spriteBatch.Draw(backTex, backRect, Color.White);
                spriteBatch.DrawString(font, $"Game Over!", new Vector2(700, 400), Color.Yellow);
                spriteBatch.DrawString(font, $"Press Enter To Start Again!", new Vector2(500, 500), Color.Yellow);
            }

            Rectangle frame = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.X);
            spriteBatch.Draw(mageSprite, new Vector2(200, 600), Color.White);


            spriteBatch.End();

            base.Draw(gameTime);
        }
        
    }
}