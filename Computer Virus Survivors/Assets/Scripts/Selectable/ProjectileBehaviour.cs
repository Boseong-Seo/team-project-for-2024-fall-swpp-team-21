using System;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour
{
    protected abstract void OnTriggerEnter(Collider other);
    private AttackEffect attackEffect = new AttackEffect();
    [SerializeField] private AttackEffectType attackEffectType;

    protected virtual void PlayAttackEffect(Vector3 hitPosition, Quaternion rotation = default)
    {
        attackEffect.Play(hitPosition, rotation, attackEffectType);
    }

    protected bool CheckOutOfScreen()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        return viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1;
    }
}
