using StarterAssets;
using UnityEngine;

public class FallingDamage : MonoBehaviour
{
    public float fallDistanceThreshold = 10f; // Distance threshold to disable the Animator
    private Animator animator;
    private CharacterController cc;
    private ThirdPersonController tpc;
    private Vector3 initialPosition;
    private bool animatorDisabled = false;
    private bool initPosSet = false;
    private bool distanceExceeded = false;

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        tpc = GetComponent<ThirdPersonController>();

    }

    void Update()
    {
        if(animator.GetBool("Grounded") == false && initPosSet == false && animatorDisabled == false)
        {
            initPosSet = true;
            initialPosition = transform.position;
        }
        // Calculate the distance fallen
        float distanceFallen = initialPosition.y - transform.position.y;

        // Check if the distance fallen exceeds the threshold
        if (distanceFallen >= fallDistanceThreshold && !animatorDisabled)
        {
            distanceExceeded = true;
            // Disable the Animator component
            animatorDisabled = true;
        }

        if (animator.GetBool("Grounded"))
        {
            initPosSet = false;
            if(distanceExceeded == true)
            {
                animator.enabled = false;
                cc.enabled = false;
                tpc.enabled = false;
            }
        }
    }
}
