using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserModel 
{
    public UserModel(string username,string email, string password)
    {
        this.username = username;
        this.password = password;
        this.email=email;
    }
    public UserModel(string username, string password)
    {
        this.username = username;
        this.password = password;

    }
    public string email { get;set; }
    public string username { get; set; }
    public string password { get; set; }

}
