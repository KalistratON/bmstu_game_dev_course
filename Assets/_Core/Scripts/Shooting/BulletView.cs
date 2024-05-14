using LearnGame.Timer;

using UnityEngine;

namespace LearnGame.Shooting
{

    public class BulletView : MonoBehaviour
    {
        public BulletModel Model { get; private set; }


        public void Initialize (Vector3 theDirection, float theMaxDistance, float theSpeed, float theDamage)
        {
            Model = new BulletModel (theDirection, theMaxDistance, theSpeed, theDamage, new UnityTimer());
            Model.OnBulletDestoy += DestoyBullet;
        }

        protected void Update()
        {
            transform.Translate (Model.Move());
        }

        private void DestoyBullet()
        {
            Model.OnBulletDestoy -= DestoyBullet;
            Destroy (gameObject);
        }
    }
}
