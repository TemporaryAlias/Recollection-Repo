using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour {

    [Header("Settings")]
    public float speed;

    public BeatBlock parentBlock;

    [Space(5)]

    [Header("Sound Effects")]
    public AudioClip missClip;

    [Space(5)]

    [Header("Particle Effects")]
    public GameObject missEffect;

    void Update () {
        //Move the beat each frame by the specified speed
        float step = speed * Time.deltaTime;

        //Beat moves from its spawn point to its specified end point. Block will be somewhere along that path
        transform.position = Vector2.MoveTowards(transform.position, parentBlock.beatEndPoint.transform.position, step);

        //If the beat reaches the end of its path, remove it and consider it a miss
        float beatDist = Vector2.Distance(transform.position, parentBlock.beatEndPoint.transform.position);

        if (beatDist < 0.1) {
            Debug.Log("MISSED!");

            Instantiate(missEffect, transform.position, transform.rotation);

            parentBlock.audioSource.PlayOneShot(missClip);
            parentBlock.RemoveBeat(0);

            Destroy(gameObject);
        }
    }

}
