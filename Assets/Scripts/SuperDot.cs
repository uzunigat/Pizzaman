using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperDot : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.tag == "Player")
        {

            UIManager.sharedInstance.AddPoints(100);
            Destroy(this.gameObject);

            GameManager.sharedInstance.MakeInvensible(15.0f);

        }

    }
}
