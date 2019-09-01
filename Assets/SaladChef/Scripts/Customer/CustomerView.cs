using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC View to implement Customer
/// </summary>
public class CustomerView : MonoBehaviour
{
    public SpriteRenderer _customerSprite;
    public TextMesh _customerOrder;
    public TextMesh _customerTimer;

    /// <summary>
    /// Displays customer order as soon as customer is instantiated.
    /// </summary>
    /// <param name="cOrder">Takes the name of the order it is supposed to have and display it</param>
    public void Init ( Veggie [] cOrder )
    {
        foreach ( Veggie v in cOrder )
        {
            _customerOrder.text += v._veggieDetails._veggieType.ToString() + "\n";
        }
    }

    /// <summary>
    /// Angry look
    /// </summary>
    public void LookAngry ( )
    {
        _customerSprite.color = Color.red;
    }

    /// <summary>
    /// Happy look
    /// </summary>
    public void LookHappy ( )
    {
        _customerSprite.color = Color.green;
    }

    /// <summary>
    /// Keeps updating customer's timer textMesh.
    /// </summary>
    /// <param name="timer"></param>
    public void UpdateTimer ( float timer )
    {
        _customerTimer.text = ((int)timer).ToString ( );
    }
}
