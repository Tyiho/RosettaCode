namespace RosettaCode.Problems._100Doors.Merl
{
	/// <summary>
	/// Toggles based on the pass and door position (where the door position is a multiple of the pass)
	/// </summary>
	public class StepDoorToggler : PredicateDoorToggler
	{
		private static Func<IStatefulDoor, uint, bool> s_StepPredicate = (door, pass) => door.Position % pass == 0;

		public StepDoorToggler() : base(s_StepPredicate) { }
	}
}
