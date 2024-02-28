using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public class GameManager : MonoBehaviour
{
    public Animator cameraToggleAnim;
    public Slider cameraToggleSlider;
    public Button cameraToggleButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RotateCamera();
    }
    
    public void CameraToggleOn()
    {
        if (cameraToggleAnim.GetBool("CameraOn"))
        {
            cameraToggleAnim.SetBool("CameraOn",false);
        }
        else
        {
            cameraToggleAnim.SetBool("CameraOn", true);
        }
    }
}
