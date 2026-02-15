namespace RosettaCode.Problems._100Doors.Merl
{
	/// <summary>
	/// A door toggler that will choose to toggle a visited door's state.
	/// The method or algorithm for determing when to toggle is up to the implementation.
	/// </summary>
	public interface IDoorToggler
	{
		void Visit(IStatefulDoor door, uint pass);
	}
}
