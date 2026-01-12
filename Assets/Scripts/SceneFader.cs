using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;
    [SerializeField] float fadeDuration = 1f;
    [SerializeField] string[] scenes;
    [SerializeField] int sceneIndex = 0;
    Light2D globalLight;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        
        FadeSceneIn();
    }

    public void FadeSceneIn()
    {
        Light2D[] lights = FindObjectsByType<Light2D>(FindObjectsSortMode.None);
        if (lights.Length == 0)
        {
            return;
        }
        
        globalLight = lights[0];
        globalLight.intensity = 0;
        DOTween.To(() => globalLight.intensity, x => globalLight.intensity = x, 1, fadeDuration);
    }

    public void EndScene()
    {
        if (globalLight == null)
        {
            return;
        }
        
        if(sceneIndex >= scenes.Length - 1)
        {
            return;
        }
        
        globalLight.intensity = 1;
        DOTween.To(() => globalLight.intensity, x => globalLight.intensity = x, 0, fadeDuration);
        LoadNextSceneAfterDelay(fadeDuration);
    }

    async void LoadNextSceneAfterDelay(float delay)
    {
        await Awaitable.WaitForSecondsAsync(delay);
        sceneIndex++;
        
        if (sceneIndex >= scenes.Length)
        {
            return;
        }
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(scenes[sceneIndex]);
    }
}
