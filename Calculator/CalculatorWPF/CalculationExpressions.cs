using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorWPF
{
    public static class CalculationExpressions
    {
        /// <summary>
        /// функция по переводу арифметического выражения в постфиксную форму
        /// </summary>
        /// <param name="arithmeticExpression"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

            string result = String.Join(" ", operands.Reverse());
            return result;
        }

        /// <summary>
        /// Функция по подсчёту числа из обратной польской записи
        /// </summary>
        /// <param name="polishExpression"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
