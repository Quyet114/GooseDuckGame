using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public GameObject canvasEndGame;
    public GameObject charater;
    public int numKey;
    private void Start()
    {
/*        charater.SetActive(true);*/

    }
    void Update()
    {
        numKey = PlayerScript.numberOfKey;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player")&& numKey>=3)
            canvasEndGame.SetActive(true);
/*            charater.SetActive(false);*/
        {
        }
    }

}
