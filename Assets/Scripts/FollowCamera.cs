using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.EventSystems;
using Slider = UnityEngine.UI.Slider;

public class FollowCamera : MonoBehaviour
{ 
    public Transform target;
    public Slider cameraToggleSlider;
    
    public float camSpeed;
    public Transform realCamera;

    public float dragspeed = .5f;
    public TMP_Text speedText;
    private Vector2 click1, click2;
    private Vector3 movePos;
    
    // 뒤에 장애물이 있으면 카메라가 플레이어에게 근접

    private void Start()
    {
        transform.position = target.position;
        Debug.Log("x: " + Screen.width + ", y: " + Screen.height);
    }

    private void Update()
    {
        speedText.text = dragspeed.ToString();
        RotateCamera();
        Move();
    }

    private void LateUpdate()
    {
        // 마우스 기준
        
        //플레이어 기준
        //transform.position = Vector3.MoveTowards(transform.position, target.position, camSpeed * Time.deltaTime);
    }
    
    void RotateCamera()
    {
        transform.eulerAngles = new Vector3(
            0f, 360f * cameraToggleSlider.value, 0f);
    }

    void Move()
    {
        // // PC 테스트(마우스)
        // if (Input.GetMouseButtonDown(0))
        // {
        //     click1 = Input.mousePosition;
        // }
        //
        // if (Input.GetMouseButton(0))
        // {
        //     if (click1.x / Screen.width > 0.21f)
        //     {
        //         click2 = Input.mousePosition;
        //         movePos = (Vector3) (click1 - click2) * Time.deltaTime * dragspeed;
        //         movePos.z = movePos.y;
        //         movePos.y = 0f;
        //         transform.Translate(movePos);
        //         click1 = Input.mousePosition;
        //     }
        // }
        //안드로이드 테스트(터치)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                click1 = touch.position - touch.deltaPosition;
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                if (click1.x / Screen.width > 0.21f)
                {
                    click2 = touch.position - touch.deltaPosition;
                    movePos = (Vector3) (click1 - click2) * Time.deltaTime * dragspeed;
                    movePos.z = movePos.y;
                    movePos.y = 0f;
                    transform.Translate(movePos);
                    click1 = touch.position - touch.deltaPosition;
                }
            }
        }
    }

    public void targetMove()
    {
        transform.position = target.position;
    }

    public void SpeedUp()
    {
        dragspeed = Mathf.Clamp(dragspeed + 0.1f, 0.1f, 1f);
        speedText.text = dragspeed.ToString();
    }

    public void SpeedDown()
    {
        dragspeed = Mathf.Clamp(dragspeed - 0.1f, 0.1f, 1f);
        speedText.text = dragspeed.ToString();
    }
}
