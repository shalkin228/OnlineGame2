using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] private float speed = 5;

    private Vector2 joystickPos;
    private Rigidbody2D _rigidbody;
    private Animator anim;
    private PhotonView photonView;
    private Joystick joystick;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        _rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            joystickPos = new Vector2(joystick.Horizontal, joystick.Vertical);

            if (joystickPos.x > 0.01f || joystickPos.x < -0.01f ||
                joystickPos.y > 0.01f || joystickPos.y < -0.01f)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            _rigidbody.MovePosition(_rigidbody.position + joystickPos * speed * Time.deltaTime);
        }
    }
}
