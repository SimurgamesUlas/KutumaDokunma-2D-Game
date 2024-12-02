using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Oyuncu : MonoBehaviour
{

    public GameObject Top;
    public GameObject TopCikisNoktasi;
    public ParticleSystem TopAtisEfekt;
    public AudioSource TopAtmaSesi;
    float AtisYonu;
    [Header("Güç Barı Ayarları")]
    public Image PowerBar;
    float PowerSayi;
    bool SonaGeldiMi = false;
    bool AtisYapiliyorMu = false;
    Coroutine PowerDongu;
    PhotonView pw;
    bool OyuncuKontrol = false;
    
    void Start()
    {
       
        pw = GetComponent<PhotonView>();


        if (pw.IsMine) 
        {
            PowerBar = GameObject.FindWithTag("PowerBar").GetComponent<Image>();
            if (PhotonNetwork.IsMasterClient) 
            {
                //gameObject.tag = "Oyuncu1";
                transform.position = GameObject.FindWithTag("OlusacakNokta1").transform.position;
                transform.rotation = GameObject.FindWithTag("OlusacakNokta1").transform.rotation;
                AtisYonu = 2f;
      
            }
            else
            {
               // gameObject.tag = "Oyuncu2";
                transform.position = GameObject.FindWithTag("OlusacakNokta2").transform.position;
                transform.rotation = GameObject.FindWithTag("OlusacakNokta2").transform.rotation;
                AtisYonu = -2f;
               
            }
        }
        Debug.Log("Oyuncu Tag: " + gameObject.tag);

        InvokeRepeating("OyunBasladiMi", 1f,.2f);
    }
 

    public void OyunBasladiMi()
    {
        if(PhotonNetwork.PlayerList.Length == 2)
        {
          
            if (pw.IsMine)
            {
              
                PowerDongu = StartCoroutine(PowerBarCalistir());
                CancelInvoke("OyunBasladiMi");

            }
            else
            {
                StopAllCoroutines();
            }
          
        }
      
    }
    IEnumerator PowerBarCalistir()
    {
        PowerBar.fillAmount = 0;
        SonaGeldiMi = false;
        while (true)
        {
            if(PowerBar.fillAmount < 1 && !SonaGeldiMi)
            {
                PowerSayi = 0.01f;
                PowerBar.fillAmount += PowerSayi;
                yield return new WaitForSeconds(0.001f);
            }
            else
            {
                SonaGeldiMi = true;
                PowerSayi = 0.01f;
                PowerBar.fillAmount -= PowerSayi;
                yield return new WaitForSeconds(0.001f);
                if(PowerBar.fillAmount == 0)
                {
                    SonaGeldiMi = false;
                }
            }
        }
    }
    
    void Update()
    {
        if (pw.IsMine)
        {
            if (Input.touchCount > 0 && !AtisYapiliyorMu && PhotonNetwork.PlayerList.Length == 2)
            {
                PhotonNetwork.Instantiate("PatlamaEfekt", TopCikisNoktasi.transform.position, TopCikisNoktasi.transform.rotation, 0, null);
                TopAtmaSesi.Play();
                GameObject TopObjem = PhotonNetwork.Instantiate("Top", TopCikisNoktasi.transform.position, TopCikisNoktasi.transform.rotation,0,null);
                TopObjem.GetComponent<PhotonView>().RPC("TagAktar", RpcTarget.All, gameObject.tag);
                Rigidbody2D rg = TopObjem.GetComponent<Rigidbody2D>();
                rg.AddForce(new Vector2(AtisYonu, 0f) * PowerBar.fillAmount * 12f, ForceMode2D.Impulse);
                AtisYapiliyorMu = true;
                StopCoroutine(PowerDongu);
                TopAtisEfekt.Play();
                
            }
        }
     
  
    }
    public void PowerOynasin()
    {
        if (PowerDongu != null)
        {
            StopCoroutine(PowerDongu);
        }
        AtisYapiliyorMu = false;
        PowerDongu = StartCoroutine(PowerBarCalistir());
    
    }

    public void Sonuc(int Deger)
    {
       
        if (pw.IsMine)
        {
            if (PhotonNetwork.IsMasterClient)
             {
            
                if(Deger == 1)
                {
                    PlayerPrefs.SetInt("ToplamMac", PlayerPrefs.GetInt("ToplamMac") + 1);
                    PlayerPrefs.SetInt("Galibiyet", PlayerPrefs.GetInt("Galibiyet") + 1);
                    PlayerPrefs.SetInt("ToplamPuan", PlayerPrefs.GetInt("ToplamPuan") + 150);
                }
                else
                {

                    PlayerPrefs.SetInt("ToplamMac", PlayerPrefs.GetInt("ToplamMac") + 1);
                    PlayerPrefs.SetInt("Maglubiyet", PlayerPrefs.GetInt("Maglubiyet") + 1);
                }
            }
            else
            {

                if (Deger == 2)
                {
                    PlayerPrefs.SetInt("ToplamMac", PlayerPrefs.GetInt("ToplamMac") + 1);
                    PlayerPrefs.SetInt("Galibiyet", PlayerPrefs.GetInt("Galibiyet") + 1);
                    PlayerPrefs.SetInt("ToplamPuan", PlayerPrefs.GetInt("ToplamPuan") + 150);
                }
                else
                {

                    PlayerPrefs.SetInt("ToplamMac", PlayerPrefs.GetInt("ToplamMac") + 1);
                    PlayerPrefs.SetInt("Maglubiyet", PlayerPrefs.GetInt("Maglubiyet") + 1);
                }

            }
        }
        Time.timeScale = 0;
    }
}
