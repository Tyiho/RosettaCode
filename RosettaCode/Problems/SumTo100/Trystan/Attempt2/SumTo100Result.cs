using System.Text;

namespace RosettaCode.Problems.SumTo100.Trystan.Attempt2;

public readonly struct SumTo100Result
{

    public int Result { get; }


    private readonly string _resultString;
    public override string ToString()
    {
        return this._resultString;
    }

    public SumTo100Result(SumTo100Parameters parameters, int key)
    {
        List<int> digits = [0];
        for (int i = 0; i < parameters.Digits.Length; i++)
        {
            digits.Add(parameters.Digits[i]);
        }

        StringBuilder sb = new StringBuilder();
        sb.Append(parameters.Digits[parameters.Digits.Length - 1]);

        Stack<int> numberStack = new Stack<int>(digits);
        var baseValue = parameters.KeyBase;

        this.Result = 0;
        for (int i = 0; i < parameters.Digits.Length; i++)
        {
            if (i != 0)
            {
                var t = parameters.Digits[parameters.Digits.Length - i - 1];
                sb.Insert(0, t.ToString());
            }

            switch (key % baseValue)
            {
                case 0: //concatenate digits
                    var a = numberStack.Pop();
                    var b = numberStack.Pop();
                    var aa = a;
                    do
                    {
                        b *= 10;
                        aa /= 10;

                    } while (aa != 0);
                    numberStack.Push(a + b);
                    sb.Insert(0, "");
                    break;
                case 1: //first allowed operator
                    if (parameters.UseAddition)
                    {
                        Result += numberStack.Pop();
                        sb.Insert(0, "+");
                    }
                    else if (parameters.UseSubtraction)
                    {
                        Result -= numberStack.Pop();
                        sb.Insert(0, "-");
                    }
                    else if (parameters.UseMultiplication)
                    {
                        Result *= numberStack.Pop();
                        sb.Insert(0, "*");
                    }
                    else
                    {
                        Result /= numberStack.Pop();
                        sb.Insert(0, "/");
                    }
                    break;
                case 2:
                    if (parameters.UseSubtraction)
                    {
                        Result -= numberStack.Pop();
                        sb.Insert(0, "-");
                    }
                    else if (parameters.UseMultiplication)
                    {
                        Result *= numberStack.Pop();
                        sb.Insert(0, "*");
                    }
                    else
                    {
                        Result /= numberStack.Pop();
                        sb.Insert(0, "/");
                    }
                    break;
                case 3:
                    if (parameters.UseMultiplication)
                    {
                        Result *= numberStack.Pop();
                        sb.Insert(0, "*");
                    }
                    else
                    {
                        Result /= numberStack.Pop();
                        sb.Insert(0, "/");
                    }
                    break;
                case 4: //division is the only remaining operator
                    Result /= numberStack.Pop();
                    sb.Insert(0, "/");
                    break;
            }
            key /= baseValue;
        }
        /*if (sb[0] == '+')
        {
            sb.Remove(0, 1);
        }*/
        if (sb[0] == '0')
        {
            sb.Remove(0, 1);
        }

        if (numberStack.Count == 1)
        {
            Result += numberStack.Pop();
        }

        this._resultString = sb.ToString();
    }
}
