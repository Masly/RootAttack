using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Video;

public enum GameStatus
{
    titleScreen,
    gameplayScreen,
    pauseScreen,
    victoryScreen,
    creditsScreen,
}

public enum GameMode
{
    timerMode,
    pointsMode
}

public class GameManager : MonoBehaviour
{

    public static GameManager i;

    [Header("Gameplay")]
    [HideInInspector] public GameStatus gameStatus;
    public int scoreToWin;
    public int initialScore;
    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;
    public GameObject player1;
    public GameObject player2;

    public GameObject crown;
    public GameMode gameMode;

    [HideInInspector] public int playersAmount;

    [Header("UI")]
    public EventSystem eventSystem;

    public GameObject titleCanvas;
    public GameObject gameplayCanvas;
    public GameObject victoryCanvas;

    public GameObject startGameButton;

    public GameObject pausedScreen;
    public GameObject resumeFromPauseButton;
    public GameObject gameplayScreen;
    public TextMeshProUGUI timerText;
    public Countdown countdown;
    private float timer;
    [SerializeField] private float matchTime;


    [Header("Player Gameplay UI")]
    public GameObject player1ScoreBox;
    public GameObject player2ScoreBox;

    [Header("Player Winner UI")]
    public GameObject player1Winner;
    public GameObject player2Winner;

    public GameObject player1WinnerMenuButton;
    public GameObject player2WinnerMenuButton;

    public BlurFXManager blurFXManager;
    private bool isSlowingDown;

    // For pausing
    private IList<Player> _playerControls;

    private void Awake()
    {
        i = this;
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        HideCanvases();
        ShowTitleScreen();

        // Needed for matchTime
        timer = 0;
        isSlowingDown = false;
    }

    private void HideCanvases()
    {
        titleCanvas.SetActive(false);
        gameplayCanvas.SetActive(false);
        victoryCanvas.SetActive(false);
        pausedScreen.SetActive(false);
    }

    public void ShowTitleScreen()
    {
        Time.timeScale = 1;

        HideCanvases();
        titleCanvas.SetActive(true);
        eventSystem.SetSelectedGameObject(startGameButton);
        gameStatus = GameStatus.titleScreen;

        // Stop victory theme and start menu theme
        AudioManager.i.PlayAudioSource(AudioManager.i.Victory, 0);
        //blurFXManager.ShowBlurEffect();

    }


    public void StartGameplay()
    {
        blurFXManager.HideBlurEffect();
        AudioManager.i.StartFightAudio();
        Time.timeScale = 1;
        HideCanvases();
        gameplayCanvas.SetActive(true);
        gameStatus = GameStatus.gameplayScreen;
        UpdateScoreUI();
        isSlowingDown = false;
        //SpawnerManager.i.SpawnFirstEnemy();
    }


    public void SetupPlayers(int newPlayersAmount)
    {
        playersAmount = newPlayersAmount;

        player1ScoreBox.SetActive(false);
        player2ScoreBox.SetActive(false);

        player1.SetActive(false);
        player2.SetActive(false);

        if (playersAmount >= 2)
        {
            scorePlayer1 = initialScore;
            scorePlayer2 = initialScore;

            player1ScoreBox.SetActive(true);
            player1.SetActive(true);

            player2ScoreBox.SetActive(true);
            player2.SetActive(true);

        }

        StartGameplay();
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            VictoryScreen(1);
        }

        if (gameStatus == GameStatus.gameplayScreen || gameStatus == GameStatus.pauseScreen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }

            if (gameStatus == GameStatus.gameplayScreen)
            {
                AdvanceTime();
            }
        }
    }

    private void AdvanceTime()
    {
        timer += Time.deltaTime;

        int timeLeft = (int)Mathf.Ceil(matchTime - timer);

        if (timeLeft > 3)
            timerText.text = timeLeft + "";
        else if (timeLeft == 3)
        {
            timerText.text = "";
            countdown.StartCountdown();
        }

        if (timer >= matchTime && gameMode == GameMode.timerMode)
        {
            // Check who has the most collectables and declare a winner
            //timerText.text = "Time out!";

            int winnerIndex = GetWinningPlayer();
            VictoryScreen(winnerIndex);
        }
        else
        {
            int winningPlayerIndex = GetWinningPlayer();
            if (winningPlayerIndex >= 0 && winningPlayerIndex <= 3)
            {
                GameObject playerWinning = GetPlayerObjectFromIndex(winningPlayerIndex);
                Vector3 crownPos = playerWinning.transform.position;
                crownPos.y += 2f;
                crown.transform.position = crownPos;
                crown.gameObject.SetActive(true);
            }
            else
            {
                crown.gameObject.SetActive(false);
            }

        }
    }

    public int GetWinningPlayer()
    {
        int retval = -1;
        if (scorePlayer1 > scorePlayer2)
        {
            retval = 0;
        }
        else 
        {
            retval = 1;
        }
        return retval;
    }

    public GameObject GetPlayerObjectFromIndex(int playerIndex)
    {
            if (playerIndex == 0)
                return player1;
            else if (playerIndex == 1)
                return player2;
        
        return null;
    }

    public void GainPoints(int playerIndex, int pointsAmount)
    {
        if (playerIndex == 0)
        {
            scorePlayer1 += pointsAmount;
        }
        else if (playerIndex == 1)
        {
            scorePlayer2 += pointsAmount;
        }

        // AudioManager.i.PlayClipList(AudioManager.i.CharacterAudios[playerIndex].Collect);
        UpdateScoreUI();

        if (gameMode == GameMode.pointsMode)
        {
            if (scorePlayer1 == scoreToWin)
            {
                VictoryScreen(0);
            }
            else if (scorePlayer2 == scoreToWin)
            {
                VictoryScreen(1);
            }
        }

    }

    public void LosePoints(int playerIndex, int pointsAmount)
    {
        if (playerIndex == 0)
        {
            if (scorePlayer1 > 0)
                scorePlayer1-= pointsAmount;
        }
        else if (playerIndex == 1)
        {
            if (scorePlayer2 > 0)
                scorePlayer2-= pointsAmount;
        }

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        print("Totally updating score UI right now");
    }

    private void HideUI(int playerIndex)
    {
        print("Wonderful UI has been just hidden");
    }

    public void VictoryScreen(int winnerIndex)
    {
        if (!isSlowingDown)
        {
            isSlowingDown = true;
            StartCoroutine(VictoryScreenCoroutine(winnerIndex));
        }
    }

    private IEnumerator VictoryScreenCoroutine(int winnerIndex)
    {
        timerText.text = "";

        float slowMotionAcceleration = 1.5f;
        AudioManager.i.PlayAudioSource(AudioManager.i.TimeOut);
        AudioManager.i.PlayAudioSource(AudioManager.i.FightTheme, 0);
        do
        {
            Time.timeScale = Mathf.Clamp(
                Time.timeScale - slowMotionAcceleration * Time.deltaTime,
                0, 1);

            yield return new WaitForFixedUpdate();

        } while (Time.timeScale > 0.1);
        gameStatus = GameStatus.victoryScreen;

        AudioManager.i.PlayAudioSource(AudioManager.i.Victory);

        blurFXManager.ShowBlurEffect();

        //gameplayCanvas.SetActive(false);
        victoryCanvas.SetActive(true);
        Time.timeScale = 0;

        int shownWinner = winnerIndex + 1;

        GameObject replayButton = player1WinnerMenuButton;
        player1Winner.SetActive(false);
        player2Winner.SetActive(false);

        if (shownWinner == 1)
        {
            player1Winner.SetActive(true);
            replayButton = player1WinnerMenuButton;
        }
        else if (shownWinner == 2)
        {
            player2Winner.SetActive(true);
            replayButton = player2WinnerMenuButton;
        }

        yield return new WaitForSecondsRealtime(1f);
        //Time.timeScale = 1;
        eventSystem.SetSelectedGameObject(replayButton);
    }

    public int GetHighestScore(int highScore = 0)
    {
        if (scorePlayer1 > highScore)
            highScore = scorePlayer1;

        if (scorePlayer2 > highScore)
            highScore = scorePlayer2;

        return highScore;
    }

    public bool AreGameControlsEnabled()
    {
        return gameStatus == GameStatus.gameplayScreen;
    }


    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int GetPlayerScore(int index)
    {
        switch (index)
        {
            case 0:
                return scorePlayer1;
            case 1:
                return scorePlayer2;
            default:
                Debug.LogError("Index del giocatore strano!");
                return -1;
        }
    }

    public void DisableAllGuiScreens()
    {
        pausedScreen.SetActive(false);
    }

    public void GoToGameplayScreen(bool wasPaused = false)
    {
        pausedScreen.SetActive(false);
        gameplayScreen.SetActive(true);
        gameStatus = GameStatus.gameplayScreen;
    }

    public void GoToPauseScreen()
    {
        gameStatus = GameStatus.pauseScreen;
        pausedScreen.SetActive(true);
        eventSystem.SetSelectedGameObject(resumeFromPauseButton);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioManager.i.PlayAudioSource(AudioManager.i.FightTheme, 2);
        GoToPauseScreen();
        blurFXManager.ShowBlurEffect();
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        AudioManager.i.PlayAudioSource(AudioManager.i.FightTheme);
        GoToGameplayScreen(true);
        blurFXManager.HideBlurEffect();
    }

    public void TogglePause()
    {
        if (Time.timeScale == 1)
            PauseGame();
        else if (Time.timeScale == 0)
            UnpauseGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }



}


