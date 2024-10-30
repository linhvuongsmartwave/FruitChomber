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
    public int levelStart;
    public bool isHint=false;
    public  bool win=false;
    public int countDestroy=0;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Awake()
    {
        LoadReSoure();
        LoadMap();
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

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
        this.row = fruitLevels[levelStart].row;
        this.col = fruitLevels[levelStart].col;
        FruitLevel level = fruitLevels[levelStart];
        PackmanLevel pack = packmanLevels[levelStart];
        Vector2 startPos = new Vector2(-(col - 1) * spacing / 2, (row - 1) * spacing / 2);
        int index = 0;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Vector2 spawnPos = new Vector2(startPos.x + j * spacing, startPos.y - i * spacing);
                GameObject enemy= Instantiate(level.enemies[index], spawnPos, Quaternion.identity);
                spawnedObjects.Add(enemy);
                index++;
            }
        }

        for (int j = 0; j < col; j++)
        {
            if (j < pack.listPackman.Count)
            {
                Vector2 spawnPos = new Vector2(startPos.x + j * spacing, startPos.y + spacing);
                GameObject enemy= Instantiate(pack.listPackman[j], spawnPos, Quaternion.Euler(0, 0, 90));
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
        if (countDestroy==row*col)
        {
            Debug.Log("Win");
            win = true;
        }

    }
}
