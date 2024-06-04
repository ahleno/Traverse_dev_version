using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiggingManager : MonoBehaviour
{
    public Transform leftHandIK;
    public Transform rightHandIK;
    public Transform headIK;
    public Transform[] leftFingerIK;
    public Transform[] rightFingerIK;

    public Transform leftHandController;
    public Transform rightHandController;
    public Transform hmd;
    public Transform[] leftFinger;
    public Transform[] rightFinger;

    public Vector3[] leftOffset;
    public Vector3[] rightOffset; // transform + rotation
    public Vector3[] headOffset;
    public Vector3[] leftFingerOffset;
    public Vector3[] rightFingerOffset;

    public float smoothValue = 0.3f;
    public float modelHeight = 1.67f;
    
    void LateUpdate()
    {
        MappingHandTransform(leftHandIK, leftHandController, true);
        MappingHandTransform(rightHandIK, rightHandController, false);
        MappingBodyTransform(headIK, hmd);
        MappingHeadTransform(headIK, hmd);
        for(int i = 0; i < 5; i++)
        {
            MappingFingerTransform(leftFingerIK[i], leftFinger[i],true,i);
            MappingFingerTransform(rightFingerIK[i], rightFinger[i], false, i);
        }
    }

    private void MappingHandTransform(Transform ik, Transform controller, bool isLeft)
    {
        var offset = isLeft ? leftOffset : rightOffset;
        ik.position = controller.TransformPoint(offset[0]);
        ik.rotation = controller.rotation * Quaternion.Euler(offset[1]);
    }

    private void MappingBodyTransform(Transform ik, Transform hmd)
    {
        this.transform.position = new Vector3(hmd.position.x, hmd.position.y - modelHeight, hmd.position.z); // 얼굴 투시 문제 때문에 x에 +0.1f
        float yaw = hmd.eulerAngles.y - 90;
        Debug.Log(yaw);
        var targetRotation = new Vector3(this.transform.eulerAngles.x,yaw,this.transform.eulerAngles.z);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(targetRotation), smoothValue);

    }

    private void MappingHeadTransform(Transform ik,Transform hmd)
    {
        ik.position = hmd.TransformPoint(headOffset[0]);
        ik.rotation = hmd.rotation * Quaternion.Euler(headOffset[1]);
    }

    private void MappingFingerTransform(Transform ik, Transform hand, bool isLeft, int fingerNum)
    {
        var offset = isLeft ? leftFingerOffset : rightFingerOffset;
        ik.position = hand.TransformPoint(offset[(fingerNum)*2]);
        ik.rotation = hand.rotation * Quaternion.Euler(offset[(fingerNum) * 2 + 1]);
    }
}
