using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] TMP_Text HP_Text;
    [SerializeField] TMP_Text XP_Text;
    [SerializeField] float warningFrostAmt;
    [SerializeField] float freezFrostAmt;
    [SerializeField] GameObject warningPanel;
    float currentFrostAmt;
    bool frostActive = false;
    private static UI_Manager instance;
    public static UI_Manager UInstance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("UI instance is null");
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void boderWarning()
    {
        Camera.main.GetComponent<FrostEffect>().maxFrost = warningFrostAmt;
        currentFrostAmt = warningFrostAmt;
        frostActive = true;
        if(!warningPanel.activeInHierarchy)
            warningPanel.SetActive(true);
        //resetFrost();
    }
    public void freeze()
    {
        Camera.main.GetComponent<FrostEffect>().maxFrost = freezFrostAmt;
        currentFrostAmt = freezFrostAmt;
        frostActive = true;
    }
    public void resetFrost()
    {
        if(frostActive)
            StartCoroutine(resetFrostRoutine());
    }
    public void updateHP(float _HP)
    {
        HP_Text.text = _HP.ToString();
    }
    public void updateXP(int _XP)
    {
        XP_Text.text = _XP.ToString();
    }
    IEnumerator resetFrostRoutine()
    {
        if(warningPanel.activeInHierarchy)
            warningPanel.SetActive(false);
        frostActive = false;
        while (currentFrostAmt > 0)
        {
            currentFrostAmt -= 0.1f;
            Camera.main.GetComponent<FrostEffect>().maxFrost = currentFrostAmt;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
