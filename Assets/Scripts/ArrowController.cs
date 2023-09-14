using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float _animationSecond;
    [SerializeField] private float _pauseArrowAfterTap = 1f;
    private Tweener _rotationTweener;
    private string _onPiece = "miss";
    private void Start()
    {
        _rotationTweener = transform.DORotate(new Vector3(0, 0, 90), _animationSecond)
            .From(new Vector3(0, 0, -90))
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _onPiece = collision.gameObject.tag;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _onPiece = "miss";
    }

    public void OnTap()
    {
        EventManager.CallOnTap(_onPiece, transform.rotation.z);
        StartCoroutine(PauseArrow());
        
    }

    IEnumerator PauseArrow()
    {
        _rotationTweener.Pause();
        yield return new WaitForSeconds(_pauseArrowAfterTap);
        _rotationTweener.Play();
    }
}
