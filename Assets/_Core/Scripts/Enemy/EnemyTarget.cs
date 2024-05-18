using UnityEngine;

namespace LearnGame.Enemy
{
    public class EnemyTarget : IEnemyTarget
    {
        public GameObject Closest { get; private set; }
        public float MaxHealth { get; private set; }

        private readonly PlayerCharacterView myPlayer;
        private readonly Transform myAgentTransform;
        private readonly float myViewRadius;

        public readonly EnemyCharacterView mySelf;

        private readonly Collider[] myColliders = new Collider[10];

        public EnemyTarget (Transform agent, PlayerCharacterView player, float viewRadius, EnemyCharacterView theEnemy)
        {
            myAgentTransform = agent;
            myPlayer = player;
            myViewRadius = viewRadius;
            mySelf = theEnemy;
            MaxHealth = theEnemy.Model.Health;
        }

        public float CurrentHealth()
        {
            if (mySelf == null || mySelf.Model == null)
            {
                return 0.0f;
            }

            return mySelf.Model.Health;
        }

        public bool IsWeaponTaken()
        {
            return mySelf == null || mySelf.Model == null ? false : mySelf.Model.HasWeapon;
        }

        public void FindClosest()
        {
            if (mySelf == null || mySelf.Model == null)
            {
                return;
            }

            float minDistance = float.MaxValue;

            var count = FindAllTargets (IsWeaponTaken() ? LayerUtils.CharacterMask : LayerUtils.PickUpMask | LayerUtils.CharacterMask);

            for (int i = 0; i < count; ++i)
            {
                var go = myColliders[i].gameObject;
                if (go == myAgentTransform.gameObject) continue;

                var distance = DistanceFromAgentTo(go);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    Closest = go;
                }
            }

            if (myPlayer != null && DistanceFromAgentTo (myPlayer.gameObject) < minDistance)
            {
                Closest = myPlayer.gameObject;
            }
        }

        public float DistanceToClosestFromAgent()
        {
            if (Closest != null)
            {
                return DistanceFromAgentTo(Closest);
            }
            return 0f;
        }
        private int FindAllTargets(int layerMask)
        {
            var size = Physics.OverlapSphereNonAlloc(myAgentTransform.position, myViewRadius, myColliders, layerMask);
            return size;
        }

        public void Retreat()
        {
            mySelf.Retreat();
        }

        private float DistanceFromAgentTo (GameObject go) => (myAgentTransform.position - go.transform.position).magnitude;
    }
}
