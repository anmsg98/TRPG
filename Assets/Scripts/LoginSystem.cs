using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoginSystem : MonoBehaviour
{
    private FirebaseAuth auth;

    public TMP_InputField id;
    public TMP_InputField pw;

    public TMP_Text messageUI;
    
    void Start()
    {
        Application.targetFrameRate = 60;
        auth = FirebaseAuth.DefaultInstance;
        messageUI.text = "";
    }

    public void Login()
    {
        string email = id.text;
        string password = pw.text;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(
            task =>
            {
                if (!task.IsCanceled && !task.IsFaulted)
                {
                    SceneManager.LoadScene("TitleScene");
                }
                else
                {
                    messageUI.text = "계정을 다시 확인하여 주십시오.";
                }
            }
        );
    }

    public void JoinSceneLoad()
    {
        SceneManager.LoadScene("JoinScene");
    }
}
