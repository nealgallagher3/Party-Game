using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private int count;
    public int time;
    private Rigidbody2D rb2d;
    public ParticleSystem smoke1;
    public ParticleSystem smoke2;
    public ParticleSystem smoke3;

    public Text openText;
    public Text objText;
    public Text winText;
    public Text timerText;

    public AudioSource soundSource;
    public AudioClip soundClipOne;
    public AudioClip soundClipTwo;
    public AudioClip soundClipThree;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 1;
        openText.text = "Stop the Fires WASD to move";
        objText.text = "The Beds on Fire!!!";
        winText.text = "";
        time = 10;
        StartCoroutine("Timer");
        /*smoke1 = GetComponent<ParticleSystem>();
        smoke2 = GetComponent<ParticleSystem>();
        smoke3 = GetComponent<ParticleSystem>();*/
        //smoke1.Play();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
        if (Input.GetKey("escape"))
            Application.Quit();
        timerText.text = ("" + time);
        EndMusic();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obj"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetObj();
        }
    }
    void SetObj()
    {
        if (count == 2)
        {
            objText.text = "Now the Hall!!";
            soundSource.clip = soundClipThree;
            soundSource.Play();
            if (smoke1.isPlaying)
            {
                smoke1.Stop();
            }
            if (!smoke2.isPlaying)
            {
                smoke2.Play();
            }
        }
        if (count == 3)
        {
            objText.text = "Don't Forget the Stove";
            soundSource.clip = soundClipThree;
            soundSource.Play();
            if (smoke2.isPlaying)
            {
                smoke2.Stop();
            }
            if (!smoke3.isPlaying)
            {
                smoke3.Play();
            }
        }
        if (count == 4)
        {
            winText.text = "You Win, now go back to sleep";
            //soundSource.clip = soundClipOne;
            //soundSource.Play();
            if (smoke3.isPlaying)
            {
                smoke3.Stop();
            }
        }
    }
    void EndMusic()
    {
        if (count == 4)
        {
            soundSource.clip = soundClipOne;
            soundSource.Play();
            Destroy(this);
        }
        if (time <= 0)
        {
            timerText.text = "0";
            winText.text = "You Lose";
            soundSource.clip = soundClipTwo;
            soundSource.Play();
            Destroy(this);
        }
    }
    IEnumerator Timer()
    {
        while (true)
        {
            if (time == 10)
            {
                yield return new WaitForSeconds(2);
            }
            yield return new WaitForSeconds(1);
            time--;
        }
    }
}
