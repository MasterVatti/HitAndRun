using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionButton : MonoBehaviour
{
    [SerializeField] 
    private Image _characterIcon;
    
    [SerializeField] 
    private TextMeshProUGUI _characterName;

    private CharacterSettings _character;

    public void Initialize(CharacterSettings character)
    {
        _character = character;
        _characterIcon.sprite = character.Icon;
        _characterName.text = character.Name;
    }
    
    public void OnClick()
    {
        EventStreams.Game.Publish(new CharacterSelectedEvent(_character));
    }
}