using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public CanvasGroup CGroup;
    public Image healthBarFiller;
    public float speedOfChange = 2f;
    float totalHealth = 100f;
    public void Init(float tHealth) {
        totalHealth = tHealth;
        healthBarFiller.fillAmount = 1f;
        hide = false;
    }
    public void UpdateHealthBar(float fillAmount) {
        //healthBarFiller.fillAmount = fillAmount;
        StartCoroutine(ChangeToHealth(fillAmount));
    }

    IEnumerator ChangeToHealth(float fAmount) {
        float preChangePct = healthBarFiller.fillAmount;
        float elapsed = 0f;
        while (elapsed < speedOfChange)
        {
            elapsed += Time.deltaTime;
            healthBarFiller.fillAmount = Mathf.Lerp(preChangePct, fAmount, elapsed / speedOfChange);
            yield return null;
        }
        healthBarFiller.fillAmount = fAmount;
    }
    public void HideBar() { hide = true; CGroup.alpha = 0; }
    private bool hide = false;
    private void Update()
    {
        if (hide) return;
        if (Vector3.Distance(transform.position, Camera.main.transform.position) < 10f)
        {
            CGroup.alpha = Mathf.Lerp(CGroup.alpha, 1, Time.deltaTime);
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }
        else
        {
            CGroup.alpha = Mathf.Lerp(CGroup.alpha, 0, Time.deltaTime);
        }
    }
}
