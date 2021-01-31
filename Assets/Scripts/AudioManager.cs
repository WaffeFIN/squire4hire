
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Simply handles audio */
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private Dictionary<string, AudioSource> audioDic;
    private Dictionary<string, int> audioChoices;
    
    void Awake() {
        DontDestroyOnLoad(gameObject);
        audioDic = new Dictionary<string, AudioSource>();
        audioChoices = new Dictionary<string, int>();

        foreach (var s in sounds) {
            audioDic.Add(s.name, s.AddToSource(gameObject.AddComponent<AudioSource>()));
        }

		string pattern = @"\d+$";
		Regex regex = new Regex(pattern);

		foreach (var key in audioDic.Keys) {
			string prefix = regex.Replace(key, "");
			if (prefix == key) continue; // does not end in digits
			if (!audioChoices.ContainsKey(prefix)) {
				audioChoices[prefix] = 0;
			}
			audioChoices[prefix]++;
		}
    }

    void OnDestroy() {
        foreach (var sound in sounds)
        {
            if (audioDic.TryGetValue(sound.name, out AudioSource source)) {
				if (source.isPlaying) {
					source.Stop();
				}
			}
        }
    }

	public void RestorePitch(string name) {
        if (audioDic.TryGetValue(name, out AudioSource source)) {
            source.pitch = 1.0f;
        }
	}

	public void LowPitch(string name) {
        if (audioDic.TryGetValue(name, out AudioSource source)) {
            source.pitch = 0.5f;
        }
	}

    public void PlayRandom(string prefix) {
		if (audioChoices.TryGetValue(prefix, out int amount)) {
			var soundFile = prefix + Mathf.Ceil(Random.Range(1, amount));
            audioDic[soundFile].Play();
        }
	}

    public void Play(string name) {
        if (audioDic.TryGetValue(name, out AudioSource source)) {
            source.Play();
        }
    }

    public void Stop(string name) {
        if (audioDic.TryGetValue(name, out AudioSource source)) {
            if (source.isPlaying) {
                source.Stop();
            }
        }
    }
}
