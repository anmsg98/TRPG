using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using TMPro;
using UnityEngine.UI;

public class JoinSystem : MonoBehaviour
{
    private FirebaseAuth auth;

    public TMP_InputField id;
    public TMP_InputField pw;

    public TMP_Text messageUI;
    
    
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        messageUI.text = "";
    }

    bool InputCheck()
    {
        string email = id.text;
        string password = pw.text;

        if (email.Length < 8)
        {
            messageUI.text = "이메일은 8자 이상으로 구성되어야 합니다.";
            return false;
        }
        else if (password.Length < 8)
        {
            messageUI.text = "비밀번호는 8자 이상으로 구성되어야 합니다.";
            return false;
        }
        messageUI.text = "";
        return true;
    }

    public void Check()
    {
        InputCheck();
    }

    public void Join()
    {
        if (!InputCheck())
        {
            return;
        }

        string email = id.text;
        string password = pw.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(
            task =>
            {
                if (!task.IsCanceled && !task.IsFaulted)
                {
                    messageUI.text = "회원가입이 완료되었습니다.";
                }
                else
                {
                    messageUI.text = "이미 사용 중이거나 형식이 바르지 않습니다.";
                }
            }
        );
    }
    
}
