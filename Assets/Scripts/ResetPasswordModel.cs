using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPasswordModel
{
    public ResetPasswordModel(string username, string newPassword, int otp)
    {
        this.username = username;
        this.newPassword = newPassword;
        this.otp = otp;
    }

    public string username { get; set; }
    public string newPassword { get; set; }
    public int otp { get; set; }
}
