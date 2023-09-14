using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _forceMagnitude;
    [SerializeField] private float _forceMagnitudeCombo;
    [SerializeField] private float _forceMagnitudeMiss;

    private Rigidbody2D _playerRigidBody;
    private string _onPiece;
    private Quaternion _arrowRotation;
    private Vector2 _forceDircation;
    private bool _hasAppliedForce = false;

    private void Start()
    {
        EventManager.OnTap.AddListener(GetDiraction);
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_hasAppliedForce)
        {
            JumpController(_onPiece);
        }
        _hasAppliedForce = false;
    }

    private void JumpController(string piece)
    {
        _forceDircation = _arrowRotation * Vector3.up;
        if (piece == "Piece") { _playerRigidBody.AddForce(_forceDircation * _forceMagnitude); }
        else if (piece == "ComboPiece") { _playerRigidBody.AddForce(_forceDircation * _forceMagnitudeCombo); }
        else { _playerRigidBody.AddForce(Vector3.down * _forceMagnitudeMiss); }

        
    }

    private void GetDiraction(string onPiece, float angle)
    {
        _onPiece = onPiece;
        _arrowRotation.z = angle;
        Debug.Log(_onPiece);
        Debug.Log(_arrowRotation);

        _hasAppliedForce = true;
    }
}
