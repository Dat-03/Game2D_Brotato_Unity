using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterUserModel
{
    public RegisterUserModel(string username, string password, string email)
    {
        this.username = username;
        this.password = password;
        this.email = email;
    }

    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }

}
