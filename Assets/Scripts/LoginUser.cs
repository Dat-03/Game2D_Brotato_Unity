using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using System.Collections;
using System;

public class LoginUser : MonoBehaviour
{
    public TMP_InputField edtUser, edtPassword, edtEmail;
    public TMP_Text txtError;
    public Button btnLogin, btnRegister;
    public Selectable first;
    private EventSystem eventSystem;
    private string apiupdate = "http://localhost:3000/quests/random";

    void Start()
    {
        eventSystem = EventSystem.current;
        first.Select();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            btnLogin.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = eventSystem
                .currentSelectedGameObject
                .GetComponent<Selectable>()
                .FindSelectableOnDown();
            if (next != null) next.Select();
        }
    }

    public void CheckLogin()
    {
        var user = edtUser.text;
        var password = edtPassword.text;

        // Kiểm tra xem người dùng đã nhập thông tin đăng nhập chưa
        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
        {
            txtError.text = "Vui lòng nhập tên đăng nhập và mật khẩu.";
            return;
        }

        UserModel userModel = new UserModel(user, password);
        StartCoroutine(Login(userModel));
    }

    IEnumerator Login(UserModel userModel)
    {
        string jsonStringRequest = JsonConvert.SerializeObject(userModel);
        var request = new UnityWebRequest("http://localhost:3000/users/login", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Lỗi kết nối: " + request.error);
            txtError.text = "Lỗi kết nối: " + request.error;
        }
        else
        {
            var jsonString = request.downloadHandler.text;
            if (!string.IsNullOrEmpty(jsonString))
            {
                LoginResponModel loginResponModel = JsonConvert.DeserializeObject<LoginResponModel>(jsonString);
                if (loginResponModel != null)
                {
                    if (loginResponModel.status == 0)
                    {
                        int idmission = Convert.ToInt32(loginResponModel.id_mission);
                        string username = loginResponModel.userName;
                        int coin = Convert.ToInt32(loginResponModel.coin);
                        PlayerPrefs.SetInt("id_mission", idmission);
                        PlayerPrefs.SetString("username", username);
                        PlayerPrefs.SetInt("coin", coin);
                        SceneManager.LoadScene(1);
                    }
                    else
                    {
                        Debug.LogError("Lỗi rồi");
                    }
                }
                else
                {
                    Debug.LogError("Lỗi phản hồi từ server: Dữ liệu không hợp lệ.");
                    txtError.text = "Lỗi phản hồi từ server: Dữ liệu không hợp lệ.";
                }
            }
            else
            {
                Debug.LogError("Lỗi phản hồi từ server: Dữ liệu trống.");
                txtError.text = "Lỗi phản hồi từ server: Dữ liệu trống.";
            }
        }
    }

    public void CheckRegister()
    {
        var username = edtUser.text;
        var password = edtPassword.text;
        var email = edtEmail.text;

        // Kiểm tra xem người dùng đã nhập đủ thông tin đăng ký chưa
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
        {
            txtError.text = "Vui lòng nhập đầy đủ thông tin đăng ký.";
            return;
        }

        RegisterUserModel registerUserModel = new RegisterUserModel(username, password, email);
        StartCoroutine(Register(registerUserModel));
    }

    IEnumerator Register(RegisterUserModel registerUserModel)
    {
        string jsonStringRequest = JsonConvert.SerializeObject(registerUserModel);
        var request = new UnityWebRequest("http://localhost:3000/users/register", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Lỗi kết nối: " + request.error);
            txtError.text = "Lỗi kết nối: " + request.error;
        }
        else
        {
            var jsonString = request.downloadHandler.text;
            if (!string.IsNullOrEmpty(jsonString))
            {
                LoginResponModel registerResponModel = JsonConvert.DeserializeObject<LoginResponModel>(jsonString);
                if (registerResponModel != null)
                {
                    if (registerResponModel.status == 0)
                    {
                        Debug.Log("Đăng ký thành công.");
                    }
                    else
                    {
                        txtError.text = registerResponModel.notification;
                    }


                }
                else
                {
                    Debug.LogError("Lỗi phản hồi từ server: Dữ liệu không hợp lệ.");
                    txtError.text = "Lỗi phản hồi từ server: Dữ liệu không hợp lệ.";
                }
            }
            else
            {
                Debug.LogError("Lỗi phản hồi từ server: Dữ liệu trống.");
                txtError.text = "Lỗi phản hồi từ server: Dữ liệu trống.";
            }
        }
    }

}
