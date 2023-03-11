using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            PhotonNetwork.Destroy(col.gameObject);
        }
        else
        {
            if(col.gameObject.TryGetComponent<PhotonView>(out var view)) 
                PhotonNetwork.Destroy(col.gameObject);
            else
            {
                Destroy(col.gameObject);
            }
        }
    }
}
