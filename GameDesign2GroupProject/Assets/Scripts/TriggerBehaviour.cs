using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBehaviour : MonoBehaviour {
    
    [Header("Trigger Settings")]
    public bool isOnceOff;
    public bool playerOnly;

    [Space(5)]

    [Header("Trigger Events")]
    public UnityEvent enterEvent;
    public UnityEvent stayEvent;
    public UnityEvent exitEvent;

    bool used = false;

    void OnTriggerEnter2D(Collider2D collision) {
        if (used) {
            return;
        }

        if (playerOnly && !collision.gameObject.CompareTag("Player")) {
            return;
        }

        enterEvent.Invoke();
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (used) {
            return;
        }

        if (playerOnly && !collision.gameObject.CompareTag("Player")) {
            return;
        }

        stayEvent.Invoke();
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (used) {
            return;
        }

        if (playerOnly && !collision.gameObject.CompareTag("Player")) {
            return;
        }

        exitEvent.Invoke();

        if (isOnceOff) {
            used = true;
        }
    }

}
