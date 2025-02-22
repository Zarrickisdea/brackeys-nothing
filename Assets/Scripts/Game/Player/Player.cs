using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ISubject
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpBufferTime = 0.1f;

    private PlayerStateMachine stateMachine;
    private Rigidbody2D body;
    private float jumpBufferCounter;

    public float MoveSpeed => moveSpeed;
    public float JumpHeight => jumpHeight;
    public float JumpBufferTime => jumpBufferTime;
    public Rigidbody2D Body => body;
    public bool CanJump { get; set; }
    public bool CanDoubleJump { get; set; }
    public bool IsJumping { get; set; }
    public bool IsCrawling { get; set; }

    private List<IObserver> observers = new List<IObserver>();
    public AudioClip wispCollisionSound;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        stateMachine = new PlayerStateMachine(this);
        stateMachine.SetState<PlayerIdleState>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        stateMachine.UpdateState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            CanJump = true;
            IsJumping = false;

            if (jumpBufferCounter > 0)
            {
                SetState<PlayerJumpState>();
                jumpBufferCounter = 0;
            }
            else if (stateMachine.CurrentState is PlayerJumpState)
            {
                SetState<PlayerIdleState>();
            }
        }

        if (collision.transform.CompareTag("Wisp"))
        {
            NotifyObservers("VariedEffect", wispCollisionSound);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            CanJump = false;
        }
    }

    public void SetState<T>() where T : PlayerBaseState
    {
        stateMachine.SetState<T>();
    }

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers(string eventType, object data)
    {
        foreach (var observer in observers)
        {
            observer.OnNotify(eventType, data);
        }
    }
}