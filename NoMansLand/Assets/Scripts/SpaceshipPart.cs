using UnityEngine;
namespace DefaultNamespace
{
    public class SpaceshipPart
    {
        //private string name;
        private float timeToCrumble;
        private Vector3 originalRelativePosition;
        private Vector3 originalRotation;
        
        public SpaceshipPart(float crumbleTime)
        {
            timeToCrumble = crumbleTime;
        }

        public Vector3 GetOriginalRelativePosition()
        {
            return originalRelativePosition;
        }

        public Vector3 GetOriginalRotation()
        {
            return originalRotation;
        }

        public float GetTimeToCrumble()
        {
            return timeToCrumble;
        }

        public void SetTimeToCrumble(float t)
        {
            timeToCrumble = t;
        }
    }
}