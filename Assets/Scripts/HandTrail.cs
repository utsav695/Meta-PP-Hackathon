using UnityEngine;
using System.Collections.Generic;

public class HandTrail : MonoBehaviour
{
    [SerializeField] private GameObject trailPrefab;

    private OVRHand hand;
    private OVRSkeleton skeleton;
    private Dictionary<OVRBone, GameObject> trails;

    private void Start()
    {
        trails = new Dictionary<OVRBone, GameObject>();
        hand = GetComponent<OVRHand>();
        skeleton = GetComponent<OVRSkeleton>();
    }

    private void Update()
    {
        if (hand.IsTracked)
        {
            foreach (OVRBone bone in skeleton.Bones)
            {
                if (!trails.ContainsKey(bone))
                {
                    trails.Add(bone, Instantiate(trailPrefab));
                }
                trails[bone].transform.SetPositionAndRotation(bone.Transform.position, bone.Transform.rotation);
            }
        }
    }
}
