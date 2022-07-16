using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] bgm;
    public Sfx[] sfx;

    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    void Awake() {

        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    void Update() {
    }

    public void PlaySfx(string name) {
        sfx[0].PlaySound(name);
    }
}
