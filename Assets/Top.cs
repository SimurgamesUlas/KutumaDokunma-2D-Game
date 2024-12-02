using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    float DarbeGucu;
    int BenKimim;

    GameObject gameKomtrol;
    GameObject Oyuncu;
    PhotonView pw;
    public AudioSource YokOlmaSesi;
    void Start()
    {
        DarbeGucu = 20;
        gameKomtrol = GameObject.FindWithTag("GameKontrol");
        pw = GetComponent<PhotonView>();
        YokOlmaSesi = GetComponent<AudioSource>();
    }
    [PunRPC]
    public void TagAktar(string GelenTag)
    {

        Oyuncu = GameObject.FindWithTag(GelenTag);

        if (GelenTag == "Oyuncu1")
        {
            BenKimim = 1;
        }
        else
        {
            BenKimim = 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("OrtadakiKutular"))
        {
            collision.gameObject.GetComponent<PhotonView>().RPC("DarbeAl", RpcTarget.All, DarbeGucu);

            Oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            PhotonNetwork.Instantiate("PatlamaEfekt", transform.position, transform.rotation, 0, null);
            YokOlmaSesi.Play();
            if (pw.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }


        }
        if (collision.gameObject.CompareTag("Oyuncu1Kule") || collision.gameObject.CompareTag("Oyuncu1"))
        {

            if (BenKimim != 1)
            {
                gameKomtrol.GetComponent<PhotonView>().RPC("DarbeVur", RpcTarget.All, 1, DarbeGucu);
            }

            Oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            PhotonNetwork.Instantiate("PatlamaEfekt", transform.position, transform.rotation, 0, null);
            YokOlmaSesi.Play();
            if (pw.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Oyuncu2Kule") || collision.gameObject.CompareTag("Oyuncu2"))
        {
            if (BenKimim != 2)
            {
                gameKomtrol.GetComponent<PhotonView>().RPC("DarbeVur", RpcTarget.All, 2, DarbeGucu);
            }
            Oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            PhotonNetwork.Instantiate("PatlamaEfekt", transform.position, transform.rotation, 0, null);
            YokOlmaSesi.Play();
            if (pw.IsMine)
            {

                PhotonNetwork.Destroy(gameObject);
            }
        }



        if (collision.gameObject.CompareTag("Zemin"))
        {

            Oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            PhotonNetwork.Instantiate("PatlamaEfekt", transform.position, transform.rotation, 0, null);
            YokOlmaSesi.Play();
            if (pw.IsMine)
            {     
                PhotonNetwork.Destroy(gameObject);
            }

        }
        if (collision.gameObject.CompareTag("Tahta"))
        {

            Oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            PhotonNetwork.Instantiate("PatlamaEfekt", transform.position, transform.rotation, 0, null);
            YokOlmaSesi.Play();
            if (pw.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }

        }
        if (collision.gameObject.CompareTag("Odul"))
        {
            gameKomtrol.GetComponent<PhotonView>().RPC("SaglikDoldur", RpcTarget.All, BenKimim);
            PhotonNetwork.Destroy(collision.transform.gameObject);
            
            Oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            PhotonNetwork.Instantiate("PatlamaEfekt", transform.position, transform.rotation, 0, null);
            if (pw.IsMine)
            { 
                PhotonNetwork.Destroy(gameObject);
            }

        }
        if (collision.gameObject.CompareTag("Top"))
        {
            gameKomtrol.GetComponent<PhotonView>().RPC("SaglikDoldur", RpcTarget.All, BenKimim);
            PhotonNetwork.Destroy(collision.transform.gameObject);
            Oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            PhotonNetwork.Instantiate("PatlamaEfekt", transform.position, transform.rotation, 0, null);
            if (pw.IsMine)
            {       
                PhotonNetwork.Destroy(gameObject);
            }

        }
    }

}
