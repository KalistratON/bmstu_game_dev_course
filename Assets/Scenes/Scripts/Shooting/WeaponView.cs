using UnityEngine;
using System.Collections;

namespace LearnGame.Shooting
{
    [RequireComponent(typeof(AudioSource))]
    public class WeaponView : MonoBehaviour
    {
        [field: SerializeField]
        public Transform myBulletSpawnPos { get; private set; }

        public WeaponModel Model { get; private set; }

        [SerializeField]
        private ParticleSystem myShootParticle;

        [SerializeField]
        private AudioSource myAudioSource;

        [SerializeField]
        public BulletView myBulletPrefab;

        public void Initialize (WeaponModel theModel)
        {
            if (Model != null)
            {
                Debug.LogWarning ("Weapon model has been already initialized!");
                return;
            }
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            Model = theModel;
            Model.Shot += Shoot;
        }

        protected void OnDestroy()
        {
            if (Model != null)
            {
                Model.Shot -= Shoot;
            }
        }

        protected void Awake()
        {
            myAudioSource = GetComponent<AudioSource>();
        }

        public void Shoot (Vector3 theTargetDirection, WeaponDescription theDescription)
        {
            var aBullet = Instantiate (myBulletPrefab, myBulletSpawnPos.position, Quaternion.identity);
            aBullet.Initialize (
                theTargetDirection, theDescription.BulletMaxDistance, theDescription.BulletSpeed, theDescription.Damage);

            myShootParticle.Play();
            myAudioSource.Play();
        }
    }
}