using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : BaseObject
{

    [SerializeField]
    private float scaleVariance = 0.2f;

    [SerializeField]
    private float rotationVariance = 180f;

    // Start is called before the first frame update
    void Awake()
    {
        this.transform.localScale = this.transform.localScale * (1 + Random.Range(-scaleVariance, scaleVariance));
        this.transform.localEulerAngles = this.transform.localEulerAngles + new Vector3(0f, Random.Range(-rotationVariance, rotationVariance), 0f);
    }
}
