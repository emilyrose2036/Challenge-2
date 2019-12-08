using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip coinpickup;
    public AudioClip enemyattack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            musicSource.PlayOneShot(coinpickup, 0.7F);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            musicSource.PlayOneShot(enemyattack, 0.7F);
        }
    }
}
