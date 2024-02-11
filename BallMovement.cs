using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 10;
    [SerializeField] private float speedIncr = 0.2f;
    [SerializeField] private Text PSCORE;
    [SerializeField] private Text AISCORE;

    private int hitcounter;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("Startball", 2f);

    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncr * hitcounter));
    }

    private void Startball()
    {
        rb.velocity = new Vector2(-1, 0) * (initialSpeed + speedIncr * hitcounter);


    }

    private void Resetball()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        hitcounter = 0;
        Invoke("Startball", 2f);
    }

    private void PlayerBounce(Transform myObject)
    {
        hitcounter++;

        Vector2 balpos = transform.position;
        Vector2 playerpos = myObject.position;

        float xDirection, yDirection;
        if( transform.position.x > 0)
        {
            xDirection = -1;
        }

        else
        {
            xDirection = 1;
        }

        yDirection = (balpos.y - playerpos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        if (yDirection == 0)
        {
            yDirection = 0.25f;
        }
        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed * hitcounter);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "AI")
        {
            PlayerBounce(collision.transform);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "RightWall")

        {
            
            PSCORE.text = (int.Parse(PSCORE.text) + 1).ToString();
            Resetball();
        }
        else if (collision.gameObject.name == "LeftWall")
        {
            
            AISCORE.text = (int.Parse(AISCORE.text) + 1).ToString();
            Resetball();
        }
    }
}
