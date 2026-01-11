using UnityEngine;
using System;

[RequireComponent(typeof(Placeable))]
public class Draggable : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float draggingAccelerationSpeed = .1f;
    [SerializeField] float constantDraggingSpeed = .05f;
    bool allowDragging = true;
    bool dragging = false;
    Vector2 lastPosition;
    public event Action<GameObject> OnDragged;

    Placeable placeable;

    void Awake()
    {
        placeable = GetComponent<Placeable>();
    }

    void Update()
    {
        if(dragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Tile tile = GridManager.grid.GetTileWithWorldPosition(mousePosition);
            Vector2 targetPosition;

            if(tile == null || tile.IsOccupied())
            {
                targetPosition = mousePosition - placeable.offset;
            }
            else
            {
                targetPosition = tile.centerPosition - placeable.offset;
            }

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, draggingAccelerationSpeed * Vector2.Distance(transform.position, targetPosition) + constantDraggingSpeed);
        }
    }

    void OnMouseDown()
    {
        if(allowDragging && isActiveAndEnabled)
        {
            SoundEffectManager.instance.PlayPickupSound();

            spriteRenderer.sortingOrder = 100;
            dragging = true;
            placeable.Remove();
            
            lastPosition = transform.position;
            OnDragged?.Invoke(this.gameObject);
        }
    }

    void OnMouseUp()
    {
        if(!dragging)
        {
            return;
        }

        spriteRenderer.sortingOrder = 0;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Tile tile = GridManager.grid.GetTileWithWorldPosition(mousePosition);

        SoundEffectManager.instance.PlayPlaceSound();
        
        bool placed = placeable.Place(tile, true);

        if(!placed)
        {
            transform.position = lastPosition;
        }
        
        dragging = false;
    }
}
