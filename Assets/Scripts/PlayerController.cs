using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{

    public float _speed = 5.0f;
    Rigidbody2D _rbD2_;
    PhotonView _pv_;



    // Start is called before the first frame update
    void Start()
    {
        _rbD2_ = GetComponent<Rigidbody2D>();
        _pv_ = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_pv_.IsMine)
        {
            PlayerMove();
            PlayerTurn();
        }  
    }

    void PlayerMove()
    {
        var _tempX_= Input.GetAxis("Horizontal");
        var _tempY_ = Input.GetAxis("Vertical");

        _rbD2_.velocity = new Vector2(_tempX_, _tempY_);// * _speed * Time.deltaTime;
    }

    void PlayerTurn()
    {
        Vector3 _mousePositionTemp_ = Input.mousePosition;

        _mousePositionTemp_ = Camera.main.ScreenToWorldPoint(_mousePositionTemp_);

        Vector2 _directionTemp_ = new Vector2( _mousePositionTemp_.x - transform.position.x,
                                               _mousePositionTemp_.y - transform.position.y  );

        transform.up = _directionTemp_;
    }

}
