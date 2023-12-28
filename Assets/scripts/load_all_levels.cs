using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class load_all_levels : MonoBehaviour
{
    public GameObject level_set_inst;
    private int broj_levela = 1;
    private bool loaduj_levele = true;
    public Sprite kljuc;



    void Start()
    {
        //level_set_inst = transform.GetChild(0).gameObject;

        /*
        if (PlayerPrefs.GetInt("preden_level_1") == 1) transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true); else transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("preden_level_1") == 0) transform.GetChild(0).transform.GetChild(0).position += new Vector3(0, -10, 0);
        transform.GetChild(0).transform.GetChild(2).position += new Vector3(-9.5f, 0, 0);
        transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        */

        /*
        for (int a = 0; a < 10; a++)
        {
            PlayerPrefs.SetInt("level_aktivan_" + a.ToString(), 0);
        }
        
        PlayerPrefs.SetInt("preden_level_5", 0);
        PlayerPrefs.SetInt("level_ima_bonove_5", 0);
        PlayerPrefs.SetInt("skupljeni_svi_coinsi_5", 0);
        */

        //Debug.Log( PlayerPrefs.GetInt("level_aktivan_6") );

        

    }

    public void pokreni_ucitavanje()
    {
        for (int a = 2; a < 100; a++)
        {
            PlayerPrefs.SetInt("level_aktivan_" + a.ToString(), 0);
        }

        int izvidac_int = 1;
        while (PlayerPrefs.GetInt("level_aktivan_" + izvidac_int.ToString()) != 0) izvidac_int++;
        Debug.Log(izvidac_int);

        for (int b = izvidac_int; b != 0; b--)
        {
            stvori_level_button();
        }
    }



    void stvori_level_button()
    {

        if (PlayerPrefs.GetInt("level_aktivan_" + broj_levela.ToString()) == 1 && loaduj_levele)
        {
            GameObject novi;
            novi = Instantiate(level_set_inst);
            novi.transform.SetParent(transform);
            novi.transform.localScale = new Vector3(1, 1, 1);
            novi.transform.GetChild(0).gameObject.GetComponent<Text>().text = broj_levela.ToString();
            if (PlayerPrefs.GetInt("skupljeni_svi_coinsi_" + broj_levela.ToString()) == 1) novi.transform.GetChild(1).gameObject.SetActive(true); else novi.transform.GetChild(1).gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("preden_level_" + broj_levela.ToString()) == 1) novi.transform.GetChild(2).gameObject.SetActive(true); else novi.transform.GetChild(2).gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("skupljeni_svi_coinsi_" + broj_levela.ToString()) != 1 && PlayerPrefs.GetInt("preden_level_" + broj_levela.ToString()) != 1) novi.transform.GetChild(0).position += new Vector3(0, -10, 0);
            if (PlayerPrefs.GetInt("preden_level_" + broj_levela.ToString()) == 1 && PlayerPrefs.GetInt("level_ima_bonove_" + broj_levela.ToString()) == 0) novi.transform.GetChild(2).position += new Vector3(-9.5f, 0, 0);
            if (PlayerPrefs.GetInt("skupljeni_svi_coinsi_" + broj_levela.ToString()) == 0 && PlayerPrefs.GetInt("level_ima_bonove_" + broj_levela.ToString()) == 1)
            {
                novi.transform.GetChild(1).gameObject.SetActive(true);
                novi.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
            }

            if (PlayerPrefs.GetInt("level_aktivan_" + (broj_levela+1).ToString() ) == 0)
            {
                loaduj_levele = false;
                GameObject novi_;
                novi_ = Instantiate(level_set_inst);
                novi_.transform.SetParent(transform);
                novi_.transform.localScale = new Vector3(1, 1, 1);
                //sprite kljucic i iskljuci zvijezdice
                novi_.GetComponent<Image>().sprite = kljuc;
                novi_.transform.GetChild(0).gameObject.SetActive(false);
                novi_.transform.GetChild(1).gameObject.SetActive(false);
                novi_.transform.GetChild(2).gameObject.SetActive(false);

            }

            broj_levela++;
        }


        
    }




















}
