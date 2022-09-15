using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class BulletController : MonoBehaviour
{
    public Rigidbody2D _rb_;
    float _bulletTimeCount_;

    [Header("Bullets Configs")]    
    public float _bulletSpeed_ = 100f;
    public float _bulletTimeLife = 5f;
    public float bulletDamage = 10f;


    // Start is called before the first frame update
    void Start()
    {
        _rb_.GetComponent<Rigidbody2D>();
        _rb_.AddForce(transform.up * _bulletSpeed_, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        if (_bulletTimeCount_ >= _bulletTimeLife)
        {
            Destroy(this.gameObject);
        }
        _bulletTimeCount_ += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerController>() && other.GetComponent<PhotonView>().IsMine)
        {
            other.GetComponent<PlayerController>().TakeDamage(bulletDamage);
        }
    }

}
