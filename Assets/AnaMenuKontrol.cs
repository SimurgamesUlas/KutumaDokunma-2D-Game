using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnaMenuKontrol : MonoBehaviour
{
    public GameObject ilkPanel;
    public GameObject İkinciPanel;
    public TMP_InputField KullaniciAdi;
    public TMP_Text VarOlanKullaniciAdi;
    public TextMeshProUGUI[] İstatistik;
    public TextMeshProUGUI ServerBilgi;

    GameObject RandomGiris;
    GameObject OdaKurVeGiris;
    // Start is called before the first frame update
    void Start()
    {
       
        if (!PlayerPrefs.HasKey("KullaniciAdiVarmi"))
        {
            PlayerPrefs.SetInt("ToplamMac", 0);
            PlayerPrefs.SetInt("Maglubiyet", 0);
            PlayerPrefs.SetInt("Galibiyet", 0);
            PlayerPrefs.SetInt("ToplamPuan", 0);
            ilkPanel.SetActive(true);
            DegerleriYaz();
        }
        else
        {
            İkinciPanel.SetActive(true);
            VarOlanKullaniciAdi.text = PlayerPrefs.GetString("KullaniciAdiVarmi");
            DegerleriYaz();
        }
    }

    public void KullaniciAdiKaydet()
    {
       
        PlayerPrefs.SetString("KullaniciAdiVarmi", KullaniciAdi.text);
        ilkPanel.SetActive(false);
        İkinciPanel.SetActive(true);
        RandomGiris = GameObject.FindWithTag("RandomGirisYap");
        OdaKurVeGiris = GameObject.FindWithTag("OdaKurVeGir");
        VarOlanKullaniciAdi.text = KullaniciAdi.text;
        RandomGiris.GetComponent<Button>().interactable = true;
        OdaKurVeGiris.GetComponent<Button>().interactable = true;
    }
    void DegerleriYaz()
    {
        İstatistik[0].text = PlayerPrefs.GetInt("ToplamMac").ToString();
        İstatistik[1].text = PlayerPrefs.GetInt("Maglubiyet").ToString();
        İstatistik[2].text = PlayerPrefs.GetInt("Galibiyet").ToString();
        İstatistik[3].text = PlayerPrefs.GetInt("ToplamPuan").ToString();
    }
}
