using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

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

    public void FinishLevel()
    {
        int levelScore = CheckWinConditions();
        Debug.Log("Level Complete! Score: " + levelScore);
    }

    int CheckWinConditions()
    {
        int score = 0;

        for(int i = 0; i < winConditionObjects.Count; i++)
        {
            Vector2 targetPosition = (Vector2)catylistConditionObject.transform.position + winConditionRelativePositions[i];
            Tile requiredTile = GridManager.grid.GetTileWithWorldPosition(targetPosition);

            if(requiredTile == null || !requiredTile.contents.Contains(winConditionObjects[i]))
            {
                continue;
            }

            score++;
        }

        return Mathf.RoundToInt((score / winConditionObjects.Count) * 100f);
    }
}
