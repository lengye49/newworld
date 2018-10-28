using UnityEngine;
using System.Collections;

public class MusicController : Singleton<MusicController>
{
    private AudioClip music;
    private AudioSource _thisAudio;


    public void Play()
    {
        if (_thisAudio == null)
            _thisAudio = this.gameObject.AddComponent<AudioSource>();
       
        string path = "music/bg";
        _thisAudio.clip = Resources.Load(path) as AudioClip;
        _thisAudio.loop = true;
        _thisAudio.Play();
    }


}
