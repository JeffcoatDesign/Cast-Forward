using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;
using System.Linq;

public class SpellArc : SpellProjectile
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _lifetime = 0.5f;
    private List<Entity> _entities = new();
    public List<Entity> chainedEntities = new();
    public int maxChainedEntities = 2;
    private Entity _target;
    private SpellArc _nextArc;
    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Entity entity = other.GetComponent<Entity>();
            if (!_entities.Contains(entity))
                _entities.Add(entity);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Entity entity = other.GetComponent<Entity>();
            if (_entities.Contains(entity))
                _entities.Remove(entity);
        }
    }
    public override void OnHit(Collider other) { }
    public void Visit(SpellArc visitor)
    {
        chainedEntities = visitor.chainedEntities;
    }
    private void LateUpdate()
    {
        if (_target != null) {
            _lineRenderer.SetPosition(0, transform.position);
            for (int i = 1; i < 5; i++)
            {
                Vector3 position = Vector3.Lerp(transform.position, _target.transform.position, 0.2f * i);
                position += Random.insideUnitSphere;
                _lineRenderer.SetPosition(i, position);
            }
            _lineRenderer.SetPosition(5, _target.transform.position);
        }
        bool hasEnts = _entities.Count > 0;
        _lineRenderer.enabled = hasEnts;
        if (!hasEnts) return;
        _entities.OrderByDescending(ent => Vector3.Distance(transform.position, ent.transform.position));
        _entities.RemoveAll(ent => chainedEntities.Contains(ent));
        if (_entities.Count > 0 && _entities[0] != null)
        {
            if (_entities[0] != _target)
            {
                _target = _entities[0];
                chainedEntities.Add(_target);
                _nextArc = Instantiate(gameObject, _target.transform).GetComponent<SpellArc>();
                _nextArc.Visit(this);
                _target.GetHit(damageMultiplier);
            }
        }
    }
}
