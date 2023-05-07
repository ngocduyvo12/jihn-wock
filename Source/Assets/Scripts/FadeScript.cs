using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public float fadeSpeed = 1f;
    Image image;
    bool fade = false;
    public bool fadeIn = false;
    public bool fadeOut = false;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    private void OnEnable()
    {
        fade = true;
    }
    private void Update()
    {
        if (fade)
        {
            if (fadeIn)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + fadeSpeed * Time.deltaTime);
                if (image.color.a >= 1f)
                {
                    if (GameManager.LOST)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    else if(GameManager.WON)
                    {
                        if (SceneManager.GetActiveScene().buildIndex + 1 < 3)
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                        }
                        else
                        {
                            PlayerPrefs.DeleteKey("SceneIndex");
                            SceneManager.LoadScene(0);
                        }
                    }
                }
            }
            if (fadeOut)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - fadeSpeed * Time.deltaTime);
                if(image.color.a <= 0f)
                {
                    image.gameObject.SetActive(false);
                }
            }
        }
    }


}