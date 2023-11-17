using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointEnd : MonoBehaviour
{   
    Animator animator;
    private int numKey;
    // Start is called before the first frame update
    void Start()
    {
        numKey = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        numKey = PlayerScript.numberOfKey;
        Debug.Log(numKey);
        if (numKey > 0)
        {
            animator.SetBool("gotKey", true);
        }
    }

}
