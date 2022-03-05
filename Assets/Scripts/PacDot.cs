using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacDot : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {

        if (otherCollider.gameObject.CompareTag("Player"))
        {
            UIManager.sharedInstance.AddPoints(50);
            Destroy(this.gameObject);

        }

    }

}
