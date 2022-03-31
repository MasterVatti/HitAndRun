using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private ZombieDeathCounter _deathCounter;
    [SerializeField] 
    private CharacterHealthBar _healthBar;
    [SerializeField] 
    private Movement _movement;

    private Dictionary<CharacterCharacteristicType, float> _characterSettingsByType;
    
    public void Initialize(float zombiesCount, Dictionary<CharacterCharacteristicType, float> characterSettingsByType)
    {
        _characterSettingsByType = characterSettingsByType;
        _deathCounter.Initialize(zombiesCount);
        _healthBar.Initialize(_characterSettingsByType[CharacterCharacteristicType.Health], _characterSettingsByType[CharacterCharacteristicType.Armor]);
        _movement.SetCharacterSpeed(_characterSettingsByType[CharacterCharacteristicType.MoveSpeed]);
    }
}