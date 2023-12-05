using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.Networking;

public class PlayerScript : MonoBehaviour
{
    //-----------------------
    public Animator animator;
    public float moveSpeed = 2;
    private Rigidbody2D rb;
    public Vector3 moveInput;
    SoundManager soundManager;
    //---------------------------
    // coin
    public static int numberOfCoins, Score;
    public TextMeshProUGUI coinText;
    public static int numberOfKey, keyPoint = 50;
    public TextMeshProUGUI KeyText;
    public TextMeshProUGUI NotifyText;
    public TextMeshProUGUI coinLever;
    // heathBar
    [SerializeField] int maxHeath;
    int curentHeath;
    public HeathBar heathBar;
    public UnityEvent onDeath;
    // Get Items
    public GameObject lightObject;
    public GameObject healthObject;
    public GameObject speedObject;
    private bool isActivated = false;
    private bool canRun = true;
    //End game
    public GameObject obCrow1, obCrow2, obCrow3;
    public GameObject canvasEndGame;
    //Check Point Cavas
    public GameObject checkP;
    // tăng tầm nhìn
    public Light2D spotlight; // Kéo và thả Spotlight 2D từ Inspector vào trường này
    public float increasedRadius = 2f; // Giá trị tăng thêm cho radius outer
                                       //
    public GameObject shield,knite;
    //
    private int score;

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
        shield.SetActive(false);
        knite.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {

        numberOfCoins = 0;
        curentHeath = maxHeath;
        numberOfKey = 0;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        lightObject.SetActive(isActivated);
        healthObject.SetActive(isActivated);
        speedObject.SetActive(isActivated);

        // load điểm load vị trí
        if (LoginScript.loginResponse.score >= 0)
        {
            score = LoginScript.loginResponse.score;
            coinText.text = (score + numberOfCoins).ToString();
        }


        if (LoginScript.loginResponse.positionX != "")
        {
            var positionX = float.Parse(LoginScript.loginResponse.positionX) +1;
            var positionY = float.Parse(LoginScript.loginResponse.positionY) -1;
            var positionZ = float.Parse(LoginScript.loginResponse.positionZ);
            transform.position = new Vector3(positionX, positionY, positionZ);
        }

        /*txtUser.text = LoginScript.loginResponse.username;*/

    }


    // nhân vật bị tấn công
    public void takeDame(int Damage)
    {
        curentHeath -= Damage;
        soundManager.PlayFSX(soundManager.death);
        if (curentHeath <= 0)
        {
            //onDeath.Invoke();
            canRun = false;
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
        canvasEndGame.SetActive(true);
        SaveScore();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }
    // Update is called once per frame
    void Update()
    {
        //transform.position += moveInput * moveSpeed * Time.deltaTime;
        Score = (score + numberOfCoins);
        coinLever.text = numberOfCoins.ToString();
        coinText.text =  numberOfCoins.ToString();
        // Key
        KeyText.text = numberOfKey.ToString();
        // thanh máu
        if (heathBar != null)
        {
            heathBar.UpdateHealth(curentHeath, maxHeath);
        }


        // di chuyển
        if (canRun)
        {
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");
            transform.position += moveInput * moveSpeed * Time.deltaTime;
        }
        // di chuyển
        if (moveInput.x != 0 || moveInput.y != 0)

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
        if (Input.GetKeyDown(KeyCode.Space) ) // Khi nhấn phím Space (có thể thay đổi)
        {
            StartCoroutine(AttackKnite());
        }


        if (Input.GetKeyDown(KeyCode.E)) // Khi nhấn phím Space (có thể thay đổi)
        {
            StartCoroutine (AttackShield());
        }

        IEnumerator AttackKnite()
        {
            knite.SetActive(true);
            animator.SetBool("Knite", true);
            yield return new WaitForSeconds(1);
            knite.SetActive(false);
            animator.SetBool("Knite", false);

        }
        IEnumerator AttackShield()
        {
            shield.SetActive(true);
            animator.SetBool("Shield", true);
            yield return new WaitForSeconds(1); 
            shield.SetActive(false);
            animator.SetBool("Shield", false);

        }
    }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Kiểm tra xem Player có va chạm với OB không
            if (collision.gameObject.CompareTag("OB"))
            {
                // Lấy Rigidbody của OB
                Rigidbody2D obRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            }
        if (collision.gameObject.CompareTag("checkpoint"))
        {
            canRun = false;

        }
        // chạm key
        if (collision.gameObject.CompareTag("Key"))
            {

                soundManager.PlayFSX(soundManager.getCoint);
                numberOfKey++;
                numberOfCoins += keyPoint;
                // animator.SetBool("getKey", true);
                Destroy(collision.gameObject);
            }
            // chạm Slime
            if (collision.gameObject.CompareTag("Slime"))
            {
                animator.SetBool("isHurt", true);
            }
        }
        // thoát khỏi collision
        private void OnCollisionExit2D(Collision2D collision)
        {
            
            if (collision.gameObject.CompareTag("Slime"))
            {
                animator.SetBool("isHurt", false);
            }

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Chạm coin
            if (collision.CompareTag("Coin"))
            {
                soundManager.PlayFSX(soundManager.hitwall);
            }
            // Chạm điểm EndGame
            if (collision.CompareTag("Finishpoint") && numberOfKey>=3)
            {
                StartCoroutine(saveS());
                soundManager.PlayFSX(soundManager.hitwall);
            if (numberOfCoins <= 50)
            {
                obCrow1.SetActive(true);
                obCrow2.SetActive(false);
                obCrow3.SetActive(false);
            }
            else if (numberOfCoins > 50 && numberOfCoins < 150)
            {
                obCrow1.SetActive(true);
                obCrow2.SetActive(true);
                obCrow3.SetActive(false);
            }
            else if (numberOfCoins >= 150)
            {
                obCrow1.SetActive(true);
                obCrow2.SetActive(true);
                obCrow3.SetActive(true);
            }
            else
            {
                obCrow1.SetActive(false);
                obCrow2.SetActive(false);
                obCrow3.SetActive(false);
            }

        }

            //Chạm các vật phẩm
            if (collision.gameObject.CompareTag("Light"))
            {
                // Tăng radius outer của spotlight khi va chạm với trigger "Light"
                StartCoroutine(Light());
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("Speed"))
            {
                // Tăng radius outer của spotlight khi va chạm với trigger "Light"
                StartCoroutine(Speed());
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("Heatlh"))
            {
                // Tăng radius outer của spotlight khi va chạm với trigger "Light"
                curentHeath = maxHeath;
                Destroy(collision.gameObject);
                StartCoroutine(Heatlh());
            }

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
        }

        IEnumerator saveS()
        {
            SaveScore();
            yield return new WaitForSeconds(1);
            canRun = false;

        }
        IEnumerator Light()
        {
            lightObject.SetActive(true);
            spotlight.pointLightOuterRadius += increasedRadius;
            yield return new WaitForSeconds(10);
            spotlight.pointLightOuterRadius -= increasedRadius;
            lightObject.SetActive(false);
        }
        IEnumerator Speed()
        {
            speedObject.SetActive(true);
            moveSpeed = 3.5f;
            yield return new WaitForSeconds(10);
            moveSpeed = 2f;
            speedObject.SetActive(false);
        }
        IEnumerator Heatlh()
        {
            healthObject.SetActive(true);
            yield return new WaitForSeconds(3);
            healthObject.SetActive(false);
        }
        IEnumerator Notify()
        {
            NotifyText.text = "Successfuly!";
        yield return new WaitForSeconds(2);
            NotifyText.text = "Save Your Point!";

    }
    // Can run
    public void CanRun()
        {
        canRun = true;
        }
        public void SavePosition()
        {
            var email = LoginScript.loginResponse.email;
            var x = transform.position.x;
            var y = transform.position.y;
            var z = transform.position.z;
            PositionModel positionModel = new PositionModel(email, x.ToString(), y.ToString(), z.ToString());
            StartCoroutine(SavePositionAPI(positionModel));
            //SavePositionAPI(positionModel);
        }

        // api lưu position
        IEnumerator SavePositionAPI(PositionModel positionModel)
        {
            //…
            string jsonStringRequest = JsonConvert.SerializeObject(positionModel);

            var request = new UnityWebRequest("https://mongo-game-api.onrender.com/v1/user/save-position", "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
                Debug.Log("Error API");
                
            }
            else
            {
                var jsonString = request.downloadHandler.text.ToString();
                ResponseModel responseModel = JsonConvert.DeserializeObject<ResponseModel>(jsonString);

                if (responseModel.status == 1)
                {
                checkP.SetActive(false);
                StartCoroutine(Notify());
                }
                else
                {
                    // gọi lại api lưu position
                }

            }
            request.Dispose();
        }


        // lưu score
        public void SaveScore()
        {
            var email = LoginScript.loginResponse.email;
            ScoreModel scoreModel = new ScoreModel(email, Score);
            StartCoroutine(saveScoreAPI(scoreModel));
            //saveScoreAPI(scoreModel);
        }

        // api lưu score
        IEnumerator saveScoreAPI(ScoreModel scoreModel)
        {
            //…
            string jsonStringRequest = JsonConvert.SerializeObject(scoreModel);

            var request = new UnityWebRequest("https://mongo-game-api.onrender.com/v1/user/save-score", "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
                Debug.Log("Error API");
            }
            else
            {
                var jsonString = request.downloadHandler.text.ToString();
                ResponseModel responseModel = JsonConvert.DeserializeObject<ResponseModel>(jsonString);

                if (responseModel.status == 1)
                {
                // set score để test
                Debug.Log("Điểm của bạn là: " + Score);
                }
                else
                {
                    // gọi lại api lưu 
                }

            }
            request.Dispose();
        }

}
