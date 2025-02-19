using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;

    Rigidbody2D body;
    bool canJump;
    bool canDoubleJump;
    bool isJumping;
    bool isCrawling;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        isCrawling = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollision Enter " + collision.transform.tag);
        if (collision.transform.CompareTag("Ground"))
        {
            canJump = true;
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("OnCollision Exit " + collision.transform.tag);
        canJump = false;
    }

    private void Update()
    {
        // Left and Right movement
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, body.linearVelocity.y);

        // Jump and Double Jump
        if (!isCrawling && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (canJump)
            {
                isJumping = true;
                canJump = false;
                canDoubleJump = true;
                OnJump();
            }
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                OnJump();
            }
        }

        // Crawl
    }

    void OnJump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpHeight);
    }
}
