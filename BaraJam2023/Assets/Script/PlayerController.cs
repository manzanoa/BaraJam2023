using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float runSpeed = 5f;

    public float collisionOffset = .05f;

    public ContactFilter2D contactFilter;

    Vector2 movementInput;
    Rigidbody2D rb;

    public Animator anim;

    const int UP = 1;
    const int DOWN = -1;

    const int RIGHT = 1;
    const int LEFT = -1;

    List<RaycastHit2D> castCollision = new List<RaycastHit2D>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(movementInput != Vector2.zero)
        {
            int count = rb.Cast(movementInput, contactFilter, castCollision, runSpeed*Time.fixedDeltaTime + collisionOffset);

            if(count == 0)
            {
                rb.MovePosition(rb.position + movementInput * runSpeed * Time.fixedDeltaTime);
            }
        }

    }

    void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();

        if(movementInput != Vector2.zero)
        {
            anim.SetFloat("Horizontal", movementInput.x);
            anim.SetFloat("Verticle", movementInput.y);
        }
        
        anim.SetFloat("Speed", movementInput.sqrMagnitude);
    }

}
