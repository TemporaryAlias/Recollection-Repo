using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform2 : MonoBehaviour
{
    Collider2D Collider;
    public bool activate, toDeactivate;

    void Start()
    {
        //gameObject.GetComponent<Renderer>().enabled = false;
        Collider = GetComponent<Collider2D>();
        activate = false;

        gameObject.GetComponent<Renderer>().enabled = false;
        Collider.enabled = false;
        activate = false;
    }

    void Update()
    {
        if (!LevelManager._instance.peeking) {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                if (!activate) {
                    //plat.gameObject.SetActive(true);
                    gameObject.GetComponent<Renderer>().enabled = true;
                    Collider.enabled = true;
                    activate = true;
                } else if (activate) {
                    //plat.gameObject.SetActive(false);
                    gameObject.GetComponent<Renderer>().enabled = false;
                    Collider.enabled = false;
                    activate = false;
                }
            }
        }
    }
}
