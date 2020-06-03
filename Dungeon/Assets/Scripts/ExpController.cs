using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Zniszczenie obiektu odpowiedzialnego za animację eksplozji
/// </summary>
public class ExpController : MonoBehaviour
{
    public void DestroyThisEffect()
    {
        Destroy(gameObject);
    }
}
