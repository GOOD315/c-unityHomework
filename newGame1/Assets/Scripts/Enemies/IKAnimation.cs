using UnityEngine;


[RequireComponent(typeof(Animator))]
public class IKAnimation : MonoBehaviour
{
    [SerializeField] private Animator animatorGO;
    [SerializeField] private Transform handObj;
    [SerializeField] private Transform lookObj;

    [SerializeField] private Transform rightFoot;
    [SerializeField] private Transform leftFoot;

    [SerializeField] private float weightRightFoot;
    [SerializeField] private float weightLeftFoot;

    [SerializeField] private bool ikActive;
    [SerializeField] private LayerMask rayLayer;

    [SerializeField] float weightRightHand = 1;

    private void Awake()
    {
        animatorGO = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (handObj)
        {
            animatorGO.SetIKPositionWeight(AvatarIKGoal.RightHand, weightRightHand);
            animatorGO.SetIKRotationWeight(AvatarIKGoal.RightHand, weightRightHand);

            animatorGO.SetIKPosition(AvatarIKGoal.RightHand, handObj.position);
            animatorGO.SetIKRotation(AvatarIKGoal.RightHand, handObj.rotation);
        }

        if (ikActive)
        {
            weightRightHand = 1;

            if (lookObj)
            {
                animatorGO.SetLookAtWeight(1);
                animatorGO.SetLookAtPosition(lookObj.position);
            }
        }
        else
        {
            weightRightHand = 0;
            animatorGO.SetLookAtWeight(0);
        }

    }

}
