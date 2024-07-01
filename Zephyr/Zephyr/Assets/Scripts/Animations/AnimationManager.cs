using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator _animator;
    private string _currentAnim;

    // Start is called before the first frame update
    void Awake()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
    }

    public void ChangeAnimState(string newAnim)
    {
        if (_currentAnim == newAnim) return;

        _animator.Play(newAnim);

        _currentAnim = newAnim;
    }


}
