using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

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
        CheckData();
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
        string txtFile = Application.persistentDataPath + "/SaveEmail.txt";
    
        FileStream filestream = new FileStream(txtFile, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(filestream, System.Text.Encoding.UTF8);
        string line;
        line = sr.ReadLine();
        id.text = line.Split(' ')[1];
        line = sr.ReadLine();
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
        sr.Close();
        filestream.Close();
    }
    public void SaveId()
    {
        //string path = "Assets/Resources/Option/SaveEmail.txt";
        string path = Application.persistentDataPath+"/SaveEmail.txt";
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

    private void CheckData()
    {
        string path = Application.persistentDataPath+"/SaveEmail.txt";
        bool fileExist = File.Exists(path);
        if (!fileExist) 
        {
            StreamWriter writer = new StreamWriter(path, false);
            writer.WriteLine("ID ");
            writer.WriteLine("CheckBox 0");
            writer.Close();
        }
    }
}
