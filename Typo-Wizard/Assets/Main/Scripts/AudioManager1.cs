using System;
using UnityEngine;

public class AudioManager1 : MonoBehaviour
{
    [System.Serializable]
    public class Sound2
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;
        [HideInInspector]
        public AudioSource source;
        public bool playOnAwake;
        public bool loop;
    }
    public Sound2[] sounds;
    void Awake()
    {
        foreach (Sound2 s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = s.playOnAwake;
            s.source.loop = s.loop;
        }
    }

    public void Start()
    {
        Play("BackGroundSound");
    }
    public void Stop(string name)
    {

        Sound2 s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Stop();

    }

    public void Play(string name)
    {
        Sound2 s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }

}
