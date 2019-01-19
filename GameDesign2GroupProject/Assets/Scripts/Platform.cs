using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform : MonoBehaviour {

    Collider2D Collider;
    Animator p_animator;
    bool activate;
    

    void Start ()
    {

        //gameObject.GetComponent<Renderer>().enabled = false;
        Collider = GetComponent<Collider2D>();
        activate = true;
        p_animator = GetComponent<Animator>();
    }
	
	void Update ()  
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            if (activate) {
                //plat.gameObject.SetActive(false);
                gameObject.GetComponent<Renderer>().enabled = false;
                Collider.enabled = false;
                activate = false;
            } else if (!activate) {
                //plat.gameObject.SetActive(true);
                gameObject.GetComponent<Renderer>().enabled = true;
                Collider.enabled = true;
                activate = true;
            }
        }
    }

    

  



    /* private void OnCollisionEnter(Collision other)
     {
         if (other.tag == "Player")
         {
             Debug.Log("Impact");
             p_animator.SetBool("Impact", true);
         }
     }*/

}
