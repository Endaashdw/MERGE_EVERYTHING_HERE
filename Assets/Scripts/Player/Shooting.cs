using System.Collections;
using UnityEngine;

namespace Player
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform shootPoint;
        
        [SerializeField] private float fireRate;
        
        private bool _isFiring;

        private void Update()
        {
            if (Input.GetButtonDown("Fire1") && !_isFiring)
                StartCoroutine(FireBullet());
        }

        private IEnumerator FireBullet()
        {
            _isFiring = true;

            while (Input.GetButton("Fire1"))
            {
                Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                
                yield return new WaitForSeconds(fireRate);
            }
            
            _isFiring = false;
        }
    }
}