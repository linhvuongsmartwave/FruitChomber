using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int row;
    public int col;
    public float spacing;
    public FruitLevel[] fruitLevels;
    public PackmanLevel[] packmanLevels;
    public bool isHint = false;
    public bool win = false;
    public int countDestroy = 0;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private int numberLevel;
    private int numberSelect;
    public Button btnHint;
    public UiPanelDotween panelWin;
    public UiPanelDotween shop;
    public TextMeshProUGUI txtLevel;
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        numberSelect = PlayerPrefs.GetInt("SelectedLevel", 0);
        numberLevel = PlayerPrefs.GetInt("CompletedLevel", 0);
        btnHint = GameObject.Find("ButtonBooster").GetComponent<Button>();
        panelWin = GameObject.Find("PanelWin").GetComponent<UiPanelDotween>();
        txtLevel = GameObject.Find("txtLevel").GetComponent<TextMeshProUGUI>();
        LoadReSoure();
        LoadMap();

    }
    private void Start()
    {


    }
    void LoadReSoure()
    {
        fruitLevels = Resources.LoadAll<FruitLevel>("FruitLevel");
        packmanLevels = Resources.LoadAll<PackmanLevel>("PackmanLevel");
        if (fruitLevels == null || fruitLevels.Length == 0)
        {
            Debug.Log("FruitLevel resources not found or empty!");
        }
        else
        {
            Debug.Log("khong van de gi");
        }
        if (packmanLevels == null || packmanLevels.Length == 0)
        {
            Debug.Log("PackmanLevel resources not found or empty!");
        }
        else
        {
            Debug.Log("khong van de gi");

        }
    }
    private void ClearSpawnedObjects()
    {
        foreach (var obj in spawnedObjects)
        {
            if (obj != null) Destroy(obj);
        }
        spawnedObjects.Clear();
    }
    public void LoadMap()
    {
        if (fruitLevels == null || packmanLevels == null || fruitLevels.Length <= numberSelect || packmanLevels.Length <= numberSelect)
        {
            Debug.Log("FruitLevels or PackmanLevels is null or not loaded properly.");
            return;
        }
        ClearSpawnedObjects();
        txtLevel.text = "Level " + (numberSelect + 1).ToString();
        this.row = fruitLevels[numberSelect].row;
        this.col = fruitLevels[numberSelect].col;
        FruitLevel level = fruitLevels[numberSelect];
        PackmanLevel pack = packmanLevels[numberSelect];
        Vector2 startPos = new Vector2(-(col - 1) * spacing / 2, (row - 1) * spacing / 2);
        int index = 0;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (index < level.enemies.Count) // Kiểm tra để đảm bảo index hợp lệ
                {
                    Vector2 spawnPos = new Vector2(startPos.x + j * spacing, startPos.y - i * spacing);
                    GameObject enemy = Instantiate(level.enemies[index], spawnPos, Quaternion.identity);
                    spawnedObjects.Add(enemy);
                }
                index++;
            }
        }

        for (int j = 0; j < col; j++)
        {
            if (j < pack.listPackman.Count)
            {
                Vector2 spawnPos = new Vector2(startPos.x + j * spacing, startPos.y + spacing);
                GameObject enemy = Instantiate(pack.listPackman[j], spawnPos, Quaternion.Euler(0, 0, 90));
                spawnedObjects.Add(enemy);
            }
        }

        for (int i = 0; i < row; i++)
        {
            int packmanIndex = col + i;
            if (packmanIndex < pack.listPackman.Count)
            {
                Vector2 spawnPos = new Vector2(startPos.x + col * spacing, startPos.y - i * spacing);
                GameObject enemy = Instantiate(pack.listPackman[packmanIndex], spawnPos, Quaternion.identity);
                spawnedObjects.Add(enemy);
            }
        }
    }
    public void CheckWin()
    {
        countDestroy++;
        if (countDestroy == row * col)
        {
            Debug.Log("Win");
            win = true;
            StartCoroutine(Win());
            Shop.Instance.ruby += 1;
            Shop.Instance.UpdateGold();
            Shop.Instance.Save();
        }

    }
    public void NextLevel()
    {
        numberSelect++;
        if (numberSelect > numberLevel) numberLevel++;
        else numberLevel = numberSelect;
        PlayerPrefs.SetInt("SelectedLevel", numberSelect);
        if (numberLevel >= numberSelect)
        {
            PlayerPrefs.SetInt("CompletedLevel", numberLevel);
            PlayerPrefs.Save();
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene("GamePlay");
        //sceneFader.FadeTo("GamePlay");
    }
    IEnumerator Win()
    {
        yield return new WaitForSeconds(1);
        panelWin.PanelFadeIn();
    }
    public void Booster()
    {
        btnHint.interactable = false;
        isHint = true;
        if (Shop.Instance.ruby >= 2)
        {
            Shop.Instance.ruby -= 2;
            Shop.Instance.Save();
            Shop.Instance.UpdateGold();
        }
        else
        {

            shop.PanelFadeIn();
        }



    }
    public void HomeScene()
    {
        SceneManager.LoadScene("HomeScene");
    }
    public void Replay()
    {
        SceneManager.LoadScene("GamePlay");
    }

}
