using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private HUD _hud;
    [SerializeField] private StartGameUI _startGameUI;
    [SerializeField] private GameOverUI _gameOverUI;
    [SerializeField] private Player _player;
    [SerializeField] private float _maxTimeSeconds;
    // Add a member variable that's a LIST (or ARRAY) of all of the objects
    //      that the player is required to kick.
    // This variable should be editable from the inspector.

    private readonly List<GameObject> _requiredItems = new List<GameObject>();
    private int _commonPoints;
    private int _uncommonPoints;
    private int _rarePoints;
    private float _timeRemaining;
    private bool _runGame;

    // ------------------------------------------------------------------------
    private void Start()
    {
        SetGameEnabled(false);

        Kickable[] kickableItems = FindObjectsByType<Kickable>(FindObjectsSortMode.None);
        foreach (Kickable kickableItem in kickableItems)
        {
            _requiredItems.Add(kickableItem.gameObject);
        }

        _hud.SetRequiredItems(_requiredItems);
    }

    // ------------------------------------------------------------------------
    // You don't need to code in this method :)
    private void Update()
    {
        if (_runGame)
        {
            _timeRemaining -= Time.deltaTime;
            _hud.UpdateTimerText(_timeRemaining);

            if (_timeRemaining <= 0)
            {
                EndGame();
            }
        }
    }

    // ------------------------------------------------------------------------
    // You don't need to code in this method :)
    public void StartGame()
    {
        _startGameUI.gameObject.SetActive(false);
        _gameOverUI.gameObject.SetActive(false);

        SetGameEnabled(true);

        _commonPoints = 0;
        _uncommonPoints = 0;
        _rarePoints = 0;
        _hud.UpdatePointsText(_commonPoints, _uncommonPoints, _rarePoints);

        _timeRemaining = _maxTimeSeconds;
        _hud.UpdateTimerText(_timeRemaining);
    }

    // ------------------------------------------------------------------------
    // You don't need to code in this method :)
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    // ------------------------------------------------------------------------
    private void EndGame()
    {
        SetGameEnabled(false);

        // Calculate the player's total points.

        // Tell the GameOverUI.
    }

    // ------------------------------------------------------------------------
    // You don't need to code in this method :)
    private void SetGameEnabled(bool enabled)
    {
        _player.enabled = enabled;
        _runGame = enabled;
    }

    // ------------------------------------------------------------------------
    public void Kicked(GameObject kickedObject, ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Common:
                _commonPoints++;
                break;
            case ItemRarity.Uncommon:
                _uncommonPoints++;
                break;
            case ItemRarity.Rare:
                _rarePoints++;
                break;
        }

        _hud.KickedItem(kickedObject);
        _hud.UpdatePointsText(_commonPoints, _uncommonPoints, _rarePoints);
    }
}
