
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Lives { get; private set; }

    public event Action<int> OnLivesChanged;    //registered in UILivesText, UILivesImage
    public event Action<int> OnCoinsChanged;    //registered in UICoinsText, UICoinsImage, AudioCoin

    private int _coins;

    private int _currentLevelIndex;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            RestartGame();
        }
    }

    internal void KillPlayer(PlayerMovementController playerMovementController)
    {
        Lives--;
        if (OnLivesChanged != null)
            OnLivesChanged(Lives);

        if (Lives <= 0)
            RestartGame();
        else
            SendPlayerToCheckpoint(playerMovementController);
    }

    private void SendPlayerToCheckpoint(PlayerMovementController playerMovementController)
    {
        if (playerMovementController == null) return;

        var checkpointManager = FindObjectOfType<CheckpointManager>();
        var checkpoint = checkpointManager.GetPlayerCheckpoint();

        if (checkpoint != null)
            playerMovementController.transform.position = checkpoint.transform.position;
        else
            playerMovementController.ResetPosition();
    }

    internal void AddCoin()
    {
        _coins++;
        if (OnCoinsChanged != null)
            OnCoinsChanged(_coins);
    }

    public void MoveToNextLevel()
    {
        _currentLevelIndex++;
        SceneManager.LoadScene(_currentLevelIndex);
    }

    private void RestartGame()
    {
        _currentLevelIndex = 0;

        Lives = 3;
        _coins = 0;
        if (OnCoinsChanged != null)
            OnCoinsChanged(_coins);

        SceneManager.LoadScene(_currentLevelIndex);
    }
}
