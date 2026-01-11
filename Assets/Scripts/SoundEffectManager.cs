using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;
    [SerializeField] AudioSource pickupAudioSource;
    [SerializeField] AudioSource placeAudioSource;
    [SerializeField] AudioSource buttonAudioSource;

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

    public void PlaySoundEffect(AudioSource source, float volume = 1f)
    {
        source.volume = volume * GameManager.instance.volume;
        source.Play();
    }

    public void PlayPickupSound(float volume = 1f)
    {
        PlaySoundEffect(pickupAudioSource, volume);
    }

    public void PlayPlaceSound(float volume = 1f)
    {
        PlaySoundEffect(placeAudioSource, volume);
    }

    public void PlayButtonSound(float volume = 1f)
    {
        PlaySoundEffect(buttonAudioSource, volume);
    }
}
