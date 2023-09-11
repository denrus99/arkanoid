using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    [SerializeField]
    private BoxCollider up;
    [SerializeField] 
    private BoxCollider bottom;

    private void Awake()
    {
        SetupBound(up, new Vector3(0.5f, 1.05f, 1));
        SetupBound(bottom, new Vector3(0.5f, -0.05f, 1));
    }

    private void SetupBound(BoxCollider boxCollider, Vector3 newPosition)
    {
	    var sizeX = (Camera.main.ViewportToWorldPoint(new Vector3(1, 1)) -
	                 Camera.main.ViewportToWorldPoint(new Vector3(0, 1))).magnitude;
	    var size = boxCollider.size;
	    size = new Vector3(sizeX, size.y, size.z);
	    boxCollider.size = size;
	    boxCollider.transform.position = Camera.main.ViewportToWorldPoint(newPosition);
    }
}
