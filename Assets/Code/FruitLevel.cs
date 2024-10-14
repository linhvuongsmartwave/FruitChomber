using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FruitLevel", menuName = "FruitLevel")]
public class FruitLevel : ScriptableObject
{
    public int col;
    public int row;
    public List<GameObject> enemies;
}