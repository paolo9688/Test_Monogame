using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Test_Monogame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Riferimento alla nostra classe mappa
        private MapManager _mapManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Impostiamo una risoluzione fissa per il test
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 480;
        }

        protected override void Initialize()
        {
            // Inizializziamo l'oggetto mappa
            _mapManager = new MapManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Carichiamo gli asset della mappa
            _mapManager.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Qui in futuro chiameremo _player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Cambiamo il colore di sfondo in un verde scuro (più adatto a un RPG)
            GraphicsDevice.Clear(new Color(34, 34, 34));

            // IMPORTANTE: SamplerState.PointClamp serve per mantenere i pixel nitidi (no sfocatura)
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // Disegniamo la mappa
            _mapManager.Draw(_spriteBatch);

            // Qui in futuro chiameremo _player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
