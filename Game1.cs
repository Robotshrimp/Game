using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameJom
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static double ScreenSizeAdjustment = 1;
        public static Vector calculationScreenSize = new Vector(3840, 2160);
        public static Rectangle ScreenBounds;
        public static int GameState = 1;
        public static bool Paused = false;
        public static MouseState mouseState;
        int XMousePos;
        int YMousePos;
        Texture2D PlayerTexture;
        Rectangle Player = new Rectangle(0, 0, 96, 96);
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            double num = (double)graphics.PreferredBackBufferWidth / 3840;
            if (num > (double)graphics.PreferredBackBufferHeight / 2160)
            {
                num = (double)graphics.PreferredBackBufferHeight / 2160;
            }

            // use this for calculations outside of drawing

            ScreenBounds = new Rectangle(
                (int)((graphics.PreferredBackBufferWidth - 3840 * num) / 2), 
                (int)((graphics.PreferredBackBufferHeight - 2160 * num) / 2), 
                (int)(3840 * num), (int)(2160 * num));
            ScreenSizeAdjustment = num;
            graphics.ApplyChanges();
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            PlayerTexture = Content.Load<Texture2D>("BasicShape");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Player.Y -= 10;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Player.Y += 10;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Player.X -= 10;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Player.X += 10;

            base.Update(gameTime);


            base.Draw(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            AutomatedDraw MainCamera = new AutomatedDraw(ScreenBounds, new Vector(Player.X + Player.Width / 2, Player.Y + Player.Height / 2),  Color.White, GameState == 2);
            MainCamera.draw(Player, PlayerTexture);
            MainCamera.draw(new Rectangle(0,0, 1000, 1000), PlayerTexture);
            MainCamera.draw(new Rectangle(-1500, -1500, 100, 100), PlayerTexture);
            AutomatedDraw paralaxDraw = new AutomatedDraw(ScreenBounds, new Vector(Player.X + Player.Width / 2, Player.Y + Player.Height / 2), Color.Green, GameState == 2, 0.5);
            paralaxDraw.draw(new Rectangle(0, 0, 1000, 1000), PlayerTexture);
            
            AutomatedDraw Base = new AutomatedDraw(ScreenBounds, Color.White);

            Button button = new Button(Base, GameState == 1);
            button.ButtonUpdate(new Rectangle(300 , 300, 1000, 300), PlayerTexture);
            if (button.Pressed)
            {
                GameState = 2;
            }



            AutomatedDraw unprocessed = new AutomatedDraw(new Rectangle(0,0,graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.Black, true, (double)1/ScreenSizeAdjustment);

            unprocessed.draw(new Rectangle(mouseState.X, mouseState.Y, 30, 40), PlayerTexture, Color.White);
            unprocessed.draw(new Rectangle(0, 0, calculationScreenSize.X, ScreenBounds.Top), PlayerTexture);
            unprocessed.draw(new Rectangle(0, ScreenBounds.Bottom, calculationScreenSize.X, ScreenBounds.Top), PlayerTexture);

            //Base.draw(new Rectangle(300, 300, 1000, 300), PlayerTexture);
            base.Draw(gameTime);
        }
    }
}
