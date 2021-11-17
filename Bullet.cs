using Extensions;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    private Vector3 _startPosition;
    private float _maxDistance;
    private Vector3 _direction;
    private float _speed;
    private bool _hasHitObject;
    
    public UnityEvent onHitEnter = new UnityEvent();
    public UnityEvent onHitEnterLeft = new UnityEvent();
    public UnityEvent onHitEnterRight = new UnityEvent();
    
    
    private void Start()
    {
        _startPosition = transform.position;
    }
    
    private void Update()
    {
        if (Vector3.Distance(_startPosition, transform.position) >= _maxDistance)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (_hasHitObject) return;
        if (other == null) return;
        _hasHitObject = true;
        OnHit(other.gameObject);
    }
    
    public void SetMaxDistance(float maxDistance)
    {
        _maxDistance = maxDistance;
    }

    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value;
    }
    
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
    
    public void OnHit(GameObject hitObject)
    {
        if(hitObject.HasComponent<Destroyable>())
        {
            hitObject.GetComponent<Destroyable>().Hit();
        }
        
        onHitEnter.Invoke();

        _hasHitObject = false;
        
        if (this._direction.x > 0) onHitEnterRight?.Invoke();
        if (this._direction.x < 0) onHitEnterLeft?.Invoke();
        
        Destroy(gameObject);
    }
}