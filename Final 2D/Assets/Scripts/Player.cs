using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _kickLeft;
    [SerializeField] private GameObject _kickRight;
    [SerializeField] private GameController _gameController;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _jump = 8f;

    private bool _facingLeft;
    private bool _isGrounded;

    private void Awake()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    // ------------------------------------------------------------------------
    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _rigidbody.linearVelocity = new Vector2(
            horizontalInput * _speed,
            _rigidbody.linearVelocity.y
        );

        if (horizontalInput != 0f)
        {
            _facingLeft = horizontalInput < 0f;
            _spriteRenderer.flipX = _facingLeft;
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            && _isGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jump);
            _isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Kick();
        }

        SetAnimatorBool("walking", horizontalInput != 0f && _isGrounded);
        SetAnimatorBool("jumping", !_isGrounded);
    }

    // ------------------------------------------------------------------------
    private void Kick()
    {
        SetAnimatorBool("kicking", true);
        CancelInvoke(nameof(StopKicking));
        Invoke(nameof(StopKicking), 0.5f);

        GameObject kickObject = _facingLeft ? _kickLeft : _kickRight;
        if (kickObject != null)
        {
            kickObject.SetActive(true);
        }
    }

    private void StopKicking()
    {
        SetAnimatorBool("kicking", false);
    }

    // ------------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    private void SetAnimatorBool(string parameterName, bool value)
    {
        if (_animator == null)
        {
            return;
        }

        foreach (AnimatorControllerParameter parameter in _animator.parameters)
        {
            if (parameter.name == parameterName && parameter.type == AnimatorControllerParameterType.Bool)
            {
                _animator.SetBool(parameterName, value);
                return;
            }
        }
    }
}
