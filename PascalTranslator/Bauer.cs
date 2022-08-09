using System;
using System.Collections.Generic;

namespace PascalTranslator
{
    public class Bauer
    {
        private Stack<string> operands = new Stack<string>();
        private Stack<char> functions = new Stack<char>();
        private int operationCounter = 1;
        private List<string> output;

        public bool Calculate(string str, out List<String> result)
        {
            output = new List<string>();
            str = $"({str})";
            int position = 0;
            object token;
            object prevToken = '_';

            do
            {
                token = GetToken(str, ref position);
                if (token is char 
                    && prevToken is char 
                    && (char)prevToken == '(' 
                    && ((char)token == '+' || (char)token == '-'))
                {
                    operands.Push("0");
                }

                if (token is string)
                {
                    operands.Push(token.ToString());
                }
                else if (!(token is null) && char.IsLetter((char)token)) 
                {
                    operands.Push(token.ToString());
                }
                else if (token is char)
                {
                    if ((char)token == ')')
                    {
                        while (functions.Count > 0 && functions.Peek() != '(')
                        {
                            PopFunction();
                        }

                        functions.Pop();
                    }
                    else
                    {
                        while (CanPop((char)token, functions))
                        {
                            PopFunction();
                        }

                        functions.Push((char)token);
                    }
                }

                prevToken = token;
            }
            while (token != null);

            if (operands.Count > 1 || functions.Count > 0)
            {
                throw new Exception("Ошибка в разборе выражения");
            }

            result = output;
            return true;
        }

        private void PopFunction()
        {
            string B = operands.Pop();
            string A = operands.Pop();
            switch (functions.Pop())
            {
                case '+':
                    operands.Push($"M{operationCounter}");
                    output.Add($"M{operationCounter++}: + {B} {A}");
                    break;
                case '-':
                    operands.Push($"M{operationCounter}");
                    output.Add($"M{operationCounter++}: - {A} {B}");
                    break;
                case '*':
                    operands.Push($"M{operationCounter}");
                    output.Add($"M{operationCounter++}: * {B} {A}");
                    break;
                case '/':
                    operands.Push($"M{operationCounter}");
                    output.Add($"M{operationCounter++}: / {A} {B}");
                    break;
            }
        }

        private bool CanPop(char operation, Stack<char> Functions)
        {
            if (Functions.Count == 0)
            {
                return false;
            }

            int firstPriority = GetPriority(operation);
            int secondPriority = GetPriority(Functions.Peek());

            return firstPriority >= 0 && secondPriority >= 0 && firstPriority >= secondPriority;
        }

        private int GetPriority(char operation)
        {
            switch (operation)
            {
                case '(':
                    return -1;
                case '*':
                case '/':
                    return 1;
                case '+':
                case '-':
                    return 2;
                default:
                    throw new Exception("Недопустимая операция");
            }
        }

        private object GetToken(string str, ref int position)
        {
            ReadWhiteSpace(str, ref position);

            if (position == str.Length)
            {
                return null;
            }

            if (char.IsDigit(str[position]))
            {
                return ReadOperand(str, ref position);
            }

            if (char.IsLetter(str[position]))
            {
                string token = string.Empty;
                while (char.IsLetterOrDigit(str[position]))
                {
                    token += str[position++];
                }

                return token;
            }
            else
            {
                return ReadFunction(str, ref position);
            }
        }

        private char ReadFunction(string str, ref int position)
        {
            return str[position++];
        }

        private string ReadOperand(string str, ref int position)
        {
            string result = "";
            while (position < str.Length && (char.IsDigit(str[position]) || str[position] == '.'))
            {
                result += str[position++];
            }

            return result;
        }

        private void ReadWhiteSpace(string str, ref int position)
        {
            while (position < str.Length && char.IsWhiteSpace(str[position]))
            {
                position++;
            }
        }
    }
}