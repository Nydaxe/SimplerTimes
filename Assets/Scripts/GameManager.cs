using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject catylistConditionObject;
    [SerializeField] List<GameObject> winConditionObjects;
    [SerializeField] List<Vector2> winConditionRelativePositions;

    public static GameManager instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float volume = 1f;

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
    }

    IEnumerator Start()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            CheckWinConditions();
        }
    }

    void CheckWinConditions()
    {
        for(int i = 0; i < winConditionObjects.Count; i++)
        {
            Vector2 targetPosition = (Vector2)catylistConditionObject.transform.position + winConditionRelativePositions[i];
            Tile requiredTile = GridManager.grid.GetTileWithWorldPosition(targetPosition);

            if(requiredTile == null || !requiredTile.contents.Contains(winConditionObjects[i]))
            {
                return;
            }
        }

        Debug.Log("You Win!");
    }
}
