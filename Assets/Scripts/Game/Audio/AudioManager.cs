using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour, IObserver
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource ambient1Source;
    [SerializeField] private AudioSource ambient2Source;
    [SerializeField] private AudioSource mainSource;
    [SerializeField] private AudioSource effectSource;

    [SerializeField] private List<AudioClip> ambientClips;
    [SerializeField] private float crossFadeDuration = 2f;

    private void Start()
    {
        audioMixer.SetFloat("Ambient1Volume", 0f);
        audioMixer.SetFloat("Ambient2Volume", -80f);
    }

    private IEnumerator CrossfadeAmbientTracks(AudioClip nextClip)
    {
        if (ambient1Source.isPlaying)
        {
            ambient2Source.clip = nextClip;
            ambient2Source.Play();

            float elapsed = 0f;
            while (elapsed < crossFadeDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / crossFadeDuration;

                audioMixer.SetFloat("Ambient1Volume", Mathf.Lerp(0f, -80f, t));
                audioMixer.SetFloat("Ambient2Volume", Mathf.Lerp(-80f, 0f, t));

                yield return null;
            }

            ambient1Source.Stop();
        }
        else
        {
            ambient1Source.clip = nextClip;
            ambient1Source.Play();

            float elapsed = 0f;
            while (elapsed < crossFadeDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / crossFadeDuration;

                audioMixer.SetFloat("Ambient2Volume", Mathf.Lerp(0f, -80f, t));
                audioMixer.SetFloat("Ambient1Volume", Mathf.Lerp(-80f, 0f, t));

                yield return null;
            }

            ambient2Source.Stop();
        }
    }

    public void UpdateMainBGMIntensity(float intensity)
    {
        audioMixer.SetFloat("MainBGMLowpass", Mathf.Lerp(22000f, 1000f, intensity));
        audioMixer.SetFloat("MainBGMReverb", Mathf.Lerp(0f, 1f, intensity));
    }

    private void PlayEffect(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void OnNotify(string eventType, object data)
    {
        switch (eventType)
        {
            case "Effect":
                PlayEffect((AudioClip)data);
                break;
            case "UpdateBGMIntensity":
                UpdateMainBGMIntensity((float)data);
                break;
        }
    }
}