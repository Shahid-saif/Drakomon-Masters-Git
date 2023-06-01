using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Manage")]
    [SerializeField] GameManager GManager;


    [Space]
    [Header("Movment")]
    [SerializeField] CharacterController Controller;
    [SerializeField] bool Onskateboard = false;


    [Space]
    [Header("Controle")]
    [SerializeField] Inputs inputs;
    [SerializeField] Vector3 direction;
    [SerializeField] float Speed = 6f;
    [SerializeField] float NormalSpeed = 6f;
    [SerializeField] float TurnSmoothTime = 0.1f;
    [SerializeField] float Gravity = 1f;
    float TurnSmoothVelocity;

    [Space]
    [Header("Animations")]
    [SerializeField]
    Animator Anim;


    [Space]
    [Header("bike proparties")]
    [SerializeField] bool Onbike = false;
    [SerializeField] float bikeSpeed = 8f;
    [SerializeField] GameObject bikeObj;
    void Start()
    {
        GManager = GameManager.Instance;
        Controller = GetComponent<CharacterController>();
        inputs = GetComponent<Inputs>();
        Anim = GetComponent<Animator>();

        GManager.OnUpdate.AddListener(OnProcess);
        GManager.OnUpdate.AddListener(inputs.OnProcess);
        if (!GManager.CanUseSkate)
        {
            Speed = NormalSpeed;
            bikeObj.SetActive(false);
            Anim.SetInteger("Bike", 0);
            return;
        }
    }

    // Update is called once per frame
   public void OnProcess()
    {
        if (!Controller.isGrounded)
        {
            Controller.Move(-Vector3.up*Gravity);
        } 
        if (!GManager.CanMove)
        {
            Anim.SetFloat("Move", 0f);
            Controller.Move(Vector3.zero);
            return;
        }
        direction = new Vector3(inputs.Horizontal,0,inputs.Vertical).normalized;
        if (direction.magnitude>=0.1f)
        {
            float TargetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg + GManager.MainCam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle,ref TurnSmoothVelocity,TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0, TargetAngle, 0f) * Vector3.forward;
            Controller.Move(moveDir.normalized * Speed * Time.deltaTime);
        }
        Anim.SetFloat("Move",direction.magnitude,0.05f,Time.deltaTime);


        //if (inputs.Intract)
        //{
        //    Interact();
        //}
        //if (inputs.Bike)
        //{
        //    SwitchToBike();
        //}
    }

    public void Interact()
    {
        GManager.OnInteract?.Invoke();
        GManager.OnInteract.RemoveAllListeners();
        GManager.InteractUiHolder.SetActive(false);
    }
    public void SwitchToBike()
    {
        if (!GManager.CanUseSkate)
        {
            Speed = NormalSpeed;
            bikeObj.SetActive(false);
            Anim.SetInteger("Bike", 0);
            return;
        }
        Onbike = !Onbike;
        if (Onbike)
        {
            Speed = bikeSpeed;
            bikeObj.SetActive(true);
            Anim.SetInteger("Bike",1);
        }
        else
        {
            Speed = NormalSpeed;
            bikeObj.SetActive(false);
            Anim.SetInteger("Bike", 0);
        }
    }
}
