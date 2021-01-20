using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region members
    
    /*
     * Move settings for better inspector adjustments
     */
    [System.Serializable]
    public class MoveSettings
    {
        public float RunVelocity = 12f;
        public float RotateVelocity = 100f;
        public float JumpVelocity = 8f;
        public float DistanceToGround = 1.3f;
        public float WallSlideSpeed = 1f;
        public LayerMask Wall;
        public LayerMask Ground;
    }

    /*
     * Input Settings for better inspector adjustments
     */
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

    public Transform Bottom;
    public Transform Top;
    public Transform SpawnPoint;

    /*
     * Private Members of PlayerController
     */
    private Animator m_Animator;
    private Rigidbody m_RigidbodyPlayer;
    private Quaternion m_TargetRotation;
    private Vector3 m_Velocity;
    private float m_ForwardInput, m_SidewardsInput, m_TurnInput, m_JumpInput;
    private bool m_Sprint;
    private bool m_CanMove = true;
    private bool m_Grounded = true;
    private int m_Score = 0;
    private int m_DoubleJ = 1;
    private bool m_CanJump = true;
    private bool m_WallSlide = false;
    
    private static bool m_DoubleJumpAquired = true;

    public static bool DoubleJumpAquired
    {
        get => m_DoubleJumpAquired;
        set => m_DoubleJumpAquired = value;
    }

    public int Score
    {
        get => m_Score;
        set => m_Score = value;
    }

    public int DoubleJ
    {
        get => m_DoubleJ;
        set => m_DoubleJ = value;
    }

    #endregion
    
    
    /*
     * Initialize some Members on Awake
     */
    private void Awake()
    {
        m_Velocity = Vector3.zero;
        m_ForwardInput = m_SidewardsInput = m_JumpInput = m_TurnInput = 0f;
        m_TargetRotation = transform.rotation;
        m_RigidbodyPlayer = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Spawn();
    }

    private void Update()
    {
        //Get Input and save in members
        GetInput();
        
        //Set Grounded boolean
        Grounded();
        
        //Turn Player based on Mouse X
        Turn();
        
        //Animate Char according to Input
        Animate();
    }

    private void FixedUpdate()
    {
        //If Movement is disabled, do nothing
        if (!m_CanMove)
        {
            m_Velocity.z = 0f;
            m_Velocity.x = 0f;
            return;
        }
        
        //Apply Physic based Movements
        Run();
        WallSlide();
        Jump();
    }

    private void GetInput()
    {
        //If Movement disabled, set Input to 0
        if (!m_CanMove)
        {
            m_ForwardInput = 0f;
            m_SidewardsInput = 0f;
            m_TurnInput = 0f;
            m_JumpInput = 0f;
            m_Sprint = false;
            return;
        }

        if (!m_CanJump)
            m_CanJump = Input.GetButtonUp("Jump");
        
        //Get and Set Input Parameters
        if (InputSetting.Forward_Axis.Length != 0)
            m_ForwardInput = Input.GetAxis(InputSetting.Forward_Axis);
        if (InputSetting.Sideways_Axis.Length != 0)
            m_SidewardsInput = Input.GetAxis(InputSetting.Sideways_Axis);
        if (InputSetting.Turn_Axis.Length != 0)
            m_TurnInput = Input.GetAxis(InputSetting.Turn_Axis);
        if (InputSetting.Jump_Button.Length != 0 && m_CanJump)
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
        //If Player holds Ctrl, we sprint
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


    private void WallSlide()
    {
        if (m_Grounded)
            return;

        Collider[] collided = Physics.OverlapCapsule(Bottom.position, Top.position, .23f, MoveSetting.Wall);

        m_WallSlide = collided.Length > 0;
        
        if(!m_WallSlide)
        {
            Collider[] collidedGround = Physics.OverlapCapsule(Bottom.position, Top.position, .23f, MoveSetting.Ground);

            m_WallSlide = collidedGround.Length > 0;
        }

        if(m_WallSlide)
        {
            m_RigidbodyPlayer.velocity = new Vector3(0f, Mathf.Clamp(m_RigidbodyPlayer.velocity.y,-MoveSetting.WallSlideSpeed, float.MaxValue), 0f);
        }
    }

    /*
     * Turn Char based on Mouse X
     */
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
        if(!m_CanJump)
            return;
        
        //Only Jump if Input is there and Char is not grounded
        if (m_JumpInput != 0 && m_Grounded)
        {
            m_RigidbodyPlayer.velocity = new Vector3(m_RigidbodyPlayer.velocity.x, MoveSetting.JumpVelocity,
                m_RigidbodyPlayer.velocity.z);
            m_CanJump = false;
            m_JumpInput = 0f;
        }
        else if (m_DoubleJumpAquired && m_JumpInput != 0 && !m_Grounded && m_DoubleJ-- > 0)
        {
            m_RigidbodyPlayer.velocity = new Vector3(m_RigidbodyPlayer.velocity.x, 1.25f*MoveSetting.JumpVelocity,
                m_RigidbodyPlayer.velocity.z);
        }
    }

    /*
     * Set Animator Parameters according to Input
     */
    private void Animate()
    {
        if (m_Animator == null)
            return;

        float movement = Mathf.Max(Mathf.Abs(m_ForwardInput), Mathf.Abs(m_SidewardsInput));
        m_Animator.SetFloat("Movement", movement);
        m_Animator.SetBool("Run", m_Sprint);
        m_Animator.SetBool("Grounded", m_Grounded);
        if (m_DoubleJ == 0)
        {
            m_Animator.SetTrigger("DoubleJ");
            m_DoubleJ--;
        }
    }
    
    private void Grounded()
    {
        m_Grounded = Physics.Raycast(transform.position, Vector3.down, MoveSetting.DistanceToGround, MoveSetting.Ground);
    }

    /*
     * Player cannot move, if Movement is disabled
     */
    public void DisableMovement()
    {
        m_CanMove = false;
    }

    /*
     * Player can move, if Movement is enabled
     */
    public void EnableMovement()
    {
        m_CanMove = true;
    }

    public void CollectCoin()
    {
        m_Score++;
    }
    
    /*
     * Spawn Player on current Spawnpoint
     */
    public void Spawn()
    {
        transform.position = SpawnPoint.position;
    }

    /*
     * Play Sounds according on what Character collides
     */
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Trampo"))
        {
            FindObjectOfType<Turntable>().PlaySound("Trampo");
        }
    }

    /*
     * Draw the Raycast Line, sent by Grounded()
     */
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * MoveSetting.DistanceToGround);
    }
}
