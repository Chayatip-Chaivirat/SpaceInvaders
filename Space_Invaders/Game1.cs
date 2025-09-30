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
        KeyboardState previousKeyBoardState;

        //========== Enemy ==========
        Enemy[,] enemyArray;
        Texture2D enemyTex;
        public bool enemyIsAlive = true;
        Vector2 enemyPos;

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

        //========== Bullet ==========
        List<Bullet> bulletList;
        Texture2D bulletTex;
        public bool bulletUsed = false;
        Bullet bullet;

        //========== Item to Remove ==========
        List<Rectangle> itemToRemove;

        //========== Game State ==========
        GameState currentGameState;

        //========== Start ==========
        public bool startButtonClicked = false;
        Texture2D startBackgroundTex;
        Vector2 startPositionBackground;

        Start startButton;
        Vector2 startButtonPos;

        Texture2D startSpriteSheetTex;
        Vector2 startSpriteSheetPos;
        Rectangle startSpriteSheetRec;
        Point frameSize = new Point(100, 100);
        Point currentFrame = new Point(0, 0);
        Point sheetSize = new Point(3, 1);
        int timeSinceLastFrame = 0;
        int millisecondPerFrame = 50;

        //========== Gameover ==========
        Texture2D gameOverBackgroundTex;
        Vector2 gameOverBackgroundPos;

        GameoverState gameOverSpriteExplosion;
        Texture2D gameOverSpriteExplosionTex;
        Vector2 gameOverSpriteExplosionPos;
        Rectangle gameOverSpriteExplosionRec;

        GameoverState gameOverSpriteStar;
        Texture2D gameOverSpriteStarTex;
        Vector2 gameOverSpriteStarPos;
        Rectangle gameOverSpriteStarRec;
        public enum GameState
        {
            Starting,
            Playing,
            GameOver
        }

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
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //========== Enemy ==========
            enemyPos = new Vector2(65, 100);
            enemyTex = Content.Load<Texture2D>("alien02_sprites");
            enemyArray = new Enemy[5, 5];

            for (int i = 0; i < 5; i++)
             {
                for (int j = 0; j < 5; j++)
                {
                    int x = (int) enemyPos.X + j * 120;
                    int y = (int) enemyPos.Y + i * 100;

                    Enemy ene = new Enemy(enemyTex, x, y);
                    enemyArray[i, j] = new Enemy(enemyTex, x, y);
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

            //========== Start ==========
            if (currentGameState == GameState.Starting)
            {
                startBackgroundTex = Content.Load<Texture2D>("Arkadkabinett-1");
                startPositionBackground = new Vector2(110, 130);

                Texture2D startButtonTex = Content.Load<Texture2D>("Startknapp");
                startButtonPos = new Vector2(250, 400);
                startButton = new Start(startButtonTex, (int)startButtonPos.X, (int)startButtonPos.Y);

                startSpriteSheetTex = Content.Load<Texture2D>("alien02_sprites");
                startSpriteSheetPos = new Vector2(260, 260);
                startSpriteSheetRec = new Rectangle(0, 0, 100, 90);
            }

            //========== Gameover ==========
                gameOverBackgroundTex = Content.Load<Texture2D>("game_over-2");
                gameOverBackgroundPos = new Vector2(90, 150);

                Texture2D gameOverSpriteExplosionTex = Content.Load<Texture2D>("explotion01_sprites");
                gameOverSpriteExplosionPos = new Vector2(150, 200);
                gameOverSpriteExplosion = new GameoverState(gameOverSpriteExplosionTex, gameOverSpriteExplosionPos);

                Texture2D gameOverSpriteStarTex = Content.Load<Texture2D>("star_01");
                gameOverSpriteStarPos = new Vector2(160, 200);
                gameOverSpriteStar = new GameoverState(gameOverSpriteStarTex,gameOverSpriteStarPos);

        }

        protected override void Update(GameTime gameTime)
        {
            //========== GameState ==========
            if (currentGameState == GameState.Starting) // start screen
            {
                startButton.Clicked();

                if (startButtonClicked == false)
                {
                    timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                    if (startButton.startButtonClicked)
                    {
                        currentGameState = GameState.Playing;
                    }

                    if (timeSinceLastFrame > millisecondPerFrame)
                    {
                        timeSinceLastFrame = 0;
                    }

                    // animation for alien on start screen
                    ++currentFrame.X;
                    if (currentFrame.X >= sheetSize.X)
                    {
                        currentFrame.X = 0;
                        ++currentFrame.Y; 
                        if (currentFrame.Y >= sheetSize.Y)
                        { 
                            currentFrame.Y = 0;
                        }
                    }

                    // Update the source rectangle to draw the current frame
                    startSpriteSheetRec = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y);
                }
            }

            if (currentGameState == GameState.Playing) // main game
            {
                //========== Enemy ==========
                // Update enemies
                foreach (Enemy ene in enemyArray)
                {
                    ene.Update();
                }

                bool noEnemyLeftInArray = true;
                foreach (Enemy ene in enemyArray)
                {
                    if (ene.enemyIsAlive == true)
                        {
                            noEnemyLeftInArray = false;
                            break;
                        }
                }

                    // Collision logic
                    foreach (Enemy ene in enemyArray)
                {

                    foreach (Bullet b in bulletList)
                    {
                        if (ene.enemyIsAlive == true && b.bulletHitBox.Intersects(ene.enemyHitBox))
                        {
                            ene.enemyIsAlive = false;
                            b.bulletUsed = true;
                            itemToRemove.Add(ene.enemyHitBox);
                            itemToRemove.Add(ene.enemyRec);
                            score += 1;

                        }

                    }

                }

                foreach (Enemy ene in enemyArray)
                {
                    if (ene.enemyIsAlive == false)
                    {
                        itemToRemove.Add(ene.enemyRec);
                    }
                } 

                //========== Bullet ==========

                foreach (Bullet b in bulletList)
                {
                    if (b.bulletPos.Y < 0)
                    {
                        b.bulletUsed = true;
                    }
                    b.Update(player.pos1, gameTime);
                } 
                // remove inactive bullets
                bulletList.RemoveAll(b => b.bulletUsed == true);

                if (Lives > 0)
                {
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
                }

                //========== Lives ==========

                // lose one life when enemy reaches bottom
                int screenHeight = Window.ClientBounds.Height;
                int stopY = screenHeight - enemyTex.Height;

                foreach (Enemy ene in enemyArray)
                {
                    if (ene.lifeLost == false && ene.enemyIsAlive == true && ene.enemyHitBox.Bottom >= screenHeight - 200)
                    {
                        if (Lives > 0)
                        {
                            Lives -= 1;
                            ene.enemyIsAlive = false;
                            ene.lifeLost = true;
                            itemToRemove.Add(ene.enemyRec);
                            itemToRemove.Add(ene.enemyHitBox);
                        }
                    }

                    //========== Player ==========
                    if (Lives > 0)
                    {
                        player.Update(Window.ClientBounds.Width);
                    }
                    
                    if (Lives <= 0 || noEnemyLeftInArray == true) // switch to game over state when no lives left OR no enemies left
                    {
                        currentGameState = GameState.GameOver;
                    } 
                }

            if (currentGameState == GameState.GameOver) // game over screen
                {
               if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        currentGameState = GameState.Starting;
                    }
            }
            }
            
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //========== Bullet ==========
            _spriteBatch.Begin();
            foreach (Bullet b in bulletList)
            {
                if (b.bulletUsed == false)
                {
                    b.Draw(_spriteBatch);
                }
            }

            //========== Enemy ==========
            foreach (Enemy ene in enemyArray)
            {
                if (ene.enemyIsAlive == true)
                {
                    ene.Draw(_spriteBatch);
                }
            }

            //========== Player ==========
            player.Draw(_spriteBatch);

            //========== Lives ==========
            for (int i = 0; i < Lives; i++)
            {
                int scale = 4;
                int w = heartTex.Width / scale;
                int h = heartTex.Height / scale;
                int x = 10 + i * (w + 5);
                int y = 10;
                _spriteBatch.Draw(heartTex, new Rectangle(x, y, w, h), Color.White); /// draw texture
            }

            //========== Title ==========
            _spriteBatch.Draw(titleTex, titlePos, Color.White);

            //========== Score ==========
            Vector2 scorePos = new Vector2(550, 10);
            if (score  > 0)
            {
                _spriteBatch.DrawString(scoreSpriteFont, "Score: " + score, scorePos, Color.Black);
            }
            Window.Title = "Space Invaders - Lives: " + Lives + " Score: " + score;

            //========== Start ==========

            if ( currentGameState == GameState.Starting)
            {
             _spriteBatch.Draw(startBackgroundTex, startPositionBackground, Color.White); // start background
             startButton.Draw(_spriteBatch); // start button
             _spriteBatch.Draw(startSpriteSheetTex, startSpriteSheetPos, startSpriteSheetRec , Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0); // alien animation on start screen

            }

            //========== Gameover ==========
            if (currentGameState == GameState.GameOver)
            {
                _spriteBatch.Draw(gameOverBackgroundTex, gameOverBackgroundPos, Color.Black); // game over background
                gameOverSpriteExplosion.Draw(_spriteBatch); // explosion animation
                gameOverSpriteStar.Draw(_spriteBatch); // star animation
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
