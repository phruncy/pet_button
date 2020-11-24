using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.ThreeD
{
	public class IcosahedronSphereBaseBodyCreator : SphereBaseBodyCreator
	{
		Icosahedron _baseBodyPrefab;

		public IcosahedronSphereBaseBodyCreator(Icosahedron baseBodyPrefab) : base()
		{
			Assert.IsNotNull(baseBodyPrefab);
			_baseBodyPrefab = baseBodyPrefab;
		}

		public override Body Create(float radius)
		{
			Icosahedron baseBody = GameObject.Instantiate(_baseBodyPrefab, null);
			baseBody.Radius = radius;
			baseBody.GenerateMesh();
			return baseBody;
		}
	}
}