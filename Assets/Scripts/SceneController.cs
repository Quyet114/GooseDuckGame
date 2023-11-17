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
        if(instance == null)
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
        if(numKey>0)
        {
            StartCoroutine(LoadLever());
        }

    }

    IEnumerator LoadLever()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        animator.SetTrigger("Start");
    }
}
