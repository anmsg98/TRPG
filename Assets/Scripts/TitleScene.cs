using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScene : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        Application.targetFrameRate = 60;
        LoadMainScene();
    }

    void LoadMainScene()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }
}
