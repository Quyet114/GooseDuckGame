using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool checkChest = true;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && checkChest)
        {
            animator.SetBool("Open", true);
            StartCoroutine(FirstFunction());
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("Open", false);
    }
    IEnumerator FirstFunction()
    {
        yield return new WaitForSeconds(1); // Chờ 2 giây
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        checkChest = false;
    }
}
