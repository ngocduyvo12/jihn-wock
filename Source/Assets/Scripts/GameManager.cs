using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool WON = false;
    public static bool LOST = false;
    public CanvasGroup tutorial;
    public GameObject gameOver;
    public GameObject gameWin;
    [SerializeField] float tutorialTimer = 15f;
    bool once = true;
    public AudioSource sfxSource;
    private void OnEnable()
    {
            if (PlayerPrefs.GetInt("SceneIndex") > SceneManager.GetActiveScene().buildIndex)
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("SceneIndex"));
            }
            else
            {
                PlayerPrefs.SetInt("SceneIndex", SceneManager.GetActiveScene().buildIndex);
            }
    }
    private void Start()
    {
        WON = false; 
        LOST = false;
        gameOver.SetActive(false);
        gameWin.SetActive(false);
    }
    private void Update()
    {
        if (tutorial != null && PlayerPrefs.GetInt("HasShownTutorial", 0) == 0)
        {
            tutorial.gameObject.SetActive(true);
            tutorialTimer -= Time.deltaTime;
            if (tutorialTimer <= 0f)
            {
                tutorialTimer = 0f;
                tutorial.alpha = tutorial.alpha - 1f * Time.deltaTime;
                if (tutorial.alpha <= 0f)
                {
                    PlayerPrefs.SetInt("HasShownTutorial", 1);
                    tutorial.gameObject.SetActive(false);
                }
            }
        }
        if (LOST)
        {
            if (once)
            {
                AudioManager.instance.PlayerDeath();
                gameOver.SetActive(true);
                once = false;
            }
        }
        if (WON)
        {
            if (once)
            {
                gameWin.SetActive(true);
                once = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}