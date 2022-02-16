using System;
using UnityEngine;

public class CharacterSelectionView : MonoBehaviour
{
    [SerializeField]
    private CharacterSettingsProvider _characterSettingsProvider;
    
    [SerializeField]
    private CharacterSelectionButtons _characterSelectionButtons;

    private void Awake()
    {
        StartCoroutine((_characterSelectionButtons.Initialize(_characterSettingsProvider)));
        
    }
}