using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Logika kluczy
/// Kiedy dany klucz ma się pojawić w edytorze dołączamy obiekty które chcemy wyświetlać
/// </summary>
public class KeyController : MonoBehaviour
{
    [SerializeField] private GameObject firstKey;
    [SerializeField] private GameObject secoundKey;

/// <summary>
/// Ustawianie w UI aktywnośći obiketów
/// </summary>
    private void FixedUpdate()
    {
        if (PermanentUI.perm != null)
        {
            if (PermanentUI.perm.greenKey)
                firstKey.SetActive(true);
            else
                firstKey.SetActive(false);
            if (PermanentUI.perm.redKey)
                secoundKey.SetActive(true);
            else
                secoundKey.SetActive(false);
        }
    }
}
