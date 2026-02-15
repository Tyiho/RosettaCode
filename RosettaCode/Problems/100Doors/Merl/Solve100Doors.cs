using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RosettaCode.Problems._100Doors.Merl
{
	public class Solve100Doors
	{
		/* 
		 * Notes:
		 * The problem looks like we make 100 passes over the doors, where we toggle the door state when the position is a multiple of the pass.
		 * There are 100 passes total
		 * We do not want algorithm optimizations so that we have a fair comparison between languages
		 * 
		 * We won't special case the first pass where everyting is toggled to true.
		 * Parallelization will be applied for demonstration but in separate methods.
		 */


		public void Solve_Basic_Unoptimized(uint doorCount, uint passCount, out IEnumerable<uint> openDoors, out IEnumerable<uint> closedDoors)
		{
			//for safety, because we must set out parameters, initialize to empty.
			openDoors = [];
			closedDoors = [];

			//we can treat the positioned doors as an array of booleans
			//where the index is the door's position and the boolean is its open state
			var doors = new bool[doorCount];

			for (uint pass = 1; pass <= passCount;  pass++) //pass is 1-based to mirror the problem description
			{ 
				/* Note: This inner loop could be run in parallel as each doors state is independent */
				for(uint door = 0; door < doorCount; door++)
				{
					//we can use the mod function to know when to toggle
					if ((door + 1) % pass == 0)
					{
						doors[door] = !doors[door];
					}
				}
			}


			//to return an answer, we'll need to take the final door states and separate into our return parameters
			List<uint> open = [];
			List<uint> closed = [];

			for (uint i=0; i < doors.Length; i++)
			{
				if (doors[i])
				{
					open.Add(i + 1);
				}
				else
				{ 
					closed.Add(i + 1);
				}
			}

			openDoors = open;
			closedDoors = closed;
		}

		public void Solve_Basic_Parallel(uint doorCount, uint passCount, out IEnumerable<uint> openDoors, out IEnumerable<uint> closedDoors)
		{
			//for safety, because we must set out parameters, initialize to empty.
			openDoors = [];
			closedDoors = [];

			//we can treat the positioned doors as an array of booleans
			//where the index is the door's position and the boolean is its open state
			var doors = new bool[doorCount];

			Parallel.For(1, passCount + 1, pass =>
			{
				Parallel.For(0, doorCount,  door =>
				{
					//we can use the mod function to know when to toggle
					if ((door + 1) % pass == 0)
					{
						doors[door] = !doors[door];
					}
				});
			});

			//to return an answer, we'll need to take the final door states and separate into our return parameters
			List<uint> open = [];
			List<uint> closed = [];

			for (uint i = 0; i < doors.Length; i++)
			{
				if (doors[i])
				{
					open.Add(i + 1);
				}
				else
				{
					closed.Add(i + 1);
				}
			}

			openDoors = open;
			closedDoors = closed;
		}

		public void Solve_Class_Unoptimized(uint doorCount, uint passCount, out IEnumerable<uint> openDoors, out IEnumerable<uint> closedDoors)
		{
			//for safety, because we must set out parameters, initialize to empty.
			openDoors = [];
			closedDoors = [];

			List<IStatefulDoor> doors = [];

			for(uint i = 1; i <= doorCount; i++)
			{ 
				doors.Add(new StatefulDoor(i));
			}

			//as this is unoptimized, we won't special case the first pass where everyting is toggled to true.

			for (uint pass = 1; pass <= passCount; pass++) 
			{
				//as each door knows its position, we're not forced to a sequential evaluation
				//but we're not going to run this in parallel just yet.
				foreach(var door in doors)
				{
					if (door.Position % pass == 0)
					{
						door.ToggleState();
					}
				}
			}

			//to return an answer, we'll need to take the final door states and separate into our return parameters
			//if extension methods are not allowed, we can use the loop as found in the most basic implementation.
			openDoors = doors.Where(d => d.IsOpen).Select(d => d.Position);
			closedDoors = doors.Where(d => !d.IsOpen).Select(d => d.Position);
		}

		public void Solve_Class_Parallel(uint doorCount, uint passCount, out IEnumerable<uint> openDoors, out IEnumerable<uint> closedDoors)
		{
			//for safety, because we must set out parameters, initialize to empty.
			openDoors = [];
			closedDoors = [];

			List<IStatefulDoor> doors = [];

			for (uint i = 1; i <= doorCount; i++)
			{
				doors.Add(new StatefulDoor(i));
			}

			//each evaluation (pass and door) is independnt, all must be hit
			//but the independence lets us parallelize both loops.
			//Parallel.For is includive lower, exclusive upper.
			Parallel.For(1, passCount + 1, pass => { 
											Parallel.ForEach(doors, door =>
											{
												if (door.Position % pass == 0)
												{
													door.ToggleState();
												}
											});
										});

			//to return an answer, we'll need to take the final door states and separate into our return parameters
			//if extension methods are not allowed, we can use the loop as found in the most basic implementation.
			openDoors = doors.Where(d => d.IsOpen).Select(d => d.Position);
			closedDoors = doors.Where(d => !d.IsOpen).Select(d => d.Position);
		}

		public void Solve_Togglers_Hierarchy_Unoptimized(uint doorCount, uint passCount, out IEnumerable<uint> openDoors, out IEnumerable<uint> closedDoors)
		{
			//for safety, because we must set out parameters, initialize to empty.
			openDoors = [];
			closedDoors = [];

			List<IStatefulDoor> doors = [];

			for (uint i = 1; i <= doorCount; i++)
			{
				doors.Add(new StatefulDoor(i));
			}

			//we could have more than one implementation
			//but this problem only has 1.
			List<IDoorToggler> togglers = [ new StepDoorToggler() ];

			//as this is unoptimized, we won't special case the first pass where everyting is toggled to true.

			for (uint pass = 1; pass <= passCount; pass++)
			{
				foreach (var door in doors)
				{
					foreach (var toggler in togglers)
					{
						toggler.Visit(door, pass);
					}
				}
			}

			//to return an answer, we'll need to take the final door states and separate into our return parameters
			openDoors = doors.Where(d => d.IsOpen).Select(d => d.Position);
			closedDoors = doors.Where(d => !d.IsOpen).Select(d => d.Position);
		}

		public void Solve_Togglers_Hierarchy_Parallel(uint doorCount, uint passCount, out IEnumerable<uint> openDoors, out IEnumerable<uint> closedDoors)
		{
			//for safety, because we must set out parameters, initialize to empty.
			openDoors = [];
			closedDoors = [];

			List<IStatefulDoor> doors = [];

			for (uint i = 1; i <= doorCount; i++)
			{
				doors.Add(new StatefulDoor(i));
			}


			List<IDoorToggler> togglers = [new StepDoorToggler()];

			Parallel.For(1, passCount + 1, pass =>
			{
				Parallel.ForEach(doors, door =>
				{
					//must cast pass because Parallel.For doesn't have an appropriate overload
					Parallel.ForEach(togglers, toggler => { toggler.Visit(door, (uint)pass); });
				});
			});

			//to return an answer, we'll need to take the final door states and separate into our return parameters
			openDoors = doors.Where(d => d.IsOpen).Select(d => d.Position);
			closedDoors = doors.Where(d => !d.IsOpen).Select(d => d.Position);
		}

		public void Solve_Togglers_Unoptimized(uint doorCount, uint passCount, out IEnumerable<uint> openDoors, out IEnumerable<uint> closedDoors)
		{
			//for safety, because we must set out parameters, initialize to empty.
			openDoors = [];
			closedDoors = [];

			List<IStatefulDoor> doors = [];

			for (uint i = 1; i <= doorCount; i++)
			{
				doors.Add(new StatefulDoor(i));
			}

			//we could have more than one implementation
			//but this problem only has 1.
			List<IDoorToggler> togglers = [new PassBasedDoorToggler()];

			//as this is unoptimized, we won't special case the first pass where everyting is toggled to true.

			for (uint pass = 1; pass <= passCount; pass++)
			{
				foreach (var door in doors)
				{
					foreach (var toggler in togglers)
					{
						toggler.Visit(door, pass);
					}
				}
			}

			//to return an answer, we'll need to take the final door states and separate into our return parameters
			openDoors = doors.Where(d => d.IsOpen).Select(d => d.Position);
			closedDoors = doors.Where(d => !d.IsOpen).Select(d => d.Position);
		}

		public void Solve_Togglers_Parallel(uint doorCount, uint passCount, out IEnumerable<uint> openDoors, out IEnumerable<uint> closedDoors)
		{
			//for safety, because we must set out parameters, initialize to empty.
			openDoors = [];
			closedDoors = [];

			List<IStatefulDoor> doors = [];

			for (uint i = 1; i <= doorCount; i++)
			{
				doors.Add(new StatefulDoor(i));
			}


			List<IDoorToggler> togglers = [new PassBasedDoorToggler()];

			Parallel.For(1, passCount + 1, pass =>
			{
				Parallel.ForEach(doors, door =>
				{
					//must cast pass because Parallel.For doesn't have an appropriate overload
					Parallel.ForEach(togglers, toggler => { toggler.Visit(door, (uint)pass); });
				});
			});

			//to return an answer, we'll need to take the final door states and separate into our return parameters
			openDoors = doors.Where(d => d.IsOpen).Select(d => d.Position);
			closedDoors = doors.Where(d => !d.IsOpen).Select(d => d.Position);
		}

	}
}
