using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems._100Doors.Merl
{
	public class PassBasedDoorToggler : IDoorToggler
	{
		public void Visit(IStatefulDoor door, uint pass)
		{
			if (door.Position % pass == 0) door.ToggleState();
		}
	}
}
