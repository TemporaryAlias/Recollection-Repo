using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceGoop : MonoBehaviour {

    public float sideMoveSpeed;
    public float bounceForce;

    bool playerIgnore = false;

    Animator anim;

    Rigidbody2D rb;

	void Start () {
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        Physics2D.IgnoreLayerCollision(8, 8);
    }
	
	void Update () {
        if (!playerIgnore) {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), LevelManager._instance.player.GetComponent<Collider2D>());

            playerIgnore = true;
        }

        rb.velocity = new Vector2(sideMoveSpeed, rb.velocity.y);

        anim.SetFloat("Y Velocity", rb.velocity.y);
	}

    public void FlipBounce() {
        sideMoveSpeed = -sideMoveSpeed;
    }

    public void BounceTrigger() {
        rb.velocity = rb.velocity = new Vector2(rb.velocity.x, 0);

        anim.SetTrigger("Land");
        rb.velocity = rb.velocity = new Vector2(rb.velocity.x, bounceForce);
    }

}
