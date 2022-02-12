using System;
using UnityEngine;

[Serializable]
public class CharacterSettings
{
    public GameObject Prefab;
    public Sprite Icon;
    public string Name;
    public CharacterCharacteristics[] Characteristics;
}