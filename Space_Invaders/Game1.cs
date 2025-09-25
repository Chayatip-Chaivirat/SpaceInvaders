using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Space_Invaders
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        KeyboardState keyBoardState;
        KeyboardState previousKeyBoardState;

        //========== Enemy ==========
        Enemy enemyClass;
        List<Enemy> enemyList;
        Texture2D enemyTex;

        //========== Lives ==========
        Texture2D heartTex;
        public int Lives = 5;

        //========== Title ==========
        Texture2D titleTex;
        Vector2 titlePos;

        //========== Score ==========
        int score = 0;
        SpriteFont scoreSpriteFont;

        //========== Player ==========
        Player player;
        public bool enemyIsAlive = true;
        public bool bulletUsed = false;
        Bullet bullet;

        //========== Bullet ==========
        List<Bullet> bulletList;
        Texture2D bulletTex;

        //========== Item to Remove ==========
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

            //========== Enemy ==========
             Vector2 enemyPos = new Vector2(65, 100);
            enemyTex = Content.Load<Texture2D>("alien02_sprites");
            enemyClass = new Enemy(enemyTex, (int)enemyPos.X, (int)enemyPos.Y);
            enemyList = new List<Enemy>();

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

            //========== Player ==========
            Texture2D playerTexture = Content.Load<Texture2D>("Ship_01-1");
            Vector2 playerPos = new Vector2(300, 800);
            player = new Player(playerTexture, playerPos);

            //========== itemToRemove ==========
            itemToRemove = new List<Rectangle>();

            //========== Heart ==========
            heartTex = Content.Load<Texture2D>("Undertale");

            //========== Title ==========
            titleTex = Content.Load<Texture2D>("titlenew");
            titlePos = new Vector2(250, 0);

            //========== Bullet ==========
            bulletTex = Content.Load<Texture2D>("bullet_1");
            bullet = new Bullet(bulletTex, playerPos);
            bulletList = new List<Bullet>();
            bulletList.Add(bullet);
            bullet.bulletUsed = true;

            //========== Score ==========
            scoreSpriteFont = Content.Load<SpriteFont>("Score");
            

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(Window.ClientBounds.Width);


            //========== Enemy ==========
            // Collision logic
            foreach (Enemy ene in enemyList)
            {

                foreach (Bullet b in bulletList)
                {
                    if (b.bulletHitBox.Intersects(ene.enemyHitBox))
                    {
                        ene.enemyIsAlive = false;
                        b.bulletUsed = true;
                        itemToRemove.Add(ene.enemyHitBox);
                        score += 1;

                    }

                }

            }

            foreach (Enemy ene in enemyList)
            {
                if (ene.enemyIsAlive == false)
                {
                    itemToRemove.Add(ene.enemyRec);
                }
            }

            // remove dead enemies
            enemyList.RemoveAll(ene => ene.enemyIsAlive == false);

            
            //========== Bullet ==========

            foreach (Bullet b in bulletList)
            {
                if (b.bulletPos.Y < 0)
                {
                    b.bulletUsed = true;
                }
                b.Update(player.pos1, gameTime);
            }

            foreach (Enemy ene in enemyList)
            {
                ene.Update();
            }

            // remove inactive bullets
            bulletList.RemoveAll(b => b.bulletUsed == true);

            KeyboardState state = Keyboard.GetState();
            previousKeyBoardState = state;


            if (previousKeyBoardState.IsKeyDown(Keys.Space))
            {
                int max_bullets = 1;
                if (bulletList.Count < max_bullets) // only one bullet at a time
                {
                    bulletList.Add(new Bullet(bulletTex, player.pos1));
                }

            }

            //========== Lives ==========

            // lose one life when enemy reaches bottom
            foreach (Enemy ene in enemyList)
            {
                if (ene.enemyPos.Y >= 750 - enemyTex.Height)
                {
                    if (Lives > 0)
                    {
                        Lives -= 1;
                        ene.enemyIsAlive = false;
                        itemToRemove.Add(ene.enemyRec);
                        itemToRemove.Add(ene.enemyHitBox);
                    }
                    else
                    {
                        Exit();
                    }
                }


            base.Update(gameTime);
            }
            
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

            for (int i = 0; i < Lives; i++)
            {
                int scale = 4;
                int w = heartTex.Width / scale;
                int h = heartTex.Height / scale;
                int x = 10 + i * (w + 5);
                int y = 10;
                _spriteBatch.Draw(heartTex, new Rectangle(x, y, w, h), Color.White); /// draw texture
                _spriteBatch.Draw(titleTex, titlePos, Color.White);
            }

            Vector2 scorePos = new Vector2(550, 10);
            if (score  > 0)
            {
                _spriteBatch.DrawString(scoreSpriteFont, "Score: " + score, scorePos, Color.Black);
            }
            Window.Title = "Space Invaders - Lives: " + Lives + " Score: " + score;

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
