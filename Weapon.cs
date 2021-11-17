using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class Weapon : Ability
{
    [SerializeField] protected float fireRate = 0.33f;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected Vector3 shootDir = new Vector3(1, 0, 0);
    [SerializeField] protected float speed = 80f;
    [SerializeField] protected float maxShootDistance = 64f;
    [SerializeField] private Vector3 scaleVector;
    private ParticleManager particle;

    protected bool _isOnCoolDown;
    protected Rigidbody _bulletRb;

    private void Start()
    {
        particle = new ParticleManager();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (_isOnCoolDown) return;

        onUseAbility?.Invoke();
        StartCoroutine(ShootBullet());
    }

    protected IEnumerator ShootBullet()
    {
        _isOnCoolDown = true;
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        
        bulletComponent.Direction = shootDir;
        bulletComponent.Speed = speed;
        
        _bulletRb = bullet.GetComponent<Rigidbody>();
        _bulletRb.velocity = shootDir * speed;

        bulletComponent.SetMaxDistance(maxShootDistance);

        yield return new WaitForSeconds(fireRate);
        _isOnCoolDown = false;
    }
    
    public void SetShootDirX(float newX)
    {
        shootDir.x = newX;
    }
}
