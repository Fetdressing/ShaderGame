using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindRotation : MonoBehaviour
{
    private Vector3 startRot;
    private Vector3 targetRot;

    [SerializeField]
    private Vector3 offsetRot = new Vector3(0.05f, 0.02f, 0);

    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private float speed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        this.startRot = this.transform.localEulerAngles;
        this.targetRot = this.startRot + offsetRot;
    }

    // Update is called once per frame
    void Update()
    {
        float offsetWindDueToWorldPos = this.transform.position.x;
        float pingPong = Mathf.PingPong((Time.time * speed) + offsetWindDueToWorldPos, 1f);
        float v = curve.Evaluate(pingPong);
        this.transform.localEulerAngles = Vector3.Slerp(startRot, targetRot, v);
    }
}
