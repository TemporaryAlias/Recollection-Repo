using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{

    public GameObject platform;

    public float moveSpeed;

    public Transform currentpoint;

    public Transform[] points;

    public int pointSelection;

    // Use this for initialization
    void Start()
    {
        currentpoint = points[pointSelection];
    }

    // Update is called once per frame
    void Update()
    {

        platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentpoint.position, Time.deltaTime * moveSpeed);

        if (platform.transform.position == currentpoint.position)
        {
            pointSelection++;

            if (pointSelection == points.Length)
            {
                pointSelection = 0;
            }
            currentpoint = points[pointSelection];

        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.transform.SetParent(null);
        }
    }

}
