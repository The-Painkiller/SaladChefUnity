using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple implementation of the IInteractableObject Interface.
/// Uses Monobehaviour and acts as parent class to act as parent for extending Model classes of all interactable objects.
/// </summary>
public class InteractableObject : MonoBehaviour, IInteractableObject
{
    [SerializeField]
    private InteractableObjects _Object;
    public InteractableObjects _ObjectType { get { return _Object; } set { } }
}
