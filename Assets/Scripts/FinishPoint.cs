using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public GameObject canvasEndGame;
    public GameObject charater;
    private void Start()
    {
/*        charater.SetActive(true);*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
            canvasEndGame.SetActive(true);
/*            charater.SetActive(false);*/
        {
        }
    }

}
