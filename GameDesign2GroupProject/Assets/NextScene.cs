using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{

    [SerializeField]
    public float delayTime = 10f;
    public int loadThisScene = 2;

    private float timeElapsed;

    public Image fadeOutImage;

    Color fadeColour;

    // Start is called before the first frame update
    void Start()
    {
        fadeColour = new Color(0, 0, 0, 0);

        fadeOutImage.color = fadeColour;

        fadeOutImage.gameObject.SetActive(false);

        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if(timeElapsed > delayTime)
        {

            FadeToScene(2);
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

}