using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] Animator animator;
    public TMP_Text txtUser, txtScore;
    void Start()
    {
        txtUser.text = LoginScript.loginResponse.username;
        txtScore.text = LoginScript.loginResponse.score.ToString();
    }
    public void MovetoGame(){
        StartCoroutine(LoadLever());
    }
    IEnumerator LoadLever()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        animator.SetTrigger("Start");
    }
}

