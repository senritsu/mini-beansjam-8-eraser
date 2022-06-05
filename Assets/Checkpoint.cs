using System;
using System.Linq;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameProgression.Checkpoint checkpoint;
    private Animator _animator;
    private static readonly int IsActiveParameter = Animator.StringToHash("IsActive");
    public bool IsActive => GameProgression.Instance.activatedCheckpoints.Contains(checkpoint);

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        _animator.SetBool(IsActiveParameter, IsActive);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (IsActive || !col.GetComponent<Player>()) return;

        GameProgression.Instance.AddCheckpoint(checkpoint);
            
        UpdateAnimator();
    }
}
