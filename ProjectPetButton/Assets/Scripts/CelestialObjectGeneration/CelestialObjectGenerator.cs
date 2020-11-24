using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Gebaeckmeeting.PetButton
{
	public class CelestialObjectGenerator : MonoBehaviour
	{
		[SerializeField]
		private CelestialObjectSurface _surfacePrefab = null;

		public CelestialObjectSurface Create(CelestialObjectSurfaceData data)
		{
			CelestialObjectSurface result = GameObject.Instantiate(_surfacePrefab, null, true);
			result.GenerateMesh(data);
			return result;
		}
	}
}

