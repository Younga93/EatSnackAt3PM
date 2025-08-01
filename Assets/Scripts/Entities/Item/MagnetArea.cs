using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagnetArea : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _duration;
    [SerializeField] private PlayerController _playerController;

    private CircleCollider2D _circleCollider;

    private const string ItemTag = "Item";

    private List<IAttractable> _attractableItems;

    public void Init(float duration, PlayerController player)
    {
        _attractableItems = new List<IAttractable>();
        _circleCollider = GetComponent<CircleCollider2D>();
        //_circleCollider.radius = _radius;
        _duration = duration;
        _playerController = player;

        if (GetComponent<Rigidbody2D>() == null)
        {
            Rigidbody2D rigid = transform.AddComponent<Rigidbody2D>();
            rigid.isKinematic = true;
        }
        StopAllCoroutines();
        StartCoroutine(EndMagnetArea(duration));
    }

    IEnumerator EndMagnetArea(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.position = _playerController.transform.position;
        foreach (IAttractable item in _attractableItems)
        {
            item.AttractedBy(transform.position);
        }
    }

    private void OnDisable()
    {
        _attractableItems = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _radius * transform.localScale.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(ItemTag))
        {
            if(collision.TryGetComponent<IAttractable>(out var attractable)) { _attractableItems.Add(attractable); }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(ItemTag))
        {
            if (collision.TryGetComponent<IAttractable>(out var attractable)) { _attractableItems.Remove(attractable); }
        }
    }
}
