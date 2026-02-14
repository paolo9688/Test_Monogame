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

    // Animazione
    private float _timer;
    private float _frameTime = 0.15f; // Velocità animazione (100ms per frame)
    private int _currentFrame;
    private int _currentRow;
    private int _currentRowToDraw;
    private SpriteEffects _flipped = SpriteEffects.None;
    private bool _isMoving;

    public Player(Vector2 startPosition)
    {
        _position = startPosition;
        _currentRow = 0;
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

        _isMoving = false;

        // Input e impostazione riga/direzione
        if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up)) { direction.Y -= 1; _currentRow = 2; _isMoving = true; }
        else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down)) { direction.Y += 1; _currentRow = 0; _isMoving = true; }
        
        if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) 
        { 
            direction.X -= 1; 
            _currentRow = 1; 
            _flipped = SpriteEffects.FlipHorizontally; // Specchia a sinistra
            _isMoving = true; 
        }
        else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right)) 
        { 
            direction.X += 1; 
            _currentRow = 1; 
            _flipped = SpriteEffects.None; // Destra normale
            _isMoving = true; 
        }

        // Se stiamo camminando, passiamo alle righe 3, 4, 5 (Walk)
        int actualRow = _isMoving ? _currentRow + 3 : _currentRow;

        // Logica Movimento
        if (direction != Vector2.Zero)
        {
            direction.Normalize();
            _position += direction * _speed * deltaTime;
        }

        // Logica Animazione (Ciclo tra i 6 frame della colonna)
        _timer += deltaTime;
        if (_timer >= _frameTime)
        {
            _currentFrame++;
            if (_currentFrame >= 6) _currentFrame = 0;
            _timer = 0;
        }

        // Aggiorniamo la riga effettiva per il Draw
        _currentRowToDraw = actualRow;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Calcolo del rettangolo sorgente basato su frame corrente e riga
        Rectangle sourceRect = new Rectangle(
            _currentFrame * _spriteWidth, 
            _currentRowToDraw * _spriteHeight, 
            _spriteWidth, 
            _spriteHeight
        );

        // Usiamo l'overload di Draw che supporta SpriteEffects per lo specchio
        spriteBatch.Draw(
            _spriteSheet, 
            _position, 
            sourceRect, 
            Color.White, 
            0f, 
            Vector2.Zero, 
            1f, 
            _flipped, 
            0f
        );
    }
}