using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bon20 : MonoBehaviour
{
    private GameObject glavni_obj;
    private Glavna_Skripta glavna_skripta;
    //private GameObject confetti;
    public GameObject nagrada_confetti;
    public GameObject coin_confetti;
    private bool samo_jednom = true;


    private void Start()
    {
        glavni_obj = GameObject.Find("Glavna_skripta_obj");
        glavna_skripta = glavni_obj.GetComponent<Glavna_Skripta>();
        //nagrada_confetti = Resources.Load<GameObject>("/nagrade_confetti/bon3_confetti.prefab");
        glavna_skripta.broj_bonova++;
        glavna_skripta.suma_levela += 20;
    }


    void Update()
    {
        if (transform.GetComponent<CircleCollider2D>().IsTouching(glavna_skripta.glavni_obj.GetComponent<CircleCollider2D>()) && samo_jednom)
        {
            glavna_skripta.glavni_obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-glavna_skripta.bon_shake_koef, glavna_skripta.bon_shake_koef), Random.Range(-glavna_skripta.bon_shake_koef, glavna_skripta.bon_shake_koef)));
            samo_jednom = false;
            glavna_skripta.skupljeni_bonovi++;
            glavna_skripta.vrijednost_bonova += 20;
            GameObject confetti_parent = new GameObject("confetti_parent");
            Destroy(confetti_parent, 3f);
            for (int a = 0; a < 3; a++)
            {
                GameObject confetti_particle = Instantiate(nagrada_confetti);
                confetti_particle.transform.SetParent(confetti_parent.transform);
                confetti_particle.transform.position = transform.position;
                confetti_particle.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-100f, 100f), Random.Range(-50f, 350f), 0f));
                Destroy(confetti_particle, Random.Range(2f, 3f));
            }

            for (int a = 0; a < 3; a++)
            {
                GameObject confetti_particle = Instantiate(coin_confetti);
                confetti_particle.transform.SetParent(confetti_parent.transform);
                confetti_particle.transform.position = transform.position;
                confetti_particle.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-100f, 100f), Random.Range(-50f, 350f), 0f));
                Destroy(confetti_particle, Random.Range(2f, 3f));
            }


            glavna_skripta.bon_pokupljen_zvuk();
            Destroy(gameObject);

        }
    }







}
