public class Weapon
{
    Projectile projectilePrefab;
    ObjectPool<Projectile> projectilePool;

    void Awake()
    {
        SetupProjectilePool();
    }

    void SetupProjectilePool()
    {
        projectilePool = new ObjectPool<Projectile>(projectilePrefab , "Projectile Pool");
    }

    public Projectile GetProjectile()
    {
        Projectile projectile = projectilePool.GetObject();
        projectile.gameObject.SetActive(true);
        return projectile;
    }
}
