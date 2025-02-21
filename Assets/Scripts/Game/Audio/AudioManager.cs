using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IObserver
{
    [SerializeField]
    private AudioSource source1;

    [SerializeField]
    private AudioSource source2;

    [SerializeField]
    private AudioSource mainSource;

    [SerializeField]
    private AudioSource effectSource;

    private void PlayEffect(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    private void PlayVariedEffect(AudioClip clip)
    {
        RandomPitch();
        effectSource.PlayOneShot(clip);
    }

    private void ResetPitch()
    {
        effectSource.pitch = 1f;
    }

    private void RandomPitch()
    {
        effectSource.pitch = Random.Range(0.95f, 1.05f);
    }

    public void OnNotify(string eventType, object data)
    {
        if (eventType == "Effect")
        {
            PlayEffect((AudioClip)data);
        }
        else if (eventType == "VariedEffect")
        {
            Debug.Log("VariedEffect");
            PlayVariedEffect((AudioClip)data);
        }
    }
}
