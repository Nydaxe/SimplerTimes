using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Timeline;

public class EndingMouseMovement : MonoBehaviour
{
    [SerializeField] float movementDuration = 1f;
    [SerializeField] float movementRange = 8f;
    IEnumerator Start()
    {
        while (true)
        {
            float movement = Random.Range(-movementRange, movementRange);
            int moveMult = Random.Range(0, 2) == 0 ? -1 : 1;

            transform.DOMoveX(transform.position.x + movement * moveMult, movementDuration).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            transform.DOMoveX(transform.position.x - movement * moveMult, movementDuration).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }
}
