using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //-----------------------
    public Animator animator;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public Vector3 moveInput;
    SoundManager soundManager;
    //---------------------------
    // coin
    public static int numberOfCoins;
    public TextMeshProUGUI coinText;
    public static int numberOfKey;
    public TextMeshProUGUI KeyText;
    // heathBar
    [SerializeField] int maxHeath;
    int curentHeath;
    public HeathBar heathBar;
    public UnityEvent onDeath;
    //
    public ParticleSystem bulletParticleSystem;
    public int maxBullets = 5;
    private int currentBullets;

/*    public void OnEnable()
    {   
        onDeath.AddListener(death);
    }
    public void OnDisable()
    {
        onDeath.RemoveListener(death);
    }*/
    public void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
        numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        curentHeath = maxHeath;
        numberOfKey = 0;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentBullets = maxBullets;

    }


    // nhân vật bị tấn công
    public void takeDame(int Damage)
    {
        curentHeath -= Damage;
        soundManager.PlayFSX(soundManager.death);
        if (curentHeath <= 0)
        {   
            //onDeath.Invoke();
            StartCoroutine(Death());
        }
    }
    // getHeatlh
    public void takeHeatlh(int Heatlh)
    {
        soundManager.PlayFSX(soundManager.getChest);
        if (curentHeath < maxHeath)
        {
            curentHeath += Heatlh;
        }
        else
        {
            curentHeath = maxHeath;
        }
    }
    // nhân vật chết
    IEnumerator Death()
    {
        animator.SetBool("isDeath", true);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }
    // Update is called once per frame
    void Update()
    {

        // Coin
        coinText.text = numberOfCoins.ToString();
        // Key
        KeyText.text = numberOfKey.ToString();

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

        if (collision.gameObject.CompareTag("Key"))
        {

            soundManager.PlayFSX(soundManager.getCoint);
            numberOfKey = 1;
           // animator.SetBool("getKey", true);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Slime"))
        {
            animator.SetBool("isHurt", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Slime"))
        {
            animator.SetBool("isHurt", false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            soundManager.PlayFSX(soundManager.hitwall);
        }
    }
}
