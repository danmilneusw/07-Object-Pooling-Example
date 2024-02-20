using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectileWithObjectPooling : MonoBehaviour
{
    public GameObject projectilePrefab; // Attach your sphere prefab here in the Inspector
    public float projectileSpeed = 20f;
    public bool recycle = false;        // recycle = true: apply object pooling
    private List<GameObject> projectiles = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject projectile = null;

        // If recycle is true, try to find an inactive projectile
        if (recycle)
        {
            foreach (GameObject p in projectiles)
            {
                if (!p.activeInHierarchy)
                {
                    projectile = p;
                    break;
                }
            }
        }

        // If no inactive projectile was found, instantiate a new one
        if (projectile == null)
        {
            projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectiles.Add(projectile);

            // Add a DestroyOrRecycleOnCollision script to the projectile
            projectile.AddComponent<DestroyOrRecycleOnCollision>();
        }

        // Set the projectile to active and set its position and rotation
        projectile.SetActive(true);
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;

        // Get the Rigidbody component and set its velocity
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;
    }
}

public class DestroyOrRecycleOnCollision : MonoBehaviour
{
    private ShootProjectileWithObjectPooling shootProjectile;
    private void Start()
    {
        shootProjectile = FindObjectOfType<ShootProjectileWithObjectPooling>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (shootProjectile.recycle)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
