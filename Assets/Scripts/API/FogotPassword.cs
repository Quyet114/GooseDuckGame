using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FogotPassword : MonoBehaviour
{
    public TMP_InputField edtUser;
    public TMP_Text txtNotify;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetPassword()
    {
        var user = edtUser.text;

        ResetPassModel resetPassModel = new ResetPassModel(user);
        StartCoroutine(FogotPass(resetPassModel));
        FogotPass(resetPassModel);
        txtNotify.text = "Loading...! Please waiting!";

    }


    IEnumerator FogotPass(ResetPassModel resetPassModel)
    {
        //…
        string jsonStringRequest = JsonConvert.SerializeObject(resetPassModel);

        var request = new UnityWebRequest("https://mongo-game-api.onrender.com/v1/password/reset-password", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            var jsonString = request.downloadHandler.text.ToString();
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonString);
            txtNotify.text = "Mail had send! Check your email!";

        }
        request.Dispose();
    }
}
