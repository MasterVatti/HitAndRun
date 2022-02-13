using System;
using System.Linq;
using Pools;
using UnityEngine;

public class CharacterSelectionButtons : MonoBehaviour
{
    [SerializeField] 
    private CharacterSelectionButton _button;
    
    [SerializeField] 
    private RectTransform _buttonsRoot;
    
    private MonoBehaviourPool<CharacterSelectionButton> _selectionButtonsPool;

    private void Awake()
    {
        _selectionButtonsPool = new MonoBehaviourPool<CharacterSelectionButton>(_button, _buttonsRoot);
    }

    public void Initialize(CharacterSettingsProvider settingsProvider)
    {
        //Awake();
        var characters = settingsProvider.GetAllCharacters();
        foreach (var character in characters)
        {
            var characterSelectionButton = _selectionButtonsPool.Take();
            characterSelectionButton.Initialize(character);
        }
        _selectionButtonsPool.UsedItems.First().Select();
    }
}