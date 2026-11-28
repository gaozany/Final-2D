using UnityEngine;

public class KickAttack : MonoBehaviour
{
    [SerializeField] public float _lifetime = 0.1f;
    [SerializeField] private Vector3 _kickAmount = Vector3.up;

    private float _timer;
    private Collider2D _hitbox;

    private void Awake()
    {
        _hitbox = GetComponent<Collider2D>();
    }

    // ------------------------------------------------------------------------
    private void OnEnable()
    {
        _timer = _lifetime;

        // Detect immediately when the short-lived kick object is activated.
        // This is more reliable than waiting for a physics trigger callback.
        Physics2D.SyncTransforms();
        if (_hitbox == null)
        {
            _hitbox = GetComponent<Collider2D>();
        }

        if (_hitbox != null)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(
                _hitbox.bounds.center,
                _hitbox.bounds.size,
                transform.eulerAngles.z
            );

            foreach (Collider2D hit in hits)
            {
                TryKick(hit);
            }
        }
    }

    // ------------------------------------------------------------------------
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    // ------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        TryKick(other);
    }

    private void TryKick(Collider2D other)
    {
        Kickable kickable = other.GetComponentInParent<Kickable>();
        if (kickable != null)
        {
            kickable.Kick(_kickAmount);
        }
    }
}
