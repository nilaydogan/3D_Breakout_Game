using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using TMPro;

public class SelectLevel : MonoBehaviour
{
    #region Singleton
    private static SelectLevel _instance;
    public static SelectLevel Instance => _instance;
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

    private Game_Manager gm;
    public TextMeshProUGUI best_score;

    private void Start()
    {
        gm = FindObjectOfType<Game_Manager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Exit();
        }
    }
    private void FixedUpdate()
    {
        printScore();
    }

    public void Load()
    {
        //int lvl = PlayerPrefs.GetInt("level", 0);
        //string _level = gm.getlvl(lvl);
        SceneManager.LoadScene(gm.level);
    }

    public void Exit()
    {
        PlayerPrefs.SetInt("isFirst", 0);
        Application.Quit();
    }
    public void printScore()
    {
        //Debug.Log(PlayerPrefs.GetInt(gm.level, 0));
        //Debug.Log("1 " + gm.bestScore);
        gm.getScore();
        //gm.BestScore();
        //Debug.Log("2 "+ gm.bestScore);
        best_score.SetText(gm.bestScore.ToString());
    }

}
