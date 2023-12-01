using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public float runSpeed = 5f;

    public float collisionOffset = .05f;

    public ContactFilter2D contactFilter;

    public bool canTakeInput;

    Vector2 movementInput;
    Rigidbody2D rb;

    public Animator anim;

    const int UP = 1;
    const int DOWN = -1;

    const int RIGHT = 1;
    const int LEFT = -1;

    List<RaycastHit2D> castCollision = new List<RaycastHit2D>();

    private void Awake()
    {
        instance = this;
    }
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

    void OnFire()
    {
        
        //if(canTakeInput)
        //{
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Down_Transition1" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Right_Transition" ||
            anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Left_Transition1" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Up_Transition1" ||
            anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Punch_Down1" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Punch_Right1" ||
            anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Punch_Left1" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Punch_Up1")
            {
                anim.SetTrigger("Punch2");
                canTakeInput = false;
            }
            else if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Down_Transition2" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Right_Transition2" ||
            anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Left_Transition2" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Up_Transition2" ||
            anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Punch_Down2" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Punch_Right2" ||
            anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Punch_Left2" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Punch_Up2")
            {
                anim.SetTrigger("Punch3");
                canTakeInput = false;
            }
            else if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle_Back" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle_Front" ||
                anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle_Right" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle_Left")
            {
                anim.SetTrigger("Punch1");
                canTakeInput = false;
            }
        //}
        
        
    }

}
