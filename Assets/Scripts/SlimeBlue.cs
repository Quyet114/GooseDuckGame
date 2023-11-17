using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBlue : MonoBehaviour
{
    public Animator animator;
    PlayerScript player;
    public int minDamage;
    public int maxDamage;
    SoundManager soundManager;
    //di chuyển cho slime ----------------------------------
    public float moveSpeed = 1.0f; // Tốc độ di chuyển
    public float moveRangeX = 1.0f; // Khoảng cách di chuyển theo trục X
    public float moveRangeY = 1.0f; // Khoảng cách di chuyển theo trục Y
    public float changeDirectionTime = 2.0f; // Thời gian giữa các hướng di chuyển
    private Vector2 moveDirection;
    private float changeDirectionTimer;
    //di chuyển cho slime +++++++++++++++++++++++++++++++++++

    // Start is called before the first frame update
    void Start()
    {
        // Khởi tạo hướng di chuyển ngẫu nhiên ban đầu
        RandomizeMoveDirection();

        //-----------
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        // Di chuyển theo hướng hiện tại
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Đếm thời gian và thay đổi hướng sau mỗi khoảng thời gian
        changeDirectionTimer += Time.deltaTime;
        if (changeDirectionTimer >= changeDirectionTime)
        {
            RandomizeMoveDirection();
            changeDirectionTimer = 0;
        }
    }

    private void RandomizeMoveDirection()
    {
        // Tạo hướng di chuyển ngẫu nhiên trong khoảng đã cho
        float randomX = Random.Range(-moveRangeX, moveRangeX);
        float randomY = Random.Range(-moveRangeY, moveRangeY);
        moveDirection = new Vector2(randomX, randomY).normalized;
    }
/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerScript>();
            InvokeRepeating("DamagePlayer", 0, 1f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
            InvokeRepeating("DamagePlayer", 0, 1f);
        }
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            player = collision.gameObject.GetComponentInParent<PlayerScript>();
            InvokeRepeating("DamagePlayer", 0, 1f);
            RandomizeMoveDirection();
            animator.SetBool("Attack", true);
            //GetComponent<LootBag>().InstantiateLoot(transform.position);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
        if (collision.gameObject.tag == "Wall")
        {
            RandomizeMoveDirection();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
            CancelInvoke("DamagePlayer");

        }
    }

    void DamagePlayer()
    {
        int damage = UnityEngine.Random.Range(minDamage, maxDamage);
        player.takeDame(damage);
    }

}

