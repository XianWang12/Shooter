using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    public PlayerStats stats { get; private set; }
    public PlayerBuffController buffs { get; private set; }

    [Header("Move info")]
    public float moveSpeed;
    public float dashSpeed;
    public Vector3 moveDir;

    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stats = GetComponent<PlayerStats>();
        buffs = GetComponent<PlayerBuffController>();
        stateMachine = new PlayerStateMachine();

        idleState=new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        deadState = new PlayerDeadState(this, stateMachine, "Dead");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        if (Time.timeScale == 0)
            return;

        if (stats.isDead)
            return;

        base.Update();
        stateMachine.currentState.Update();
        
        UseSkillCheck();
    }

    protected override void FixedUpdate()
    {
        if (stats.isDead) 
            return;
        
        base.FixedUpdate();
        stateMachine.currentState.FixedUpdate();
        
        Rotate();
    }

    private void UseSkillCheck()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&SkillManager.instance.bait.CanUse())
            SkillManager.instance.bait.Use();

        if(Input.GetKeyDown(KeyCode.LeftShift)&&SkillManager.instance.flash.CanUse())
            SkillManager.instance.flash.Use();

        if(Input.GetKeyDown(KeyCode.F)&&SkillManager.instance.turret.CanUse())
            SkillManager.instance.turret.Use();

        if(Input.GetKeyDown(KeyCode.Q)&&SkillManager.instance.landmine.CanUse())
            SkillManager.instance.landmine.Use();
    }

    private void Rotate()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit groundHit;

        if (Physics.Raycast(cameraRay, out groundHit, 120, groundLayer))
        {
            Vector3 targetPosition = groundHit.point;
            targetPosition.y = transform.position.y;
            Vector3 directionToTarget = (targetPosition - transform.position).normalized;
            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f));
            }
        }
    }

    public override void Die()
    {
        base.Die();
        buffs?.ClearAllBuffs();
        stateMachine.ChangeState(deadState);
    }
}
