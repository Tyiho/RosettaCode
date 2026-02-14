namespace RosettaCode.Problems._100Doors.Merl
{
	/// <summary>
	/// A door which has an ID and has a toggleable state.
	/// </summary>
	public interface IStatefulDoor
	{
		uint Position { get;}
		bool IsOpen { get;}
		void ToggleState();
	}
}
