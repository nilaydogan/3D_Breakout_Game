using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    #region Singleton
    private static Menu _instance;
    public static Menu Instance => _instance;
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

    public Button enterButton;
    public Animator cameraAnim;
    public Animator canvasAnim;
    public GameObject MainMenu;
    public GameObject PlayLevel;
    public GameObject SelectLevel;

    public GameObject Sun;

    private string trigger;
    private bool isRotated = false;

    [HideInInspector]
    public string level;

    private Game_Manager gm;

    private void Start()
    {
        gm = FindObjectOfType<Game_Manager>();
        int x = PlayerPrefs.GetInt("isFirst", 0);
        Debug.Log("x " + x);

        if (x == 0)
        {
            PlayerPrefs.SetInt("start_from", 0);
        }
        else if (x == 1)
        {
            start_from();
        }
    }

    public void cameraDown()
    {
        cameraAnim.SetTrigger("Start_To_Menu");
        enterButton.gameObject.SetActive(false);

        StartCoroutine(WaitForStart());
    }

    public void In_Frozen()
    {
        CloseSelectLevel();
        cameraAnim.SetTrigger("In_Frozen");
        trigger = "Out_Frozen";
        gm.level = "Level1";
        PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.SetInt("isFirst", 1);
        PlayerPrefs.SetInt("start_from", 1);
        StartCoroutine(PlacementSelected());
    }

    public void In_Tundra()
    {
        CloseSelectLevel();
        cameraAnim.SetTrigger("In_Tundra");
        trigger = "Out_Tundra";
        gm.level = "Level2";
        PlayerPrefs.SetInt("level", 2);
        PlayerPrefs.SetInt("isFirst", 1);
        PlayerPrefs.SetInt("start_from", 2);
        StartCoroutine(PlacementSelected());
    }

    public void In_Earth()
    {
        CloseSelectLevel();
        cameraAnim.SetTrigger("In_Earth");
        trigger = "Out_Earth";
        gm.level = "Level3";
        PlayerPrefs.SetInt("level", 3);
        PlayerPrefs.SetInt("isFirst", 1);
        PlayerPrefs.SetInt("start_from", 3);
        RotateSun();
        StartCoroutine(PlacementSelected());
    }

    public void In_Temperate()
    {
        CloseSelectLevel();
        cameraAnim.SetTrigger("In_Temperate");
        trigger = "Out_Temperate";
        gm.level = "Level4";
        PlayerPrefs.SetInt("level", 4);
        PlayerPrefs.SetInt("isFirst", 1);
        PlayerPrefs.SetInt("start_from", 4);
        RotateSun();
        StartCoroutine(PlacementSelected());
    }

    public void In_Desert()
    {
        CloseSelectLevel();
        cameraAnim.SetTrigger("In_Desert");
        trigger = "Out_Desert";
        gm.level = "Level5";
        PlayerPrefs.SetInt("level", 5);
        PlayerPrefs.SetInt("isFirst", 1);
        PlayerPrefs.SetInt("start_from", 5);
        RotateSun();
        StartCoroutine(PlacementSelected());
    }

    public void Out_Anim()
    {
        PlacementBack();
        if (isRotated)
        {
            Sun.transform.RotateAround(Sun.transform.position, Vector3.up, 160f);
            isRotated = false;
        }
        cameraAnim.SetTrigger(trigger);
        StartCoroutine(OpenSelectLevel());
    }

    void RotateSun()
    {
        Sun.transform.RotateAround(Sun.transform.position, Vector3.up, -160f);
        isRotated = true;
    }

    IEnumerator OpenSelectLevel()
    {
        yield return new WaitForSeconds(2f);
        SelectLevel.GetComponent<CanvasGroup>().alpha = 1;
        SelectLevel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void CloseSelectLevel()
    {
        SelectLevel.GetComponent<CanvasGroup>().alpha = 0;
        SelectLevel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void Main_to_Options()
    {
        canvasAnim.SetTrigger("Main_To_Options");
    }

    public void Options_to_Main()
    {
        canvasAnim.SetTrigger("Options_To_Main");
    }

    public void Main_To_Select()
    {
        MainMenu.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
        MainMenu.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
        StartCoroutine(OpenSelectLevel());
        canvasAnim.SetTrigger("Main_To_Select");
    }

    public void Select_To_Main()
    {
        canvasAnim.SetTrigger("Out_Select");
        CloseSelectLevel();
        StartCoroutine(WaitForStart());
    }

    IEnumerator PlacementSelected()
    {
        yield return new WaitForSeconds(2f);
        PlayLevel.GetComponent<CanvasGroup>().alpha = 1;
        PlayLevel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void PlacementBack()
    {
        //yield return new WaitForSeconds(2f);
        PlayLevel.GetComponent<CanvasGroup>().alpha = 0;
        PlayLevel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(2f);
        MainMenu.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 1;
        MainMenu.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void From_Frozen()
    {
        CloseSelectLevel();
        cameraAnim.SetTrigger("Start_from_frozen");
        trigger = "Out_Frozen";
        gm.level = "Level1";
        //PlayerPrefs.SetInt("start_from", 1);
        StartCoroutine(PlacementSelected());
    }

    public void From_Tundra()
    {
        CloseSelectLevel();
        cameraAnim.SetTrigger("Start_from_tundra");
        trigger = "Out_Tundra";
        gm.level = "Level2";
        //PlayerPrefs.SetInt("start_from", 2);
        StartCoroutine(PlacementSelected());
    }

    public void From_Earth()
    {
        CloseSelectLevel();
        cameraAnim.SetTrigger("Start_from_earth");
        RotateSun();
        trigger = "Out_Earth";
        gm.level = "Level3";
        //PlayerPrefs.SetInt("start_from", 3);
        StartCoroutine(PlacementSelected());
    }

    public void From_Temperate()
    {
        CloseSelectLevel();
        cameraAnim.SetTrigger("Start_from_temperate");
        RotateSun();
        trigger = "Out_Temperate";
        gm.level = "Level4";
        //PlayerPrefs.SetInt("start_from", 4);
        StartCoroutine(PlacementSelected());
    }

    public void From_Desert()
    {
        CloseSelectLevel();
        cameraAnim.SetTrigger("Start_from_desert");
        RotateSun();
        trigger = "Out_Desert";
        gm.level = "Level5";
        //PlayerPrefs.SetInt("start_from", 5);
        StartCoroutine(PlacementSelected());
    }

    public void start_from()
    {
        int x = PlayerPrefs.GetInt("start_from", 0);
        if (x == 1)
        {
            enterButton.gameObject.SetActive(false);
            From_Frozen();
            PlayerPrefs.SetInt("start_from", 0);

        }
        else if (x == 2)
        {
            enterButton.gameObject.SetActive(false);
            From_Tundra();
            PlayerPrefs.SetInt("start_from", 0);

        }
        else if (x == 3)
        {
            enterButton.gameObject.SetActive(false);
            From_Earth();
            PlayerPrefs.SetInt("start_from", 0);

        }
        else if (x == 4)
        {
            enterButton.gameObject.SetActive(false);
            From_Temperate();
            PlayerPrefs.SetInt("start_from", 0);

        }
        else if(x == 5)
        {
            enterButton.gameObject.SetActive(false);
            From_Desert();
            PlayerPrefs.SetInt("start_from", 0);
        }
    }
}
