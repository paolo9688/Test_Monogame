using Microsoft.Xna.Framework;

public class Camera
{
    // Più alto è il valore, più "vicini" saremo. 3f o 4f è ottimo per pixel art 16x16 o 32x32.
    public float Zoom { get; set; } = 4f; 
    public Vector2 Position { get; set; }
    public Matrix Transform { get; private set; }

    public void Follow(Player target, int screenWidth, int screenHeight)
    {
        // Calcoliamo la matrice per centrare il player
        // 1. Spostiamo l'origine al centro dello schermo
        // 2. Applichiamo lo zoom
        // 3. Sottraiamo la posizione del player
        var focus = Matrix.CreateTranslation(
            -target._position.X - (target._spriteWidth / 2), 
            -target._position.Y - (target._spriteHeight / 2), 
            0);

        var offset = Matrix.CreateTranslation(
            screenWidth / 2, 
            screenHeight / 2, 
            0);

        Transform = focus * Matrix.CreateScale(Zoom) * offset;
    }
}