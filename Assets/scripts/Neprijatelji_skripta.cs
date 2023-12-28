using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Neprijatelji_skripta : MonoBehaviour {

    private GameObject glavni_obj;
    private Glavna_Skripta glavna_skripta;
    //private Transform prosli_transform;
    //public RuntimeAnimatorController odebaba;
    //public GameObject trica;
    private bool zvuk_iskoristen = true;
    public GameObject pop_animation;
    private bool jednom = true;
    private bool zvuk_zid_moze = true;

    private void Start()
    {
    glavni_obj = GameObject.Find("Glavna_skripta_obj");
    glavna_skripta = glavni_obj.GetComponent<Glavna_Skripta>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "glavni")
        {
            glavna_skripta.enemy_touched = true;
            if (zvuk_iskoristen) { glavna_skripta.enemy_touched_zvuk(); zvuk_iskoristen = false; }
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("nema_enemy_level" + glavna_skripta.koji_level_int().ToString()) == 1 || Input.GetKey(KeyCode.P) && jednom)
        {
            glavna_skripta.enemy_puko_zvuk();
            Destroy(Instantiate(pop_animation, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity), 0.5f);
            transform.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 1);
            jednom = false;
        }


        if (zvuk_zid_moze)
        {
            float neki_broj = 7;
            if (transform.GetComponent<CircleCollider2D>().IsTouching(glavna_skripta.lijevo))
            {
                float volume_V = (transform.GetComponent<Rigidbody2D>().velocity.x / neki_broj);
                if (volume_V > 1) volume_V = 1; if (volume_V < 0) volume_V = 0;
                glavna_skripta.audio_source1_1.PlayOneShot(glavna_skripta.kick1, volume_V);
                Debug.Log("lijevo  " + volume_V);
                StartCoroutine(zvuk_delay());
            }
            if (transform.GetComponent<CircleCollider2D>().IsTouching(glavna_skripta.desno))
            {
                float volume_V = (-transform.GetComponent<Rigidbody2D>().velocity.x / neki_broj);
                if (volume_V > 1) volume_V = 1; if (volume_V < 0) volume_V = 0;
                glavna_skripta.audio_source1_1.PlayOneShot(glavna_skripta.kick1, volume_V);
                Debug.Log("desno  " + volume_V);
                StartCoroutine(zvuk_delay());
            }
            if (transform.GetComponent<CircleCollider2D>().IsTouching(glavna_skripta.gore))
            {
                float volume_V = (-transform.GetComponent<Rigidbody2D>().velocity.y / neki_broj);
                if (volume_V > 1) volume_V = 1; if (volume_V < 0) volume_V = 0;
                glavna_skripta.audio_source1_1.PlayOneShot(glavna_skripta.kick1, volume_V);
                Debug.Log("gore  " + volume_V);
                StartCoroutine(zvuk_delay());
            }
            if (transform.GetComponent<CircleCollider2D>().IsTouching(glavna_skripta.dole))
            {
                float volume_V = (transform.GetComponent<Rigidbody2D>().velocity.y / neki_broj);
                if (volume_V > 1) volume_V = 1; if (volume_V < 0) volume_V = 0;
                glavna_skripta.audio_source1_1.PlayOneShot(glavna_skripta.kick1, volume_V);
                Debug.Log("dole  " + volume_V);
                StartCoroutine(zvuk_delay());
            }

        }


        //GetComponent<Animator>().runtimeAnimatorController = odebaba; 
        //GetComponent<SpriteRenderer>() = Resources.Load<SpriteRenderer>("Assets/skice/neprijatelji/1/3.png").;
        //gameObject.SpriteRenderer = trica.GetComponent<SpriteRenderer>();
    }


    private IEnumerator zvuk_delay()
    {
        zvuk_zid_moze = false;
        yield return new WaitForSeconds(0.1f);
        zvuk_zid_moze = true;
    }

    public void object_touching_enemy(BoxCollider2D object_collider)
    {
        if (zvuk_zid_moze)
        {
            float neki_broj = 7;
            if (transform.GetComponent<CircleCollider2D>().IsTouching(object_collider))
            {
                float volume_V = 0;
                if (transform.GetComponent<Rigidbody2D>().velocity.x > transform.GetComponent<Rigidbody2D>().velocity.y
                    || transform.GetComponent<Rigidbody2D>().velocity.x > -transform.GetComponent<Rigidbody2D>().velocity.y
                    || -transform.GetComponent<Rigidbody2D>().velocity.x > transform.GetComponent<Rigidbody2D>().velocity.y
                    || -transform.GetComponent<Rigidbody2D>().velocity.x > -transform.GetComponent<Rigidbody2D>().velocity.y
                    )
                {
                    if (transform.GetComponent<Rigidbody2D>().velocity.x > 0) volume_V = (transform.GetComponent<Rigidbody2D>().velocity.x / neki_broj);
                    else volume_V = (-transform.GetComponent<Rigidbody2D>().velocity.x / neki_broj);
                }
                if (transform.GetComponent<Rigidbody2D>().velocity.y > transform.GetComponent<Rigidbody2D>().velocity.x
                   || transform.GetComponent<Rigidbody2D>().velocity.y > -transform.GetComponent<Rigidbody2D>().velocity.x
                   || -transform.GetComponent<Rigidbody2D>().velocity.y > transform.GetComponent<Rigidbody2D>().velocity.x
                   || -transform.GetComponent<Rigidbody2D>().velocity.y > -transform.GetComponent<Rigidbody2D>().velocity.x
                   )
                {
                    if (transform.GetComponent<Rigidbody2D>().velocity.y > 0) volume_V = (transform.GetComponent<Rigidbody2D>().velocity.y / neki_broj);
                    else volume_V = (-transform.GetComponent<Rigidbody2D>().velocity.y / neki_broj);
                }

                if (volume_V > 1) volume_V = 1; if (volume_V < 0) volume_V = 0;
                glavna_skripta.audio_source1_1.PlayOneShot(glavna_skripta.kick1, volume_V);
                Debug.Log("box  " + volume_V);
                StartCoroutine(zvuk_delay());
            }

        }
    }

    public void object_touching_enemy(PolygonCollider2D object_collider)
    {
        if (zvuk_zid_moze)
        {
            float neki_broj = 7;
            if (transform.GetComponent<CircleCollider2D>().IsTouching(object_collider))
            {
                float volume_V = 0;
                if (transform.GetComponent<Rigidbody2D>().velocity.x > transform.GetComponent<Rigidbody2D>().velocity.y
                    || transform.GetComponent<Rigidbody2D>().velocity.x > -transform.GetComponent<Rigidbody2D>().velocity.y
                    || -transform.GetComponent<Rigidbody2D>().velocity.x > transform.GetComponent<Rigidbody2D>().velocity.y
                    || -transform.GetComponent<Rigidbody2D>().velocity.x > -transform.GetComponent<Rigidbody2D>().velocity.y
                    )
                {
                    if (transform.GetComponent<Rigidbody2D>().velocity.x > 0) volume_V = (transform.GetComponent<Rigidbody2D>().velocity.x / neki_broj);
                    else volume_V = (-transform.GetComponent<Rigidbody2D>().velocity.x / neki_broj);
                }
                if (transform.GetComponent<Rigidbody2D>().velocity.y > transform.GetComponent<Rigidbody2D>().velocity.x
                   || transform.GetComponent<Rigidbody2D>().velocity.y > -transform.GetComponent<Rigidbody2D>().velocity.x
                   || -transform.GetComponent<Rigidbody2D>().velocity.y > transform.GetComponent<Rigidbody2D>().velocity.x
                   || -transform.GetComponent<Rigidbody2D>().velocity.y > -transform.GetComponent<Rigidbody2D>().velocity.x
                   )
                {
                    if (transform.GetComponent<Rigidbody2D>().velocity.y > 0) volume_V = (transform.GetComponent<Rigidbody2D>().velocity.y / neki_broj);
                    else volume_V = (-transform.GetComponent<Rigidbody2D>().velocity.y / neki_broj);
                }

                if (volume_V > 1) volume_V = 1; if (volume_V < 0) volume_V = 0;
                glavna_skripta.audio_source1_1.PlayOneShot(glavna_skripta.kick1, volume_V);
                Debug.Log("katapult  " + volume_V);
                StartCoroutine(zvuk_delay());
            }

        }
    }



    //do maina
}
