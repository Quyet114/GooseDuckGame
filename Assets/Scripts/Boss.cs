using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    private Animator animator;
    PlayerScript player;
    public int minDamage;
    public int maxDamage;
    //di chuyển cho slime ----------------------------------
    public float moveSpeed = 1.0f; // Tốc độ di chuyển
    public float moveRangeX = 1.0f; // Khoảng cách di chuyển theo trục X
    public float moveRangeY = 1.0f; // Khoảng cách di chuyển theo trục Y
    public float changeDirectionTime = 2.0f; // Thời gian giữa các hướng di chuyển
    private Vector2 moveDirection;
    private float changeDirectionTimer;
    //di chuyển cho slime +++++++++++++++++++++++++++++++++++a
    // Start is called before the first frame update
    int maxHealth = 5;
    private bool canRun = true;

    //-----
    int curentHeath;
    public HeatlhBarBoss heathBar;
    public UnityEvent onDeath;
    void Start()
    {
        curentHeath = maxHealth;
        heathBar.UpdateBar(curentHeath, maxHealth);
        if (canRun )
        {
            MoveDirection();
        }

        //-----------
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(canRun)
        {
            // Di chuyển theo hướng hiện tại
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            // Đếm thời gian và thay đổi hướng sau mỗi khoảng thời gian
            changeDirectionTimer += Time.deltaTime;
            if (changeDirectionTimer >= changeDirectionTime)
            {
                MoveDirection();
                changeDirectionTimer = 0;
            }
        }
        if(curentHeath == 0)
        {
            StartCoroutine(Death());
        }
        if (heathBar != null)
        {
            heathBar.UpdateHealth(curentHeath, maxHealth);
        }
    }
    IEnumerator Death()
    {
        animator.SetBool("die", true);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }
    private void MoveDirection()
    {
        // Tạo hướng di chuyển ngẫu nhiên trong khoảng đã cho
        float randomX = Random.Range(-moveRangeX, moveRangeX);
        float randomY = Random.Range(-moveRangeY, moveRangeY);
        moveDirection = new Vector2(randomX, randomY).normalized;
        if (randomX > 0)
        {
            transform.localScale = new Vector3(1, 1, 0);

        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //GetComponent<LootBag>().InstantiateLoot(transform.position);
            player = collision.gameObject.GetComponentInParent<PlayerScript>();
            InvokeRepeating("DamagePlayer", 0, 1f);
            MoveDirection();
            animator.SetBool("hit", true);
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
            CancelInvoke("DamagePlayer");
            animator.SetBool("hit", false);
        }
    }

    void DamagePlayer()
    {
        int damage = UnityEngine.Random.Range(minDamage, maxDamage);
        player.takeDame(damage);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
/*        if (other.CompareTag("Shield")) // Kiểm tra va chạm với đối tượng có tag "DamageObject"
        {
            StartCoroutine(StandStill());
        }
        if (other.CompareTag("Knite")) // Kiểm tra va chạm với đối tượng có tag "DamageObject"
        {
            StartCoroutine(StandStill());
            currentHealth -= 2;
            }*/
        if (other.gameObject.CompareTag("Shield")) // Kiểm tra va chạm với đối tượng có tag "DamageObject"
        {
            StartCoroutine(StandStill2());

        }
        if (other.gameObject.CompareTag("Knite")) // Kiểm tra va chạm với đối tượng có tag "DamageObject"
        {
            StartCoroutine(StandStill());
        }
    }
    IEnumerator StandStill()
    {
        curentHeath--;
        canRun = false;
        yield return new WaitForSeconds(2);
        canRun = true;

    }
    IEnumerator StandStill2() { 
        canRun = false;
        yield return new WaitForSeconds(2);
        canRun = true;

    }
}
