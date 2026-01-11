using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static Grid grid;
    [SerializeField] int xSize;
    [SerializeField] int ySize;
    [SerializeField] float tileSize;
    [SerializeField] Vector2 origin;
    

    void Awake()
    {
        grid = new Grid(xSize, ySize, origin, tileSize);
    }
}
