using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour, IObserver
{
    [SerializeField] private AudioMixer audioMixer;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource ambient1Source;
    [SerializeField] private AudioSource ambient2Source;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioSource mainSource;

    [Header("Audio Settings")]
    [SerializeField] private List<AudioClip> ambientClips;
    [SerializeField] private float crossFadeDuration = 2f;

    private int currentAmbientIndex = 0;
    private bool isTransitioning = false;

    private void Start()
    {

        ambient1Source.clip = GetRandomAmbientClip();
        ambient1Source.Play();

        StartCoroutine(MonitorAmbientTracks());
    }

    private AudioClip GetRandomAmbientClip()
    {
        int newIndex;
        do
        {
            newIndex = Random.Range(0, ambientClips.Count);
        } while (newIndex == currentAmbientIndex && ambientClips.Count > 1);

        currentAmbientIndex = newIndex;
        return ambientClips[currentAmbientIndex];
    }

    private IEnumerator MonitorAmbientTracks()
    {
        bool useFirstSource = true;

        while (true)
        {
            if (!isTransitioning)
            {
                AudioSource currentSource = useFirstSource ? ambient1Source : ambient2Source;
                AudioSource nextSource = useFirstSource ? ambient2Source : ambient1Source;

                if (currentSource.time >= currentSource.clip.length - crossFadeDuration)
                {
                    nextSource.clip = GetRandomAmbientClip();
                    nextSource.Play();
                    StartCoroutine(CrossfadeUsingMixer(useFirstSource));
                    useFirstSource = !useFirstSource;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator CrossfadeUsingMixer(bool fadeFromFirst)
    {
        isTransitioning = true;
        float elapsed = 0f;

        while (elapsed < crossFadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / crossFadeDuration;

            audioMixer.SetFloat("AmbientBlend", fadeFromFirst ? t : 1 - t);

            yield return null;
        }

        audioMixer.SetFloat("AmbientBlend", fadeFromFirst ? 1 : 0);
        isTransitioning = false;
    }

    public void UpdateMainBGMIntensity(float intensity)
    {
        audioMixer.SetFloat("MainBGMIntensity", intensity);
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