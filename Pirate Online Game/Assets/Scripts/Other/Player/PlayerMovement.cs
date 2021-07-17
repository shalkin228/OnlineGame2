using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class PlayerMovement : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private float speed = 5;
    [SerializeField] private Transform playerSprites, playerThings;

    private Vector2 joystickPos;
    private Rigidbody2D _rigidbody;
    private Animator anim;
    private PhotonView photonView;
    private Joystick joystick;
    private bool flipX;

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

            if(joystickPos.x > 0.01f)
            {
                flipX = false;
                FlipX();
            }
            else if(joystickPos.x < -0.01f)
            {
                flipX = true;
                FlipX();
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

    private void FlipX()
    {
        foreach(Transform sprite in playerSprites)
        {
            sprite.GetComponent<SpriteRenderer>().flipX = flipX;
        }

        foreach(Transform thing in playerThings)
        {
            thing.GetComponent<SpriteRenderer>().flipX = flipX;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(flipX);
        }
        else
        {
            flipX = (bool)stream.ReceiveNext();
            FlipX();
        }
    }
}
