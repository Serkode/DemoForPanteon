using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    CharecterMove Charecter;
    public GameObject startButton, startButton2;
    public bool gameStart = false, begin = false, reStart = false;
    float startTime = 4;
    public Text countdown;
    void Start()
    {
        Charecter = GameObject.FindGameObjectWithTag("Player").GetComponent<CharecterMove>();
        startButton.SetActive(true);
        startButton2.SetActive(false);
        countdown.enabled = true; ;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {
        if(begin)
        {
            startTime -= Time.deltaTime;
            countdown.text = (int)startTime + "";
            if (startTime <= 1)
            {
                countdown.enabled = false;
                startTime = 4;
                gameStart = true;
                begin = false;
            }
        }

        if(Charecter.endGame)
        {
            Charecter.GetComponent<CharecterMove>().enabled = false;
        }
    }

    public void GameStart()
    {
        begin = true;
        startButton.SetActive(false);
    }

    public void StartButton2()
    {
        reStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
