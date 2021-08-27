using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;
    public LayerMask MovementMask;
    Camera cam;
    PlayerMotor motor;

    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, MovementMask))
            {
                motor.MoveToPoint(hit.point);
                RemoveFocus(); // Stop focusing any object
            }   
            
        }

        // if we press right mouse
        if (Input.GetMouseButtonDown(1))
        {
            // we create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // check if we hit an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                // if we did set it as our focus
                if (interactable != null)
                {
                    SetFocus(interactable);
                }

            }

        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if(focus != null)
                focus.OnDeFocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);

        }
        
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDeFocused();
            
        focus = null;
        motor.StopFollowingTarget();
    }
}
