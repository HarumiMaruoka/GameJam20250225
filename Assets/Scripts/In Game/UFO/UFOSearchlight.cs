using System;
using UnityEngine;

public class UFOSearchlight : MonoBehaviour
{
    public Vector3 Offset;

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
        transform.position = transform.parent.position + Offset;
    }
}
