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

    private int leftHash;
    private int rightHash;

    private Vector3 rightFootPos;
    private Vector3 leftFootPos;

    private void Awake()
    {
        animatorGO = GetComponent<Animator>();
        rightHash = Animator.StringToHash("rightLeg");
        leftHash = Animator.StringToHash("leftLeg");
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
            weightRightFoot = animatorGO.GetFloat(rightHash);
            weightLeftFoot = animatorGO.GetFloat(leftHash);

            animatorGO.SetIKPositionWeight(AvatarIKGoal.RightFoot, weightRightHand);
            animatorGO.SetIKRotationWeight(AvatarIKGoal.RightFoot, weightRightHand);
            animatorGO.SetIKPositionWeight(AvatarIKGoal.LeftFoot, weightLeftFoot);
            animatorGO.SetIKRotationWeight(AvatarIKGoal.LeftFoot, weightLeftFoot);

            RaycastHit hit;

            if (Physics.Raycast(rightFoot.position, Vector3.down, out hit, 3f, rayLayer))
            {
                rightFootPos = hit.point;
            }
            animatorGO.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos);


            if (lookObj)
            {
                lookToObj();
            }
        }
        else
        {
            weightRightHand = 0;
            animatorGO.SetLookAtWeight(0);
        }

    }

    private void lookToObj()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, 10f);
        float lookRange = 10f;
        GameObject lookObj = null;
        foreach (Collider col in colls)
        {
            if (Vector3.Distance(transform.position, col.transform.position) < lookRange)
            {
                if (col.gameObject.tag != "Floor" && col.gameObject.tag != "Ethan")
                {
                    lookRange = Vector3.Distance(transform.position, col.transform.position);
                    lookObj = col.gameObject;
                }
            }
        }
        if (lookObj != null)
        {
            print(lookObj.gameObject.name);
            animatorGO.SetLookAtWeight(1);
            animatorGO.SetLookAtPosition(lookObj.transform.position);
        }
    }

}
