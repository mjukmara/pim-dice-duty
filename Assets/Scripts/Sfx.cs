using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx : MonoBehaviour
{
	public AudioSource audioSource;
    public Sound[] sounds;

    Dictionary<string, Sound> soundByName = new Dictionary<string, Sound>();

    void Start() {
        foreach (Sound s in sounds) {
            soundByName.Add(s.soundName, s);
        }
    }

    public void PlaySound(string soundName) {
        Sound s;
        soundByName.TryGetValue(soundName, out s);
        if (s != null) {
            audioSource.PlayOneShot(s.clip);
        }
    }
}
