using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject[] hardHazards;

    public Vector3 spawnValues;

    public int hazardCount;
    public int hardCount;

    public float spawnWait;
    public float spawnWaitHard;
    public float startWait;
    public float waveWait;
    public float ScrollBackgroundTime;

    
    public int score;

    public Text ScoreText;

    public Text TitleText;
    public Text HardText;
    public Text PlayText;

    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private bool gameStart;

    private GameObject background;
    private GameObject blaster;

    public AudioSource musicSource;
    public AudioClip menuMusic;
    public AudioClip fightMusic;
    public AudioClip hardMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;



    void Start ()
    {
        musicSource.clip = menuMusic;

        musicSource.Play();

        gameStart = true;
        gameOver = false;
        restart = false;

        TitleText.text = "THE Space Shooter";
        PlayText.text = "[X] Play STANDARD MODE";
        HardText.text = "[Z] Play HARD MODE";
        restartText.text = "";
        gameOverText.text = "";
        ScoreText.text = "";

        score = 0;
        background = GameObject.Find("Background");
        blaster = GameObject.Find("Player");
        //UpdateScore();
        //StartCoroutine (SpawnWaves());
    }

    void Update ()
    {
        

        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.F))
            {
                SceneManager.LoadScene("TheFinalProject");
            }
        }
    }


    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);

                if (score >= 200)
                {
                    StartCoroutine(ScrollSpeedup());
                }
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'F' to Return to Main Menu";
                restart = true;
                break;
            }
        }
    }

    IEnumerator HardWaves()
    {
       
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hardCount; i++)
            {
                GameObject hazardHard = hardHazards[Random.Range(0, hardHazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazardHard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWaitHard);

                if (score >= 200)
                {
                    StartCoroutine(ScrollSpeedup());
                }
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'F' to Return to Main Menu";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 200)
        {
            musicSource.clip = winMusic;

            musicSource.Play();

            Peace();
            gameOverText.text = "You win! Game Created by Charles Price";
            gameOver = true;
            restart = true;
        }
    }

    IEnumerator ScrollSpeedup ()
    {
            while (background.GetComponent<BGScroller>().scrollSpeed > -60.0f)
        {
                yield return new WaitForSeconds(ScrollBackgroundTime);
                background.GetComponent<BGScroller>().scrollSpeed *= 1.01f;
       }


    }

    void Peace ()
    {
        blaster.GetComponent<MeshCollider>().enabled = false;
    }

    public void GameOver ()
    {
        musicSource.clip = loseMusic;

        musicSource.Play();
        gameOverText.text = "Game Over! Game Created by Charles Price";
        gameOver = true;
    }

    void FixedUpdate()
    {
        if (gameStart == true && Input.GetKeyDown(KeyCode.X))
        {

            musicSource.clip = fightMusic;

            musicSource.Play();

            gameStart = false;
            TitleText.text = "";
            PlayText.text = "";
            HardText.text = "";
            UpdateScore();
            StartCoroutine(SpawnWaves());

        }

        if (gameStart == true && Input.GetKeyDown(KeyCode.Z))
        {

            musicSource.clip = hardMusic;

            musicSource.Play();

            gameStart = false;
            TitleText.text = "";
            PlayText.text = "";
            HardText.text = "";
            UpdateScore();
            StartCoroutine(HardWaves());
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
