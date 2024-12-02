using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameKontrol : MonoBehaviour
{


    [Header("Oyuncu Sağlık Ayarlari Ve İşlemleri")]
    public Image Oyuncu1SaglikBar;
    float Oyuncu1Saglik = 100;
    public Image Oyuncu2SaglikBar;
    float Oyuncu2Saglik = 100;
    PhotonView pw;

    GameObject Oyuncu1;
    GameObject Oyuncu2;

    bool BasladiMi;
    bool OyunBittiMi = false;
    int Limit;
    float BeklemeSuresi;
    int OlusturmaSayisi;

    public GameObject[] Noktalar;
    void Start()
    {
        pw = GetComponent<PhotonView>();
        BasladiMi = false;
        Limit = 4;
        BeklemeSuresi = 15f;
    }
    IEnumerator OlusturmayaBasla()
    {
        OlusturmaSayisi = 0;  
        while (true && BasladiMi)
        {
            if (Limit == OlusturmaSayisi)
            {
                BasladiMi = false;
            }
            yield return new WaitForSeconds(BeklemeSuresi);
            int OlusanDeger = Random.Range(0, 7);
            PhotonNetwork.Instantiate("Odul", Noktalar[OlusanDeger].transform.position, Noktalar[OlusanDeger].transform.rotation, 0, null);
            OlusturmaSayisi++;
        }
    }
    [PunRPC]
    public void Basla()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            BasladiMi = true;
            StartCoroutine(OlusturmayaBasla());
        
        }
    }
    [PunRPC]
    public void DarbeVur(int Kriter,float DarbeGucu)
    {
        switch (Kriter)
        {
            case 1:
               
                    Oyuncu1Saglik -= DarbeGucu;
                    Oyuncu1SaglikBar.fillAmount = Oyuncu1Saglik / 100;
                    if (Oyuncu1Saglik <= 0)
                    {

                    foreach (GameObject Objem in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                    {
                        if (Objem.gameObject.CompareTag("OyunSonuPanel"))
                        {
                            Objem.gameObject.SetActive(true);
                            GameObject.FindWithTag("OyunSonuBilgi").GetComponent<TextMeshProUGUI>().text = "2.Oyuncu Galip";

                        } 
                    }
                    Kazanan(2);
                   /* Oyuncu1 = GameObject.FindWithTag("Oyuncu1");
                    Oyuncu2 = GameObject.FindWithTag("Oyuncu2");
                    Oyuncu1.GetComponent<PhotonView>().RPC("Maglup", RpcTarget.All);
                    Oyuncu1.GetComponent<PhotonView>().RPC("Galip", RpcTarget.All);*/
          
                }
               
                break;
            case 2:
              
                    Oyuncu2Saglik -= DarbeGucu;
                    Oyuncu2SaglikBar.fillAmount = Oyuncu2Saglik / 100;
                    if (Oyuncu2Saglik <= 0){
                    foreach (GameObject Objem in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                    {
                        if (Objem.gameObject.CompareTag("OyunSonuPanel"))
                        {
                            Objem.gameObject.SetActive(true);
                            GameObject.FindWithTag("OyunSonuBilgi").GetComponent<TextMeshProUGUI>().text = "1.Oyuncu Galip";

                        }
                    }
                    Kazanan(1);
                   /* Oyuncu1 = GameObject.FindWithTag("Oyuncu1");
                    Oyuncu2 = GameObject.FindWithTag("Oyuncu2");
                    Oyuncu2.GetComponent<PhotonView>().RPC("Galip", RpcTarget.All);
                    Oyuncu2.GetComponent<PhotonView>().RPC("Maglup", RpcTarget.All);*/
              
                }
               
                break;
        }

    }
    [PunRPC]
    public void AnaMenu()
    {
        GameObject.FindWithTag("SunucuYonetimi").GetComponent<SunucuYonetim>().ButonLaMi = true;
        PhotonNetwork.LoadLevel(0);
    }
    public void NormalCikis()
    {
        PhotonNetwork.LoadLevel(0);
    }
    void Kazanan(int Deger)
    {
        if (!OyunBittiMi)
        {
            GameObject.FindWithTag("Oyuncu1").GetComponent<Oyuncu>().Sonuc(Deger);
            GameObject.FindWithTag("Oyuncu2").GetComponent<Oyuncu>().Sonuc(Deger);
            OyunBittiMi = true;
        }
    }
    public void SaglikDoldur(int HangiOyuncu)
    {
        switch (HangiOyuncu)
        {
            case 1:
                if(Oyuncu1Saglik != 100)
                {
                    Oyuncu1Saglik += 30;
                }
                if (Oyuncu1Saglik > 100)
                {
                    Oyuncu1Saglik = 100;
                    Oyuncu1SaglikBar.fillAmount = Oyuncu1Saglik / 100;
                }
                else
                {
                    Oyuncu1SaglikBar.fillAmount = Oyuncu1Saglik / 100;
                }
               

                break;
            case 2:
                if (Oyuncu2Saglik != 100)
                {
                    Oyuncu2Saglik += 30;
                }
                if (Oyuncu2Saglik > 100)
                {
                    Oyuncu2Saglik = 100;
                    Oyuncu2SaglikBar.fillAmount = Oyuncu2Saglik / 100;
                }
                else
                {
                    Oyuncu2SaglikBar.fillAmount = Oyuncu2Saglik / 100;
                }
             
                break;
        }
    }
  
}
