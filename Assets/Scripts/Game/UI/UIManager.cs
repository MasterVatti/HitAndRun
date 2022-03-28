using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private ZombieDeathCounter _deathCounter;
    [SerializeField] 
    private CharacterHealthBar _healthBar;
    
    public void Initialize(float zombiesCount, CharacterSettings characterSettings)
    {
        _deathCounter.Initialize(zombiesCount);
        _healthBar.Initialize(GetCurrentCharactericsic(characterSettings.Characteristics, CharacterCharacteristicType.Health));
    }

    private float GetCurrentCharactericsic(CharacterCharacteristics[] characterCharacteristics, CharacterCharacteristicType type)
    {
        foreach (var characterCharacteristic in characterCharacteristics)
        {
            if (characterCharacteristic.Type == type)
            {
               return Mathf.Clamp01(characterCharacteristic.Value / (float)characterCharacteristic.MaxValue);
            }
        }
        return 0;
    }
}