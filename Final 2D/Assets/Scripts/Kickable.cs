using UnityEngine;

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare
}

public class Kickable : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _explosionVfxPrefab;
    [SerializeField] private ItemRarity _rarity = ItemRarity.Common;

    private bool _wasKicked;
    private GameController _gameController;

    // ------------------------------------------------------------------------
    // You don't need to code in this method :)
    private void Start ()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        _gameController = GameObject.FindFirstObjectByType<GameController>();
    }

    // ------------------------------------------------------------------------
    public void Kick(Vector3 force)
    {
        if (_wasKicked || _rigidbody == null)
        {
            return;
        }

        _wasKicked = true;
        _rigidbody.AddForce(force, ForceMode2D.Impulse);

        if (_gameController != null)
        {
            _gameController.Kicked(gameObject, _rarity);
        }
    }

    // ------------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_wasKicked)
        {
            Explode();
        }
    }

    // ------------------------------------------------------------------------
    private void Explode()
    {
        if (_explosionVfxPrefab != null)
        {
            Instantiate(_explosionVfxPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
