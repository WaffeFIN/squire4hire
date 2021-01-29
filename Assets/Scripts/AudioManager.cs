using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Simply handles audio */
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private Dictionary<string, AudioSource> audio_dic;
    
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        audio_dic = new Dictionary<string, AudioSource>();

        foreach (var s in sounds) {
            audio_dic.Add(s.name, s.AddToSource(gameObject.AddComponent<AudioSource>()));
        }
    }

    void OnDestroy() {
        foreach (var sound in sounds)
        {
            if (audio_dic.TryGetValue(sound.name, out AudioSource source)) {
				if (source.isPlaying) {
					source.Stop();
				}
			}
        }
    }

	public void RestorePitch(string name) {
        if(audio_dic.TryGetValue(name, out AudioSource source)) {
            source.pitch = 1.0f;
        }
	}

	public void LowPitch(string name) {
        if(audio_dic.TryGetValue(name, out AudioSource source)) {
            source.pitch = 0.5f;
        }
	}

    public void Play(string name) {
        if(audio_dic.TryGetValue(name, out AudioSource source)) {
            source.Play();
        }
    }

    public void Stop(string name) {
        if (audio_dic.TryGetValue(name, out AudioSource source)) {
            if (source.isPlaying) {
                source.Stop();
            }
        }
    }
}
