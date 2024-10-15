using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int row;
    public int col;
    public float spacing;
    public List<FruitLevel> fruitLevels = new List<FruitLevel>();
    public List<PackmanLevel> packmanLevels = new List<PackmanLevel>();
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
        PackmanLevel pack = packmanLevels[levelStart];
        Vector2 startPos = new Vector2(-(col - 1) * spacing / 2, (row - 1) * spacing / 2);
        int index = 0;


        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Vector2 spawnPos = new Vector2(startPos.x + j * spacing, startPos.y - i * spacing);
                Instantiate(level.enemies[index], spawnPos, Quaternion.identity);
                index++;
            }
        }
        int halfCount = pack.listPackman.Count / 2;

        // Sinh nửa đầu của pack.listPackman nằm trên đầu lưới
        for (int i = 0; i < halfCount; i++)
        {
            Vector2 spawnPos = new Vector2(startPos.x + i * spacing, startPos.y + spacing); // Trên hàng đầu
            Instantiate(pack.listPackman[i], spawnPos, Quaternion.identity);
        }

        // Sinh nửa sau của pack.listPackman nằm bên phải lưới
        for (int i = halfCount; i < pack.listPackman.Count; i++)
        {
            int rowIndex = i - halfCount; // Xác định hàng tương ứng cho phần bên phải
            Vector2 spawnPos = new Vector2(startPos.x + col * spacing, startPos.y - rowIndex * spacing); // Bên phải cột cuối
            Instantiate(pack.listPackman[i], spawnPos, Quaternion.identity);
        }
    }


}
