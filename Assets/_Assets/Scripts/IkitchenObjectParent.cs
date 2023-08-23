using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IkitchenObjectParent
{
    public Transform GetKitchenObjectFollowTransform();
    public void SetKitchenObject(KitchenObject kitchenObject);
    public void ClearKitchenObject();
    public bool HasKitchenObjectTop();
}
