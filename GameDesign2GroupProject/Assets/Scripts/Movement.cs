using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [Header("Settings")]
    public int moveSpeed;
    public int jumpForce;
    public KeyCode jumpKey;
    
    bool isFlipped = false;
    bool isFalling = false;
    
    public Vector3 RespawnPoint;

    [Space(5)]

    [Header("Sound Effects")]
    public AudioClip jumpClip;
    public AudioClip landClip;

    GroundChecker groundCheck;

    Animator anim;
    Rigidbody2D rb2d;
    AudioSource audioSource;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<GroundChecker>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        RespawnPoint = transform.position;
    }

    void FixedUpdate() {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        rb2d.velocity = new Vector2(moveX, rb2d.velocity.y);

        if (Input.GetAxis("Horizontal") < 0 && !isFlipped || Input.GetAxis("Horizontal") > 0 && isFlipped) {
            Flip();

            isFlipped = !isFlipped;
        }
    }

    void Update() {
        if (Input.GetKeyDown(jumpKey) && groundCheck.isGrounded) {
            rb2d.AddForce(new Vector2(0, jumpForce));
            audioSource.PlayOneShot(jumpClip);

            anim.SetTrigger("Jump");
            anim.SetBool("isWalking", false);
        }

        if (rb2d.velocity.y < 0) {
            anim.SetTrigger("Falling");
            isFalling = true;
        }

        if (isFalling && groundCheck.isGrounded)  {
            audioSource.PlayOneShot(landClip);

            anim.SetTrigger("Land");
            isFalling = false;
        }

        if (rb2d.velocity != Vector2.zero && !anim.GetBool("isWalking") && groundCheck.isGrounded && !isFalling) {
            anim.SetBool("isWalking", true);
        } else if (rb2d.velocity == Vector2.zero && anim.GetBool("isWalking") || isFalling) {
            anim.SetBool("isWalking", false);
        }
    }

    public void SetVelocity(Vector2 newVelocity) {
        rb2d.velocity = newVelocity;
    }

    void Flip() {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Checkpoint") {
            RespawnPoint = other.transform.position;
        }

        if (other.tag == "FallDetector") {
            transform.position = RespawnPoint;
        }
        

    }

    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

}
