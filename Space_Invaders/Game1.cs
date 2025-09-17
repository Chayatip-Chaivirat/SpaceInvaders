using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Space_Invaders
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D enemyTex;
        Texture2D bulletTex;
        Enemy enemyClass;
        List<Enemy> enemy;
        Player player;
        public bool enemyIsAlive = true;
        Bullet bullet;


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
            player = new Player(playerTexture, playerPos);

            // TODO: use this.Content to load your game content here

            enemyTex = Content.Load<Texture2D>("alien02_sprites");
            
            enemy = new List<Enemy>();


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int x = (int) enemyPos.X + j * 120;
                    int y = (int) enemyPos.Y + i * 100;

                    Enemy ene = new Enemy(enemyTex, x, y);
                    enemy.Add(ene);
                }
            }

            bulletTex = Content.Load<Texture2D>("bullet_1");
            bullet = new Bullet(bulletTex, playerPos);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            

         

            // TODO: Add your update logic here
            {
                
            }

            player.Update(Window.ClientBounds.Width);
            bullet.Update();

            Rectangle enemyRec = Enemy.enemyRec;
            Rectangle bulletRec = Bullet.bulletRec;
            if (bulletRec.Intersects(enemyRec))
            {
                enemyIsAlive = false;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            for (int i = 0; i < enemy.Count; i++)
            {
                if (enemyIsAlive == true)
                {
                    enemy[i].Draw(_spriteBatch);
                }
                //enemy[i].Draw(_spriteBatch);
            }
            player.Draw(_spriteBatch);
            bullet.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
