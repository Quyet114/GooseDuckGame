using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int numKey;
    public static SceneController instance;
    [SerializeField] Animator animator;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
         numKey = PlayerScript.numberOfKey;

    }
    public void NextLever()
    {
        if(numKey==1)
        {
            StartCoroutine(LoadLever());
        }

    }
    public void StartGame()
    {
            StartCoroutine(StartG());
    }
    public void BackMenu()
    {
        StartCoroutine(Menu());
    }
    public void ReloadGame()
    {
        StartCoroutine(ReloadLever());
    }
    IEnumerator Menu()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
        animator.SetTrigger("Start");
    }
    IEnumerator StartG()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        animator.SetTrigger("Start");
    }

    IEnumerator LoadLever()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        animator.SetTrigger("Start");
    }
    IEnumerator ReloadLever()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        animator.SetTrigger("Start");
    }
}
