using UnityEngine;









namespace Tanks.Explosions
{
	/// <summary>
	/// Enum representing the type of explosion
	/// </summary>
	public enum ExplosionClass
	{
		Large,
		Small,
		ExtraLarge,
		TankExplosion,
		TurretExplosion,
		BounceExplosion,
		ClusterExplosion,
		FiringExplosion,
        Num
	}

    public enum BulletClass
    {
        FiringExplosion,
        ClusterExplosion,
        PulseExplosion,
        Num
    }

    public enum PLAYGAMEMODEL
    {
        PLAYGAME_FPS,
        PLAYGAME_TPS,
    }

	public class ExplosionSettings : ScriptableObject
	{
		public string       id;
        public BulletClass  explosionClass;
		public float        explosionRadius;
		public float        damage;
		public float        physicsRadius;
		public float        physicsForce;
        public float        moventspedd;
        public float        lifetime;
		[Range(0, 1)]
		public float shakeMagnitude;
	}
}