using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalHealthbar.fillAmount = playerHealth.currentHealth / 5;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthbar.fillAmount = playerHealth.currentHealth / 5;
    }
}

