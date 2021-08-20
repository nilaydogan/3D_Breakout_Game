using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    #region Singleton
    private static Game_Manager _instance;
    public static Game_Manager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        
    }
    #endregion

    public List<Collectable> collectables;

    public List<Collectable> rare;

    [HideInInspector]
    public int collected;        //will be used to buy things

    [HideInInspector]
    public int score;         //different for every level

    [HideInInspector]
    public int coins;

    [HideInInspector]
    public int multiply = 1;

    [Range(0, 100)]
    public float chance;

    [Range(0, 100)]
    public float rareChance;

    [HideInInspector]
    public bool starCollected = false;

    [HideInInspector]
    public int bestScore;

    [HideInInspector]
    public string level;

    private void Start()
    {
        score = 0;
    }

    public void addScore(int value)
    {
        collected += (value * multiply);
        coins += (value * multiply);
    }

    public void spendCoin(int price)
    {
        collected -= price;
    }

    private void FixedUpdate()
    {
        if (starCollected)
        {
            starCollected = false;
            StartCoroutine(WaitForDeactivation());
        }
    }

    IEnumerator WaitForDeactivation()
    {
        yield return new WaitForSeconds(30f);
        multiply = 1;
    }

    public void getScore()
    {
        int lvl = PlayerPrefs.GetInt("level", 0);
        string _level = getlvl(lvl);
        bestScore = PlayerPrefs.GetInt(_level, 0);
    }

    public void BestScore()
    {
        int lvl = PlayerPrefs.GetInt("level", 0);
        string _level = getlvl(lvl);
        Debug.Log("gelen lvl: " + _level + " " + score + " " + bestScore);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt(_level, score);
        }
    }

    public string getlvl(int lvl)
    {
        if (lvl == 1)
        {
            return "Level1";
        }else if(lvl == 2)
        {
            return "Level2";
        }
        else if (lvl == 3)
        {
            return "Level3";
        }
        else if (lvl == 4)
        {
            return "Level4";
        }
        else if (lvl == 5)
        {
            return "Level5";
        }
        return null;
    }
}
