namespace CalculatorConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // 18 / 3 = 6
                // результат должен быть таким: 825*+132*+4-/
                string polishNotation = CalculationExpressions.ArithmeticToPolishNotation("(8+2*5)/(1+3*2-4)"); 
                
                Console.WriteLine("Должно быть: 825*+132*+4-/");
                Console.WriteLine("Ответ: " + polishNotation);

                double result = CalculationExpressions.PolishToValue(polishNotation);
                Console.WriteLine(result);

                Console.WriteLine();
                string input1 = "13+6*2+3*3*(4-1*2+10)";
                Console.WriteLine("Исходная строка: " + input1);
                string test1 = CalculationExpressions.ArithmeticToPolishNotation(input1);
                Console.WriteLine("Ответ: " + test1);
                double result1 = CalculationExpressions.PolishToValue(test1);
                Console.WriteLine(result1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }

    public static class CalculationExpressions
    {
        public static string ArithmeticToPolishNotation(string arithmeticExpression)
        {
            Stack<string> operands = new Stack<string>();
            Stack<char> operations = new Stack<char>();

            int startPosition = 0;
            int operandLength = 0;
            int leftBracket = 0;
            int rightBracket = 0;

            for (int i = 0; i < arithmeticExpression.Length; i++)
            {
                if ((arithmeticExpression[i] >= '0' && arithmeticExpression[i] < '9') ||
                        arithmeticExpression[i] == ',')
                {
                    if (operandLength == 0)
                        startPosition = i;

                    operandLength++;
                    if (i == arithmeticExpression.Length - 1)
                    {
                        operands.Push(arithmeticExpression.Substring(startPosition, operandLength));
                    }

                    continue;
                }

                if (arithmeticExpression[i] == '(')
                {
                    operations.Push(arithmeticExpression[i]);
                    leftBracket++;
                }
                else if (arithmeticExpression[i] == '*' || arithmeticExpression[i] == '/')
                {
                    operations.Push(arithmeticExpression[i]);
                }
                else if (arithmeticExpression[i] == '+' || arithmeticExpression[i] == '-')
                {
                    if (operations.Count > 0 && (operations.Peek() == '*' || operations.Peek() == '/'))
                    {
                        if (operandLength != 0)
                        {
                            operands.Push(arithmeticExpression.Substring(startPosition, operandLength));
                            operandLength = 0;
                        }

                        while (operations.Count > 0 && operations.Peek() != '(')
                        {
                            operands.Push(operations.Pop().ToString());
                        }
                    }
                    operations.Push(arithmeticExpression[i]);
                }
                else if (arithmeticExpression[i] == ')')
                {
                    if (operandLength != 0)
                    {
                        operands.Push(arithmeticExpression.Substring(startPosition, operandLength));
                        operandLength = 0;
                    }

                    rightBracket++;
                    if (rightBracket > leftBracket)
                        throw new Exception("Ошибка!!! Количество открывающихся скобок меньше числа закрывающихся");
                    while (operations.Peek() != '(')
                    {
                        operands.Push(operations.Pop().ToString());
                    }
                    // удаляем '('
                    operations.Pop();
                }

                if (operandLength != 0)
                {
                    operands.Push(arithmeticExpression.Substring(startPosition, operandLength));
                    operandLength = 0;
                }
            }
            // перекидываем остатки
            while (operations.Count > 0)
            {
                operands.Push(operations.Pop().ToString());
            }

            //string result = String.Concat(operands.Reverse());
            string result = String.Join(" ", operands.Reverse());
            return result;
        }

        public static double PolishToValue(string polishExpression)
        {
            Stack<double> result = new Stack<double>();
            string[] input = polishExpression.Split().ToArray();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == "+" || input[i] == "-" || input[i] == "*" || input[i] == "/")
                {
                    double value2 = result.Pop();
                    double value1 = result.Pop();
                    switch (input[i])
                    {
                        case "+":
                            result.Push(value1 + value2);
                            break;
                        case "-":
                            result.Push(value1 - value2);
                            break;
                        case "*":
                            result.Push(value1 * value2);
                            break;
                        case "/":
                            if (value2 == 0)
                                throw new Exception("Ошибка, деление на ноль!");
                            result.Push(value1 / value2);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    double value = Convert.ToDouble(input[i]);
                    result.Push(value);
                }
            }

            return result.Pop();
        }
    }
}
