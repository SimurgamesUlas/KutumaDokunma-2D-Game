using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Odul : MonoBehaviour
{
    PhotonView pw;
    // Start is called before the first frame update
    void Start()
    {
        pw = GetComponent<PhotonView>();
        StartCoroutine(YokOl());
    }

    IEnumerator YokOl()
    {
        yield return new WaitForSeconds(10f);
        if (pw.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);

        }
    }
}
