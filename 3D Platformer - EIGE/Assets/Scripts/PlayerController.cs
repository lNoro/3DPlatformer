using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region members
    
    [System.Serializable]
    public class MoveSettings
    {
        public float RunVelocity = 12f;
        public float RotateVelocity = 100f;
        public float JumpVelocity = 8f;
        public float DistanceToGround = 1.3f;
        public LayerMask Ground;
    }

    [System.Serializable]
    public class InputSettings
    {
        public String Forward_Axis = "Vertical";
        public String Sideways_Axis = "Horizontal";
        public String Turn_Axis = "Mouse X";
        public String Jump_Button = "Jump";
        public KeyCode Sprint_Button = KeyCode.LeftControl;
    }

    public MoveSettings MoveSetting;
    public InputSettings InputSetting;

    private Animator m_Animator;
    private Rigidbody m_RigidbodyPlayer;
    private Quaternion m_TargetRotation;
    private Vector3 m_Velocity;
    private float m_ForwardInput, m_SidewardsInput, m_TurnInput, m_JumpInput;
    private bool m_Sprint;
    private bool m_CanMove = true;
    private bool m_Grounded = true;

    private int m_Score = 0;

    public int Score
    {
        get => m_Score;
        set => m_Score = value;
    }

    #endregion
    
    
    private void Awake()
    {
        m_Velocity = Vector3.zero;
        m_ForwardInput = m_SidewardsInput = m_JumpInput = m_TurnInput = 0f;
        m_TargetRotation = transform.rotation;
        m_RigidbodyPlayer = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GetInput();
        Grounded();
        Turn();
        Animate();
    }

    private void FixedUpdate()
    {
        if (!m_CanMove)
        {
            m_Velocity.z = 0f;
            m_Velocity.x = 0f;
            return;
        }
        
        Run();
        Jump();
    }

    private void GetInput()
    {
        if (!m_CanMove)
        {
            m_ForwardInput = 0f;
            m_SidewardsInput = 0f;
            m_TurnInput = 0f;
            m_JumpInput = 0f;
            m_Sprint = false;
            return;
        }
        
        if (InputSetting.Forward_Axis.Length != 0)
            m_ForwardInput = Input.GetAxis(InputSetting.Forward_Axis);
        if (InputSetting.Sideways_Axis.Length != 0)
            m_SidewardsInput = Input.GetAxis(InputSetting.Sideways_Axis);
        if (InputSetting.Turn_Axis.Length != 0)
            m_TurnInput = Input.GetAxis(InputSetting.Turn_Axis);
        if (InputSetting.Jump_Button.Length != 0)
            m_JumpInput = Input.GetAxisRaw(InputSetting.Jump_Button); 
        if (m_Sprint)
        {
            m_Sprint = !Input.GetKeyUp(InputSetting.Sprint_Button);
        }
        else
        {
            m_Sprint = Input.GetKeyDown(InputSetting.Sprint_Button);
        }
    }

    private void Run()
    {
        float speed = MoveSetting.RunVelocity;
        if (m_Sprint)
        {
            speed *= 1.5f;
        }
        
        m_Velocity.z = m_ForwardInput * speed;
        m_Velocity.y = m_RigidbodyPlayer.velocity.y;
        m_Velocity.x = m_SidewardsInput * MoveSetting.RunVelocity;

        m_RigidbodyPlayer.velocity = transform.TransformDirection(m_Velocity);
    }

    private void Turn()
    {
        if (Mathf.Abs(m_TurnInput) > 0)
        {
            m_TargetRotation *= Quaternion.AngleAxis(MoveSetting.RotateVelocity * m_TurnInput * Time.deltaTime, Vector3.up);
            transform.rotation = m_TargetRotation;
        }
    }

    private void Jump()
    {
        //Start Physics after Timer
        if (m_JumpInput != 0 && m_Grounded)
        {
            m_RigidbodyPlayer.velocity = new Vector3(m_RigidbodyPlayer.velocity.x, MoveSetting.JumpVelocity,
                m_RigidbodyPlayer.velocity.z);
        }
    }

    private void Animate()
    {
        if (m_Animator == null)
            return;

        float movement = Mathf.Max(Mathf.Abs(m_ForwardInput), Mathf.Abs(m_SidewardsInput));
        m_Animator.SetFloat("Movement", movement);
        m_Animator.SetBool("Run", m_Sprint);
        m_Animator.SetBool("Grounded", m_Grounded);
    }
    
    private void Grounded()
    {
        m_Grounded = Physics.Raycast(transform.position, Vector3.down, MoveSetting.DistanceToGround, MoveSetting.Ground);
    }

    public void DisableMovement()
    {
        m_CanMove = false;
    }

    public void EnableMovement()
    {
        m_CanMove = true;
    }

    public void CollectCoin()
    {
        m_Score++;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Trampo"))
        {
            FindObjectOfType<Turntable>().PlaySound("Trampo");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * MoveSetting.DistanceToGround);
    }
}
