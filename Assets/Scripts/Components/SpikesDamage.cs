using UnityEngine;

public class SpikesDamage : MonoBehaviour
{
[SerializeField] protected float damage;
    protected void OnTriggerEnter2D(Collider2D collision){
    if(collision.tag == "Player")
        collision.GetComponent<Health>().TakeDamage(damage);
    }

}
