using System.Collections.Generic;
using UnityEngine;

public class CharacterSettingsProvider : MonoBehaviour
{
    [SerializeField] 
    private CharacterSettings[] _characterSettings;
    
    private Dictionary<string, CharacterSettings> _characterSettingsByName = new Dictionary<string, CharacterSettings>();

    private void Awake()
    {
        foreach (var characterSettings in _characterSettings)
        {
            _characterSettingsByName[characterSettings.Name] = characterSettings;
        }
    }

    public CharacterSettings[] GetAllCharacters()
    {
        return _characterSettings;
    }
    
    public CharacterSettings GetCharacter(string name)
    {
        return _characterSettingsByName[name];
    }
}