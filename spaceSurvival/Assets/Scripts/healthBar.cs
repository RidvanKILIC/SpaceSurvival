using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    #region Variables
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float chipSpeed;
    [SerializeField] float lerpTimer;
    [SerializeField] Image frontHealthBar;
    [SerializeField] Image backHealthBar;
    [SerializeField] Slider _healthBar;
    [SerializeField] Slider _shieldBar;
    [SerializeField] TMPro.TMP_Text healthText;
    [SerializeField] TMPro.TMP_Text shieldText;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        //healthBar = transform.Find("slider").GetComponent<Slider>();
        //healthText = transform.Find("text").GetComponent<TMPro.TMP_Text>();
        //setMaxHealth(100);
    }
    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        updateHeathUI();
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    decraseHealth(10);
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    increaseHealth(10);
        //}
    }
    void updateHeathUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = currentHealth / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.fixedDeltaTime;
            float percemtComplete = lerpTimer / chipSpeed;
            percemtComplete *= percemtComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percemtComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.fixedDeltaTime;
            float percemtComplete = lerpTimer / (chipSpeed + 4);
            percemtComplete *= percemtComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percemtComplete);
        }
    }
    public void setMaxHealth(float _health)
    {
        maxHealth = _health;
        currentHealth = _health;
    }

    public void decraseHealth(float _damage)
    {
        lerpTimer = 0;
        currentHealth -= _damage;
        if (currentHealth < 0f)
            currentHealth = 0f;
    }
    public void increaseHealth(float _heal) 
    {
        //lerpTimer = 0;
        currentHealth += _heal;
        if (currentHealth > 100f)
            currentHealth = 100f;
    }

}