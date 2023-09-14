using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailPanel : MonoBehaviour
{
    [SerializeField] private float _timeOnDail = 3f;
    [SerializeField] private GameObject[] _piecePrefabs;
    [SerializeField] private GameObject _comboPiece;
    private GameObject _piece;
    private Transform _pieceSpawner;

    private float _maxDegree = 150;
    private float _minDegree = 0;
    private float _angleBetween = 55;

    private int _numberOfPieces = 2;

    private List<float> usedAngles = new List<float>();

    private void Awake()
    {
        EventManager.TimerIsDone.AddListener(OnTimerIsDone);
        _pieceSpawner = GameObject.Find("PiecesSpawner").GetComponent<Transform>();
    }

    private void PlaceRandomPieces()
    {
        _piece = _piecePrefabs[Random.Range(0, _piecePrefabs.Length)];

        usedAngles.Clear();

        for (int i = 0; i < _numberOfPieces; i++)
        {
            float randomAngle;
            bool angleIsValid;

            do
            {
                randomAngle = Random.Range(_minDegree, _maxDegree);
                angleIsValid = !usedAngles.Exists(angle => Mathf.Abs(Mathf.DeltaAngle(angle, randomAngle)) < _angleBetween);
            } while (!angleIsValid);

            usedAngles.Add(randomAngle);

            float pieceRadius = _piece.GetComponent<SpriteRenderer>().bounds.size.x / 2;

            Instantiate(_piece, transform.position, Quaternion.Euler(0f, 0f, randomAngle + pieceRadius), _pieceSpawner.transform);
        }
    }

    private void PlaceComboPiece()
    {
        float angleBetweenPieces = (usedAngles[0] + usedAngles[1]) / 2;
        Instantiate(_comboPiece, transform.position, Quaternion.Euler(new Vector3(0, 0, angleBetweenPieces)), _pieceSpawner.transform);
    }

    private void SpawnPiecesPeriodically()
    {
        StartCoroutine(SpawnPieces());
    }

    IEnumerator SpawnPieces()
    {
        while (true)
        {
            PlaceRandomPieces();
            PlaceComboPiece();

            yield return new WaitForSeconds(_timeOnDail);

            DeleteAllChildObjects();
        }
    }

    private void OnTimerIsDone(bool timerIsDone)
    {
        if (timerIsDone)
        {
            SpawnPiecesPeriodically();
        }
    }

    public void DeleteAllChildObjects()
    {
        if (_pieceSpawner != null)
        {
            for (int i = _pieceSpawner.childCount - 1; i >= 0; i--)
            {
                Transform childTransform = _pieceSpawner.GetChild(i);
                Destroy(childTransform.gameObject);
            }
        }
    }

}
