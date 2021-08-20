using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public enum BreakoutGameState { playing, won, lost };

public class BreakoutGame : MonoBehaviour
{
    public static BreakoutGame SP;

    public Transform ballPrefab;

    private int totalBlocks;
    private int blocksHit;
    private BreakoutGameState gameState;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreCollected;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI totalscore;
    private Game_Manager gm;
    private Paddle paddle;

    public Image image;
    public Animator reviveAnim;
    public Animator transitionAnim;

    [HideInInspector]
    public bool isRevived = false;
    [HideInInspector]
    public int revive = 0;
    private bool playAnim = false;

    [HideInInspector]
    public bool ballSpawned = false;
    int score = 0;
    void Awake()
    {
        SP = this;
        blocksHit = 0;
        gameState = BreakoutGameState.playing;
        totalBlocks = GameObject.FindGameObjectsWithTag("Pickup").Length;

        gm = FindObjectOfType<Game_Manager>();
        paddle = FindObjectOfType<Paddle>();

        Time.timeScale = 1.0f;
        StartCoroutine(WaitForAnimation());
    }

    private void Update()
    {
        int coins = gm.coins;
        scoreText.SetText(blocksHit + "/" + totalBlocks);
        scoreCollected.SetText(coins.ToString());
        
        totalscore.SetText(score.ToString());
        
        speedText.SetText(paddle.moveSpeed.ToString());

        if (Input.GetKeyDown(KeyCode.R))
        {
            image.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transitionAnim.SetTrigger("FadeOut");
            StartCoroutine(WaitForScene());
        }

        if (isRevived && !playAnim)
        {
            ShowText();
            playAnim = true;
        }
    }

    void SpawnBall()
    {
        Instantiate(ballPrefab, new Vector3(0.75f, 1.0f, 0f), Quaternion.identity);
    }

    void OnGUI()
    {
        if (gameState == BreakoutGameState.lost)
        {
            image.gameObject.SetActive(true);
        }
        else if (gameState == BreakoutGameState.won)
        {
            transitionAnim.SetTrigger("FadeOut");
            StartCoroutine(WaitForScene());
        }
    }

    public void TryAgain()
    {
        Application.LoadLevel(Application.loadedLevel);
        image.gameObject.SetActive(false);
    }

    public void HitBlock()
    {
        blocksHit++;
        score += 10;
        //For fun:
        if (blocksHit % 10 == 0) //Every 10th block will spawn a new ball
        {
            ballSpawned = true;
            SpawnBall();
        }


        if (blocksHit >= totalBlocks)
        {
            //gm.score += blocksHit;
            //gm.BestScore();
            score += gm.score * 10;
            gm.score = score;
            //Debug.Log("gelen score: " + gm.score);
            gm.getScore();
            //Debug.Log("bst score: " + gm.bestScore);
            gm.BestScore();
            //Debug.Log("bst2 score: " + PlayerPrefs.GetInt(gm.level,0));
            WonGame();
        }
    }

    public void WonGame()
    {
        //Time.timeScale = 0.0f; //Pause game
        
        gameState = BreakoutGameState.won;
        
    }

    public void LostBall()
    {
        int ballsLeft = GameObject.FindGameObjectsWithTag("Player").Length;
        if (ballsLeft <= 1)
        {
            //Was the last ball..
            if (revive > 0)
            {
                revive = 0;
                Instantiate(ballPrefab, new Vector3(0.75f, 1.0f, -4.15f), Quaternion.identity);
                StartCoroutine(WaitForDeactivation());
            }
            else
                SetGameOver();
        }
    }

    void ShowText()
    {
        reviveAnim.SetTrigger("FadeIn");
    }

    public void SetGameOver()
    {
        Time.timeScale = 0.0f; //Pause game
        gameState = BreakoutGameState.lost;
    }

    IEnumerator WaitForDeactivation()
    {
        yield return new WaitForSeconds(2f);
        reviveAnim.SetTrigger("FadeOut");
    }
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        SpawnBall();
    }
    IEnumerator WaitForScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("LevelSelect");
    }
}
