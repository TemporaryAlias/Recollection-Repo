using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {

    public bool isGrounded = false;

    List<GameObject> grounds = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            grounds.Add(collision.gameObject);

            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("MovingPlatform")) {
            grounds.Add(collision.gameObject);

            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            grounds.Remove(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("MovingPlatform")) {
            grounds.Remove(collision.gameObject);
        }

        if (grounds.Count == 0) {
            isGrounded = false;
        }
    }

}
