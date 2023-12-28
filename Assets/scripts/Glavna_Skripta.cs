using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Glavna_Skripta : MonoBehaviour {

    //public GameObject glavni_prefabs;
    public GameObject katapult, katapult_gl;
    public GameObject glavni_obj;
    public GameObject[] glavni;
    public GameObject[] katapulti_svi;
    private Touch prvi_dodir;
    Vector2 razlika_vec2;
    float jacina_ld;
    bool ne_ponavljaj = true; // SJETI SE OVOGA****** STAVI KAO TRUE AKO SE PONOVI SCENE REPEAT I SL 
    public float jacina_ispaljivanja01, jacina_ispaljivanja;
    private Transform parent;
    public BoxCollider2D lijevo, desno, gore, dole; 

    public GameObject nextlevel_canvas, repeat_canvas;
    //private GameObject zvijezda1, zvijezda2, zvijezda3;
    private GameObject finish_collider, miganje;
    public bool zvijezde_skupljene, repeat_dole, nextlevel_dole, samo_jednom, enemy_touched;
    public bool katapult_ispaljen = false;
    public int broj_bonova = 0;
    public int broj_zvijezda = 0;
    public int vrijednost_bonova = 0;
    public int skupljeni_bonovi = 0;
    //                       LEVEL            1  2  3  4  5  6  7  8  9
    //private int[] broj_bonova_levela =   { 0, 0, 1, 1 };
    //private int[] broj_zvijezda_levela = { 0, 2, 1, 1 };
    public int suma_levela;
    public int skinute_zvijezde;

    public float vrijeme_mirovanja_glavnog = 0;
    public GameObject done_text, repeat_next_level, enemy_touched_prikaz, zvijezde_repeat_prikaz;
    public Text skinute_zv_prikaz, dobijeni_coinsi_prikaz, skinute_zv_prikaz_repeat;
    public Image fadeinout;

    //zvukovi
    public AudioSource audio_source1;
    public AudioSource audio_source1_1;
    public AudioSource audio_source2;
    public AudioSource audio_source3;
    public AudioSource audio_source4;
    public AudioSource audio_source5;
    public AudioSource audio_source6;
    public AudioClip bon_pokupljen;
    public AudioClip button_click;
    public AudioClip coinsi;
    public AudioClip enemy_puko;
    public AudioClip katapult_brzo;
    public AudioClip katapult_sporo;
    public AudioClip kick1;
    public AudioClip kick2;
    public AudioClip ouch_enemy;
    public AudioClip wiii_glavni;
    public AudioClip zvijezda_pokupljena;
    public AudioClip theme_sound;
    public AudioClip konopac;
    public AudioClip none;
    //

    public bool pc = true;
    private bool zvuk_zid_moze = true;
    public float bon_shake_koef = 20;

    private void Start()
    {
        fadeout();
        audio_source6.Play();
        //broj_bonova = broj_bonova_levela[SceneManager.GetActiveScene().buildIndex];
        //broj_zvijezda = broj_zvijezda_levela[SceneManager.GetActiveScene().buildIndex];

        //glavni[0] = GameObject.Find("/Assets/Animations/glavni/glavni1");
        //glavni[0] = Resources.Load("Animations/glavni/glavni1.prefab") as GameObject;
        //glavni[0] = Resources.Load<GameObject>("Animations/glavni/glavni_prefabs.prefab");

        //katapult = GameObject.Find("katapult");
        katapult = Instantiate(katapulti_svi[PlayerPrefs.GetInt("koji_se_katapult_koristi")]);
        katapult_gl = katapult.transform.GetChild(0).gameObject;

        //samo_jednom_home = true;
        nextlevel_canvas = GameObject.Find("nextlevel_canvas");
        repeat_canvas = GameObject.Find("repeat_canvas");
        //pocetni_canvas = GameObject.Find("pocetni_canvas");
        //if (SceneManager.GetActiveScene().buildIndex == 0) { home_active = true; } else { home_active = false; }
        /*if (samo_jednom_home && home_active)
        {
            pocetni_canvas.transform.position -= new Vector3(0, 1, 0);
            pocetni_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, -10, 0);
            samo_jednom_home = false;
        }
        */
        //zvijezda1 = GameObject.FindGameObjectsWithTag("zv1")[0];
        //if (broj_zvijezda == 2) zvijezda2 = GameObject.FindGameObjectsWithTag("zv2")[0];
        //if (broj_zvijezda == 3) zvijezda3 = GameObject.FindGameObjectsWithTag("zv3")[0];
        finish_collider = GameObject.Find("finish_collider");
        miganje = GameObject.Find("miganje");
        glavni_obj = Instantiate(glavni[0]) as GameObject;
        glavni_obj.transform.SetParent(katapult_gl.transform);
        glavni_obj.GetComponent<Rigidbody2D>().simulated = false;

        if (PlayerPrefs.GetInt("preden_level_" + koji_level_int().ToString()) != 1) PlayerPrefs.SetInt("preden_level_" + koji_level_int().ToString(), 0);
        if (PlayerPrefs.GetInt("preden_level_" + koji_level_int().ToString()) != 1) PlayerPrefs.SetInt(("skupljena_suma_levela_" + koji_level_int().ToString()), 0);
        //if (PlayerPrefs.GetInt(("level_aktivan_" + koji_level_int().ToString())) != 1) 
        PlayerPrefs.SetInt(("level_aktivan_" + koji_level_int().ToString()), 1);


    }

    private void Update()
    {
        //Debug.Log(enemy_touched);
        //if (glavni_obj.GetComponent<CircleCollider2D>().IsTouching(zvijezda1.GetComponent<CircleCollider2D>())) zvijezda_dodirnuta(zvijezda1);
        //if (broj_zvijezda == 2) if (glavni_obj.GetComponent<CircleCollider2D>().IsTouching(zvijezda2.GetComponent<CircleCollider2D>())) zvijezda_dodirnuta(zvijezda2);
        //if (broj_zvijezda == 3) if (glavni_obj.GetComponent<CircleCollider2D>().IsTouching(zvijezda3.GetComponent<CircleCollider2D>())) zvijezda_dodirnuta(zvijezda3);
        if (glavni_obj.GetComponent<CircleCollider2D>().IsTouching(finish_collider.GetComponent<BoxCollider2D>())) dodirnut_finish_collider();

        //if (SceneManager.GetActiveScene().name == home) Debug.Log("homeee");
        //Debug.Log(SceneManager.GetActiveScene().name);

        if (!nextlevel_dole && !repeat_dole && !katapult_ispaljen)
        {
            //android upravljanje
            if (!pc) {
                if (Input.GetTouch(0).phase == TouchPhase.Began && ne_ponavljaj) { prvi_dodir = Input.GetTouch(0); ne_ponavljaj = false; }
                if (Input.GetTouch(0).phase == TouchPhase.Began && !ne_ponavljaj) { prvi_dodir.position = new Vector2(Input.touches[0].position.x + jacina_ld * 200, Input.touches[0].position.y); }
                razlika_vec2 = new Vector2(Input.touches[0].position.x - prvi_dodir.position.x, Input.touches[0].position.y - prvi_dodir.position.y);
                if (razlika_vec2.x < 200 && razlika_vec2.x > 0) { jacina_ld = -razlika_vec2.x * 1 / 200; }
                if (razlika_vec2.x > -200 && razlika_vec2.x < 0) { jacina_ld = -razlika_vec2.x * 1 / 200; }
                katapult.transform.eulerAngles = new Vector3(0, 0, jacina_ld * 45 / 1);
                if (razlika_vec2.x < -200) { prvi_dodir.position += new Vector2(razlika_vec2.x + 200, 0); }
                if (razlika_vec2.x > 200) { prvi_dodir.position += new Vector2(razlika_vec2.x - 200, 0); }

                if (razlika_vec2.y > 0) { prvi_dodir.position += new Vector2(0, razlika_vec2.y); }
                if (razlika_vec2.y < -100) { prvi_dodir.position += new Vector2(0, razlika_vec2.y + 100); }
                if (razlika_vec2.y > 0) jacina_ispaljivanja01 = 0;
                else if (razlika_vec2.y < -100) jacina_ispaljivanja01 = 1;
                else { jacina_ispaljivanja01 = 0.01f * -razlika_vec2.y; }
                jacina_ispaljivanja = 400 + (jacina_ispaljivanja01 * 600);

                if (jacina_ispaljivanja > 400 && Input.GetTouch(0).phase == TouchPhase.Ended) ispali();
                katapult_gl.transform.localPosition = new Vector3(0, 0.9f + jacina_ispaljivanja01 * -0.6f, 0);

            }

            if (pc)
            { 
                //pc upravljanje
                if (Input.GetKey(KeyCode.W))
                {
                    if (jacina_ispaljivanja01 > 0.01f) jacina_ispaljivanja01 -= 0.05f;
                }
                
                if (Input.GetKey(KeyCode.S))
                {
                    if (jacina_ispaljivanja01 < 0.99f) jacina_ispaljivanja01 += 0.05f;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    if (jacina_ld > -0.99f) jacina_ld -= 0.05f;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    if (jacina_ld < 0.99f) jacina_ld += 0.05f;
                }

                


                if (Input.GetKey(KeyCode.F))
                {
                    if (jacina_ispaljivanja01 > 0.05f) ispali();
                }

                jacina_ispaljivanja = 400 + (jacina_ispaljivanja01 * 600);
                katapult.transform.eulerAngles = new Vector3(0, 0, jacina_ld * 45 / 1);
                katapult_gl.transform.localPosition = new Vector3(0, 0.9f + jacina_ispaljivanja01 * -0.6f, 0);




            }
            
        }


        if (Input.GetKey(KeyCode.R) && pc)
        {
            SceneManager.LoadScene(koji_level_int());
        }

        if ((broj_zvijezda - skinute_zvijezde) == 0) zvijezde_skupljene = true;


        if (zvuk_zid_moze)
        {
            float neki_broj = 7;
            if (glavni_obj.GetComponent<CircleCollider2D>().IsTouching(lijevo))
            {
                float volume_V = (glavni_obj.GetComponent<Rigidbody2D>().velocity.x / neki_broj);
                if (volume_V > 1) volume_V = 1; if (volume_V < 0) volume_V = 0;
                audio_source1.PlayOneShot(kick1, volume_V);
                Debug.Log("lijevo  " + volume_V);
                StartCoroutine(zvuk_delay());
            }
            if (glavni_obj.GetComponent<CircleCollider2D>().IsTouching(desno))
            {
                float volume_V = (-glavni_obj.GetComponent<Rigidbody2D>().velocity.x / neki_broj);
                if (volume_V > 1) volume_V = 1; if (volume_V < 0) volume_V = 0;
                audio_source1.PlayOneShot(kick1, volume_V);
                Debug.Log("desno  " + volume_V);
                StartCoroutine(zvuk_delay());
            }
            if (glavni_obj.GetComponent<CircleCollider2D>().IsTouching(gore))
            {
                float volume_V = (-glavni_obj.GetComponent<Rigidbody2D>().velocity.y / neki_broj);
                if (volume_V > 1) volume_V = 1; if (volume_V < 0) volume_V = 0;
                audio_source1.PlayOneShot(kick1, volume_V);
                Debug.Log("gore  " + volume_V);
                StartCoroutine(zvuk_delay());
            }
            if (glavni_obj.GetComponent<CircleCollider2D>().IsTouching(dole))
            {
                float volume_V = (glavni_obj.GetComponent<Rigidbody2D>().velocity.y / neki_broj);
                if (volume_V > 1) volume_V = 1; if (volume_V < 0) volume_V = 0;
                audio_source1.PlayOneShot(kick1, volume_V);
                Debug.Log("dole  " + volume_V);
                StartCoroutine(zvuk_delay());
            }

        }

        //Debug.Log((-glavni_obj.GetComponent<Rigidbody2D>().velocity.x / neki_broj));
        if (glavni_obj.transform.position.y > 1)
        {
            katapult_gl.GetComponent<Rigidbody2D>().simulated = true;
            katapult_gl.GetComponent<Rigidbody2D>().isKinematic = true;
        }

    }
    private void FixedUpdate()
    {
        //pocetni_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //Debug.Log(pocetni_canvas.transform.position.y < 718f);
        if (nextlevel_canvas.transform.position.y < 718f || nextlevel_canvas.transform.position.y > 727f ) { nextlevel_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); }
        if (repeat_canvas.transform.position.y < 718f || repeat_canvas.transform.position.y > 727f) { repeat_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); }
        //if (pocetni_canvas.transform.position.y < 718f || pocetni_canvas.transform.position.y > 727f) { pocetni_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); }

        if (glavni_obj.GetComponent<Rigidbody2D>().velocity != new Vector2(0, 0)) vrijeme_mirovanja_glavnog = 0;
        else vrijeme_mirovanja_glavnog += 0.02f;
        if (vrijeme_mirovanja_glavnog > 1 && katapult_ispaljen) dodirnut_finish_collider();

    }

    void ispali ()
    {
        audio_source4.PlayOneShot(katapult_brzo, 1);
        audio_source5.PlayOneShot(wiii_glavni, 1);
        vrijeme_mirovanja_glavnog = 0;
        katapult_ispaljen = true;
        samo_jednom = true;
        jacina_ispaljivanja01 = 0;
        glavni_obj.transform.SetParent(parent);
        glavni_obj.GetComponent<Rigidbody2D>().simulated = true;
        glavni_obj.GetComponent<Rigidbody2D>().AddForce(katapult_gl.transform.up * jacina_ispaljivanja);
    }

    /*
    private void zvijezda_dodirnuta(GameObject koja_zvijezda)
    {
        koja_zvijezda.SetActive(false);
        skinute_zvijezde++;
    }
    */

    private void dodirnut_finish_collider()
    {
        if (enemy_touched) zvijezde_skupljene = false;
        if (zvijezde_skupljene && samo_jednom)
        {
            //
            if (broj_bonova == 0) PlayerPrefs.SetInt(("level_ima_bonove_" + koji_level_int().ToString()), 0);
            else PlayerPrefs.SetInt(("level_ima_bonove_" + koji_level_int().ToString()), 1);
            //if (PlayerPrefs.GetInt(("level_aktivan_" + (koji_level_int() + 1).ToString())) != 1) PlayerPrefs.SetInt(("level_aktivan_" + (koji_level_int() + 1).ToString()), 0);
            //
            if (PlayerPrefs.GetInt("preden_level_" + koji_level_int().ToString()) != 1) PlayerPrefs.SetInt("preden_level_" + koji_level_int().ToString(), 1);
            //
            int vec_uzeti_coinsi = PlayerPrefs.GetInt(("skupljena_suma_levela_" + koji_level_int().ToString()));
            if (vrijednost_bonova > vec_uzeti_coinsi)
            {
                PlayerPrefs.SetInt(("skupljena_suma_levela_" + koji_level_int().ToString()), vec_uzeti_coinsi + (vrijednost_bonova - vec_uzeti_coinsi));
                Debug.Log("dodano coinsa : " + vec_uzeti_coinsi + (vrijednost_bonova - vec_uzeti_coinsi));
            }
            //
            if (broj_bonova - skupljeni_bonovi < 1) 
            { 
                done_text.SetActive(true); 
                repeat_next_level.SetActive(false); 
                PlayerPrefs.SetInt(("skupljena_suma_levela_" + koji_level_int().ToString()), suma_levela);
                PlayerPrefs.SetInt(("skupljeni_svi_coinsi_" + koji_level_int().ToString()), 1);
            }
            else { 
                done_text.SetActive(false); 
                repeat_next_level.SetActive(true); 
            }
            //skinute_zv_prikaz, dobijeni_coinsi_prikaz;
            skinute_zv_prikaz.text = skinute_zvijezde.ToString() + "/" + broj_zvijezda.ToString();
            dobijeni_coinsi_prikaz.text = vrijednost_bonova.ToString();
            //PlayerPrefs.SetInt("Level", (PlayerPrefs.GetInt("Level", 0) + 1));
            nextlevel_dole = true;
            nextlevel_canvas.transform.position -= new Vector3(0, 1, 0);
            nextlevel_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, -15, 0);
            audio_source3.PlayOneShot(konopac, 0.5f);
            StartCoroutine(konopac_zvuk_stop());
            samo_jednom = false;
        }
        if (!zvijezde_skupljene && samo_jednom)
        {
            //enemy_touched_prikaz, zvijezde_repeat_prikaz;
            if (enemy_touched) { enemy_touched_prikaz.SetActive(true); zvijezde_repeat_prikaz.SetActive(false); }
            else { enemy_touched_prikaz.SetActive(false); zvijezde_repeat_prikaz.SetActive(true); }
            skinute_zv_prikaz_repeat.text = skinute_zvijezde.ToString() + "/" + broj_zvijezda.ToString();
            repeat_canvas.transform.position -= new Vector3(0, 1, 0);
            repeat_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, -15, 0);
            audio_source3.PlayOneShot(konopac, 0.5f);
            StartCoroutine(konopac_zvuk_stop());
            repeat_dole = true;
            samo_jednom = false;
        }   
            
    }

    public void start_button ()
    {
        button_click_zvuk();
        nextlevel_canvas.transform.position += new Vector3(0, 1, 0);
        nextlevel_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, 15, 0);
        nextlevel_dole = false;

        int vec_uzeti_coinsi = PlayerPrefs.GetInt(("skupljena_suma_levela_" + koji_level_int().ToString()));
        if (vrijednost_bonova > vec_uzeti_coinsi)
        {
            PlayerPrefs.SetInt(("skupljena_suma_levela_" + koji_level_int().ToString()), vec_uzeti_coinsi + (vrijednost_bonova - vec_uzeti_coinsi));
        }
        //PlayerPrefs.SetInt(("skupljena_suma_levela_" + koji_level_int().ToString()), suma_levela);



        StartCoroutine(load_scene());

    }

    public void repeat_button ()
    {
        button_click_zvuk();
        if (repeat_dole) { repeat_canvas.transform.position += new Vector3(0, 1, 0);
            repeat_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, 15, 0);
            repeat_dole = false;
        }
        if (nextlevel_dole)
        {
            int vec_uzeti_coinsi = PlayerPrefs.GetInt(("skupljena_suma_levela_" + koji_level_int().ToString()));
            if (vrijednost_bonova > vec_uzeti_coinsi)
            {
                PlayerPrefs.SetInt(("skupljena_suma_levela_" + koji_level_int().ToString()), vec_uzeti_coinsi + (vrijednost_bonova - vec_uzeti_coinsi));
            }
        }
        StartCoroutine(load_scene());
    }
    
    IEnumerator load_scene()
    {
        fadein();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
    
    IEnumerator load_home_scene()
    {
        if (nextlevel_dole)
        {
            nextlevel_canvas.transform.position += new Vector3(0, 1, 0);
            nextlevel_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, 15, 0);
            nextlevel_dole = false;
        }
        if (repeat_dole)
        {
            repeat_canvas.transform.position += new Vector3(0, 1, 0);
            repeat_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, 15, 0);
            repeat_dole = false;
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
        /*pocetni_canvas = GameObject.Find("pocetni_canvas");
        yield return new WaitForSeconds(0.1f);
        pocetni_canvas.transform.position -= new Vector3(0, 1, 0);
        pocetni_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, -10, 0);
        */
    }

    public void home_button ()
    {
        button_click_zvuk();
        StartCoroutine(load_home_scene());
    }

    

    public int koji_level_int()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void fadein()
    {
        fadeinout.enabled = true;
        fadeinout.canvasRenderer.SetAlpha(0);
        fadeinout.CrossFadeAlpha(1, 0.5f, false);
    }
    public void fadeout()
    {
        fadeinout.enabled = true;
        fadeinout.canvasRenderer.SetAlpha(1);
        fadeinout.CrossFadeAlpha(0, 0.5f, false);
        StartCoroutine(ugasi_fadeinout());
    }
    IEnumerator ugasi_fadeinout()
    {
        yield return new WaitForSeconds(0.5f);
        fadeinout.enabled = false;
    }

    public void button_click_zvuk()
    {
        audio_source1.PlayOneShot(button_click, 1);
    }

    public void bon_pokupljen_zvuk()
    {
        audio_source3.PlayOneShot(bon_pokupljen, 1);
        audio_source2.PlayOneShot(coinsi, 1);
    }

    public void zvijezda_pokupljen_zvuk()
    {
        audio_source3.PlayOneShot(zvijezda_pokupljena, 1);
    }

    public void enemy_touched_zvuk()
    {
        audio_source3.PlayOneShot(ouch_enemy, 1);
    }

    public void enemy_puko_zvuk()
    {
        audio_source3.PlayOneShot(enemy_puko, 1);
    }

    private IEnumerator zvuk_delay()
    {
        zvuk_zid_moze = false;
        yield return new WaitForSeconds(0.1f);
        zvuk_zid_moze = true;
    }

    private IEnumerator konopac_zvuk_stop()
    {
        yield return new WaitForSeconds(0.5f);
        audio_source3.Stop(); 
        audio_source3.PlayOneShot(none);
    }

    public void object_touching(BoxCollider2D object_collider)
    {
        if (zvuk_zid_moze) 
        { 
            float neki_broj = 7;
            if (glavni_obj.GetComponent<CircleCollider2D>().IsTouching(object_collider))
            {
                float volume_V = 0;
                if (glavni_obj.GetComponent<Rigidbody2D>().velocity.x > glavni_obj.GetComponent<Rigidbody2D>().velocity.y 
                    || glavni_obj.GetComponent<Rigidbody2D>().velocity.x > -glavni_obj.GetComponent<Rigidbody2D>().velocity.y
                    || -glavni_obj.GetComponent<Rigidbody2D>().velocity.x > glavni_obj.GetComponent<Rigidbody2D>().velocity.y
                    || -glavni_obj.GetComponent<Rigidbody2D>().velocity.x > -glavni_obj.GetComponent<Rigidbody2D>().velocity.y
                    )
                {
                    if (glavni_obj.GetComponent<Rigidbody2D>().velocity.x > 0) volume_V = (glavni_obj.GetComponent<Rigidbody2D>().velocity.x / neki_broj);
                    else volume_V = (-glavni_obj.GetComponent<Rigidbody2D>().velocity.x / neki_broj);
                }
                if(glavni_obj.GetComponent<Rigidbody2D>().velocity.y > glavni_obj.GetComponent<Rigidbody2D>().velocity.x 
                   || glavni_obj.GetComponent<Rigidbody2D>().velocity.y > -glavni_obj.GetComponent<Rigidbody2D>().velocity.x
                   || -glavni_obj.GetComponent<Rigidbody2D>().velocity.y > glavni_obj.GetComponent<Rigidbody2D>().velocity.x
                   || -glavni_obj.GetComponent<Rigidbody2D>().velocity.y > -glavni_obj.GetComponent<Rigidbody2D>().velocity.x
                   )
                {
                    if (glavni_obj.GetComponent<Rigidbody2D>().velocity.y > 0) volume_V = (glavni_obj.GetComponent<Rigidbody2D>().velocity.y / neki_broj);
                    else volume_V = (-glavni_obj.GetComponent<Rigidbody2D>().velocity.y / neki_broj);
                }

                if (volume_V > 1) volume_V = 1; if (volume_V < 0) volume_V = 0;
                audio_source1.PlayOneShot(kick1, volume_V);
                Debug.Log("box  " + volume_V);
                StartCoroutine(zvuk_delay());
            }

        }
    }

    public void object_touching(PolygonCollider2D object_collider)
    {
        if (zvuk_zid_moze)
        {
            float neki_broj = 7;
            if (glavni_obj.GetComponent<CircleCollider2D>().IsTouching(object_collider))
            {
                float volume_V = 0;
                if (glavni_obj.GetComponent<Rigidbody2D>().velocity.x > glavni_obj.GetComponent<Rigidbody2D>().velocity.y
                    || glavni_obj.GetComponent<Rigidbody2D>().velocity.x > -glavni_obj.GetComponent<Rigidbody2D>().velocity.y
                    || -glavni_obj.GetComponent<Rigidbody2D>().velocity.x > glavni_obj.GetComponent<Rigidbody2D>().velocity.y
                    || -glavni_obj.GetComponent<Rigidbody2D>().velocity.x > -glavni_obj.GetComponent<Rigidbody2D>().velocity.y
                    )
                {
                    if (glavni_obj.GetComponent<Rigidbody2D>().velocity.x > 0) volume_V = (glavni_obj.GetComponent<Rigidbody2D>().velocity.x / neki_broj);
                    else volume_V = (-glavni_obj.GetComponent<Rigidbody2D>().velocity.x / neki_broj);
                }
                if (glavni_obj.GetComponent<Rigidbody2D>().velocity.y > glavni_obj.GetComponent<Rigidbody2D>().velocity.x
                   || glavni_obj.GetComponent<Rigidbody2D>().velocity.y > -glavni_obj.GetComponent<Rigidbody2D>().velocity.x
                   || -glavni_obj.GetComponent<Rigidbody2D>().velocity.y > glavni_obj.GetComponent<Rigidbody2D>().velocity.x
                   || -glavni_obj.GetComponent<Rigidbody2D>().velocity.y > -glavni_obj.GetComponent<Rigidbody2D>().velocity.x
                   )
                {
                    if (glavni_obj.GetComponent<Rigidbody2D>().velocity.y > 0) volume_V = (glavni_obj.GetComponent<Rigidbody2D>().velocity.y / neki_broj);
                    else volume_V = (-glavni_obj.GetComponent<Rigidbody2D>().velocity.y / neki_broj);
                }

                if (volume_V > 1) volume_V = 1; if (volume_V < 0) volume_V = 0;
                audio_source1.PlayOneShot(kick1, volume_V);
                Debug.Log("katapult  " + volume_V);
                StartCoroutine(zvuk_delay());
            }

        }
    }

    



    // od maina
}
