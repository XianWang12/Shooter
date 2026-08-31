using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected float xInput;
    protected float yInput;
    protected float stateTimer;
    protected string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        stateTimer = 0f;
        player.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        stateTimer += Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
}
