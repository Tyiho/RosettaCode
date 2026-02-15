using System;
using System.Collections.Generic;
using System.Text;

namespace RosettaCode.Problems._100Doors.Merl
{
	/// <summary>
	/// Toggler allows algorithm-based toggling of a door's state
	/// </summary>
	public abstract class PredicateDoorToggler : IDoorToggler
	{
		/// <summary>
		/// Use if there should never be a match.
		/// </summary>
		protected static Func<IStatefulDoor, uint, bool> NeverMatchPredicate = (door, pass) => false;

		//shouldn't be able to construct w/o setting, but we want a default to be safe.
		private Func<IStatefulDoor, uint, bool> m_TogglePredicate = NeverMatchPredicate;

		protected PredicateDoorToggler(Func<IStatefulDoor, uint, bool> predicate)
		{
			m_TogglePredicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
		}


		public virtual void Visit(IStatefulDoor door, uint pass)
		{
			if (m_TogglePredicate(door, pass)) door.ToggleState();
		}
	}
}
