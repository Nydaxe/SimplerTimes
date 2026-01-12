using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] Vector2Int[] additionalOccupiedTiles;

    public Vector2 offset;

    public Tile occupiedTile {get; private set;}

    public event Action<Tile> onPlace;


    public bool Place(Tile tile, bool allowPlaceNeighbors = false)
    {
        if(additionalOccupiedTiles.Length > 0)
        {
            foreach(Vector2Int offset in additionalOccupiedTiles)
            {
                Tile additionalTile = GridManager.grid.GetTile(tile.x + offset.x, tile.y + offset.y);
                if(additionalTile == null || !AbleToPlace(additionalTile))
                {
                    return false;
                }
            }
        }
        else if(!AbleToPlace(tile))
        {
            bool foundNewTile = false;

            Tile[] neighbors = tile.GetNeighbors();
            neighbors = neighbors.OrderBy(tile => Vector2.Distance(transform.position, tile.centerPosition)).ToArray();

            foreach(Tile neighbor in neighbors)
            {
                if(neighbor != null && AbleToPlace(neighbor))
                {
                    foundNewTile = true;
                    tile = neighbor;
                    break;
                }
            }

            if(!foundNewTile)
            {
                return false;
            }
        }

        if(!AbleToPlace(tile) || tile == null)
        {
            return false;
        }

        foreach(Vector2Int additionalTileOffset in additionalOccupiedTiles)
        {
            Tile additionalTile = GridManager.grid.GetTile(tile.x + additionalTileOffset.x, tile.y + additionalTileOffset.y);
            additionalTile.AddItem(gameObject);
        }
        tile.AddItem(gameObject);
        occupiedTile = tile;

        onPlace?.Invoke(tile);
        transform.position = tile.centerPosition - offset;
        
        return true;
    }

    public bool AbleToPlace(Tile tile)
    {
        return !tile.IsOccupied();
    }

    public void Remove()
    {
        foreach(Vector2Int additionalTileOffset in additionalOccupiedTiles)
        {
            Tile additionalTile = GridManager.grid.GetTile(occupiedTile.x + additionalTileOffset.x, occupiedTile.y + additionalTileOffset.y);
            additionalTile.RemoveItem(gameObject);
        }

        occupiedTile.RemoveItem(gameObject);
    }
    
    void Start()
    {
        Place(GridManager.grid.GetTileWithWorldPosition((Vector2)transform.position + offset));
    }
}