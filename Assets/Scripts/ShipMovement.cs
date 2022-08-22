using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float _dragCoefficient;
    [SerializeField] private float _thrust;

    private float _force;
    private float _acceleration;
    private float _newVel;
    private float _k1, _k2, _k3, _k4;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        DoSimulation();
    }

    private void DoSimulation()
    {
        _force = _thrust - (_dragCoefficient * _rb.velocity.z);
        _acceleration = _force / _rb.mass;
        _k1 = _acceleration * Time.fixedDeltaTime;

        _force = _thrust - (_dragCoefficient * (_rb.velocity.z + (_k1 / 2f)));
        _acceleration = _force / _rb.mass;
        _k2 = _acceleration * Time.fixedDeltaTime;

        _force = _thrust - (_dragCoefficient * (_rb.velocity.z + (_k2 / 2f)));
        _acceleration = _force / _rb.mass;
        _k3 = _acceleration * Time.fixedDeltaTime;

        _force = _thrust - (_dragCoefficient * (_rb.velocity.z + _k3));
        _acceleration = _force / _rb.mass;
        _k4 = _acceleration * Time.fixedDeltaTime;

        _newVel = _rb.velocity.z + (_k1 + 2 * _k2 + 2 * _k3 + _k4) / 6f;
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, _newVel);
    }
}
