using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzamanScript : MonoBehaviour
{

    public float speed = 0.4f;
    Vector2 destination = Vector2.zero;



    private void Awake()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;

    }

    void Start()
    {

        destination = this.transform.position;

    }

    void FixedUpdate()
    {

        if (GameManager.sharedInstance.gameStarted && !GameManager.sharedInstance.gamePaused)
        {

            GetComponent<AudioSource>().volume = 0.2f;
            // Calcular nuevo punto a donde ir, en funcion a la variable destino y velocidad
            Vector2 newPos = Vector2.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);

            // Transportar a Pizzaman con el Rigidbody
            GetComponent<Rigidbody2D>().MovePosition(newPos);

            float distanceToDestination = Vector2.Distance((Vector2)this.transform.position, destination);

            Debug.Log("Pacman está en: " + this.transform.position + "y su destino es: " + destination + " y se encuentra a: " + distanceToDestination + " de su destino ");


            if (distanceToDestination < 1.3f)
            {

                // UP
                if (Input.GetKey(KeyCode.UpArrow) && CanMoveTo(Vector2.up))
                {
                    destination = (Vector2)this.transform.position + Vector2.up;
                }

                // RIGHT
                if (Input.GetKey(KeyCode.RightArrow) && CanMoveTo(Vector2.right))
                {
                    destination = (Vector2)this.transform.position + Vector2.right;
                }

                // DOWN
                if (Input.GetKey(KeyCode.DownArrow) && CanMoveTo(Vector2.down))
                {
                    destination = (Vector2)this.transform.position + Vector2.down;
                }

                // LEFT
                if (Input.GetKey(KeyCode.LeftArrow) && CanMoveTo(Vector2.left))
                {
                    destination = (Vector2)this.transform.position + Vector2.left;
                }

                Vector2 dir = destination - (Vector2)this.transform.position;
                //Animacion en funcion del vector de movimiento ...

                GetComponent<Animator>().SetFloat("DirX", dir.x);
                GetComponent<Animator>().SetFloat("DirY", dir.y);

            }

        }

        else
        {

            GetComponent<AudioSource>().volume = 0.0f;

        }
        
    }

    bool CanMoveTo(Vector2 dir)
    {

        Vector2 pacmanPos = this.transform.position;

        // Trazar linea del objetivo a donde quiero ir, hacia PizzaMan

        RaycastHit2D hit = Physics2D.Linecast(pacmanPos + dir, pacmanPos);
        Debug.DrawLine(pacmanPos, pacmanPos + dir);

        Collider2D pacmanCollider = GetComponent<Collider2D>();
        Collider2D hitCollider = hit.collider;

        if(hitCollider == pacmanCollider)
        {

            return true;

        }

        return false;

    }
}
