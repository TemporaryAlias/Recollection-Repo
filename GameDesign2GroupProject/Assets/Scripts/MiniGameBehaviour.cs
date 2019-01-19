using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MiniGameBehaviour : MonoBehaviour {

    List<BeatBlock> blocks = new List<BeatBlock>();

    int currentBeatmap = 0;

    Color memoryColour;

    [Header("Score Settings")]
    public float minigameScore;
    public float scoreToReach;

    public Text minigameScoreText;

    public Slider scoreProgressBar;

    [Space(5)]

    [Header("Memory Settings")]

    public SpriteRenderer memorySprite;

    public GameObject blockingCloud;

    [Space(5)]

    [Header("Time Intervals")]
    public float startInterval = 1;
    public float betweenMapInterval = 1;
    public float endInterval = 1;

    [Space(5)]

    [Header("Beat Blocks")]
    public BeatBlock wBlock;
    public BeatBlock aBlock;
    public BeatBlock sBlock;
    public BeatBlock dBlock;

    [Space(5)]

    [Header("Beatmaps")]
    public List<Beatmap> beatsmaps = new List<Beatmap>();

    [Space(5)]

    [Header("Sound Effects")]
    public AudioClip collectMemoryClip;

    [Space(5)]

    [Header("End Events")]
    public UnityEvent winEvent;
    public UnityEvent failEvent;

    AudioSource audioSource;

    bool collected;

    void Start () {
        //Add all the hit blocks to a list for spawning
        blocks.Add(wBlock);
        blocks.Add(aBlock);
        blocks.Add(sBlock);
        blocks.Add(dBlock);
        
        memoryColour = Color.white;
        memoryColour.a = 0;

        memorySprite.color = memoryColour;

        scoreProgressBar.maxValue = scoreToReach;

        audioSource = GetComponent<AudioSource>();

        //Begin the minigame and focuses camera on minigame
        LevelManager._instance.ChangeState("Minigame");
        LevelManager._instance.SetCameraFocus(gameObject);
        StartCoroutine("BeatmapsPlayer");
    }

    void Update() {
        minigameScoreText.text = "Score: " + minigameScore;

        float alphaValue = Mathf.Lerp(memoryColour.a, minigameScore / scoreToReach, Time.deltaTime);

        memoryColour.a = Mathf.Clamp01(alphaValue);
        memorySprite.color = memoryColour;

        scoreProgressBar.value = Mathf.Clamp(minigameScore, 0, scoreToReach);

        if (!collected && minigameScore >= scoreToReach) {
            collected = true;
            blockingCloud.SetActive(false);
            audioSource.PlayOneShot(collectMemoryClip);
        }
    }

    void beatSpawner(int index) {
        //Spawn a beat at the block referenced with the current maps beat speed
        blocks[index].SpawnBlock(beatsmaps[currentBeatmap].beatSpeed);
    }

    IEnumerator TextureLineReader() {
        //Set this texture to the beatmaps texture
        Texture2D beatMapTexture = beatsmaps[currentBeatmap].beatMapTexture;

        //Go through each line, with intervals inbetween, and read each pixel on the line. If the pixel is red, spawn a beat on the block that corrisponds to that pixel
        for (int i = beatMapTexture.height - 1; i >= 0; i--) {
            for (int u = 0; u < beatMapTexture.width; u++) {
                //Read colour of the pixel
                Color pixel = beatMapTexture.GetPixel(u, i);

                //If red, spawn beat on block that corrisponds to the pixel
                if (pixel == Color.red) {
                    beatSpawner(u);
                }
            }

            //Wait a few seconds once a line has been read before moving onto the next
            yield return new WaitForSeconds(beatsmaps[currentBeatmap].lineInterval);
        }

        yield return new WaitForSeconds(betweenMapInterval);
        currentBeatmap++;
        StartCoroutine("BeatmapsPlayer");
    }

    IEnumerator BeatmapsPlayer() {
        if (currentBeatmap == 0) {
            //If its the first map, wait a few seconds for animations and failsafe measures
            yield return new WaitForSeconds(startInterval);
        }

        if (currentBeatmap < beatsmaps.Count) {
            StartCoroutine("TextureLineReader");
        } else {
            //Wait a few seconds before ending the minigame, to give the beats time to all be hit or missed
            yield return new WaitForSeconds(endInterval);

            //Invoke the win or loss event, depending on final score
            LevelManager._instance.ChangeState("Platformer");
            LevelManager._instance.CameraFocusPlayer();

            if (minigameScore >= scoreToReach) {
                winEvent.Invoke();
            } else {
                blockingCloud.SetActive(true);

                failEvent.Invoke();
            }
        }
    }

}
