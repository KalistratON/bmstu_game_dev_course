using UnityEngine;

namespace LearnGame
{
    public class TransformModel
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public TransformModel (Vector3 thePosition, Quaternion theRotation)
        {
            Position = thePosition;
            Rotation = theRotation;
        }
    }
}
