using UnityEngine;
namespace DefaultNamespace
{
    public class SpaceshipPart
    {
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

		public void SetOriginalRelativePosition(Vector3 opos)
		{
			originalRelativePosition = opos;
		}

		public void SetOriginalRotation(Vector3 orot)
		{
			originalRotation = orot;
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

		public void ReturnPieceToShip(GameObject g) {
			g.transform.localPosition = originalRelativePosition;
			g.transform.eulerAngles = originalRotation;
			g.GetComponent<Rigidbody>().isKinematic = true;
		}
    }
}