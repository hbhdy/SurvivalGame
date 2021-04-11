using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSSObjectCategory : MonoBehaviour
{
    public ObjectInfo[] poolTable;

    public int GetTableLength()
    {
        return poolTable.Length;
    }
}
