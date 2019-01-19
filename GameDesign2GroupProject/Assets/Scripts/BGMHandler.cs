using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMHandler : MonoBehaviour {

    public AudioClip defaultBGM;

    AudioSource source;

	void Start () { 
        source = GetComponent<AudioSource>();

        source.clip = defaultBGM;
        source.Play();
	}
	
	public void ChangeBGM(AudioClip newBGM) {
        StartCoroutine("BGMFadeOut", newBGM);
    }

    public void EndBGM() {
        StartCoroutine("BGMEnd");
    }

    IEnumerator BGMFadeOut(AudioClip newBGM) {
        while(source.volume > 0) {
            source.volume -= 0.03f;

            yield return new WaitForSeconds(0.1f);
        }

        source.Stop();

        yield return new WaitForSeconds(0.5f);

        source.clip = newBGM;
        source.volume = 0.3f;
        source.Play();
    }

    IEnumerator BGMEnd() {
        while (source.volume > 0) {
            source.volume -= 0.03f;

            yield return new WaitForSeconds(0.1f);
        }

        source.Stop();
    }

}
