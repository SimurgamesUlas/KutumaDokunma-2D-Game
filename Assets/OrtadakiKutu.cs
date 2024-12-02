using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrtadakiKutu : MonoBehaviour
{
    float Saglik = 100;
    public Image HealthBar;
    public GameObject SaglikCanvas;
    GameObject gameKontrol;
    PhotonView pw;
    public AudioSource KutuYokOlmaSesi;
    // Update is called once per frame
    void Start()
    {
        gameKontrol = GameObject.FindWithTag("GameKontrol");
        pw = GetComponent<PhotonView>();
        KutuYokOlmaSesi = GetComponent<AudioSource>();
    }

    [PunRPC]
    public void DarbeAl(float darbegucu) { 
    
            Saglik -= darbegucu;
            HealthBar.fillAmount = Saglik / 100;
            if (Saglik <= 0)
            {
                //gameKontrol.GetComponent<GameKontrol>().SesVeEfekOlustur(2, gameObject);
                PhotonNetwork.Instantiate("KutuKirilmaEfekti", transform.position, transform.rotation, 0, null);
                KutuYokOlmaSesi.Play();
                PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                StartCoroutine(CanvasCikar());
            }
        
    }
    IEnumerator CanvasCikar()
    {
        if (!SaglikCanvas.activeInHierarchy)
        {
            SaglikCanvas.SetActive(true);
            yield return new WaitForSeconds(2f);
            SaglikCanvas.SetActive(false);
        }
    }
}
