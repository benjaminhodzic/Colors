using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class katapult : MonoBehaviour
{
    private GameObject glavni_obj;
    private Glavna_Skripta glavna_skripta;
    private GameObject nep;
    private Neprijatelji_skripta nep_skripta;

    void Start()
    {
        glavni_obj = GameObject.Find("Glavna_skripta_obj");
        glavna_skripta = glavni_obj.GetComponent<Glavna_Skripta>();

        nep = GameObject.FindGameObjectsWithTag("enemy")[0];
        nep_skripta = glavni_obj.GetComponent<Neprijatelji_skripta>();
    }


    void Update()
    {
        glavna_skripta.object_touching(transform.GetComponent<BoxCollider2D>());
        nep_skripta.object_touching_enemy(transform.GetComponent<BoxCollider2D>());
    }
}
