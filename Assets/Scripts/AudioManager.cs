using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioSource;

    public AudioClip enemyShoot;
    public AudioClip hurt;
    public AudioClip jump;
    public AudioClip medkit;
    public AudioClip playerDeath;
    public AudioClip playerShoot;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        

    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void EnemyShoot()
    {
        audioSource.PlayOneShot(enemyShoot);
    }
    public void Hurt()
    {
        audioSource.PlayOneShot(hurt);
    }
    public void Jump()
    {
        audioSource.PlayOneShot(jump);
    }
    public void MedKit()
    {
        audioSource.PlayOneShot(medkit);
    }
    public void PlayerDeath()
    {
        audioSource.PlayOneShot(playerDeath);
    }
    public void PlayerShoot()
    {
        audioSource.PlayOneShot(playerShoot);
    }

}