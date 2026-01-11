using UnityEngine;

public class DabbingBear : MonoBehaviour
{
    //This is to be placed on the D block
    [SerializeField] Placeable placeable;
    [SerializeField] GameObject DBlock;
    [SerializeField] GameObject ABlock;
    [SerializeField] GameObject BBlock;
    [SerializeField] SpriteRenderer bear;
    [SerializeField] Sprite dabbingBearDabSprite;
    [SerializeField] int xOffset;

    void Start()
    {
        placeable.onPlace += OnPlaced;
    }

    void OnPlaced(Tile tile)
    {
        if(GridManager.grid.GetTile(tile.x + xOffset, tile.y).contents.Contains(DBlock) &&GridManager.grid.GetTile(tile.x + 1 + xOffset, tile.y).contents.Contains(ABlock) && GridManager.grid.GetTile(tile.x + 2 + xOffset, tile.y).contents.Contains(BBlock))
        {
            bear.sprite = dabbingBearDabSprite;
        }
    }
}
