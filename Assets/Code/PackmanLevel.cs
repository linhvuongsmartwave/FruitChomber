using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PackmanLevel", menuName = "PackmanLevel")]
public class PackmanLevel : ScriptableObject
{
    public List<GameObject> listPackman;
}