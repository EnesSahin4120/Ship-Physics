using UnityEngine;

public class SwimmingObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private WaterSettings _waterSettings;

    private Rigidbody _rb;

    private float _objectBottomY
    {
        get
        {
            return _meshRenderer.bounds.min.y;
        }
    }
    private float _objectTopY
    {
        get
        {
            return _meshRenderer.bounds.max.y;
        }
    }
    private float _objectSurfaceArea
    {
        get
        {
            return transform.localScale.x * transform.localScale.z;
        }
    }
    private float _hb
    {
        get
        {
            return Mathf.Abs(_objectBottomY - _waterSettings.WaterPos_Y);
        }
    }
    private float _ht
    {
        get
        {
            return Mathf.Abs(_objectTopY - _waterSettings.WaterPos_Y);
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        if (_objectBottomY < _waterSettings.WaterPos_Y)
            _rb.AddForce(Vector3.up * UpwardForceMagnitude(),ForceMode.Force);

        if (_objectTopY < _waterSettings.WaterPos_Y)
            _rb.AddForce(-Vector3.up * DownwardForceMagnitude(),ForceMode.Force);
    }

    private float UpwardForceMagnitude() 
    {
        return Mathf.Abs(_waterSettings.BuoyancyFactor * _hb * _objectSurfaceArea);
    }

    private float DownwardForceMagnitude()
    {
        return Mathf.Abs(_waterSettings.BuoyancyFactor * _ht * _objectSurfaceArea);
    }
}
