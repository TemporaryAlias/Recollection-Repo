using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepressionGoop : MonoBehaviour {

    [Header("Settings")]
    public int damageDealt;

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Player player = collision.GetComponent<Player>();

            player.TakeDamage(damageDealt);
        }
    }

}
