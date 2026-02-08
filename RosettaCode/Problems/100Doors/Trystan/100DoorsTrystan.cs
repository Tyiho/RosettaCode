namespace RosettaCode.Problems._100Doors.Trystan;
public class _100DoorsTrystan
{
    public bool[] Result { get; private set; } = new bool[100];


    private readonly bool[] _doors = new bool[100];
    
    public void RunProblem()
    {
        for (int i = 0; i < this._doors.Length; i++)
        {
            this._doors[i] = false;
        }

        for (int i = 0; i < this._doors.Length; i++)
        {
            for (int j = 1; j <= 100; j++)
            {
                if (((i + 1) % j) == 0)
                {
                    this._doors[i] = !this._doors[i];
                }
            }
        }

        this.Result = this._doors;
    }
}
