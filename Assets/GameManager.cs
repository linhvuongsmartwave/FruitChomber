using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public List<GameObject> prefabsFruit = new List<GameObject>();
    public int row ;
    public int col ;
    public float spacing = 1.5f;
    public List<FruitLevel> fruitLevels = new List<FruitLevel>();
    public int levelStart;



    private void Awake()
    {
        LoadMap();

    }
    public void LoadMap()
    {
        this.row = fruitLevels[levelStart].row;
        this.col = fruitLevels[levelStart].col;
        FruitLevel level = fruitLevels[levelStart];
        Vector2 startPos = new Vector2((col - 1) * spacing / 2, (row - 1) * spacing / 2);
        int index = 0;


        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Vector2 spawnPos = new Vector2(startPos.x - j * spacing, startPos.y - i * spacing);
                Instantiate(level.enemies[index], spawnPos, Quaternion.identity);
                index++;
            }
        }
    }


}
