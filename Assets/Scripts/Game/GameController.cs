using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField] 
    private CharacterController characterController;
    [SerializeField] 
    private UIManager _uiManager;
    [SerializeField] 
    private ZombieManager _zombieManager;
    [SerializeField] 
    private Canvas _gameOverGUI;
    [SerializeField] 
    private Canvas _gameWinGUI;
    [SerializeField] 
    private BulletManager _bulletManager;

    private Dictionary<CharacterCharacteristicType, float> _characterSettingsByType = new Dictionary<CharacterCharacteristicType, float>();
    
    public void StartGame(CharacterSettings character)
    {
        InitializeCurrentCharacteristics(character.Characteristics);
        
        characterController.SpawnCharacter(character);
        _uiManager.Initialize(_zombieManager.GetAllZombiesQuantity(), _characterSettingsByType);
        _bulletManager.SetWeaponCharacteristics( _characterSettingsByType[CharacterCharacteristicType.AttackPower], _characterSettingsByType[CharacterCharacteristicType.FireRate]);
        
    }

    private void InitializeCurrentCharacteristics(CharacterCharacteristics[] characterCharacteristics)
    {
        foreach (var characterCharacteristic in characterCharacteristics)
        {
            _characterSettingsByType[characterCharacteristic.Type] = (characterCharacteristic.Value / (float)characterCharacteristic.MaxValue);
        }
    }

    public Canvas GetGameOverGUI()
    {
        return _gameOverGUI;
    }
    
    public Canvas GetGameWinGUI()
    {
        return _gameWinGUI;
    }
}