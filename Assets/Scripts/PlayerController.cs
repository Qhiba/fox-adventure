using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int jumpPower;

    [SerializeField] private Vector2 startingPosition;

    private bool onGround = true;

    private Rigidbody2D rb2d;
    private Animator animator;
    private GameObject audioManager;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector2(startingPosition.x, startingPosition.y);
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioManager = GameObject.Find("Audio Source");
        PlayerAnimation("idle");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = transform.position;

        if (!Data.isDead)
        {
            float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(move, 0, 0);

            if (onGround)
            {
                PlayerMovedDirection(currentPos);

                if (Input.GetButtonDown("Jump"))
                {
                    onGround = false;
                    PlayerAnimation("jump");
                    rb2d.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            onGround = true;
        }

        if (collision.gameObject.tag.Equals("Trap"))
        {
            Data.isDead = true;
            PlayerAnimation("death");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Gem"))
        {
            audioManager.GetComponent<AudioManager>().PlaySound("gem");
            Data.gem++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag.Equals("Goal"))
        {
            Data.isWon = true;
        }
    }

    private void PlayerMovedDirection(Vector2 playerPos)
    {
        if(transform.position.x == playerPos.x)
        {
            PlayerAnimation("idle");
        }
        else if(transform.position.x > playerPos.x)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
            PlayerAnimation("run");
        }
        else
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            PlayerAnimation("run");
        }
    }

    private void PlayerAnimation(string animate)
    {
        

        switch (animate)
        {
            case "idle":
                animator.SetBool("run", false);
                animator.SetBool("jump", false);
                animator.SetBool("death", false);
                animator.Play("PlayerIdle");
                break;
            case "run":
                animator.SetBool("run", true);
                animator.SetBool("jump", false);
                animator.SetBool("death", false);
                break;
            case "jump":
                animator.SetBool("run", false);
                animator.SetBool("jump", true);
                animator.SetBool("death", false);
                break;
            case "death":
                animator.SetBool("run", false);
                animator.SetBool("jump", false);
                animator.SetBool("death", true);
                break;
            default:
                break;
        }
    }
}
