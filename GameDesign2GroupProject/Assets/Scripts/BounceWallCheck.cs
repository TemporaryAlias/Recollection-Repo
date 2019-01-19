using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceWallCheck : MonoBehaviour {
    
    BounceGoop parent;

	void Start () {
        parent = GetComponentInParent<BounceGoop>();
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform")) {
            parent.FlipBounce();
        }
    }

}
