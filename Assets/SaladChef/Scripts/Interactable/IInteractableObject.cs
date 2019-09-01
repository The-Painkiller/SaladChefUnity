using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for interactable objects.
/// Uses a single property of InteractableObjects type to determine which Object the instance of this class is.
/// </summary>
public interface IInteractableObject
{
    InteractableObjects _ObjectType { get; set; }
}
