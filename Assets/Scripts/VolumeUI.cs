using UnityEngine;

public class VolumeUI : MonoBehaviour
{
    public void SetObjectActiveOrInactive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
