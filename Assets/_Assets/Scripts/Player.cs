using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IkitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterEventArgs : EventArgs
    {
        public BaseCounter selected;
    }
    public bool IsWalking { get { return isWalking; } }

    [SerializeField] float moveSpeed;
    [SerializeField] InputController inputController;
    [SerializeField] LayerMask counterLayerMask;
    [SerializeField] Transform kitchenObjectHoldPoint;


    bool isWalking;

    Vector3 lastInteractDir;
    float interactDistance = 2f;
    BaseCounter selectedCounter;
    KitchenObject kitchenObject;

    void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("There are two player object");
        }
        Instance = this;
    }

    void Start()
    {
        inputController.OnInteraction += InputController_OnInteraction;
    }
   
    void Update()
    {
        PlayerMovement();

        IsInteracting();
    }
   
    void InputController_OnInteraction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    void IsInteracting()
    {
        Vector2 inputVector = inputController.GetNormalizedVector();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if(moveDir != Vector3.zero )
            lastInteractDir = moveDir;
        // we use a raycast 
        // we set the origin, distance,the object it hit and it max distance
        if(Physics.Raycast(transform.position,lastInteractDir,out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)){
                  
                if (baseCounter != selectedCounter)
               {
                    SetSelectedCounter(baseCounter);
               }
            }
            else
            {
                SetSelectedCounter(null);
            }

        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    void PlayerMovement()
    {
        Vector2 inputVector = inputController.GetNormalizedVector();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float playerSize = .7f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDir, moveDistance);

        if (canMove)
            transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero ? true : false;

        float turnSpeed = 5f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, turnSpeed * Time.deltaTime);

    }

    void SetSelectedCounter(BaseCounter seletedCounter) {

        this.selectedCounter = seletedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterEventArgs
        {
            selected = selectedCounter

        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObjectTop()
    {
        return kitchenObject != null;
    }
}
