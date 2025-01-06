using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth;
    private Animator animator;
    private bool dead;

    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRender;

    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }

    public void TakeDamage(float damageTaken){
        currentHealth = Mathf.Clamp(currentHealth - damageTaken, 0, startingHealth);

        if (currentHealth > 0){
            //player hurt
            //animator.SetTrigger("hurt");
            //iframes
            //StartCoroutine(Invulnerability());
        } else {
            
           // if (!dead){
           //     animator.SetTrigger("dead");
                //player dead
            //    if(GetComponent<PlayerMovement>() != null)
            //        GetComponent<PlayerMovement>().enabled = false;

                //Enemy dead
           //     if(GetComponentInParent<EnemyPatrol>() != null)
           //         GetComponentInParent<EnemyPatrol>().enabled = false;
            //    if(GetComponent<TrunkEnemy>() != null)
           //         GetComponent<TrunkEnemy>().enabled = false;
                
                dead = true;
            }
            
    }

    public void IncreaseHealth(float addedValue){
        currentHealth = Mathf.Clamp(currentHealth + addedValue, 0, startingHealth);
    }

    private  IEnumerator Invulnerability(){
        Physics2D.IgnoreLayerCollision(8, 9, true);
        //invulnerability duration
        for (int i = 0; i<numberOfFlashes; i++){
            spriteRender.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration/ (numberOfFlashes));
            spriteRender.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration/ (numberOfFlashes));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}

