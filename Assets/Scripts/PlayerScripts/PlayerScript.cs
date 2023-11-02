using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public Vector3 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position += moveInput * moveSpeed * Time.deltaTime;
        if(moveInput.x != 0 || moveInput.y !=0)

        {
            animator.SetBool("isRunning", true);
            if (moveInput.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 0);

            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 0);
            }

        }
        else
        {
            animator.SetBool("isRunning", false);
        }

    }
}
