using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] _objs;
    [SerializeField] private int[] _valueofobj;
    [SerializeField] private Transform _parent;

    [SerializeField] private int _xPosA;
    [SerializeField] private int _xPosB;
    [SerializeField] private int _zPosA;
    [SerializeField] private int _zPosB;

    private Vector3 _position;

    private void Start()
    {
        for (int i = 0; i < _valueofobj.Length; i++)
        {
            for (int j = 0; j < _valueofobj[i]; j++)
            {
                _position = new Vector3(Random.Range(_xPosA, _xPosB), 0, Random.Range(_zPosA, _zPosB));
                Instantiate(_objs[i], _position, Quaternion.identity, _parent);
            }
        }
    }
}
