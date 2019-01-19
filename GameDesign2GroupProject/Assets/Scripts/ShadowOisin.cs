using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowOisin : MonoBehaviour {

    public int damageDealt;

    public float moveSpeed;

    public Transform startPoint, endPoint;

    Transform nextPoint;

	void Start () {
        transform.position = startPoint.position;

        nextPoint = endPoint;
	}
	
	void Update () {
        float dist = Vector2.Distance(transform.position, nextPoint.position);

        if (dist < 0.5) {
            if (nextPoint == endPoint) {
                nextPoint = startPoint;
            } else if (nextPoint == startPoint) {
                nextPoint = endPoint;
            }

            Flip();
        }

        transform.position = Vector2.MoveTowards(transform.position, nextPoint.position, moveSpeed);
	}

    void Flip() {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Player player = collision.GetComponent<Player>();

            player.TakeDamage(damageDealt);
        }
    }

}
