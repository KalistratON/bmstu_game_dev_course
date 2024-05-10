using UnityEngine;

namespace LearnGame.Shooting
{
    public interface IShootingTarget
    {
        BaseCharacterModel GetTarget (Vector3 thePosition, float theRadius);
    }
}
