using System;
using UnityEngine;


public class PlayerControls : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator playerAnimator;
    public float playerSpeed = 1f;
    public float playerJumpSpeed = 1f, jumpFrequency = 1f, nextJumpTime;  
    
    private bool _facingRight = true;
    
    public bool isGrounded = false;
    public Transform groundCheckPos;
    public float groundCheckRad;
    public LayerMask groundCheckLayer;
    
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
        OnGroundCheck();
        
        if (_facingRight && playerRB.velocity.x < 0)
        {
            TurnAround();
        }
        
        else if (!_facingRight && playerRB.velocity.x > 0)
        {
            TurnAround();
        }

        if (Input.GetAxis("Vertical") > 0 && isGrounded && (nextJumpTime < Time.timeSinceLevelLoad))
        {
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency;
            Jump();
        }
    }

    

    void TurnAround()
    {
        _facingRight = !_facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x = -tempLocalScale.x;
        transform.localScale = tempLocalScale;

    }

    void MoveHorizontal()
    {
        playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * playerSpeed, playerRB.velocity.y);
        playerAnimator.SetFloat("playerSpeed", Math.Abs(playerRB.velocity.x));
    }
    
    void Jump()
    {
        playerRB.AddForce(new Vector2(0f, playerJumpSpeed));
    }

    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRad, groundCheckLayer);
        playerAnimator.SetBool("isGrounded", isGrounded);
    }
}
