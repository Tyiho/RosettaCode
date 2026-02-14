namespace RosettaCode.Problems._100Doors.Merl
{
	/// <summary>
	/// A simple door.
	/// </summary>
	public class StatefulDoor : IStatefulDoor
	{
		private object m_StateLock = new();
		private bool m_IsOpen;

		public uint Position { get; private set;}

		public StatefulDoor(uint position, bool initialState = false) 
		{
			Position = position;
			m_IsOpen = initialState;
		}

		public bool IsOpen
		{ 
			get 
			{
				lock (m_StateLock)
				{
					return m_IsOpen;
				}
			}
		}

		public void ToggleState()
		{
			lock (m_StateLock)
			{ 
				m_IsOpen = !m_IsOpen;
			}
		}
	}
}
