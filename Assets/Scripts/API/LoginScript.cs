using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class LoginScript : MonoBehaviour
{

    public TMP_InputField edtUser, edtPass;
    public TMP_Text txtError;
    public Selectable first;
    private EventSystem _eventSystem;
    public Button btnLogin;
    // Start is called before the first frame update
    void Start()
    {
        _eventSystem = EventSystem.current;
        first.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void checkLogin()
    {
        var user = edtUser.text;
        var pass = edtPass.text;

        UserModel userModel = new UserModel(user,pass);
        if(user.Equals("abc") && pass.Equals("abc"))
        {
            SceneManager.LoadScene(1);

        }
        else
        {
            txtError.text = "Login failed!";
        }
    }

}
