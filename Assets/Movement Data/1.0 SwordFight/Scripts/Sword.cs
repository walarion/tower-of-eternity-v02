using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int DamageValue = 10;
    public GameObject hitFx;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DamageController>())
        {
            other.GetComponent<DamageController>().Damage(DamageValue);
            GameObject hit = Instantiate<GameObject>(hitFx);
            hit.transform.position = other.ClosestPointOnBounds(transform.position);
            Destroy(hit, 2f);
        }
        else if (other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().Damage(DamageValue);
            GameObject hit = Instantiate<GameObject>(hitFx);
            hit.transform.position = other.ClosestPointOnBounds(other.transform.position);
            Destroy(hit, 2f);
        }
    }
}
