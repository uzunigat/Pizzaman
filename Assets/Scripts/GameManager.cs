using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager sharedInstance;
    public bool gameStarted = false;
    public bool gamePaused = false;

    public AudioClip pauseAudio;
    public float invensibleTime = 0.0f;

    private void Awake()
    {

        if (sharedInstance == null) sharedInstance = this;
        StartCoroutine(StartGame());

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P) && gameStarted)
        {

            this.gamePaused = !this.gamePaused;
            if (gamePaused) PlayPauseMusic();
            else StopPauseMusic();

        }

        if(invensibleTime > 0)
        {

            invensibleTime -= Time.deltaTime;

        }
        
    }

    void PlayPauseMusic() {

        AudioSource source = GetComponent<AudioSource>();
        source.clip = pauseAudio;
        source.loop = true;

        source.Play();

    }

    void StopPauseMusic()
    {

        GetComponent<AudioSource>().Stop();

    }

    IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(4.0f);
        this.gameStarted = true;


    }

    public void MakeInvensible(float numberOfSeconds)
    {

        this.invensibleTime = numberOfSeconds;

    }

    public void RestartGame()
    {

        SceneManager.LoadScene("MainMapScene");

    }

}
