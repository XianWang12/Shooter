using UnityEngine;

public class Entity : MonoBehaviour
{
    public LayerMask groundLayer;

    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    { 
    }

    protected virtual void FixedUpdate()
    {
    }

    public virtual void Die()
    {
    }
}
