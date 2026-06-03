using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float speed;
    public float jf;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horiz * speed, rb.linearVelocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0, jf));
        }
    }

}
