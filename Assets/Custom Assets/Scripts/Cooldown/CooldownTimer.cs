using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CooldownTimer : MonoBehaviour
{
    [SerializeField] private Image cooldownImage;
    [SerializeField] private TMP_Text cooldownText;

    private float timeToFire = 0;
    private float cooldown = 0;
    private float cooldownTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        cooldownText.gameObject.SetActive(false);
        cooldownImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < timeToFire)
        {            
            cooldownTimer -= Time.deltaTime;
            cooldownText.gameObject.SetActive(true);
            cooldownText.text = Mathf.RoundToInt(cooldownTimer).ToString();
            cooldownImage.fillAmount = cooldownTimer / cooldown;
        }
        else
        {
            cooldownTimer = cooldown;
            cooldownText.gameObject.SetActive(false);
            cooldownImage.fillAmount = 0;
        }            
    }


    public void SetCooldownAndTimeToFire(float spellCooldown, float timeToShoot)
    {
        cooldown = spellCooldown;
        timeToFire = timeToShoot;
    }
}
