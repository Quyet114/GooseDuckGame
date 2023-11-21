using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.Networking;

public class LoginUser: MonoBehaviour {
    public TMP_InputField edtUser, edtPass;
    public TMP_Text txtError;
    public Selectable first;
    private EventSystem eventSystem;
    public Button btnLogin;
     void Start() {
        eventSystem = EventSystem.current;
        first.Select();
    }
    // void Update() {
    //     if (Input.GetKey(KeyCode.Return)){
    //         btnLogin.onClick.Invoke();
    //     }    
    //     if (Input.GetKeyDown(Keycode.Tab)){
    //         Selectable next = eventSystem
    //             .currentSelectedGameObject
    //             .GetComponent<Selectable>()
    //             .FindSelectableOnDown();
    //         if (next != null) next.Select();
    //     }
    //     if (Input.GetKey(KeyCode.LeftShift)){
    //         Selectable next = eventSystem
    //             .currentSelectedGameObject
    //             .GetComponent<Selectable>()
    //             .FindSelectableOnUp();
    //             if (next != null) next.Select();
    //     }
    // }
    public void CheckLogin(){
        var user = edtUser.text;
        var pass = edtPass.text;

        UserModel userModel = new UserModel(user, pass);
        StartCoroutine(Login(userModel));
        Login(userModel);
        Debug.Log(user);
        
    }
    public void LoadRegister()
    {
        SceneManager.LoadScene("Register");
    }
    IEnumerator Login(UserModel userModel){
        string jsonStringRequest = JsonConvert.SerializeObject(userModel);
        Debug.Log(jsonStringRequest);
        var request = new UnityWebRequest("https://mongo-game-api.onrender.com/v1/auth/login", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("====>");
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("<====");
            var jsonString = request.downloadHandler.text.ToString();
            Debug.Log(jsonString);
            LoginResponseModel loginResponseModel = JsonConvert.DeserializeObject<LoginResponseModel>(jsonString);
            if(loginResponseModel.status == 1){
                SceneManager.LoadScene("Menu");
            }
            else{
                txtError.text = loginResponseModel.notification;
            }  
            
        }
    }
}
