namespace RosettaCode.Problems._100Doors.Trystan;
public class _100DoorsTrystan
{
    public bool[] Result { get; private set; } = new bool[100];


    private bool[] _doors = new bool[100];
    
    public void RunProblem()
    {
        this._doors = new bool[100];
        for (int i = 0; i < this._doors.Length; i++)
        {
            this._doors[i] = false;
        }

        for (int i = 0; i < this._doors.Length; i++)
        {
            for (int j = 1; j <= 100; j++)
            {
                if ((j % (i + 1)) == 0)
                {
                    this._doors[j - 1] = !this._doors[j - 1];
                }
            }
        }

        this.Result = this._doors;
    }
}
