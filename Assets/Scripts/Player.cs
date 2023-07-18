using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;

    private SpriteRenderer _renderer;
    private Animator _animator;
    private bool _isMoving = false;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            _isMoving = true;
            _renderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
            _isMoving = true;
            _renderer.flipX = true;
        }
        else
        {
            _isMoving = false;
        }

        _animator.SetFloat("Speed", _isMoving ?  _speed : 0);
    }
}
