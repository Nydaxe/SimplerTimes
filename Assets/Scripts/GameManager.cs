using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    public float volume;

    void Start()
    {
        
    }
}

public struct GameSettings
{
    public float musicVolume;
    public float sfxVolume;
}
