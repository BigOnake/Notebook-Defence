using UnityEngine;

public interface IProjectileMover
{
    Vector2 ModifyMovement(Projectile projectile);
    Quaternion? ModifyRotation(Projectile projectile);
}
