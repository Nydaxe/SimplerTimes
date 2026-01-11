using Unity.VisualScripting;
using UnityEngine;

public class Grid
{
    public Tile[,] tiles;
    public Vector2 origin {get; private set;}
    public float tileSize {get; private set;}
    public int xSize {get; private set;}
    public int ySize {get; private set;}

    public Grid(int xSize, int ySize, Vector2 origin, float tileSize)
    {
        this.origin = origin;
        this.tileSize = tileSize;
        this.xSize = xSize;
        this.ySize = ySize;

        tiles = new Tile[xSize, ySize];

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                Vector2 centerWorldPosition = origin + new Vector2((x + .5f) * tileSize, (y + .5f) * tileSize);

                tiles[x, y] = new Tile(new Vector2(x,y), centerWorldPosition, tileSize);
            }
        }
    }

    public Tile GetTileWithWorldPosition(Vector2 position)
    {
        return GetTile(Mathf.FloorToInt((position.x - origin.x)/tileSize), Mathf.FloorToInt((position.y - origin.y)/tileSize));
    }
    
    public Tile GetTile(int x, int y)
    {
        if (x < 0 || y < 0 || x >= xSize || y >= ySize) 
            return null;
            
        return tiles[x,y];
    }
}
