using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour {

    public List<string> dialogue = new List<string>();

    public Image fadeOutImage;

    public Text dialogueText;

    Color fadeColour;
    Color textColour;

    void Start () {
        fadeColour = new Color(0, 0, 0, 0);

        fadeOutImage.color = fadeColour;

        fadeOutImage.gameObject.SetActive(false);

        if (dialogue.Count > 0) {
            textColour = Color.white;
            dialogueText.color = textColour;
            StartCoroutine("Dialogue");
        }
	}

    public void StartFadeIn() {
        StartCoroutine("FadeIn");
    }

    public void FadeToScene(int nextSceneIndex) {
        StartCoroutine("FadeOut", nextSceneIndex);
    }

    IEnumerator FadeOut(int nextSceneIndex) {
        fadeOutImage.gameObject.SetActive(true);

        while (fadeColour.a < 1) {
            fadeColour.a += 0.02f;
            
            fadeOutImage.color = fadeColour;

            yield return new WaitForSeconds(0.02f);
        }

        LevelManager._instance.ChangeSceneByIndex(nextSceneIndex);
    }

    IEnumerator FadeIn() {
        fadeOutImage.gameObject.SetActive(true);
        fadeColour.a = 1;

        while (fadeColour.a > 0) {
            fadeColour.a -= 0.01f;

            fadeOutImage.color = fadeColour;

            yield return new WaitForSeconds(0.02f);
        }

        fadeOutImage.gameObject.SetActive(false);
    }

    IEnumerator Dialogue() {
    int diaNum = 0;

        while (diaNum < dialogue.Count) {
            textColour.a = 0;

            dialogueText.text = dialogue[diaNum];
            dialogueText.color = textColour;

            yield return new WaitForSeconds(1f);

            while (textColour.a < 1) {
                textColour.a += 0.01f;

                dialogueText.color = textColour;

                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(1f);

            while (textColour.a > 0) {
                textColour.a -= 0.01f;

                dialogueText.color = textColour;

                yield return new WaitForSeconds(0.01f);
            }

            diaNum++;
        }

        yield return new WaitForSeconds(1f);

        FadeToScene(2);
    }

}
