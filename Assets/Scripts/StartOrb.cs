using UnityEngine;

public class StartOrb : MonoBehaviour
{
    public float HoldTimer { get; private set; }

    private enum Side { Left, Right }

    [SerializeField] private Side side;
    [SerializeField] private Transform followHand;

    private bool stay;

    private void OnTriggerEnter(Collider other)
    {
        transform.GetComponent<AudioSource>().Play();
        OVRSkeleton skeleton = other.GetComponent<OVRSkeleton>();
        if (skeleton && ((skeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.HandLeft && side == Side.Left) ||
                (skeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.HandRight && side == Side.Right)))
        {
            stay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OVRSkeleton skeleton = other.GetComponent<OVRSkeleton>();
        if (skeleton && ((skeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.HandLeft && side == Side.Left) ||
                (skeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.HandRight && side == Side.Right)))
        {
            stay = false;
        }
    }

    private void Update()
    {
        transform.position = followHand.position;

        if (stay)
        {
            HoldTimer += Time.deltaTime;
        }
        else
        {
            HoldTimer = 0f;
        }
    }
}
