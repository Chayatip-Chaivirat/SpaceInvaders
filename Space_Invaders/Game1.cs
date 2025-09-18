using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Space_Invaders
{
    public class Game1 : Game
    {
        private KeyboardState previousState;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D enemyTex;
        Texture2D heartTex;
        Texture2D titleTex;
        Vector2 titlePos;


        List<Enemy> enemy;
        int score = 0;
        

        Player player;
        private Vector2 pos1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 700;
            _graphics.PreferredBackBufferHeight = 950;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D playerTexture = Content.Load<Texture2D>("Ship_01-1");
            Vector2 playerPos = new Vector2(300, 800);
            Vector2 startPos = new Vector2(65, 100);
            player = new Player(playerTexture, playerPos);

            // TODO: use this.Content to load your game content here

            heartTex = Content.Load<Texture2D>("Undertale");

            enemyTex = Content.Load<Texture2D>("alien02_sprites");

            titleTex = Content.Load<Texture2D>("titlenew");
            titlePos = new Vector2(250, 0);
           
         

            enemy = new List<Enemy>();


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int x = (int) startPos.X + j * 120;
                    int y = (int) startPos.Y + i * 100;

                    Enemy ene = new Enemy(enemyTex, x, y);
                    enemy.Add(ene);
                }
            }

            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < 10; i++) 
            {
                if (enemy == null)
                    score = 0;
                else score < 1;
               
 

              

            // TODO: Add your update logic here
            {

            }

            player.Update(Window.ClientBounds.Width);

            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.K) && previousState.IsKeyUp(Keys.K))
            { // lose one life when K is pressed
                if (player.Lives > 0)
                    player.Lives--;
            }
            // exit game if no lives left
            if (player.Lives == 0) 
                Exit();

            previousState = state;

            base.Update(gameTime);
            }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].Draw(_spriteBatch);
            }
            player.Draw(_spriteBatch);
            for (int i = 0; i < player.Lives; i++)
            {
                int scale = 4;
                int w = heartTex.Width / scale;
                int h = heartTex.Height / scale;
                int x = 10 + i * (w + 5);
                int y = 10;
                _spriteBatch.Draw(heartTex, new Rectangle(x, y, w, h), Color.White); /// draw texture
                _spriteBatch.Draw(titleTex, titlePos, Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
