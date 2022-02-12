using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCharacteristicView : MonoBehaviour
{
    [SerializeField]
    private Scrollbar _value;
    
    [SerializeField]
    private TextMeshProUGUI _name;
    
    public void Initialize(CharacterCharacteristics characteristics)
    {
        _value.size = Mathf.Clamp01(characteristics.Value / (float)characteristics.MaxValue);
        _name.text = characteristics.Name;
    }
}