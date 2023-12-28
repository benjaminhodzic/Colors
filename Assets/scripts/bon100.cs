using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class bon100 : MonoBehaviour
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
        glavna_skripta.suma_levela += 100;
    }


    void Update()
    {
        if (transform.GetComponent<CircleCollider2D>().IsTouching(glavna_skripta.glavni_obj.GetComponent<CircleCollider2D>()) && samo_jednom)
        {
            glavna_skripta.glavni_obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-glavna_skripta.bon_shake_koef, glavna_skripta.bon_shake_koef), Random.Range(-glavna_skripta.bon_shake_koef, glavna_skripta.bon_shake_koef)));
            samo_jednom = false;
            glavna_skripta.skupljeni_bonovi++;
            glavna_skripta.vrijednost_bonova += 100;
            GameObject confetti_parent = new GameObject("confetti_parent");
            Destroy(confetti_parent, 3f);
            for (int a = 0; a < 10; a++)
            {
                GameObject confetti_particle = Instantiate(nagrada_confetti);
                confetti_particle.transform.SetParent(confetti_parent.transform);
                confetti_particle.transform.position = transform.position;
                confetti_particle.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-100f, 100f), Random.Range(-50f, 350f), 0f));
                Destroy(confetti_particle, Random.Range(2f, 3f));
            }

            for (int a = 0; a < 10; a++)
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














    /*
                GameObject confetti_particle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                confetti_particle.transform.SetParent(confetti_parent.transform);
                confetti_particle.GetComponent<SphereCollider>().enabled = false;
                confetti_particle.transform.position = transform.position;
                confetti_particle.transform.localScale = new Vector3(0.07f, 0.07f, 0.0001f);
                confetti_particle.GetComponent<Renderer>().material.color = new Color((1.0f / 255) * 255, (1.0f / 255) * 30, (1.0f / 255) * 30, 0.3f);
                Material materijal = confetti_particle.GetComponent<Renderer>().material;
                //confetti_particle.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                confetti_particle.GetComponent<Renderer>().material.SetFloat("_Metallic", 1f);
                confetti_particle.GetComponent<Renderer>().material.SetFloat("_Glossiness", 0.5f);
                Rigidbody gameObjectsRigidBody = confetti_particle.AddComponent<Rigidbody>();
                confetti_particle.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 350f), 0f));
                //confetti_particle.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0f));
                Destroy(confetti_particle, Random.Range(2f, 3f));
                */





}
