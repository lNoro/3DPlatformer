using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float ChaseSpeed;
    public float Speed;
    public float RotationSpeed;
    public GameObject Prey;
    public EnemyBehaviour Behaviour;
    public List<WayPoint> WayPoints;
    public float DistanceThreshold;
    public float AttackRange;
    public GameObject UnlockDoor;
    public GameObject HealthBar;

    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private float m_Movement;
    private bool m_Dead;
    private int m_CurrentWayPoint;

    public enum EnemyBehaviour
    {
        LineOfSight,
        Intercept,
        PatternMovement,
        ChasePatternMovement,
        Hide,
        Attack
    }
    
    
    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Turn(Vector3 direction_p)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction_p);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed);
    }
    
    private void FixedUpdate()
    {
        switch (Behaviour)
        {
            case EnemyBehaviour.LineOfSight:
                ChaseLineOfSight(Prey.transform.position, ChaseSpeed);
                Attack(Prey.transform.position);
                break;
            
            case EnemyBehaviour.Intercept:
                Intercept(Prey.transform.position);
                break;
            
            case EnemyBehaviour.PatternMovement:
                break;
            
            case EnemyBehaviour.ChasePatternMovement:
                PatternMovement();
                break;
            
            case EnemyBehaviour.Hide:
                break;
            
            case EnemyBehaviour.Attack:
                Attack(Prey.transform.position);
                break;
            
            default:
                break;
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        if (PlayerVisible(Prey.transform.position))
        {
            Behaviour = EnemyBehaviour.LineOfSight; 
        }
        else
        {
            Behaviour = EnemyBehaviour.ChasePatternMovement;
        }
    }

    
    bool PlayerInRange()
    {
        return false;
    }

    
    private void ChaseLineOfSight(Vector3 targetPosition_p, float speed_p)
    {
        m_Animator.SetBool("FightMode", true);
        Vector3 direction = targetPosition_p - transform.position;
        direction.Normalize();
        Turn(direction);
        
        m_Rigidbody.velocity = new Vector3(direction.x * speed_p, m_Rigidbody.velocity.y, direction.z * speed_p);
        m_Animator.SetFloat("Movement", Speed);
    }
    
    
    private void Intercept(Vector3 targetPosition_p)
    {
        Vector3 enemyPosition = transform.position;
        Vector3 preyPosition = Prey.transform.position;
        Vector3 velocityRelative = Prey.GetComponent<Rigidbody>().velocity - m_Rigidbody.velocity;
        Vector3 distance = targetPosition_p - enemyPosition;
        
        float timeToClose = distance.magnitude / velocityRelative.magnitude;
        
        Vector3 predictedInterceptionPoint = targetPosition_p + (timeToClose * Prey.GetComponent<Rigidbody>().velocity );
        
        Vector3 direction = predictedInterceptionPoint - enemyPosition;
        direction.Normalize();
        m_Rigidbody.velocity = new Vector3(direction.x * ChaseSpeed, m_Rigidbody.velocity.y, direction.z * ChaseSpeed); 
    }
    
    
    private void PatternMovement()
    {
        ChaseLineOfSight(WayPoints[m_CurrentWayPoint].transform.position, Speed);
        
        if(Vector3.Distance(transform.position, WayPoints[m_CurrentWayPoint].transform.position) < DistanceThreshold)
        {
            m_CurrentWayPoint = (m_CurrentWayPoint + 1) % WayPoints.Count; //modulo to restart at the beginning.
        }
    }

    
    private bool PlayerVisible(Vector3 targetPosition_p)
    {
        Vector3 directionToTarget = targetPosition_p - transform.position;
        directionToTarget.Normalize();
        RaycastHit hit;
        Physics.Raycast(gameObject.transform.position, directionToTarget, out hit);
        if (hit.collider == null)
        {
            return false;
        }
        return hit.collider.gameObject.tag.Equals("Player");
    }

    private void Attack(Vector3 targetPosition_p)
    {
        if(Vector3.Distance(transform.position, targetPosition_p) <= AttackRange)
        {
            m_Animator.SetTrigger("Attack");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            m_Animator.SetTrigger("Dead");
            FindObjectOfType<Turntable>().PlaySound("SlimeDie");
            HealthBar.GetComponentInChildren<Healthbar>().Die();
            Behaviour = EnemyBehaviour.Hide;
            UnlockDoor.GetComponent<Gate>().Disabled = false;
            enabled = false;
        }
    }
}
