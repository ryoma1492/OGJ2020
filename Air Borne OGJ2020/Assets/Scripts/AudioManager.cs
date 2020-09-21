using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Xml.Serialization;
using System.Linq;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    [SerializeField] private float delay = 1f;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Destroy");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.isLoop;
            s.source.time = s.startTime;
            s.source.name = s.name;
        }
    }
    public void PlayClip(string soundname)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundname);
        if (s == null)
        {
            Debug.LogWarning("Sound: '" + soundname + "' is not Found");
            return;
        }
        s.source.Play();
    }
    public void PlayPartialClip(string soundname, float startTime, float duration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundname);
        if (s == null)
        {
            Debug.LogWarning("Sound: '" + soundname + "' is not Found");
            return;
        }
        s.source.time = startTime;
        s.source.Play();
        s.source.SetScheduledEndTime((double)duration);
    }
    public void PlayClipAfterDone(string soundname)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundname);
        if (s == null)
        {
            Debug.LogWarning("Sound: '" + soundname + "' is not Found");
            return;
        }
        if (!s.source.isPlaying)
        {
            s.source.Play();
        }
    }
    public void PlayAtVolume(string soundname, float clipVolume)
    {
        float origVolume;
        Sound s = Array.Find(sounds, sound => sound.name == soundname);
        if (s == null)
        {
            Debug.LogWarning("Sound: '" + soundname + "' is not Found");
            return;
        }
        origVolume = s.volume;
        s.volume = clipVolume;
        s.source.Play();
        s.volume = origVolume;

    }
    public void StopClip(string soundname)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundname);
        if (s == null)
        {
            Debug.LogWarning("Sound: '" + soundname + "' is not Found");
            return;
        }
        if (s.source.isPlaying)
        {
            s.source.Stop();
        }
    }
    public void FadeClip (string soundname, string soundname2)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundname);
        if (s == null)
        {
            Debug.LogWarning("Sound: '" + soundname + "' is not Found");
            return;
        }
        Sound s2 = Array.Find(sounds, sound => sound.name == soundname2);
        if (s2 == null)
        {
            Debug.LogWarning("Sound2: '" + soundname2 + "' is not Found");
            return;
        }
        StartCoroutine(CRFadeClip(s,s2));
    }

    IEnumerator CRFadeClip(Sound soundname, Sound name2)
    {

        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / delay);
            yield return null;
        }
        soundname.source.Stop();
        name2.source.Play();
    }
    // Update is called once per frame
    void Update()
    {
    }
}
