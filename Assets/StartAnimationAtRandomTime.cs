using UnityEngine;

public class StartAnimationAtRandomTime : MonoBehaviour
{
    private void Start()
    {
        var animator = GetComponent<Animator>();
        var state = animator.GetCurrentAnimatorStateInfo(0);
        animator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
