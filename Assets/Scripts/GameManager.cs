using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject catylistConditionObject;
    [SerializeField] List<GameObject> winConditionObjects;
    [SerializeField] List<Vector2> winConditionRelativePositions;
    [SerializeField] GameObject doneButton;
    [SerializeField] GameObject drawing;
    [SerializeField] TextMeshProUGUI scoreText;

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

    void Start()
    {
        SceneFader.instance.FadeSceneIn();

        if(drawing == null)
            return;

        drawing.SetActive(true);
        doneButton.SetActive(false);
        StartCoroutine("ShowDrawing");
    }

    public float volume = 1f;

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
    }

    public async void FinishLevel()
    {
        int levelScore = CheckWinConditions();
        Debug.Log("Level Complete! Score: " + levelScore);
        scoreText.text = "Score: " + levelScore + "%";
        scoreText.gameObject.SetActive(true);
        await Awaitable.WaitForSecondsAsync(4f);
        SceneFader.instance.EndScene();
    }

    int CheckWinConditions()
    {
        int score = 0;

        for(int i = 0; i < winConditionObjects.Count; i++)
        {
            Vector2 targetPosition = catylistConditionObject.GetComponent<Placeable>().occupiedTile.centerPosition + winConditionRelativePositions[i];
            Tile requiredTile = GridManager.grid.GetTileWithWorldPosition(targetPosition);

            if(requiredTile == null || !requiredTile.contents.Contains(winConditionObjects[i]))
            {
                Debug.Log(winConditionObjects[i].name + " not in " + requiredTile.x + ", " + requiredTile.y);
                continue;
            }

            score++;
        }

        return Mathf.RoundToInt(score / winConditionObjects.Count * 100f);
    }

    IEnumerator ShowDrawing()
    {
        Debug.Log("Showing drawing");

        yield return new WaitForSeconds(5f);

        drawing.GetComponent<SpriteRenderer>().DOFade(0f, 1f);
        yield return new WaitForSeconds(1f);

        drawing.SetActive(false);

        doneButton.SetActive(true);
    }
}
