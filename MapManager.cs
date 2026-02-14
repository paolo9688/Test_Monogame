using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class MapManager
{
    private Texture2D _tileset;
    private int _tileSize = 16;
    private int _mapWidth = 20;  // Numero di tile in orizzontale
    private int _mapHeight = 15; // Numero di tile in verticale

    // Un array 2D per i dati della mappa (0 = erba tipo A, 1 = erba tipo B, ecc.)
    private int[,] _mapData;

    public MapManager()
    {
        // Generiamo una mappa vuota (tutti 0)
        _mapData = new int[_mapWidth, _mapHeight];
    }

    public void LoadContent(ContentManager content)
    {
        // Carica l'immagine dei tile
        _tileset = content.Load<Texture2D>("Grass_Middle");
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                // Calcoliamo la posizione a schermo
                Vector2 position = new Vector2(x * _tileSize, y * _tileSize);
                
                // Per ora disegniamo sempre il primo tile del tileset
                // Rectangle(x_nel_tileset, y_nel_tileset, larghezza, altezza)
                Rectangle sourceRect = new Rectangle(0, 0, _tileSize, _tileSize);

                spriteBatch.Draw(_tileset, position, sourceRect, Color.White);
            }
        }
    }
}