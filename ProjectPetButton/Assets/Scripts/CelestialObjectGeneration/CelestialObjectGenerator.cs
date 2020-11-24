using Gebaeckmeeting.ThreeD;
using UnityEngine;

namespace Gebaeckmeeting.PetButton
{
	public class CelestialObjectGenerator : MonoBehaviour
	{
		[SerializeField]
		private Sphere _bodyPrefab = null;

		public Sphere Create(SphereData data)
		{
			Sphere result = GameObject.Instantiate(_bodyPrefab, null, true);
			result.SetData(data);
			result.GenerateMesh();
			return result;
		}
	}
}

