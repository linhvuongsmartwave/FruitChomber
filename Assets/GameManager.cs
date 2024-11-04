using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int row;
    public int col;
    public float spacing;
    public FruitLevel[] fruitLevels;
    public PackmanLevel[] packmanLevels;
    //public int levelStart;
    public bool isHint = false;
    public bool win = false;
    public int countDestroy = 0;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    public GameObject effectWin1;
    public GameObject effectWin2;
    private int numberLevel;
    private int numberSelect;

    private void Awake()
    {

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        numberSelect = PlayerPrefs.GetInt("SelectedLevel", 0);
        numberLevel = PlayerPrefs.GetInt("CompletedLevel", 0);

    }
    private void Start()
    {
        LoadReSoure();
        LoadMap();
    }
    void LoadReSoure()
    {
        fruitLevels = Resources.LoadAll<FruitLevel>("FruitLevel");
        packmanLevels = Resources.LoadAll<PackmanLevel>("PackmanLevel");
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
        ClearSpawnedObjects();
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
                Vector2 spawnPos = new Vector2(startPos.x + j * spacing, startPos.y - i * spacing);
                GameObject enemy = Instantiate(level.enemies[index], spawnPos, Quaternion.identity);
                spawnedObjects.Add(enemy);
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
        //sceneFader.FadeTo("GamePlay");
    }
    IEnumerator Win()
    {
        yield return new WaitForSeconds(1);
        GameObject eff1 = Instantiate(effectWin1, Vector2.zero, Quaternion.identity);
        GameObject eff2 = Instantiate(effectWin2, Vector2.zero, Quaternion.identity);
    }
}
