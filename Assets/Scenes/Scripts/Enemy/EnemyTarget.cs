using UnityEngine;

namespace LearnGame.Enemy
{
    public class EnemyTarget
    {
        public GameObject Closest { get; private set; }

        private readonly Transform myAgentTransform;
        private readonly float myViewRadius;
        private readonly PlayerCharacterView myPlayer;
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; set; }
        public bool IsWeaponTaken { get; set; } = false;

        private readonly Collider[] myColliders = new Collider[10];

        public EnemyTarget (Transform agent, PlayerCharacterView player, float viewRadius, float maxHealth)
        {
            myAgentTransform = agent;
            myPlayer = player;
            myViewRadius = viewRadius;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void FindClosest()
        {
            float minDistance = float.MaxValue;
            var count = FindAllTargets(IsWeaponTaken ? LayerUtils.CharacterMask : LayerUtils.PickUpMask | LayerUtils.CharacterMask);

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

        private float DistanceFromAgentTo(GameObject go) => (myAgentTransform.position - go.transform.position).magnitude;
    }
}
