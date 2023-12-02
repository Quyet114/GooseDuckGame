using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RegisterScript : MonoBehaviour
{
    public TMP_InputField edtUserName, edtPass, edtConfirmPass;
    public TMP_Text txtError, txtNoti;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void kiemTraDangKy()
    {
        string userName = edtUserName.text;
        string pass = edtPass.text;
        string confirmPass = edtConfirmPass.text;
        if (pass.Equals(confirmPass))
        {
            UserModel userModel = new UserModel(userName, pass);
            CheckSignup(userModel);
            StartCoroutine(CheckSignup(userModel));
        }
        else
        {
            txtNoti.SetText("Confirm Password wrong");
            if (userName.Equals("") || pass.Equals(""))
            {
                txtNoti.SetText("email and password is not empty");

            }
        }
    }

    IEnumerator CheckSignup(UserModel userModel)
    {
        //…
        string jsonStringRequest = JsonConvert.SerializeObject(userModel);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/register", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            Debug.Log("Error API");
            txtNoti.SetText("Failed");
        }


        //respone signup
        else
        {
            var jsonString = request.downloadHandler.text.ToString();
            ResponseModel responseModel = JsonConvert.DeserializeObject<ResponseModel>(jsonString);
            string notification = responseModel.notification;
            if (notification != null)
            {
                txtNoti.text = notification;
            }

        }
        request.Dispose();
    }
}
