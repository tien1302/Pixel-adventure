using Assets.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
    public float swordDamage = 1f;
    public float knockbackForce = 15f;
    public Collider2D swordCollider;
    public Vector3 faceRight = new Vector3(1, 0, 0);
    public Vector3 faceLeft = new Vector3(-1, 0, 0);

    void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogWarning("Swrod collider not set");
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            //Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector3 parentPosition = transform.parent.position;

            Vector2 direction = (collider.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;
            //collider.SendMessage("OnHit", swordDamage, knockback);
            damageableObject.OnHit(swordDamage, knockback);
        }
    }

    void IsFacingRight(bool isFacingRight)
    {
        if (isFacingRight)
        {
            gameObject.transform.localPosition = faceRight;
        }
        else
        {
            gameObject.transform.localPosition = faceLeft;
        }
    }

}
