using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    private Animator doorAnimator;
    private bool isPlayerNear = false;
    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra khi đối tượng va chạm vào cửa
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetBool("isOpen", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Kiểm tra khi đối tượng rời khỏi cửa
        if (other.CompareTag("Player"))
        {
            // Kích hoạt animation "OpenCloseDoor" ngược lại
            doorAnimator.SetBool("isOpen", false);
        }
    }

}
