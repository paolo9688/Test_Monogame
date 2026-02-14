using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player
{
    private Texture2D _spriteSheet;
    public Vector2 _position;
    private float _speed = 70f; // Pixel al secondo

    // Definiamo la dimensione della singola sprite
    public int _spriteWidth = 32;
    public int _spriteHeight = 32;

    public Player(Vector2 startPosition)
    {
        _position = startPosition;
    }

    public void LoadContent(ContentManager content)
    {
        // Assicurati che il nome "PlayerSheet" corrisponda a quello nel MGCB Editor
        _spriteSheet = content.Load<Texture2D>("Player");
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        KeyboardState state = Keyboard.GetState();
        Vector2 direction = Vector2.Zero;

        // Input semplice per movimento scattante
        if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up)) direction.Y -= 1;
        if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down)) direction.Y += 1;
        if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) direction.X -= 1;
        if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) direction.X += 1;

        // Normalizziamo la direzione per evitare che in diagonale vada più veloce
        if (direction != Vector2.Zero)
        {
            direction.Normalize();
            _position += direction * _speed * deltaTime;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Sorgente: Riga 1, Colonna 1 (Coordinate 0,0)
        Rectangle sourceRect = new Rectangle(0, 0, _spriteWidth, _spriteHeight);

        // Disegniamo il player
        spriteBatch.Draw(_spriteSheet, _position, sourceRect, Color.White);
    }
}