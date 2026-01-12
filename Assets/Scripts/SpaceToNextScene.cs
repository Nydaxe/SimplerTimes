using UnityEngine;

public class SpaceToNextScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneFader.instance.EndScene();
        }
    }
}
