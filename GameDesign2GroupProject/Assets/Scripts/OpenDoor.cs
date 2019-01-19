using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

    Collider2D Collider;
    

    void Start ()
    {
        Collider = GetComponent<Collider2D>();
        
    }
	
	
	void Update ()
    {
		
	}

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            Destroy(gameObject);

        }
    }


    
}
