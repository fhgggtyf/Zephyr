using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepFromRotation : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
