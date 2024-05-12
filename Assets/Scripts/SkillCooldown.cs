using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldown : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] float cooldown;
    float lastUse;
    [SerializeField] Image cooldownImage;

   
    void Update()
    {
        if (Time.time - cooldown > lastUse)
            button.interactable = true;
        else
            button.interactable = false;

        if (Input.GetKeyDown(KeyCode.Alpha3) && Time.time - cooldown > lastUse)
        {
           button.onClick.Invoke();
        }

        cooldownImage.fillAmount = 1 - (Time.time - lastUse) / cooldown;

    }

    public void UseSkill()
    {
        lastUse = Time.time;
    }




}
