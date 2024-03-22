using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public static Enemy instance { get; set; }
    public int idx;
    public GameObject target;
    public Vector3 m_targetPos;
    private Vector3 velocity = new Vector3(0f, 0f, 0f);
    public float min = 99f;
    public Vector2Int newPos = new Vector2Int(0, 0);
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        m_targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void FindDis()
    {
        FindRoute.instance.FindDis(currentPos, moveDistnace,"Enemy", transform);
    }
    
    public void Move()
    {
        transform.position = Vector3.SmoothDamp(transform.position, m_targetPos, ref velocity, moveSpeedCoef);
    }
}
