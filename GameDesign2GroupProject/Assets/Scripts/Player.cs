using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [Header("Player Settings")]
    public int maxHealthPoints;

    public float invulnTime;

    public GameObject deathEffect;

    [Space(5)]

    [Header("UI Settings")]
    public Text healthText;

    [Space(5)]

    [Header("Sound Effects")]
    public AudioClip damageClip;

    int healthPoints;

    bool invuln, dead;

    Animator anim;

    AudioSource audioSource;

    void Start () {
        dead = false;
        healthPoints = maxHealthPoints;

        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
	
	void Update () {
        healthText.text = "HP: " + healthPoints + "/" + maxHealthPoints;
	}

    IEnumerator Invulnerability(float invulnTimer) {
        audioSource.PlayOneShot(damageClip);

        invuln = true;
        anim.SetBool("Invulnerable", true);

        yield return new WaitForSeconds(invulnTimer);

        anim.SetBool("Invulnerable", false);
        invuln = false;
    }

    public void TakeDamage(int damageTaken) {
        if (!invuln && !dead) {
            healthPoints -= damageTaken;
            
            if (healthPoints <= 0) {
                dead = true;
                StartCoroutine("Death");
            } else {
                StartCoroutine("Invulnerability", invulnTime);
            }
        }
    }

    IEnumerator Death() {
        Movement mover = GetComponent<Movement>();
        mover.SetVelocity(Vector3.zero);
        mover.enabled = false;

        audioSource.PlayOneShot(damageClip);
        anim.SetTrigger("Dead");

        yield return new WaitForSeconds(1f);

        Instantiate(deathEffect, transform);

        yield return new WaitForSeconds(1.5f);

        LevelManager._instance.ResetScene();
    }

}
