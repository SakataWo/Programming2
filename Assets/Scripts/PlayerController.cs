using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Transform trans;
    Rigidbody2D body;

    [SerializeField] float speed; 
    [SerializeField] float jumpForce;

    bool jumpInput;
    bool isGrounded;
    bool isWalking;

    void Start()
    {
        trans = GetComponent<Transform>(); 
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        walk();
        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpInput = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            jumpInput = false;
        }
    }
    void FixedUpdate() 
    {
        if (jumpInput && isGrounded)
        {
            jump();
        }
    }
    void walk()
    {
        if (Input.GetKey(KeyCode.D)) 
        {
            trans.position += transform.right * Time.deltaTime * speed; 
            trans.rotation = Quaternion.Euler(0, 0, 0); 
            isWalking = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            trans.position += transform.right * Time.deltaTime * speed;
            trans.rotation = Quaternion.Euler(0, 180, 0); 
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
    void jump()
    {
        body.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.collider.tag == "Ground") 
        {
            for (int i = 0; i < collision.contacts.Length; i++) 
            {
                if (collision.contacts[i].normal.y > 0.5)
                {
                    isGrounded = true;
                }
            }
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public bool GetIsWalking()
    {
        return isWalking;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }
}