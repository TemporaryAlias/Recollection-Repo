using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBlock : MonoBehaviour {

    [Header("Hit Key")]
    public KeyCode blockHitKey;

    [Space(5)]

    [Header("Beat Variables")]
    public GameObject beatPrefab;

    public Sprite beatSprite;

    [Space(5)]

    public BeatSpawnPoint beatSpawnPoint;
    public BeatEndPoint beatEndPoint;

    [Space(5)]

    [Header("Distance Thresholds")]
    public float missDist;
    public float hitDist;
    public float goodDist;

    [Space(5)]

    [Header("Sound Effects")]
    public AudioClip missClip;
    public AudioClip hitClip;

    [Space(5)]

    [Header("Particle Effects")]
    public GameObject hitEffect;
    public GameObject goodHitEffect;
    public GameObject greatHitEffect;
    public GameObject missEffect;

    List<GameObject> beats = new List<GameObject>();

    Animator anim;

    MiniGameBehaviour minigame;

    public AudioSource audioSource;

	void Start () {
        //Set the spawn and end points for associated beats
        beatSpawnPoint = gameObject.GetComponentInChildren<BeatSpawnPoint>();
        beatEndPoint = gameObject.GetComponentInChildren<BeatEndPoint>();
        anim = gameObject.GetComponentInChildren<Animator>();
        minigame = GetComponentInParent<MiniGameBehaviour>();
        audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
        //If the hit key is pressed, check to see if any beats are associated with this block. If they are, calculate the distance from closest beat to the block and affect score accordingly
        if (Input.GetKeyDown(blockHitKey)) {
            anim.SetTrigger("Interact");

            if (beats.Count != 0) {

                float beatDist = Vector2.Distance(transform.position, beats[0].transform.position);

                if (beatDist <= missDist && beatDist >= hitDist) {
                    Debug.Log("HIT!");

                    Instantiate(hitEffect, beats[0].transform.position, beats[0].transform.rotation);
                    audioSource.PlayOneShot(hitClip);

                    anim.SetTrigger("Hit");
                    minigame.minigameScore += 10;
                } else if (beatDist < hitDist && beatDist >= goodDist) {
                    Debug.Log("GREAT!");

                    Instantiate(goodHitEffect, beats[0].transform.position, beats[0].transform.rotation);
                    audioSource.PlayOneShot(hitClip);

                    anim.SetTrigger("Hit");
                    minigame.minigameScore += 20;
                } else if (beatDist < goodDist) {
                    Debug.Log("AWESOME!");

                    Instantiate(greatHitEffect, beats[0].transform.position, beats[0].transform.rotation);
                    audioSource.PlayOneShot(hitClip);

                    anim.SetTrigger("Hit");
                    minigame.minigameScore += 40;
                } else {
                    audioSource.PlayOneShot(missClip);

                    Instantiate(missEffect, beats[0].transform.position, beats[0].transform.rotation);
                    anim.SetTrigger("Miss");
                    Debug.Log("MISSED!");
                }

                //Destroy the beat after the calculations are done and remove it from the list to make room for the next one

                Destroy(beats[0]);
                beats.Remove(beats[0]);
            }
        }
	}

    public void SpawnBlock(float newSpeed) {
        //Spawn beat at the beat spawn point
        GameObject newBeat = Instantiate(beatPrefab, beatSpawnPoint.transform.position, beatSpawnPoint.transform.rotation, gameObject.transform.parent);
        Beat beat = newBeat.GetComponent<Beat>();

        //Set the beats parent block to this block and add it to the beat list
        beat.parentBlock = this;
        beat.speed = newSpeed;

        SpriteRenderer beatSpriteRend = beat.GetComponent<SpriteRenderer>();
        beatSpriteRend.sprite = beatSprite;

        beats.Add(newBeat);
    }

    //Remove a beat from the beat list
    public void RemoveBeat(int index) {
        beats.Remove(beats[index]);
    }

}