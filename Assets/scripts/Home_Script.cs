using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home_Script : MonoBehaviour {

    public GameObject pocetni_canvas;
    public GameObject levels_panel;
    private load_all_levels load_all_levels;
    public Image fadeinout;
    public AudioSource home_audiosource;
    public AudioClip button_click;


    void Start () {
        fadeout();
        pocetni_canvas.transform.position -= new Vector3(0, 1, 0);
        pocetni_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, -15, 0);

        load_all_levels = levels_panel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<load_all_levels>();
    }
	
	void Update () {
        if (pocetni_canvas.transform.position.y < 718f || pocetni_canvas.transform.position.y > 727f) { pocetni_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); }
    }

    public void start_button()
    {
        button_click_zvuk();
        pocetni_canvas.transform.position += new Vector3(0, 1, 0);
        pocetni_canvas.GetComponent<Rigidbody>().velocity = new Vector3(0, 15, 0);
        StartCoroutine(load_scene());
    }

    IEnumerator load_scene()
    {
        fadein();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }

    public void levels_button()
    {
        button_click_zvuk();
        levels_panel.SetActive(true);
        load_all_levels.pokreni_ucitavanje();
    }

    public void close_levels_panel()
    {
        button_click_zvuk();
        levels_panel.SetActive(false);
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

    public void settings_button()
    {
        button_click_zvuk();
    }

    public void store_button()
    {
        button_click_zvuk();
    }

    public void ratethegame_button()
    {
        button_click_zvuk();
    }

    public void button_click_zvuk()
    {
        home_audiosource.PlayOneShot(button_click, 1);
    }









}
