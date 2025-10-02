using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;
using System.Runtime.Intrinsics;

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
        Rectangle startBackgroundRec;

        Start startButton;
        Vector2 startButtonPos;

        Texture2D startSpriteSheetTex;
        Vector2 startSpriteSheetPos;
        Rectangle startSpriteSheetRec;
        Point frameSize = new Point(100, 100);
        Point currentFrame = new Point(0, 0);
        Point sheetSize = new Point(3, 1);
        int timeSinceLastFrame = 0;
        int millisecondPerFrame = 1000;

        //========== Gameover ==========
        Texture2D gameOverTex;
        Vector2 gameOverPos;

        Texture2D gameOverBackgroundTex;
        Rectangle gameOverBackgroundRec;

        GameoverState gameOverSpriteExplosion;
        Vector2 gameOverSpriteExplosionPos;

        GameoverState gameOverSpriteStar;
        Vector2 gameOverSpriteStarPos;
        
        GameoverState gameOverSpriteStar2;
        Vector2 gameOverSpriteStar2Pos;

        GameoverState gameOverSpriteExplosion2;
        Vector2 gameOverSpriteExplosion2Pos;

        GameoverState gameOverSpriteExplosion3;
        Vector2 gameOverSpriteExplosion3Pos;
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

            Texture2D alientopTex = Content.Load<Texture2D>("alien03_sprites");
            Texture2D alienmidTex = Content.Load<Texture2D>("alien01_sprites");
            Texture2D alienbottomTex = Content.Load<Texture2D>("orangemonster");

            enemyArray = new Enemy[5, 5];

            for (int i = 0; i < 5; i++)
             {
                for (int j = 0; j < 5; j++)
                {
                    int x = (int) enemyPos.X + j * 120;
                    int y = (int) enemyPos.Y + i * 100;

                    Texture2D tex;
                    int points;

                    if (i == 0) // top
                    {
                        tex = alientopTex;
                        points = 5;
                    }
                    else if (i == 1 || i == 2) // mid
                    {
                        tex = alienmidTex;
                        points = 3;
                    }
                    else // bottom
                    {
                        tex = enemyTex;
                        points = 1;
                    }   

                    enemyArray[i, j] = new Enemy(tex, x, y, points);
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
                startBackgroundRec = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);

                Texture2D startButtonTex = Content.Load<Texture2D>("Startknapp");
                startButtonPos = new Vector2(250, 400);
                startButton = new Start(startButtonTex, (int)startButtonPos.X, (int)startButtonPos.Y);

                startSpriteSheetTex = Content.Load<Texture2D>("alien02_sprites");
                startSpriteSheetPos = new Vector2(260, 260);
                startSpriteSheetRec = new Rectangle(0, 0, 100, 90);

            }

            //========== Gameover ==========
            gameOverTex = Content.Load<Texture2D>("game_over-2");
            gameOverPos = new Vector2(90, 250);

            gameOverBackgroundTex = Content.Load<Texture2D>("Arkadkabinett-1");
            gameOverBackgroundRec = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);

            Texture2D gameOverSpriteExplosionTex = Content.Load<Texture2D>("explotion01_sprites");
            gameOverSpriteExplosionPos = new Vector2(150, 200);
            gameOverSpriteExplosion = new GameoverState(gameOverSpriteExplosionTex, gameOverSpriteExplosionPos);

            Texture2D gameOverSpriteStarTex = Content.Load<Texture2D>("star_01");
            gameOverSpriteStarPos = new Vector2(160, 200);
            gameOverSpriteStar = new GameoverState(gameOverSpriteStarTex,gameOverSpriteStarPos);

            gameOverSpriteStar2Pos = new Vector2(160, 200);
            gameOverSpriteStar2 = new GameoverState(gameOverSpriteStarTex, gameOverSpriteStar2Pos);

            gameOverSpriteExplosion2Pos = new Vector2(170, 200);
            gameOverSpriteExplosion2 = new GameoverState(gameOverSpriteExplosionTex, gameOverSpriteExplosion2Pos);

           gameOverSpriteExplosion3Pos = new Vector2(180, 200);
            gameOverSpriteExplosion3 = new GameoverState(gameOverSpriteExplosionTex, gameOverSpriteExplosion3Pos);

        }

        protected override void Update(GameTime gameTime)
        {
            //========== GameState ==========

            //==============================
            //          Start Screen
            //==============================

            // Movement logic

            bool hitWall = false;

            if (currentGameState == GameState.Starting)
            {
                //========== Enemy ==========
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

            //==============================
            //          Main Game
            //==============================
            
            if (currentGameState == GameState.Playing)
            {
                //========== Enemy ==========
                // Update enemies

                    //========== Enemy ==========

                    foreach (Enemy ene in enemyArray)
                    {
                        if (ene.enemyIsAlive)
                        {
                            ene.Update();

<<<<<<< Updated upstream
                            if (ene.enemyPos.X <= 0 || ene.enemyPos.X >= 950 - enemyTex.Width)
                            {
                                hitWall = true;
                            }
                        }
                    }

                    if (hitWall)
                    {
                        foreach (Enemy ene in enemyArray)
                        {
                            ene.MoveDown();
                        }
                    }

                    bool noEnemyLeftInArray = true;
                    foreach (Enemy ene in enemyArray)
                    {
                        if (ene.enemyIsAlive == true)
=======
           
            // Collision logic
         
                bool noEnemyLeftInArray = true;
                foreach (Enemy ene in enemyArray)
                {
                    if (ene.enemyIsAlive == true)
>>>>>>> Stashed changes
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
                }

            //==============================
            //        Game Over Screen
            //==============================
            
            if (currentGameState == GameState.GameOver)
            {
                gameOverSpriteStar.Update(gameTime);
                gameOverSpriteExplosion.Update(gameTime);
                gameOverSpriteExplosion2.Update(gameTime);
                gameOverSpriteExplosion3.Update(gameTime);
                gameOverSpriteStar2.Update(gameTime);

                // switch to start screen when pressing Enter or Space
                KeyboardState state = Keyboard.GetState();
                if (previousKeyBoardState.IsKeyUp(Keys.Enter) && previousKeyBoardState.IsKeyDown(Keys.Enter) || previousKeyBoardState.IsKeyUp(Keys.Space) && previousKeyBoardState.IsKeyDown(Keys.Space)) // not working
                {
                    Lives = 5;
                    LoadContent();
                    currentGameState = GameState.Starting;
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
            if (score > 0)
            {
                _spriteBatch.DrawString(scoreSpriteFont, "Score: " + score, scorePos, Color.Black);
            }
            Window.Title = "Space Invaders - Lives: " + Lives + " Score: " + score;

            //========== Start ==========

            if ( currentGameState == GameState.Starting)
            {
             _spriteBatch.Draw(startBackgroundTex, startBackgroundRec, Color.White); // start background
             startButton.Draw(_spriteBatch); // start button
             _spriteBatch.Draw(startSpriteSheetTex, startSpriteSheetPos, startSpriteSheetRec , Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0); // alien animation on start screen

            }

            //========== Gameover ==========
            if (currentGameState == GameState.GameOver)
            {
                _spriteBatch.Draw(gameOverBackgroundTex, gameOverBackgroundRec, Color.White);
                _spriteBatch.Draw(gameOverTex, gameOverPos, Color.Black); // game over background
                gameOverSpriteExplosion.Draw(_spriteBatch); // explosion animation
                gameOverSpriteStar.Draw(_spriteBatch); // star animation
                gameOverSpriteExplosion3.Draw(_spriteBatch);
                gameOverSpriteStar2.Draw(_spriteBatch);
                gameOverSpriteExplosion2.Draw(_spriteBatch);
            }


            _spriteBatch.End();

            base.Draw(gameTime);


        }

}

}


