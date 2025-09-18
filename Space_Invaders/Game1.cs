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
        List<Enemy> enemyList;
        Player player;
        public bool enemyIsAlive = true;
        public bool bulletUsed = false;
        Bullet bullet;

        List<Rectangle> itemToRemove;


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

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            

         

            // TODO: Add your update logic here

            player.Update(Window.ClientBounds.Width);
            bullet.Update(player.pos1);


            
            foreach (Enemy ene in enemyList)
            {
                if (bullet.bulletHitBox.Intersects(ene.enemyHitBox))
                {
                    bullet.bulletHitBox.Intersects(ene.enemyHitBox);
                    ene.enemyIsAlive = false;
                    bullet.bulletUsed = true;
                    itemToRemove.Add(ene.enemyHitBox);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            //enemyClass.Draw(_spriteBatch);
            if (bullet.bulletUsed == false)
            {
                bullet.Draw(_spriteBatch);
            }
                

            foreach (Enemy ene in enemyList)
            {
                if (ene.enemyIsAlive == true)
                {
                    ene.Draw(_spriteBatch);
                }
                //   for (int i = 0; i < enemyList.Count; i++)
                //{
                //    enemyList[i].Draw(_spriteBatch);
                //}
            

            }
            
            
            //if (enemyIsAlive == true)
            //{
            //    foreach (Enemy ene in enemyList)
            //    {
            //        ene.Draw(_spriteBatch);
            //    }
            //}

            //for (int i = 0; i < enemyList.Count; i++)
            //{
            //    enemyList[i].Draw(_spriteBatch);
            //    if (enemyIsAlive == false)
            //    {
            //        enemyList.RemoveAt(i);
            //    }
            //}


            player.Draw(_spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
