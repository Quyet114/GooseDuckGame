using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerScript : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public Vector3 moveInput;

    // heathBar
    [SerializeField] int maxHeath;
    int curentHeath;
    public HeathBar heathBar;
    public UnityEvent onDeath;


    public void OnEnable()
    {
        onDeath.AddListener(death);
    }
    public void OnDisable()
    {
        onDeath.RemoveListener(death);
    }

    // Start is called before the first frame update
    void Start()
    {
        curentHeath = maxHeath;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }


    // nhân vật bị tấn công
    public void takeDame(int Damage)
    {
        curentHeath -= Damage;
        if(curentHeath <= 0)
        {   
            onDeath.Invoke();
        }
    }
    // nhân vật chết
    public void death()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (heathBar != null)
        {
            heathBar.UpdateHealth(curentHeath, maxHeath);
        }
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            takeDame(2);
        }

    }



    public float pushForce = 0.1f; // Lực đẩy

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra xem Player có va chạm với OB không
        if (collision.gameObject.CompareTag("OB"))
        {
            // Lấy Rigidbody của OB
            Rigidbody2D obRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

        }
    }
}
