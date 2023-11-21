using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{
    bool _hasDied = false;
    SpriteRenderer _sr;

    [SerializeField]
    Sprite _deadSprite;
    [SerializeField]
    ParticleSystem _particleSystem;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die());
        }   
    }

    IEnumerator Die()
    {
        _hasDied = true;

        _sr.sprite = _deadSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied)
            return false;

        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;

        if (collision.contacts[0].normal.y < -0.5)
            return true;

        return false;
    }
}
