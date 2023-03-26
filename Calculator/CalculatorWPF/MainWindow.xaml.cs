using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Создадим переменную, которая будет запоминать в себе последний введённый символ
        // Если она равна 0, тобыло введено число (0 - 9)
        // Если равна 1, то операция или спец.символ ('-', '+', '*', '/', '(' )
        // Если равна 2, то была ')'
        // Если равна 3, то была ','
        // Если равна -1, то в строке ещё ничего нет
        private int lastSymbol = -1;

        private bool isCommaInNumber = false;
        private int openBracketCounter = 0;
        private int closeBracketCounter = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Comma_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol == 0 && !isCommaInNumber)
            {
                expressionBox.Text += ',';
                lastSymbol = 3;
                isCommaInNumber = true;
            }
        }

        private void button_Number_0_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol != 2)
            {
                expressionBox.Text += 0;
                lastSymbol = 0;
            }
        }

        private void button_Number_1_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol != 2)
            {
                expressionBox.Text += 1;
                lastSymbol = 0;
            }
        }

        private void button_Number_2_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol != 2)
            {
                expressionBox.Text += 2;
                lastSymbol = 0;
            }
        }

        private void button_Number_3_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol != 2)
            {
                expressionBox.Text += 3;
                lastSymbol = 0;
            }
        }

        private void button_Number_4_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol != 2)
            {
                expressionBox.Text += 4;
                lastSymbol = 0;
            }
        }

        private void button_Number_5_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol != 2)
            {
                expressionBox.Text += 5;
                lastSymbol = 0;
            }
        }

        private void button_Number_6_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol != 2)
            {
                expressionBox.Text += 6;
                lastSymbol = 0;
            }
        }

        private void button_Number_7_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol != 2)
            {
                expressionBox.Text += 7;
                lastSymbol = 0;
            }
        }

        private void button_Number_8_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol != 2)
            {
                expressionBox.Text += 8;
                lastSymbol = 0;
            }
        }

        private void button_Number_9_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol != 2)
            {
                expressionBox.Text += 9;
                lastSymbol = 0;
            }
        }

        private void button_Erase_Click(object sender, RoutedEventArgs e)
        {
            expressionBox.Text = "";
            lastSymbol = -1;
            isCommaInNumber = false;
            openBracketCounter = 0;
            closeBracketCounter = 0;
        }

        private void button_C_Click(object sender, RoutedEventArgs e)
        {
            string text = expressionBox.Text;
            if (!string.IsNullOrEmpty(text))
            {
                expressionBox.Text = text.Substring(0, text.Length - 1);

                if (text.Length == 1)
                    lastSymbol = -1;
                else
                {
                    char c = text[text.Length - 2];

                    if (c >= '0' && c <= '9')
                        lastSymbol = 0;
                    else if (c == '(' || c == '/' || c == '*' || c == '-' || c == '+')
                        lastSymbol = 1;
                    else if (c == ')')
                        lastSymbol = 2;
                    else if (c == '.')
                        lastSymbol = 3;
                }
            }
        }

        private void button_Div_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol == 0 || lastSymbol == 2)
            {
                expressionBox.Text += "/";
                lastSymbol = 1;
                isCommaInNumber = false;
            }
        }

        private void button_Mult_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol == 0 || lastSymbol == 2)
            {
                expressionBox.Text += "*";
                lastSymbol = 1;
                isCommaInNumber = false;
            }
        }

        private void button_Minus_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol == 0 || lastSymbol == 2)
            {
                expressionBox.Text += "-";
                lastSymbol = 1;
                isCommaInNumber = false;
            }
        }

        private void button_Plus_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol == 0 || lastSymbol == 2)
            {
                expressionBox.Text += "+";
                lastSymbol = 1;
                isCommaInNumber = false;
            }
        }

        private void button_OpenBracket_Click(object sender, RoutedEventArgs e)
        {
            if (lastSymbol == 1 || lastSymbol == -1)
            {
                expressionBox.Text += "(";
                lastSymbol = 1;
                isCommaInNumber = false;
                openBracketCounter++;
            }
        }

        private void button_CloseBracket_Click(object sender, RoutedEventArgs e)
        {
            if (closeBracketCounter != openBracketCounter && (lastSymbol == 0 || lastSymbol == 2))
            {
                expressionBox.Text += ")";
                lastSymbol = 2;
                isCommaInNumber = false;
                closeBracketCounter++;
            }
        }

        private void button_Equality_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string polishNotation = CalculationExpressions.ArithmeticToPolishNotation(expressionBox.Text);
                double result = CalculationExpressions.PolishToValue(polishNotation);
                expressionBox.Text = "";
                lastSymbol = -1;
                isCommaInNumber = false;
                textBlockAnswer.Text = "Результат: " + result + $"\n{polishNotation}";
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                if (error == "Стек пуст.")
                    error = "Вы допустили ошибку в записи выражения";
                textBlockAnswer.Text = error;
            }
        }
    }
}
