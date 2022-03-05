using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Transform[] wayPoints;
    int currentWayPoint = 0;
    bool isStarted = false;

    public float speed = 0.3f;
    public bool shouldWaitHome = false;

    private void Update()
    {
        if(GameManager.sharedInstance.invensibleTime > 0)
        {

            GetComponent<Animator>().SetBool("Invensible", true);

        }

        else
        {

            GetComponent<Animator>().SetBool("Invensible", false);

        }
    }

    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.gameStarted && !GameManager.sharedInstance.gamePaused) {

            GetComponent<AudioSource>().volume = 0.2f;

            if (!shouldWaitHome) {

                // Distancia entre el fantasma y el punto de destino
                float distanceToWaypoint = Vector2.Distance((Vector2)this.transform.position,
                                                            (Vector2)wayPoints[currentWayPoint].position);

                if (distanceToWaypoint < 0.1f)
                {
                    if (!isStarted) isStarted = true;

                    currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;

                    if (isStarted && currentWayPoint == 0) currentWayPoint++;

                    Vector2 newDirection = wayPoints[currentWayPoint].position - this.transform.position;
                    GetComponent<Animator>().SetFloat("DirX", newDirection.x);
                    GetComponent<Animator>().SetFloat("DirY", newDirection.y);

                }

                else
                {

                    Vector2 newPos = Vector2.MoveTowards(this.transform.position,
                                                         wayPoints[currentWayPoint].position,
                                                         speed * Time.deltaTime);

                    GetComponent<Rigidbody2D>().MovePosition(newPos);

                }

            }
            
        }

        else
        {

            GetComponent<AudioSource>().volume = 0.0f;

        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.tag == "Player")
        {
            if(GameManager.sharedInstance.invensibleTime <= 0)
            {
                GameManager.sharedInstance.gameStarted = false;
                Destroy(otherCollider.gameObject);

                StartCoroutine(Restartgame());
            }

            else
            {
                UIManager.sharedInstance.AddPoints(1000);
                GameObject home = GameObject.Find("GhostHome");
                this.transform.position = home.transform.position;
                this.currentWayPoint = 0;
                this.isStarted = false;
                this.shouldWaitHome = true;
                StartCoroutine(AwakeFromHome());

            }
        }


    }

    IEnumerator AwakeFromHome()
    {

        yield return new WaitForSecondsRealtime(3.0f);
        this.shouldWaitHome = false;

        this.speed *= 1.2f; // Cada vez que el fantasma despierta, es 20% más rápido
    }

    IEnumerator Restartgame()
    {

        yield return new WaitForSecondsRealtime(3.0f);
        GameManager.sharedInstance.RestartGame();

    }
}
