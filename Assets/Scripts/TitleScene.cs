using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class TitleScene : MonoBehaviour
{
    public int android;
    public TMP_Text userInfo;
    public Camera cam;
    void Start()
    {
        android = 1;
        userInfo.text += PlayerInfo.auth.CurrentUser.UserId;
    }
    
    void Update()
    {
        Application.targetFrameRate = 60;
        LoadMainScene();
    }

    void LoadMainScene()
    {
        // 안드로이드 터치
        if (android == 1)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                    pointerEventData.position = touch.position;
                    List<RaycastResult> results = new List<RaycastResult>();
                    EventSystem.current.RaycastAll(pointerEventData, results);

                    for (int i = 1; i < 2; i++)
                    {
                        if (results[i].gameObject.layer == LayerMask.NameToLayer("BackGround"))
                        {
                            SceneManager.LoadScene("GameScene");
                        }
                    }
                }
            }
        }
        else
        {
            // 컴퓨터 테스트용 마우스 클릭
            if (Input.GetMouseButtonDown(0))
            {
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                pointerEventData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerEventData, results);

                for (int i = 1; i < 2; i++)
                {
                    if (results[i].gameObject.layer == LayerMask.NameToLayer("BackGround"))
                    {
                        SceneManager.LoadScene("GameScene");
                    }
                }
            }
        }
    }

    public void QNA()
    {
        string subject = EscapeURL("문의사항 및 질문");
        
        Application.OpenURL("mailto:anmsg98@gmail.com" + "?subject=" + subject);
    }

    private string EscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }
}
