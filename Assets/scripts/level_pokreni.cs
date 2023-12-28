using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class level_pokreni : MonoBehaviour
{
    private GameObject glavni_obj;
    private Home_Script home_script;
    void Start()
    {
        glavni_obj = GameObject.Find("glavna_skripta_obj");
        home_script = glavni_obj.GetComponent<Home_Script>();

        gameObject.GetComponent<Button>().onClick.AddListener(pokreni_level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void pokreni_level()
    {
        StartCoroutine(load_scene());
    }

    IEnumerator load_scene()
    {
        
        string level = transform.GetChild(0).gameObject.GetComponent<Text>().text;
        if (PlayerPrefs.GetInt("preden_level_" + level) != 1 && PlayerPrefs.GetInt("skupljeni_svi_coinsi_" + level) != 1 && PlayerPrefs.GetInt("level_ima_bonove_" + level) == 1) 
        {
            home_script.fadein();
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(int.Parse(level)); 
        }
        if (PlayerPrefs.GetInt("preden_level_" + level) != 1 && PlayerPrefs.GetInt("level_ima_bonove_" + level) == 0)
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(int.Parse(level));
        }
        //else ispisi it's done

    }

    




}
