using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Serializable]
    private class Stat
    {
        public int m_level;
        public int m_hp;
        public int m_mp;
        public int m_exp;
        public int m_maxExp;

        public int m_moveDistance;
        public float m_moveSpeedCoef;
        public float m_maxMoveSpeedCoef;

        public int m_attackDamage;
        public float m_attackSpeedCoef;
        public float m_maxAttackSpeedCoef;
        public float m_criticalPercent;

        public int m_armor;

        public Vector2Int m_currentPos;
    }

    [Serializable]
    private class Status
    {
        public bool m_isAttack;
        public bool m_isDamaged;
    }

    // 스탯 및 상태
    [SerializeField]
    private Stat m_stat;

    [SerializeField]
    private Status m_status;

    // 컴포넌트
    private Animator m_animator;
    private Collider m_collider;

    public int level
    {
        set
        {
            if ((value < 0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_level = value;
        }

        get
        {
            return m_stat.m_level;
        }
    }

    public int hp
    {
        set
        {
            if ((value < 0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_hp = value;
        }

        get
        {
            return m_stat.m_hp;
        }
    }

    public int mp
    {
        set
        {
            if ((value < 0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_mp = value;
        }

        get
        {
            return m_stat.m_mp;
        }
    }

    public int exp
    {
        set
        {
            if ((value < 0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_exp = value;
        }

        get
        {
            return m_stat.m_exp;
        }
    }

    public int maxExp
    {
        set
        {
            if ((value < 0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_maxExp = value;
        }

        get
        {
            return m_stat.m_maxExp;
        }
    }

    public int moveDistnace
    {
        set
        {
            if((value<0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_moveDistance = value;
        }

        get
        {
            return m_stat.m_moveDistance;
        }
    }
    public float moveSpeedCoef
    {
        set
        {
            if ((value < 0.0f) || (value > float.MaxValue))
            {
                return;
            }

            m_stat.m_moveSpeedCoef = value;
        }

        get
        {
            return m_stat.m_moveSpeedCoef;
        }
    }

    public float maxMoveSpeedCoef
    {
        set
        {
            if ((value < 0.0f) || (value > float.MaxValue))
            {
                return;
            }

            m_stat.m_maxMoveSpeedCoef = value;
        }

        get
        {
            return m_stat.m_maxMoveSpeedCoef;
        }
    }

    public int attackDamgage
    {
        set
        {
            if ((value < 0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_attackDamage = value;
        }

        get
        {
            return m_stat.m_attackDamage;
        }
    }

    public float attackSpeedCoef
    {
        set
        {
            if ((value < 0.0f) || (value > float.MaxValue))
            {
                return;
            }

            m_stat.m_attackSpeedCoef = value;
        }

        get
        {
            return m_stat.m_attackSpeedCoef;
        }
    }

    public float maxAttackSpeedCoef
    {
        set
        {
            if ((value < 0.0f) || (value > float.MaxValue))
            {
                return;
            }

            m_stat.m_maxAttackSpeedCoef = value;
        }

        get
        {
            return m_stat.m_maxAttackSpeedCoef;
        }
    }

    public float criticalPercent
    {
        set
        {
            if ((value < 0.0f) || (value > float.MaxValue))
            {
                return;
            }

            m_stat.m_criticalPercent = value;
        }

        get
        {
            return m_stat.m_criticalPercent;
        }
    }

    public int armor
    {
        set
        {
            if ((value < 0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_armor = value;
        }

        get
        {
            return m_stat.m_armor;
        }
    }
    
    public Vector2Int currentPos
    {
        set
        {
            m_stat.m_currentPos = value;
        }
        
        get
        {
            return m_stat.m_currentPos;
        }
    }
    
    public bool isAttack
    {
        get
        {
            return m_status.m_isAttack;
        }

        set
        {
            m_status.m_isAttack = value;
        }
    }

    public bool isDamaged
    {
        get
        {
            return m_status.m_isDamaged;
        }

        set
        {
            m_status.m_isDamaged = value;
        }
    }

    public Animator animator
    {
        get
        {
            return m_animator;
        }
    }

    public Collider collider
    {
        get
        {
            return m_collider;
        }
    }
    
    
    protected virtual void Awake()
    {
        m_animator = transform.GetComponent<Animator>();
        m_collider = transform.GetComponent<Collider>();
    }

    public virtual void Attack()
    {

    }

    public virtual void UseSkill(int skillNum)
    {

    }

    public virtual void Hit(float duration)
    {

    }

    protected IEnumerator HitCoroutine(float duration)
    {
        yield return null;
    }
}
