using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using System.Collections;

public class ForgotPass : MonoBehaviour
{
    public TMP_Text txtError;
    public TMP_InputField txtUser, txtOTP, txtNewpass, txtEmail;
    public GameObject resetPasswordPanel, sendOTPPanel, loginPanel;
    // Replace with your actual server IP

    public void SendOTP()
    {
        string userEmail = txtUser.text;

        if (string.IsNullOrEmpty(userEmail))
        {
            txtError.text = "Please enter an email.";
            return;
        }

        StartCoroutine(SendOTPAPI(userEmail));

    }

    IEnumerator SendOTPAPI(string userEmail)
    {
        string url = "http://localhost:3000/users/forgotpassword";

        WWWForm form = new WWWForm();
        form.AddField("email", userEmail);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending OTP request: " + request.error);
                txtError.text = "Failed to send OTP.";
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;

                // Handle the response
                // Parse JSON response and show appropriate UI based on status
            }
        }
    }

    public void ResetPassword()
    {
        string userEmail = txtUser.text;
        string newPass = txtNewpass.text;
        int otp;

        if (!int.TryParse(txtOTP.text, out otp))
        {
            txtError.text = "Định dạng OTP không hợp lệ.";
            return;
        }

        StartCoroutine(ResetPassAPI(userEmail, otp, newPass));
    }

    IEnumerator ResetPassAPI(string userEmail, int otp, string newPassword)
    {
        string url = "http://localhost:3000/users/resetpassword";

        WWWForm form = new WWWForm();
        form.AddField("email", userEmail);
        form.AddField("otp", otp.ToString());
        form.AddField("newPassword", newPassword);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Lỗi khi đặt lại mật khẩu: " + request.error);
                txtError.text = "Không thể đặt lại mật khẩu.";
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;

                // Xử lý phản hồi
                // Phân tích phản hồi JSON và hiển thị giao diện người dùng phù hợp dựa trên trạng thái
            }
        }
    }


}
