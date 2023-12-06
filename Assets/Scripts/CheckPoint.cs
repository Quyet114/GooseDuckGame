using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CheckPoint : MonoBehaviour

{
    public GameObject canvasCheckpoint;
    public bool notify;
    public TextMeshProUGUI NotifyText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        notify = PlayerScript.Noti;
        if (notify)
        {
            NotifyText.SetText("Successfuly!");
        }
        else
        {
            NotifyText.SetText("Save your position!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            canvasCheckpoint.SetActive(true);
        }
    }
}
