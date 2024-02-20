using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab; // Attach your sphere prefab here in the Inspector
    public float projectileSpeed = 20f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at position and rotation of whatever game object this script is attached to
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

        // Get the Rigidbody component of the projectile and set its velocity
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;

        // Add a DestroyOnCollision script to the projectile
        projectile.AddComponent<DestroyOnCollision>();
    }
}

public class DestroyOnCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
