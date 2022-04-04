using UnityEngine;

public class BombManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _bombObject;
    [SerializeField] 
    private int _count;
    [SerializeField] 
    private float[] _oneBombPositionX;
    [SerializeField] 
    private float[] _oneBombPositionZ;

    private void Start()
    {
        for (int i = 0; i < _count; i++)
        {
            var bomb = Instantiate(_bombObject, transform);
            bomb.transform.position = new Vector3(_oneBombPositionX[i], 0f, _oneBombPositionZ[i]);
        }
    }
}