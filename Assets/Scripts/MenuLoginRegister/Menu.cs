using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button Play;
    public Button Opion;
    public Button Exit; 
    void Start()
    {
        Play.onClick.AddListener(MovetoGame);
        // Opion.onClick.AddListener(MovetoGame);
        // Exit.onClick.AddListener(MovetoGame);
    }
    void MovetoGame(){
       // SceneManager.LoadScene("Quyet");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    }

