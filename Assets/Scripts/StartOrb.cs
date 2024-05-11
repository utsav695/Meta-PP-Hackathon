using UnityEngine;

public class StartOrb : MonoBehaviour
{
    public float HoldTimer { get; private set; }

    private enum Side { Left, Right }

    [SerializeField] private Side side;
    [SerializeField] private Transform followHand;

    private AudioSource audioSource;
    private bool stay;

    private void OnEnable()
    {
        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource)
        {
            audioSource.Stop();
            audioSource.time = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OVRSkeleton skeleton = other.GetComponent<OVRSkeleton>();
        if (skeleton && ((skeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.HandLeft && side == Side.Left) ||
                (skeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.HandRight && side == Side.Right)))
        {

            if (audioSource)
            {
                audioSource.time = 0f;
                audioSource.Play();
            }

            stay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OVRSkeleton skeleton = other.GetComponent<OVRSkeleton>();
        if (skeleton && ((skeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.HandLeft && side == Side.Left) ||
                (skeleton.GetSkeletonType() == OVRSkeleton.SkeletonType.HandRight && side == Side.Right)))
        {
            if (audioSource)
            {
                audioSource.time = 0f;
                audioSource.Stop();
            }

            stay = false;
        }
    }

    private void Update()
    {
        if (followHand)
        {
            transform.position = followHand.position;
        }

        if (stay && !MainManager.Instance.StartOrbRotating)
        {
            HoldTimer += Time.deltaTime;
        }
        else
        {
            HoldTimer = 0f;
        }
    }

    private void OnDisable()
    {
        HoldTimer = 0f;
        stay = false;
    }
}
