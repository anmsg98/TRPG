using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private Vector3 m_targetPos;
    private Vector3 velocity = new Vector3(0f, 0f, 0f);

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        m_targetPos = new Vector3(currentPos.y * 0.2222f, 0f, currentPos.x * 0.2222f);
        transform.position = new Vector3(currentPos.y * 0.2222f, 0f, currentPos.x * 0.2222f);
        FindRoute.instance.FindDis(currentPos, moveDistnace);
    }

    void Update()
    {
        PickObject();
    }

    public void FindDis()
    {
        FindRoute.instance.FindDis(currentPos, moveDistnace);
    }
    
    void PickObject()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Grid")
                {
                    currentPos = hit.transform.GetComponent<Grid>().currentLoc;
                    m_targetPos = hit.transform.position;
                    m_targetPos.y = 0f;
                    StartCoroutine(FindRoute.instance.FollowCamera());
                    animator.SetTrigger("Run");
                }
            }
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.name == "Grid")
                    {
                        currentPos = hit.transform.GetComponent<Grid>().currentLoc;
                        m_targetPos = hit.transform.position;
                        m_targetPos.y = 0f;
                        StartCoroutine(FindRoute.instance.FollowCamera());
                        animator.SetTrigger("Run");
                    }
                }
            }
        }

        transform.LookAt(m_targetPos);
        transform.position = Vector3.SmoothDamp(transform.position,m_targetPos, ref velocity, moveSpeedCoef);
    }
}
