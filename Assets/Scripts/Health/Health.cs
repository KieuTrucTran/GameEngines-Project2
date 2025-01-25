using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth;
    public GameObject solid;
    public GameObject fluid;
    public GameObject gas;
    public GameObject PlayerParent;

    private bool dead;

    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRender1, spriteRender2, spriteRender3;

    private void Awake()
    {
        currentHealth = startingHealth;
        //animator = GetComponent<Animator>();
        spriteRender1 = solid.GetComponent<SpriteRenderer>();
        spriteRender2 = fluid.GetComponent<SpriteRenderer>();
        spriteRender3 = gas.GetComponent<SpriteRenderer>();
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
            //iframes
            StartCoroutine(Invulnerability());
        } else {
            
           // if (!dead){
           //     animator.SetTrigger("dead");
                //player dead
            if(GetComponent<PlayerMovement>() != null)
                GetComponent<PlayerMovement>().enabled = false;
            solid.SetActive(false);
            fluid.SetActive(false);
            gas.SetActive(false);
            PlayerParent.SetActive(false);
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
            spriteRender1.color = new Color(1, 0, 0, 0.5f);
            spriteRender2.color = new Color(1, 0, 0, 0.5f);
            spriteRender3.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration/ (numberOfFlashes));
            spriteRender1.color = Color.white;
            spriteRender2.color = Color.white;
            spriteRender3.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration/ (numberOfFlashes));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}

