using UnityEngine;
using UnityEngine.UI;

public class ChecklistItemUI : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _checkImage;
    [SerializeField] private Sprite _filledCheckSprite;

    [SerializeField] private GameObject _requiredItem;
    public GameObject RequiredItem => _requiredItem;

    // ------------------------------------------------------------------------
    public void SetItem (GameObject itemObject)
    {
        _requiredItem = itemObject;

        SpriteRenderer itemRenderer = itemObject.GetComponent<SpriteRenderer>();
        if (_itemImage != null && itemRenderer != null)
        {
            _itemImage.sprite = itemRenderer.sprite;
        }

    }

    // ------------------------------------------------------------------------
    public void FoundItem ()
    {
        if (_checkImage != null && _filledCheckSprite != null)
        {
            _checkImage.sprite = _filledCheckSprite;
        }
    }

}
