using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zvijezda : MonoBehaviour
{
    private GameObject glavni_obj;
    private Glavna_Skripta glavna_skripta;

    private void Start()
    {
        glavni_obj = GameObject.Find("Glavna_skripta_obj");
        glavna_skripta = glavni_obj.GetComponent<Glavna_Skripta>();
        glavna_skripta.broj_zvijezda++;
    }


    void Update()
    {
        if (transform.GetComponent<CircleCollider2D>().IsTouching(glavna_skripta.glavni_obj.GetComponent<CircleCollider2D>()))
        {
            glavna_skripta.glavni_obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0f, glavna_skripta.bon_shake_koef), Random.Range(0f, glavna_skripta.bon_shake_koef)));
            glavna_skripta.skinute_zvijezde++;
            glavna_skripta.zvijezda_pokupljen_zvuk();
            Destroy(gameObject);

        }
    }







}
