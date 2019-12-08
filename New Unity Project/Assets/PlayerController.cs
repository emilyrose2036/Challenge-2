using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text countText;
    public Text winText;
    public Text livesText;
    public Text timeText;
    private int count;
    private int lives;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    private float timeLeft = 75.0f;
    private bool facingRight = true;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        count = 0;
        lives = 3;
        winText.text = "";
        SetCountText();
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeText.text = "Time:" + (timeLeft).ToString("0");
        if (timeLeft < 0)
            GameOver();
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 1);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }


        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Powerup"))
        {
            other.gameObject.SetActive(false);
            lives = lives + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            winText.text = "You win! Game created by Emily Rose!";
        }
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            winText.text = "You lose!";
            Destroy(this);
        }
        if (count == 4)
        {
            transform.position = new Vector2(56.61f, 3.78f);
            lives = 3;
            livesText.text = "Lives: " + lives.ToString();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            anim.SetInteger("State", 0);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            anim.SetInteger("State", 1);
        }
    }
    public void GameOver()
    {
        winText.text = "You lose!";
        Destroy(this);
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}