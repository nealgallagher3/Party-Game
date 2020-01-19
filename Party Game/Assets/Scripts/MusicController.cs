using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    private int time;
    // Start is called before the first frame update
    void Start()
    {
        time = 10;
        musicSource.clip = musicClipOne;
        musicSource.Play();
        StartCoroutine("Timer");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }
    IEnumerator Timer()
    {
        while (true)
        {
            if (time == 10)
            {
                yield return new WaitForSeconds(2);
                musicSource.clip = musicClipTwo;
                musicSource.Play();
            }
            yield return new WaitForSeconds(1);
            time--;
            if (time < 1)
            {
                musicSource.Stop();
            }
        }
    }
}
