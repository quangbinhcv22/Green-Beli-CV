using GRBESystem.Entity;
using GRBESystem.Entity.Element;
using UnityEngine;

public class BattleElementTagParticle : MonoBehaviour
{
    public HeroElement elementTag;
    private ParticleSystem _particleSystem;

    public ParticleSystem GetParticleSystem { get { return this._particleSystem; } }

    private void Awake()
    {
        this._particleSystem = GetComponent<ParticleSystem>();
    }
}
