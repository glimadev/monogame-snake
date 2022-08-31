using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnakeGame.Entity;
using System;

namespace SnakeGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _sprites;
        private SpriteFont _font;
        private Snake snake;
        private Food food;
        private Score score;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 1200;
            _graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(160);
        }

        protected override void Initialize()
        {
            InitVars();
            base.Initialize();
        }

        private void InitVars()
        {
            score = new Score();
            snake = new Snake();
            snake.Create();
            food = new Food();
            food.Create(snake);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _sprites = Content.Load<Texture2D>("sprites");
            _font = Content.Load<SpriteFont>("font");
        }

        protected override void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyState.IsKeyDown(Keys.Escape))
                Exit();
            if (keyState.IsKeyDown(Keys.Up) && snake.GetDirection() != Direction.DOWN) snake.SetDirection(Direction.UP);
            if (keyState.IsKeyDown(Keys.Down) && snake.GetDirection() != Direction.UP) snake.SetDirection(Direction.DOWN);
            if (keyState.IsKeyDown(Keys.Left) && snake.GetDirection() != Direction.RIGHT) snake.SetDirection(Direction.LEFT);
            if (keyState.IsKeyDown(Keys.Right) && snake.GetDirection() != Direction.LEFT) snake.SetDirection(Direction.RIGHT);

            if (food.WasEated(snake.GetHead()))
            {
                food.Create(snake);
                snake.Grow();
                score.Increase();
            }

            if (snake.Collided())
            {
                InitVars();
            }

            snake.Move();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            snake.GetBodyParts().ForEach(part => _spriteBatch.Draw(_sprites,
                  new Rectangle(part.XPosition, part.YPosition, 30, 30)
                , new Rectangle(0, 0, 30, 30), Color.White));
            _spriteBatch.Draw(_sprites, food.GetPosition(), food.GetSpritePosition(), Color.White);
            _spriteBatch.DrawString(_font, $"Score {score.GetScore()} THANKS FOR WATCHING! SUBSCRIBE AND SHARE!", new Vector2(0, 0), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}