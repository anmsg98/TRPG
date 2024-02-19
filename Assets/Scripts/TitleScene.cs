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
        LoadMainScene();
    }

    void LoadMainScene()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    void TextFadeInOut()
    {
        
    }
    
}
