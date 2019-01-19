using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path2 : MonoBehaviour {

    Collider2D Collider, Collider1;

    void Start () {
        gameObject.GetComponent<Renderer>().enabled = false;
        Collider = GetComponent<BoxCollider2D>();
        Collider1 = GetComponent<PolygonCollider2D>();
    }
	
	
	void Update () {
		
	}
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameObject.GetComponent<Renderer>().enabled = true;

            
            Collider1.enabled = true;
            if (Collider.isTrigger)
            {
                gameObject.GetComponent<Renderer>().enabled = true;
                Collider.enabled = true;
            }

         

        }
    }
}
