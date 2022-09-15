using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float _speed = 5.0f;
    Rigidbody2D _rbD2_;
    PhotonView _pv_;

    [Header("Player Heath")]
    public float _playerHeathMax = 100f;
    float _playerHeathCurrent_;
    public Image _imgPlayerHealthFill;

    [Header("Bullet")]
    public GameObject _spawnBulletPosition;
    public GameObject _objBulletPrefab;
    public GameObject _obj_Pv_BulletPrefab;


    // Start is called before the first frame update
    void Start()
    {
        HeathManager(_playerHeathMax);

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
            ShootBullet();
        }

        /*if (Input.GetMouseButtonDown(1))
        {
            HeathManager(-10f);
        }*/
    }

    void HeathManager(float _tempValue_)
    {
        _playerHeathCurrent_ += _tempValue_;
        _imgPlayerHealthFill.fillAmount = _playerHeathCurrent_/100f;
    }

    #region --- Player Movement ---
    void PlayerMove()
    {
        var _tempX_= Input.GetAxis("Horizontal");
        var _tempY_ = Input.GetAxis("Vertical");
        _rbD2_.velocity = new Vector2(_tempX_, _tempY_) * _speed * Time.deltaTime;
    }

    void PlayerTurn()
    {
        Vector3 _mousePositionTemp_ = Input.mousePosition;

        _mousePositionTemp_ = Camera.main.ScreenToWorldPoint(_mousePositionTemp_);

        Vector2 _directionTemp_ = new Vector2( _mousePositionTemp_.x - transform.position.x,
                                               _mousePositionTemp_.y - transform.position.y );

        transform.up = _directionTemp_;
    }
    #endregion--- Player Movement ---

    #region --- Shooting ---

    void ShootBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pv_.RPC("ShootBulletRPC", RpcTarget.All);
        }
        if (Input.GetMouseButtonDown(1))
        {
            PhotonNetwork.Instantiate(_obj_Pv_BulletPrefab.name, _spawnBulletPosition.transform.position, _spawnBulletPosition.transform.rotation, 0);
        }
    }

    [PunRPC]
    void ShootBulletRPC()
    {
        Instantiate(_objBulletPrefab, _spawnBulletPosition.transform.position, _spawnBulletPosition.transform.rotation);
    }

    public void TakeDamage(float _tempValue_)
    {
        _pv_.RPC("TakeDamageRPC", RpcTarget.AllBuffered, _tempValue_);
    }

    [PunRPC]
    void TakeDamageRPC(float _tempValue_)
    {
        HeathManager(_tempValue_);
    }



    #endregion --- Shooting ---
}
