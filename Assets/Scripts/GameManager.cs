using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }
    
    public Animator cameraToggleAnim;
    public Slider cameraToggleSlider;
    public Button cameraToggleButton;
    public Transform gridParent;
    public Transform enemyParent;
    public bool turn = true;
    
    
    void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void EndOfTurn()
    {
        turn = false;
        StartCoroutine(FindDis());
    }

    IEnumerator FindDis()
    {
        for (int i = 0; i < enemyParent.childCount; i++) 
        {
             enemyParent.GetChild(i).transform.GetComponent<Enemy>().FindDis();
             yield return new WaitForSeconds(1f);
        }
       
        turn = true;
        Player.instance.FindDis();
    }
}
