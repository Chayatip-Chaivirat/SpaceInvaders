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
        KeyboardState keyBoardState;
        KeyboardState previousKeyBoardState;
        Texture2D enemyTex;

        Texture2D bulletTex;
        Enemy enemyClass;
        List<Enemy> enemyList;

        Texture2D heartTex;


        List<Enemy> enemy;
        


        Player player;
        public bool enemyIsAlive = true;
        public bool bulletUsed = false;
        Bullet bullet;

        List<Rectangle> itemToRemove;
        List<Bullet> bulletList;


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
            Vector2 enemyPos = new Vector2(65, 100);
            enemyTex = Content.Load<Texture2D>("alien02_sprites");
            player = new Player(playerTexture, playerPos);
            enemyClass = new Enemy(enemyTex, (int)enemyPos.X, (int)enemyPos.Y);
            

            // TODO: use this.Content to load your game content here

            
            
            enemyList = new List<Enemy>();
            itemToRemove = new List<Rectangle>();

            heartTex = Content.Load<Texture2D>("Undertale");

            enemyTex = Content.Load<Texture2D>("alien02_sprites");

            enemy = new List<Enemy>();


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int x = (int) enemyPos.X + j * 120;
                    int y = (int) enemyPos.Y + i * 100;

                    Enemy ene = new Enemy(enemyTex, x, y);
                    enemyList.Add(ene);
                }
            }

            bulletTex = Content.Load<Texture2D>("bullet_1");
            bullet = new Bullet(bulletTex, playerPos);
            bulletList = new List<Bullet>();
            bulletList.Add(bullet);
            bullet.bulletUsed = true;

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();




            // TODO: Add your update logic here

            player.Update(Window.ClientBounds.Width);
            
            
            //bullet.Update(player.pos1, gameTime);


            
            foreach (Enemy ene in enemyList)
            {

                foreach (Bullet b in bulletList)
                {
                    if (b.bulletHitBox.Intersects(ene.enemyHitBox))
                    {
                        b.bulletHitBox.Intersects(ene.enemyHitBox);
                        ene.enemyIsAlive = false;
                        b.bulletUsed = true;
                        itemToRemove.Add(ene.enemyHitBox);
                    }
                }

            }

            foreach (Bullet b in bulletList)
            {
                b.Update(player.pos1, gameTime);
            }

                KeyboardState state = Keyboard.GetState();
            previousKeyBoardState = state;


            //if (previousKeyBoardState.IsKeyDown(Keys.Space))
            //{
            //    bullet.bulletPos.Y -= 75;
            //}

            if(previousKeyBoardState.IsKeyDown(Keys.Space))
            {
                bulletList.Add(new Bullet(bulletTex, player.pos1));
                bullet.bulletUsed = false;
            }

            foreach (Bullet b in bulletList)
            {
                if (b.bulletPos.Y < 0)
                {
                    b.bulletPos.Y = player.pos1.Y;
                }

            }

            //if (bulletList.Count < 0)
            //{
            //    bulletList.Add(new Bullet(bulletTex, player.pos1));

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
            foreach (Bullet b in bulletList)
            {
                if (b.bulletUsed == false)
                {
                    b.Draw(_spriteBatch);
                }
            }


            foreach (Enemy ene in enemyList)
            {
                if (ene.enemyIsAlive == true)
                {
                    ene.Draw(_spriteBatch);
                }
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
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
