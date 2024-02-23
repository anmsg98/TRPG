using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;

public class LoginSystem : MonoBehaviour
{
    private FirebaseAuth auth;
    private bool checkBox = false;
    
    public TMP_InputField id;
    public TMP_InputField pw;
    public TMP_Text messageUI;

    public Image checkBoxImg;
    
    void Start()
    {
        Application.targetFrameRate = 60;
        auth = FirebaseAuth.DefaultInstance;
        LoadLoginSetting();
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
                    PlayerInfo.auth = auth;
                    SaveId();
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

    public void LoadLoginSetting()
    {
        string textAsset = Application.streamingAssetsPath + "/Option/SaveEmail.txt";
        StreamReader reader = new StreamReader(textAsset);
        string line;
        line = reader.ReadLine();
        id.text = line.Split(' ')[1];
        line = reader.ReadLine();
        if (Convert.ToInt32(line.Split(' ')[1]) == 1)
        {
            checkBox = true;
            checkBoxImg.sprite = Resources.Load<Sprite>("Sprites/checkOn");
        }
        else
        {
            checkBox = false;
            checkBoxImg.sprite = Resources.Load<Sprite>("Sprites/checkOff");
        }
        reader.Close();
    }
    public void SaveId()
    {
        string path = Application.streamingAssetsPath + "/Option/SaveEmail.txt";
        StreamWriter writer = new StreamWriter(path, false);
        if (checkBox)
        {
            writer.WriteLine("ID " + auth.CurrentUser.Email);
            writer.WriteLine("CheckBox 1" );
        }
        else
        {
            writer.WriteLine("ID ");
            writer.WriteLine("CheckBox 0");
        }
        writer.Close();
    }

    public void CheckOn()
    {
        if (checkBox)
        {
            checkBoxImg.sprite = Resources.Load<Sprite>("Sprites/checkOff");
            checkBox = false;
        }
        else
        {
            checkBoxImg.sprite = Resources.Load<Sprite>("Sprites/checkOn");
            checkBox = true;
        }
    }
}
