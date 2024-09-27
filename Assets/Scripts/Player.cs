using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private int heath = 0;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private float _canFire = -1f;
    [SerializeField] float _speed = 4.0f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] int _life = 5;

    private Spawnman _spawnMan;
    Vector3 initial = new Vector3(0, 0, 0);
    void Start()
    {
        transform.position = initial;
        _spawnMan = GameObject.Find("Spawnman").GetComponent<Spawnman>();
        if (_spawnMan == null)
        {
            Debug.LogError("Ops");

        }
    }
    void Update()
    {
        CalculateMovement();
        PlayerFire();
    }
    void PlayerFire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && heath < 5)
        {
            heath = heath + 1;
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && heath >= 5)
        {
            _canFire = Time.time + _fireRate;
            heath = 0;
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        if (transform.position.x >= 10)
        {
            transform.position = new Vector3(-10, transform.position.y, 0);
        }

        else if (transform.position.x <= -10)
        {
            transform.position = new Vector3(10, transform.position.y, 0);
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0, 9), 0);
    }
    public void Damage()
    {
        _life -= 1;
        if (_life <= 0)
        {
            _spawnMan.PlayerDeath();
            Destroy(this.gameObject);
        }
    }

}
