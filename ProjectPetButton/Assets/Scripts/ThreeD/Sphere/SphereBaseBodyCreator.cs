namespace Gebaeckmeeting.ThreeD
{
	/// <summary>
    /// Constructs and returns the geometric base body for the creation of a sphere
    /// </summary>
    public abstract class SphereBaseBodyCreator
    {
        public abstract Body Create(float radius);
    }
}