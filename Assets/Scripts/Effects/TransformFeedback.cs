using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFeedback : MonoBehaviour
{
    private IEnumerator animationIE = null;

    [Header("Animation")]

    [SerializeField]
    private AnimationCurve pokeCurve;

    [SerializeField]
    private AnimationCurve deathCurve;

    [SerializeField]
    private Transform effectObject; // Object that we play transform effects on.

    private Vector3 effectObjectLocalStartPos;

    private Quaternion effectObjectLocalStartRot;

    [Header("Particle Systems")]

    [SerializeField]
    private ParticleSystem[] bloodParticleSystemPrefabs;

    private void PlayBloodEffect(float normalizedForce)
    {
        if (bloodParticleSystemPrefabs != null && bloodParticleSystemPrefabs.Length > 0)
        {
            int nearestIndex = Mathf.FloorToInt(normalizedForce * (float)bloodParticleSystemPrefabs.Length);
            ParticleSystem spawnParticleSystem = Instantiate(bloodParticleSystemPrefabs[nearestIndex].gameObject).GetComponent<ParticleSystem>();
            spawnParticleSystem.transform.position = this.transform.position;
            Destroy(spawnParticleSystem.gameObject, 2f);
        }
    }

    public void DisplayHit(Vector3 dir, float norForce)
    {
        // Play blood effect.
        PlayBloodEffect(norForce);

        if (animationIE == null)
        {
            effectObject.transform.localPosition = effectObjectLocalStartPos;
            effectObject.transform.localRotation = effectObjectLocalStartRot;

            dir = new Vector3(dir.x, 0, dir.z);

            animationIE = PokeAwayIE(dir, norForce);
            StartCoroutine(animationIE);
        }
    }

    private IEnumerator PokeAwayIE(Vector3 dir, float norForce)
    {
        norForce = System.Math.Max(norForce, 0.08f); // Make it have a minimum value.

        const float baseForce = 1.7f;
        float effectTime = 0.8f * norForce;
        float accumTime = 0f;

        Vector3 turnAroundVec = Vector3.Cross(Vector3.up, dir.normalized);

        while (accumTime < effectTime && effectObject != null)
        {
            float currVal = pokeCurve.Evaluate(accumTime / effectTime);
            effectObject.RotateAround(effectObject.transform.position, turnAroundVec, currVal * 10);

            effectObject.transform.localPosition = effectObjectLocalStartPos + (dir * currVal * norForce * baseForce);

            accumTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        effectObject.transform.localPosition = effectObjectLocalStartPos;
        effectObject.transform.localRotation = effectObjectLocalStartRot;
        animationIE = null;
    }

    private void Awake()
    {
        effectObjectLocalStartPos = effectObject.localPosition;
        effectObjectLocalStartRot = effectObject.localRotation;
    }
}
