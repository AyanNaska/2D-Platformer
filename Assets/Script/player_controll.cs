using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
public class player_controll : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float doubleJumpForce = 7f;
    private bool isGrounded;
    private bool canDoubleJump;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public GameObject prefab;
    private ButtonControll bvalue;
    public bool jbutton;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bvalue = GameObject.FindObjectOfType<ButtonControll>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = bvalue.horizontalInput;
        Vector2 movement = new Vector2(moveHorizontal, 0f);
        if (moveHorizontal != 0)
        {
            rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (rb.velocity.x > 0f)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (rb.velocity.x < 0f)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    void Update()
    {
        if (TCKInput.GetAction("jumpBtn", EActionEvent.Down))
        {
            if (isGrounded)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                canDoubleJump = true;
                isGrounded = false;
            }
            else if (canDoubleJump)
            {
                rb.AddForce(new Vector2(0f, doubleJumpForce), ForceMode2D.Impulse);
                canDoubleJump = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

