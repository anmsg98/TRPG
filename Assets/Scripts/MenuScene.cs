using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuScene : MonoBehaviour
{
    public TMP_Text userInfo;
    void Start()
    {
        userInfo.text = PlayerInfo.auth.CurrentUser.Email + "님이 로그인 하였습니다.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
