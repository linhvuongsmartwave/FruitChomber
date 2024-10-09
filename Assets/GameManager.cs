using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();
    public int row = 3;
    public int col = 3;
    public float spacing = 1.5f;


    private void Awake()
    {
        // Tính toán điểm bắt đầu dựa trên số lượng hàng, cột và khoảng cách
        Vector2 startPos = new Vector2((col - 1) * spacing / 2, (row - 1) * spacing / 2);
        int index = 0;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                // Điều chỉnh vị trí spawn để các điểm trải ra từ tâm
                Vector2 spawnPos = new Vector2(startPos.x - j * spacing, startPos.y - i * spacing);

                // Instantiate từng GameObject từ mảng prefab
                Instantiate(prefabs[index], spawnPos, Quaternion.identity);
                index++;
            }
        }
    }


}
