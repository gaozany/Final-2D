using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private Transform _checklistParent;
    [SerializeField] private ChecklistItemUI _checklistItemPrefab;
    [SerializeField] private TMP_Text _timerText;
    // Add member variables for all of the Text you'll need to update.

    private List<ChecklistItemUI> _checklistItems;

    private void Awake()
    {
        if (_timerText == null)
        {
            Transform timerObject = transform.Find("Timer");
            if (timerObject != null)
            {
                _timerText = timerObject.GetComponentInChildren<TMP_Text>(true);
            }
        }
    }

    // ------------------------------------------------------------------------
    public void SetRequiredItems (List<GameObject> items)
    {
        _checklistItems = new List<ChecklistItemUI>();
        _checklistItems.AddRange(_checklistParent.GetComponentsInChildren<ChecklistItemUI>(true));

        for (int i = 0; i < items.Count; i++)
        {
            ChecklistItemUI checklistItem;
            if (i < _checklistItems.Count)
            {
                checklistItem = _checklistItems[i];
            }
            else if (_checklistItemPrefab != null)
            {
                checklistItem = Instantiate(_checklistItemPrefab, _checklistParent);
                _checklistItems.Add(checklistItem);
            }
            else
            {
                break;
            }

            checklistItem.SetItem(items[i]);
        }
    }

    // ------------------------------------------------------------------------
    public void KickedItem(GameObject item)
    {
        if (_checklistItems == null)
        {
            _checklistItems = new List<ChecklistItemUI>(
                _checklistParent.GetComponentsInChildren<ChecklistItemUI>(true)
            );
        }

        foreach (ChecklistItemUI checklistItem in _checklistItems)
        {
            if (checklistItem.RequiredItem == item)
            {
                checklistItem.FoundItem();
                return;
            }
        }
    }

    // ------------------------------------------------------------------------
    public void UpdatePointsText (int commonPoints, int uncommonPoints, int rarePoints)
    {
        // Update all of the text.
    }

    // ------------------------------------------------------------------------
    public void UpdateTimerText (float timeLeft)
    {
        if (_timerText != null)
        {
            _timerText.text = Mathf.Max(0f, timeLeft).ToString("00.00");
        }
    }
}
