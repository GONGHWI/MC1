using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{

    
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    public int jumpCount = 2;
    CapsuleCollider2D capsulecollider;
    SpriteRenderer spriteRenderer;

    public float point;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsulecollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {

            rigid.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
            jumpCount--;



        }

        if (Input.GetButtonUp("Horizontal"))
        {

            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        //방향전환


        //방향전환







    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h * 1, ForceMode2D.Impulse);


        if (rigid.velocity.x > maxSpeed) //Right Max
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

        else if (rigid.velocity.x < maxSpeed * (-1)) //Left Max
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);


        if (rigid.velocity.y < 0)
        {


            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {

                    jumpCount = 2;
                }

            }
        }
    }


    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gold")
        {
           
            collision.gameObject.SetActive(false);
            point++;

        }

        if (collision.gameObject.tag == "Finish" && point == 10)
        {
            SceneManager.LoadScene("Stage2");


            //Next Stage
        }


        if (collision.gameObject.tag == "Event")
        {
           
                Time.timeScale = 1;
                SceneManager.LoadScene("Stage1");
            }



        }


    }

    






