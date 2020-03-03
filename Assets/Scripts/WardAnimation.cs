using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardAnimation : MonoBehaviour
{
    [SerializeField] private GameObject innerRing;
    [SerializeField] private GameObject outterRing;
    [SerializeField] private GameObject stabilizer;
    [SerializeField] private float innerRingSpeed;
    [SerializeField] private float outterRingSpeed;
    [SerializeField] private float stabilizerSpeed;

    private float rotationDirectionInnerRing;
    private float rotationDirectionOutterRing;
    private float rotationDirectionStabilizer;

    void Initialize()
    {
        rotationDirectionInnerRing = innerRing.transform.rotation.y;
        rotationDirectionOutterRing = outterRing.transform.rotation.y;
        rotationDirectionStabilizer = stabilizer.transform.rotation.y;
    }

    void AnimateInnerRing()
    {
        rotationDirectionInnerRing += innerRingSpeed * Time.deltaTime;
        if (rotationDirectionInnerRing > 360)
        {
            rotationDirectionInnerRing -= 360;
        }
        innerRing.transform.rotation = Quaternion.Euler(0f, rotationDirectionInnerRing, 90f);
    }

    void AnimateOutterRing()
    {
        rotationDirectionOutterRing += outterRingSpeed * Time.deltaTime;
        if (rotationDirectionOutterRing > 360)
        {
            rotationDirectionOutterRing -= 360;
        }
        outterRing.transform.rotation = Quaternion.Euler(0f, rotationDirectionOutterRing, 90f);
    }

    void AnimateStabilizer()
    {
        rotationDirectionStabilizer += stabilizerSpeed * Time.deltaTime;
        if (rotationDirectionStabilizer > 360)
        {
            rotationDirectionStabilizer -= 360;
        }
        stabilizer.transform.rotation = Quaternion.Euler(0f, rotationDirectionStabilizer, 0f);
    }

    void Animate()
    {
        AnimateInnerRing();
        AnimateOutterRing();
        AnimateStabilizer();
    }

    private void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
    }
}
