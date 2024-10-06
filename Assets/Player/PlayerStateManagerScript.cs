using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManagerScript : MonoBehaviour
{
    public PlayerBaseStateScript currentState;
    public PlayerIddleState iddleState = new PlayerIddleState();
    public PlayerMovingState movingState = new PlayerMovingState();
    public PlayerJumpingState jumpingState = new PlayerJumpingState();
    public PlayerFallState fallingState = new PlayerFallState();
    public PlayerAttackState attackingState = new PlayerAttackState();
    public PlayerRollState rollingState = new PlayerRollState();

    public void Start()
    {
        currentState = iddleState;
        currentState.EnterState(this);
    }
    public void Update()
    {

        currentState.UpdateState(this);
        Debug.Log(currentState.ToString());
    }
    public void SwitchState(PlayerBaseStateScript state)
    {
        currentState = state;
        Debug.Log($"Changing state to {state.ToString()}.");
        currentState.EnterState(this);
       
    }

    public IEnumerator RollCoroutine(PlayerRollState rollingState,float direction, float dashForce, float dashDuration,Rigidbody2D rb,bool isDashing)
    {
        Debug.Log(currentState.ToString() + "INSIDE COROUTINE STATE");
        dashDuration = 1f;
        Debug.Log("hola");
        Vector2 velocity = rb.velocity;
        float gravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(direction, 0f) * dashForce;
        yield return new WaitForSeconds(dashDuration);
        Debug.Log(isDashing + "IS DASHING");
        Debug.Log(currentState.ToString() + "INSIDE COROUTINE STATE 2");
        rb.gravityScale = gravity;
        rollingState.EndRoll();
    }

}
