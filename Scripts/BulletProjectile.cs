using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    [SerializeField] private Transform VfxHitgreen;
    [SerializeField] private Transform VfxHitRed;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float Speed = 50f;
        bulletRigidbody.linearVelocity = transform.forward * Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletTarget>() != null) 
        {
            // Hit target
            Instantiate(VfxHitRed, transform.position,Quaternion.identity);
        } 
        else 
        { 
            // Hit Something else
            Instantiate(VfxHitgreen, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
