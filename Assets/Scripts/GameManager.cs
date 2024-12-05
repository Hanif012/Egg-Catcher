using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Score = 0;
    public int Total = -1;

    private enum GameState
    {
        MainMenu,
        InGame,
        GameOver,
        Pause,
        Win
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Your update logic here
    }
}
