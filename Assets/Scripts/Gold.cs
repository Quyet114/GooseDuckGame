using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{   
    SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            PlayerScript.numberOfCoins++;
            PlayerPrefs.SetInt("NumberOfCoins", PlayerScript.numberOfCoins);
            Destroy(gameObject, 1f);
        }
    }
}
