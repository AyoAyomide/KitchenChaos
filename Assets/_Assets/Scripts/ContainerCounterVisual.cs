using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] ContainerCounter container;

    Animator animator;
    const string OPEN_CLOSE = "OpenClose";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        container.OnPlayerGrabbedObject += Container_OnPlayerGrabbedObject;
    }

    private void Container_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
