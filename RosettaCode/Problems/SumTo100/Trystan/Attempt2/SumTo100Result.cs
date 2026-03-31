using System.Text;

namespace RosettaCode.Problems.SumTo100.Trystan.Attempt2;

public readonly struct SumTo100Result
{

    public double Result { get; }


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
        Stack<double> multiplicationStack = new Stack<double>();

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
                        double t = numberStack.Pop();
                        while(multiplicationStack.Count > 0)
                        {
                            t *= multiplicationStack.Pop();
                        }
                        Result += t; //add the product
                        sb.Insert(0, "+");
                    }
                    else if (parameters.UseSubtraction)
                    {
                        double t = numberStack.Pop();
                        while (multiplicationStack.Count > 0)
                        {
                            t *= multiplicationStack.Pop();
                        }
                        Result -= t; //subtract the product

                        sb.Insert(0, "-");
                    }
                    else if (parameters.UseMultiplication)
                    {
                        multiplicationStack.Push(numberStack.Pop());
                        sb.Insert(0, "*");
                    }
                    else
                    {
                        multiplicationStack.Push((double)1/numberStack.Pop());
                        sb.Insert(0, "/");
                    }
                    break;
                case 2:
                    if (parameters.UseSubtraction)
                    {
                        double t = numberStack.Pop();
                        while (multiplicationStack.Count > 0)
                        {
                            t *= multiplicationStack.Pop();
                        }
                        Result -= t; //subtract the product
                        sb.Insert(0, "-"); 
                    }
                    else if (parameters.UseMultiplication)
                    {
                        multiplicationStack.Push(numberStack.Pop());
                        sb.Insert(0, "*");
                    }
                    else
                    {
                        multiplicationStack.Push((double)1 / numberStack.Pop());
                        sb.Insert(0, "/");
                    }
                    break;
                case 3:
                    if (parameters.UseMultiplication)
                    {
                        multiplicationStack.Push(numberStack.Pop());
                        sb.Insert(0, "*");
                    }
                    else
                    {
                        multiplicationStack.Push((double)1 / numberStack.Pop());
                        sb.Insert(0, "/");
                    }
                    break;
                case 4: //division is the only remaining operator
                    multiplicationStack.Push((double)1 / numberStack.Pop());
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
            double t = numberStack.Pop();
            while (multiplicationStack.Count > 0)
            {
                t *= multiplicationStack.Pop();
            }
            Result += t; //add the product
        }

        this._resultString = sb.ToString();
    }
}
