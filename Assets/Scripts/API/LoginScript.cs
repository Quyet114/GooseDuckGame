using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class LoginScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    public TMP_InputField edtUser, edtPass;
    public TMP_Text txtError;
    public static LoginResponse loginResponse;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void kiemTraDangNhap()
    {
        var user = edtUser.text;
        var pass = edtPass.text;

        UserModel userModel = new UserModel(user,pass);
        CheckLogin(userModel);
        StartCoroutine(CheckLogin(userModel));
        txtError.text = "Loading...! Please waiting!";

    }
    public void MovetoGame()
    {
        StartCoroutine(LoadLever());
    }
    IEnumerator LoadLever()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        animator.SetTrigger("Start");
    }

    IEnumerator CheckLogin(UserModel userModel)
    {
        //…
        string jsonStringRequest = JsonConvert.SerializeObject(userModel);

        /*var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/login", "POST");*/
        var request = new UnityWebRequest("https://mongo-game-api.onrender.com/v1/auth/login", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            txtError.text = "Wrong user or Password!";
        }
        else
        {
            var jsonString = request.downloadHandler.text.ToString();
            loginResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonString);

            if(loginResponse.status == 0)
            {
                txtError.text = loginResponse.notification;
                Debug.Log(txtError.text);
                Debug.Log("status:" + loginResponse.status);
            }
            else
            {
                string positionX = loginResponse.positionX;
                string positionY = loginResponse.positionY;
                string positionZ = loginResponse.positionZ;
                string username = loginResponse.username;
                /*int score = loginResponse.score;*/
                Bringvalue.username = username;
                Bringvalue.positionX = positionX;
                Bringvalue.positionY = positionY;
                Bringvalue.positionZ = positionZ;
                /* Bringvalue.score = score;*/
                MovetoGame();
                Debug.Log("status:" + loginResponse.status);
                Debug.Log(username);
            }

        }
        request.Dispose();
    }

}
