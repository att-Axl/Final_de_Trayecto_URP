using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualHeadLook : MonoBehaviour
{
    public string headBoneName = "mixamorig:Head"; 
    public float rotationSpeed = 5f;
    public float maxAngle = 360f;

    private Transform headBone;
    private Transform player;

    void Start()
    {
        headBone = FindDeepChild(transform, headBoneName);

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void LateUpdate()
    {
        if (headBone == null || player == null) return;

        Vector3 direction = player.position - headBone.position;

        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        float angle = Quaternion.Angle(headBone.rotation, targetRotation);

        if (angle < maxAngle)
        {
            headBone.rotation = Quaternion.Slerp(headBone.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;
            Transform result = FindDeepChild(child, name);
            if (result != null)
                return result;
        }
        return null;
    }
}