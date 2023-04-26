using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    public string Name { get; private set; }

    public void SavePlayerName(NameEditor nameEditor)
    {
        Name = nameEditor.GetNewName();
    }
}
