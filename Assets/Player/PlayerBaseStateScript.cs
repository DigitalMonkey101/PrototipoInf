using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class PlayerBaseStateScript 
{
    public abstract void EnterState(PlayerStateManagerScript player);
    public abstract void UpdateState(PlayerStateManagerScript player);
    public abstract void OnCollisionEnter2D(PlayerStateManagerScript player);


   
}















public class PlayerIddleState : PlayerBaseStateScript
{
    public override void EnterState(PlayerStateManagerScript player)
    {

    }
    public override void UpdateState(PlayerStateManagerScript player)
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            player.SwitchState(player.jumpingState);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0)
        {
            player.SwitchState(player.movingState);
        }
    }
    public override void OnCollisionEnter2D(PlayerStateManagerScript player)
    {

    }
}












public class PlayerMovingState : PlayerBaseStateScript
{
    Rigidbody2D rb;
    float direction;
    float moveSpeed;
    public override void EnterState(PlayerStateManagerScript player)
    {
        moveSpeed = 7f;
            rb = player.GetComponent<Rigidbody2D>();
    }
    public override void UpdateState(PlayerStateManagerScript player)
    {
        direction = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(direction * moveSpeed,rb.velocity.y);
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            player.SwitchState(player.jumpingState);
        }
        if (rb.velocity.magnitude == 0)
        {
            player.SwitchState(player.iddleState);
        }
    }
    public override void OnCollisionEnter2D(PlayerStateManagerScript player)
    {

    }
}











public class PlayerJumpingState : PlayerBaseStateScript
{
    Rigidbody2D rb;
    float jumpForce = 15f;
    float direction;
    float moveSpeed;
 
    public override void EnterState(PlayerStateManagerScript player)
    {
        moveSpeed = 7f;
       rb = player.GetComponent<Rigidbody2D>();
       rb.velocity = Vector2.up * jumpForce;
    
    }
    public override void UpdateState(PlayerStateManagerScript player)
    {

        direction = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.SwitchState(player.rollingState);
        }
        //check and return to other state if not jumping 
        if (rb.velocity.y  == 0f)
        {
            if (rb.velocity.x == 0f)
            {
            player.SwitchState(player.iddleState);
            }
            else
            {
            player.SwitchState(player.movingState);
            }
        } 
        else if (rb.velocity.y < 0f)
        {
            player.SwitchState(player.fallingState);
        }
    }
    public override void OnCollisionEnter2D(PlayerStateManagerScript player)
    {

    }
}
public class PlayerAttackState : PlayerBaseStateScript
{
    public override void EnterState(PlayerStateManagerScript player)
    {

    }
    public override void UpdateState(PlayerStateManagerScript player)
    {

    }
    public override void OnCollisionEnter2D(PlayerStateManagerScript player)
    {

    }
}
public class PlayerRollState : PlayerBaseStateScript
{
    float direction;
    float dashForce;
    float dashDuration;
    public bool isDashing;

    Rigidbody2D rb;
    public override void EnterState(PlayerStateManagerScript player)
    {
        Debug.Log("enterRoll");
        rb = player.GetComponent<Rigidbody2D>();
        dashForce = 30f;
        dashDuration = 1f;
        direction = Input.GetAxisRaw("Horizontal");
        isDashing = true;
        player.StartCoroutine(player.RollCoroutine(this,direction,dashForce,dashDuration,rb,isDashing));
        Debug.Log(player.currentState);
    }
    public override void UpdateState(PlayerStateManagerScript player)
    {
        Debug.Log(isDashing);
        if (!isDashing)
        {
            if (rb.velocity.x == 0f)
            {
                player.SwitchState(player.iddleState);
            }
            else
            {
                player.SwitchState(player.movingState);
            }
        }

    }
    public override void OnCollisionEnter2D(PlayerStateManagerScript player)
    {

    }
    public void EndRoll()
    {
        isDashing = false;
    }
}
public class PlayerFallState : PlayerBaseStateScript
{
    Rigidbody2D rb;
    public override void EnterState(PlayerStateManagerScript player)
    {
        rb = player.GetComponent<Rigidbody2D>();
    }
    public override void UpdateState(PlayerStateManagerScript player)
    {
        if(rb.velocity.y == 0f)
        {
            if(rb.velocity.x != 0f)
            {
            player.SwitchState(player.movingState);
            }
            else if(rb.velocity.x == 0f)
            {
                player.SwitchState(player.iddleState);
            }
        }
    }
    public override void OnCollisionEnter2D(PlayerStateManagerScript player)
    {

    }
}