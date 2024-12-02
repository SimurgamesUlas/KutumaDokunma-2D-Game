using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
public class SunucuYonetim : MonoBehaviourPunCallbacks
{
    GameObject  ServerBilgi;
    GameObject AdKaydet;
    GameObject RandomGiris;
    GameObject OdaKurVeGiris;
    public bool ButonLaMi;
    void Start()
    {
      
        ServerBilgi = GameObject.FindWithTag("ServerBilgi");
        AdKaydet = GameObject.FindWithTag("AdKaydetButton");
        RandomGiris = GameObject.FindWithTag("RandomGirisYap");
        OdaKurVeGiris = GameObject.FindWithTag("OdaKurVeGir");
       
        PhotonNetwork.ConnectUsingSettings();
        DontDestroyOnLoad(gameObject);
    }

    public override void OnConnectedToMaster()
    {
        ServerBilgi.GetComponent<TextMeshProUGUI>().text = "Servere Bağlandı";
     
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        ServerBilgi.GetComponent<TextMeshProUGUI>().text = "Lobiye Bağlandı";
        if (!PlayerPrefs.HasKey("KullaniciAdiVarmi"))
        {
            AdKaydet.GetComponent<Button>().interactable = true;
        }
        else
        {
            RandomGiris.GetComponent<Button>().interactable = true;
            OdaKurVeGiris.GetComponent<Button>().interactable = true;
        }
        
    }
    public void RandomGirisYap()
    {
    
        PhotonNetwork.LoadLevel(1);
        PhotonNetwork.JoinRandomRoom();
    }
    public void OdaOlusturVeGir()
    {
      
        PhotonNetwork.LoadLevel(1);
        string odaadi = Random.Range(0, 9546643).ToString();
        PhotonNetwork.JoinOrCreateRoom(odaadi,new RoomOptions {MaxPlayers = 2,IsOpen = true,IsVisible = true},TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {

        InvokeRepeating("BilgileriKontrolEt", 0, 1f);
        GameObject objem = PhotonNetwork.Instantiate("Oyuncu", Vector3.zero, Quaternion.identity, 0, null);
        objem.GetComponent<PhotonView>().Owner.NickName = PlayerPrefs.GetString("KullaniciAdiVarmi");

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            objem.gameObject.tag = "Oyuncu1";
         
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2 && PhotonNetwork.PlayerList.Length == 2) 
        {
            objem.gameObject.tag = "Oyuncu2";
            GameObject.FindWithTag("GameKontrol").gameObject.GetComponent<PhotonView>().RPC("Basla", RpcTarget.All);
        }
      
       
    }
    public override void OnLeftRoom()
    {
        if (ButonLaMi)
        {
             Time.timeScale = 1;
             PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Time.timeScale = 1;
            PhotonNetwork.ConnectUsingSettings();
            PlayerPrefs.SetInt("ToplamMac", PlayerPrefs.GetInt("ToplamMac")+1);
            PlayerPrefs.SetInt("Maglubiyet", PlayerPrefs.GetInt("Maglubiyet") + 1);
        }



    }
    public override void OnLeftLobby()
    {

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
       
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (ButonLaMi)
        {
            Time.timeScale = 1;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Time.timeScale = 1;
            PhotonNetwork.ConnectUsingSettings();
            PlayerPrefs.SetInt("ToplamMac", PlayerPrefs.GetInt("ToplamMac") + 1);
            PlayerPrefs.SetInt("Galibiyet", PlayerPrefs.GetInt("Galibiyet") + 1);
            PlayerPrefs.SetInt("ToplamPuan", PlayerPrefs.GetInt("ToplamPuan") + 150);
        }

        InvokeRepeating("BilgileriKontrolEt", 0, 1f);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        ServerBilgi.GetComponent<TextMeshProUGUI>().text = "Random Bir Odaya Girilemedi";
        
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ServerBilgi.GetComponent<TextMeshProUGUI>().text = "Odaya Girilemedi";
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ServerBilgi.GetComponent<TextMeshProUGUI>().text = "Oda Oluşturulamadı";
    }

    void BilgileriKontrolEt()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            GameObject.FindWithTag("OyuncuBekleniyor").SetActive(false);
            GameObject.FindWithTag("Oyuncuİsim1").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindWithTag("Oyuncuİsim2").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[1].NickName;
            CancelInvoke("BilgileriKontrolEt");
        }
        else
        {
            GameObject.FindWithTag("OyuncuBekleniyor").SetActive(true);
            GameObject.FindWithTag("Oyuncuİsim1").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindWithTag("Oyuncuİsim2").GetComponent<TextMeshProUGUI>().text = ".........";
        }
        
    }
}
 