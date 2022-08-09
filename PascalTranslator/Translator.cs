using PascalTranslator;
using System;
using System.Collections.Generic;
using System.Text;

public class Translator
{
    private string buf;
    private enum type
    {
        Идентификатор,
        Разделитель,
        Литерал
    }

    private string[] input;
    private readonly List<string> keyWords = new List<string>() { "for", "to", "do", "begin", "end", "var", "integer", "real", "float", "downto" }; // 1
    private readonly List<string> separators = new List<string>() { ";", ":=", "=", "+", "-", "*", "/", ",", ".", ":", "(", ")", ":", "<", ">", "<=", ">=", "<>" }; // 2
    private List<string> variables = new List<string>(); // 3
    private List<string> numbers = new List<string>(); // 4 
    private List<string> links = new List<string>();
    private List<string> bauerOuput = new List<string>();

    private List<string> lexicList = new List<string>();
    private List<type> typeList = new List<type>();

    private readonly List<char> FSP = new List<char>() { ':' }; // First symbol of symbol pairs
    private readonly List<char> SSP = new List<char>() { '=' }; // Second symbol of symbol pairs

    private Stack<int> state;
    private Stack<string> program;
    private int shiftCounter = 0;

    public Translator(string[] input)
    {
        this.input = input;
    }

    public void LexicalAnalise()
    {
        for (int i = 0; i < input.Length; i++)
        {        
            try
            {
                StringLexicalAnalise(input[i]);
            }
            catch (AnaliseException ex)
            {
                throw new AnaliseException($"Ошибка в строке {i + 1}: {ex.Message}");
            }
        }

        foreach (var item in lexicList)
        {
            if (item.Length > 8)
            {
                throw new AnaliseException($"Слишком большой идентификатор - {item}");
            }
        }

        LexicalClassification();
    }

    private void StringLexicalAnalise(string input)
    {
        buf = string.Empty;
        input = input.ToLower();
        StringBuilder sbInput = new StringBuilder(input);
        for (int i = 0; i < sbInput.Length; i++)
        {
            if (sbInput[i] == '/')
            {
                if (i + 1 < sbInput.Length && sbInput[i + 1] == '/')
                {
                    sbInput = sbInput.Remove(i, sbInput.Length - i);
                }
            }
        }

        for (int i = 0; i < sbInput.Length; i++)
        {
            if (sbInput[i] == ' ')
            {
                continue;
            }
            if ((i < sbInput.Length) && Char.IsLetter(sbInput[i]))
            {
                if (!(sbInput[i] >= 97 && sbInput[i] <= 122))
                {
                    throw new AnaliseException($"Недопустимый символ - {sbInput[i]}");
                }

                while ((i < sbInput.Length) && Char.IsLetterOrDigit(sbInput[i]))
                {
                    if (!(sbInput[i] >= 97 && sbInput[i] <= 122))
                    {
                        throw new AnaliseException($"Недопустимый символ - {sbInput[i]}");
                    }

                    buf += sbInput[i];

                    if ((i + 1 >= sbInput.Length) || !Char.IsLetterOrDigit(sbInput[i + 1]))
                    {
                        lexicList.Add(buf);
                        typeList.Add(type.Идентификатор);
                        buf = "";

                    }

                    i++;
                }
            }

            while ((i < sbInput.Length) && Char.IsDigit(sbInput[i]))
            {
                buf += sbInput[i];
                if ((i + 1 >= sbInput.Length) || !Char.IsDigit(sbInput[i + 1]))
                {
                    lexicList.Add(buf);
                    typeList.Add(type.Литерал);
                    buf = "";
                }

                i++;
            }

            if (i + 1 < sbInput.Length && FSP.Contains(sbInput[i]))
            {
                if (SSP.Contains(sbInput[i + 1]) && SSP.IndexOf(sbInput[i + 1]) == FSP.IndexOf(sbInput[i]))
                {
                    lexicList.Add((sbInput[i].ToString() + sbInput[i + 1].ToString()));
                    typeList.Add(type.Разделитель);
                    i += 2;
                }
            }

            while ((i < sbInput.Length) && (Char.IsSymbol(sbInput[i]) || Char.IsPunctuation(sbInput[i])))
            {
                buf += sbInput[i];
                if ((i + 1 >= sbInput.Length) || !Char.IsLetterOrDigit(sbInput[i]))
                {
                    lexicList.Add(buf);
                    typeList.Add(type.Разделитель);
                    buf = "";
                }

                i++;
            }

            i--;
        }
    }

    private void LexicalClassification()
    {
        for (int i = 0; i < lexicList.Count; i++)
        {
            if (typeList[i] == type.Идентификатор)
            {
                if (keyWords.Contains(lexicList[i]))
                {
                    links.Add($"1,{keyWords.IndexOf(lexicList[i])}");
                }
                else
                {
                    if (variables.Contains(lexicList[i]))
                    {
                        links.Add($"3,{variables.IndexOf(lexicList[i])}");
                    }
                    else
                    {
                        variables.Add(lexicList[i]);
                        links.Add($"3,{variables.IndexOf(lexicList[i])}");
                    }
                }
            }
            else if (typeList[i] == type.Разделитель)
            {
                if (separators.Contains(lexicList[i]))
                {
                    links.Add($"2,{separators.IndexOf(lexicList[i])}");
                }
                else
                {
                    throw new AnaliseException($"Неизвестный разделитель - {lexicList[i]}");
                }
            }
            else
            {
                if (numbers.Contains(lexicList[i]))
                {
                    links.Add($"4,{numbers.IndexOf(lexicList[i])}");
                }
                else
                {
                    numbers.Add(lexicList[i]);
                    links.Add($"4,{numbers.IndexOf(lexicList[i])}");
                }
            }
        }
    }

    public void SyntaxAnalise()
    {
        state = new Stack<int>();
        program = new Stack<string>();
        shiftCounter = 0;
        state.Push(0);
        bool completed = false;
        while (!completed)
        {
            switch (state.Peek())
            {
                case 0: State0(ref completed); continue;
                case 1: State1(); continue;
                case 2: State2(); continue;
                case 3: State3(); continue;
                case 4: State4(); continue;
                case 5: State5(); continue;
                case 6: State6(); continue;
                case 7: State7(); continue;
                case 8: State8(); continue;
                case 9: State9(); continue;
                case 10: State10(); continue;
                case 11: State11(); continue;
                case 12: State12(); continue;
                case 13: State13(); continue;
                case 14: State14(); continue;
                case 15: State15(); continue;
                case 17: State17(); continue;
                case 18: State18(); continue;
                case 19: State19(); continue;
                case 20: State20(); continue;
                case 21: State21(); continue;
                case 22: State22(); continue;
                case 23: State23(); continue;
                case 24: State24(); continue;
                case 25: State25(); continue;
                case 26: State26(); continue;
                case 27: State27(); continue;
                case 28: State28(); continue;
                case 29: State29(); continue;
                case 30: State30(); continue;
                case 31: State31(); continue;
                case 32: State32(); continue;
                case 33: State33(); continue;
                case 34: State34(); continue;
                case 35: State35(); continue;
                case 36: State36(); continue;
                case 37: State37(); continue;
                case 38: State38(); continue;
                case 39: State39(); continue;
                case 40: State40(); continue;
                case 41: State41(); continue;
                case 42: State42(); continue;
                default: throw new AnaliseException("Неизвестное состояние");
            }
        }
    }

    #region Syntax Analise functions

    private void Shift()
    {
        if (shiftCounter >= lexicList.Count)
        {
            throw new AnaliseException("Неожиданное завершение программы");
        }

        if (variables.Contains(lexicList[shiftCounter]))
        {
            program.Push("id");
            shiftCounter++;
            return;
        }

        if (numbers.Contains(lexicList[shiftCounter]))
        {
            program.Push("lit");
            shiftCounter++;
            return;
        }

        program.Push(lexicList[shiftCounter++]);
    }

    private void Fold(int count, string item)
    {
        for (int i = 0; i < count; i++)
        {
            program.Pop();
            state.Pop();
        }

        program.Push(item);
    }

    private void ComplexMathOperation()
    {
        string str = string.Empty;
        while (lexicList[shiftCounter] != "to" && lexicList[shiftCounter] != "downto" && lexicList[shiftCounter] != ";")
        {
            str += lexicList[shiftCounter++];
        }

        if (string.IsNullOrEmpty(str))
        {
            throw new AnaliseException("Пустое арифметическое выражение");
        }

        Bauer bauer = new Bauer();
        try
        {
            List<string> bauerResult = new List<string>();
            if (bauer.Calculate(str, out bauerResult))
            {
                if (bauerResult.Count == 0)
                {
                    bauerOuput.Add($"{str}");
                }
                else
                {
                    bauerOuput.Add($"{str}:");
                }

                for (int i = 0; i < bauerResult.Count; i++)
                {
                    bauerOuput.Add($"   {bauerResult[i]}");
                }
            }
            else
            {
                throw new AnaliseException($"Ошибка при разборе арифметического выражения");
            }
        }
        catch
        {
            throw new AnaliseException($"Ошибка при разборе арифметического выражения {str}");
        }

        program.Push("expr");
    }

    private void State0(ref bool completed)
    {
        if (shiftCounter < lexicList.Count)
        {
            Shift();
        }

        if (program.Peek() == "<S>")
        {
            completed = true;
            return;
        }

        if (program.Peek() == "<prog>")
        {
            state.Push(1);
            return;
        }

        if (program.Peek() == "var")
        {
            state.Push(2);
            return;
        }

        throw new AnaliseException("var", program.Peek());
    }

    private void State1()
    {
        if (program.Peek() == "<prog>")
        {
            Fold(1, "<S>");
            return;
        }
    }

    private void State2()
    {
        if (program.Peek() == "var")
        {
            Shift();
        }

        if (program.Peek() == "<Список опис.>")
        {
            state.Push(3);
            return;
        }

        if (program.Peek() == "<Описание>")
        {
            state.Push(4);
            return;
        }

        if (program.Peek() == "<Список переменных>")
        {
            state.Push(5);
            return;
        }

        if (program.Peek() == "id")
        {
            state.Push(6);
            return;
        }

        throw new AnaliseException($"var или идентификатор", program.Peek());
    }

    private void State3()
    {
        if (program.Peek() == "<Список опис.>")
        {
            Shift();
        }

        if (program.Peek() == "begin")
        {
            state.Push(8);
            return;
        }

        if (program.Peek() == "<Описание>")
        {
            state.Push(4);
            return;
        }

        if (program.Peek() == "<Список переменных>")
        {
            state.Push(5);
            return;
        }

        if (program.Peek() == "id")
        {
            state.Push(6);
            return;
        }

        throw new AnaliseException("begin или идентификатор", program.Peek());
    }

    private void State4()
    {
        if (program.Peek() == "<Описание>")
        {
            Fold(1, "<Список опис.>");
            return;
        }

        throw new AnaliseException("State4");
    }

    private void State5()
    {
        if (program.Peek() == "<Список переменных>")
        {
            Shift();
        }

        if (program.Peek() == ",")
        {
            state.Push(13);
            return;
        }

        if (program.Peek() == ":")
        {
            state.Push(9);
            return;
        }

        throw new AnaliseException(", или :", program.Peek());
    }

    private void State6()
    {
        if (program.Peek() == "id")
        {
            Fold(1, "<Список переменных>");
            return;
        }

        throw new AnaliseException("State6");
    }

    private void State7()
    {
        if (program.Peek() == "id")
        {
            Fold(3, "<Список переменных>");
            return;
        }

        throw new AnaliseException("State7");
    }

    private void State8()
    {
        if (program.Peek() == "begin")
        {
            Shift();
        }

        if (program.Peek() == "<Список операторов>")
        {
            state.Push(14);
            return;
        }

        if (program.Peek() == "<Оператор>")
        {
            state.Push(15);
            return;
        }

        if (program.Peek() == "<Присваивание>")
        {
            state.Push(17);
            return;
        }

        if (program.Peek() == "<Присваивание в цикле>")
        {
            state.Push(18);
            return;
        }

        if (program.Peek() == "<Цикл>")
        {
            state.Push(19);
            return;
        }

        if (program.Peek() == "for")
        {
            state.Push(20);
            return;
        }

        if (program.Peek() == "id")
        {
            state.Push(21);
            return;
        }

        throw new AnaliseException("for или идентификатор", program.Peek());
    }

    private void State9()
    {
        if (program.Peek() == ":")
        {
            Shift();
        }

        if (program.Peek() == "<Тип>")
        {
            state.Push(22);
            return;
        }

        if (program.Peek() == "integer")
        {
            state.Push(10);
            return;
        }

        if (program.Peek() == "real")
        {
            state.Push(11);
            return;
        }

        if (program.Peek() == "float")
        {
            state.Push(12);
            return;
        }

        throw new AnaliseException("integer, real, float", program.Peek());
    }

    private void State10()
    {
        if (program.Peek() == "integer")
        {
            Fold(1, "<Тип>");
            return;
        }

        throw new AnaliseException("State10");
    }

    private void State11()
    {
        if (program.Peek() == "real")
        {
            Fold(1, "<Тип>");
            return;
        }

        throw new AnaliseException("State11");
    }

    private void State12()
    {
        if (program.Peek() == "float")
        {
            Fold(1, "<Тип>");
            return;
        }

        throw new AnaliseException("State12");
    }

    private void State13()
    {
        if (program.Peek() == ",")
        {
            Shift();
        }

        if (program.Peek() == "id")
        {
            state.Push(7);
            return;
        }

        throw new AnaliseException("идентификатор", program.Peek());
    }

    private void State14()
    {
        if (program.Peek() == "<Список операторов>")
        {
            Shift();
        }

        if (program.Peek() == "<Присваивание>")
        {
            state.Push(17);
            return;
        }

        if (program.Peek() == "<Присваивание в цикле>")
        {
            state.Push(18);
            return;
        }

        if (program.Peek() == "<Цикл>")
        {
            state.Push(19);
            return;
        }

        if (program.Peek() == "for")
        {
            state.Push(20);
            return;
        }

        if (program.Peek() == "id")
        {
            state.Push(21);
            return;
        }

        if (program.Peek() == "<Оператор>")
        {
            state.Push(38);
            return;
        }

        if (program.Peek() == "end")
        {
            state.Push(23);
            return;
        }

        throw new AnaliseException("for, end или идентификатор", program.Peek());
    }

    private void State15()
    {
        if (program.Peek() == "<Оператор>")
        {
            Fold(1, "<Список операторов>");
            return;
        }

        throw new AnaliseException("State15");
    }

    private void State17()
    {
        if (program.Peek() == "<Присваивание>")
        {
            Fold(1, "<Оператор>");
            return;
        }

        throw new AnaliseException("State17");
    }

    private void State18()
    {
        if (program.Peek() == "<Присваивание в цикле>")
        {
            Shift();
        }

        if (program.Peek() == ";")
        {
            state.Push(35);
            return;
        }

        throw new AnaliseException(";", program.Peek());
    }

    private void State19()
    {
        if (program.Peek() == "<Цикл>")
        {
            Fold(1, "<Оператор>");
            return;
        }

        throw new AnaliseException("State19");
    }

    private void State20()
    {
        if (program.Peek() == "for")
        {
            Shift();
        }

        if (program.Peek() == "<Присваивание в цикле>")
        {
            state.Push(24);
            return;
        }

        if (program.Peek() == "id")
        {
            state.Push(21);
            return;
        }

        throw new AnaliseException("идентификатор", program.Peek());
    }

    private void State21()
    {
        if (program.Peek() == "id")
        {
            Shift();
        }

        if (program.Peek() == ":=")
        {
            state.Push(25);
            return;
        }

        throw new AnaliseException(":=", program.Peek());
    }

    private void State22()
    {
        if (program.Peek() == "<Тип>")
        {
            Shift();
        }

        if (program.Peek() == ";")
        {
            state.Push(40);
            return;
        }

        throw new AnaliseException(";", program.Peek());
    }

    private void State23()
    {
        if (program.Peek() == "end")
        {
            Shift();
        }

        if (program.Peek() == ".")
        {
            state.Push(42);
            return;
        }

        throw new AnaliseException(".", program.Peek());
    }

    private void State24()
    {
        if (program.Peek() == "<Присваивание в цикле>")
        {
            Shift();
        }

        if (program.Peek() == "to")
        {
            state.Push(27);
            return;
        }

        if (program.Peek() == "downto")
        {
            state.Push(28);
            return;
        }

        throw new AnaliseException("to или downto", program.Peek());
    }

    private void State25()
    {
        ComplexMathOperation();
        if (program.Peek() == ":=")
        {
            Shift();
        }

        if (program.Peek() == "expr")
        {
            state.Push(26);
            return;
        }

        throw new AnaliseException("арифметическое выражение", program.Peek());
    }

    private void State26()
    {
        if (program.Peek() == "expr")
        {
            Fold(3, "<Присваивание в цикле>");
            return;
        }

        throw new AnaliseException("State26");
    }

    private void State27()
    {
        if (program.Peek() == "to")
        {
            Shift();
        }

        if (program.Peek() == "<Операнд>")
        {
            state.Push(29);
            return;
        }

        if (program.Peek() == "id")
        {
            state.Push(30);
            return;
        }

        if (program.Peek() == "lit")
        {
            state.Push(31);
            return;
        }

        throw new AnaliseException("Идентификатор или литерал", program.Peek());
    }

    private void State28()
    {
        if (program.Peek() == "downto")
        {
            Shift();
        }

        if (program.Peek() == "<Операнд>")
        {
            state.Push(29);
            return;
        }

        if (program.Peek() == "id")
        {
            state.Push(30);
            return;
        }

        if (program.Peek() == "lit")
        {
            state.Push(31);
            return;
        }

        throw new AnaliseException("идентификатор или литерал", program.Peek());
    }

    private void State29()
    {
        if (program.Peek() == "<Операнд>")
        {
            Shift();
        }

        if (program.Peek() == "do")
        {
            state.Push(32);
            return;
        }

        throw new AnaliseException("do", program.Peek());
    }

    private void State30()
    {
        if (program.Peek() == "id")
        {
            Fold(1, "<Операнд>");
            return;
        }

        throw new AnaliseException("State30");
    }

    private void State31()
    {
        if (program.Peek() == "lit")
        {
            Fold(1, "<Операнд>");
            return;
        }

        throw new AnaliseException("State31");
    }

    private void State32()
    {
        if (program.Peek() == "do")
        {
            Shift();
        }

        if (program.Peek() == "<Блок операторов>")
        {
            state.Push(33);
            return;
        }

        if (program.Peek() == "<Оператор>")
        {
            state.Push(39);
            return;
        }

        if (program.Peek() == "<Цикл>")
        {
            state.Push(19);
            return;
        }

        if (program.Peek() == "for")
        {
            state.Push(20);
            return;
        }

        if (program.Peek() == "begin")
        {
            state.Push(34);
            return;
        }

        throw new AnaliseException("for или begin", program.Peek());
    }

    private void State33()
    {
        if (program.Peek() == "<Блок операторов>")
        {
            Fold(6, "<Цикл>");
            return;
        }

        if (program.Peek() == "<Блок операторов>")
        {
            Fold(6, "<Цикл>");
            return;
        }

        throw new AnaliseException("State33");
    }

    private void State34()
    {
        if (program.Peek() == "begin")
        {
            Shift();
        }

        if (program.Peek() == "<Оператор>")
        {
            state.Push(15);
            return;
        }

        if (program.Peek() == "<Список операторов>")
        {
            state.Push(36);
            return;
        }

        if (program.Peek() == "<Присваивание>")
        {
            state.Push(17);
            return;
        }

        if (program.Peek() == "<Присваивание в цикле>")
        {
            state.Push(18);
            return;
        }

        if (program.Peek() == "<Цикл>")
        {
            state.Push(19);
            return;
        }

        if (program.Peek() == "for")
        {
            state.Push(20);
            return;
        }

        if (program.Peek() == "id")
        {
            state.Push(21);
            return;
        }

        throw new AnaliseException("for или идентификатор");
    }

    private void State35()
    {
        if (program.Peek() == ";")
        {
            Fold(2, "<Присваивание>");
            return;
        }

        throw new AnaliseException("State35");
    }

    private void State36()
    {
        if (program.Peek() == "<Список операторов>")
        {
            Shift();
        }

        if (program.Peek() == "end")
        {
            state.Push(37);
            return;
        }

        if (program.Peek() == "<Оператор>")
        {
            state.Push(38);
            return;
        }

        if (program.Peek() == "<Присваивание>")
        {
            state.Push(17);
            return;
        }

        if (program.Peek() == "<Присваивание в цикле>")
        {
            state.Push(18);
            return;
        }

        if (program.Peek() == "<Цикл>")
        {
            state.Push(19);
            return;
        }

        if (program.Peek() == "for")
        {
            state.Push(20);
            return;
        }

        if (program.Peek() == "id")
        {
            state.Push(21);
            return;
        }

        throw new AnaliseException("end, for или идентификатор", program.Peek());
    }

    private void State37()
    {
        if (program.Peek() == "end")
        {
            Shift();
        }

        if (program.Peek() == ";")
        {
            state.Push(41);
            return;
        }

        throw new AnaliseException(";", program.Peek());
    }

    private void State38()
    {
        if (program.Peek() == "<Оператор>")
        {
            Fold(2, "<Список операторов>");
            return;
        }

        throw new AnaliseException("State38");
    }

    private void State39()
    {
        if (program.Peek() == "<Оператор>")
        {
            Fold(1, "<Блок операторов>");
            return;
        }

        throw new AnaliseException("State39");
    }

    private void State40()
    {
        if (program.Peek() == ";")
        {
            Fold(4, "<Описание>");
            return;
        }

        throw new AnaliseException("State40");
    }

    private void State41()
    {
        if (program.Peek() == ";")
        {
            Fold(4, "<Блок операторов>");
            return;
        }

        throw new AnaliseException("State41");
    }

    private void State42()
    {
        if (program.Peek() == ".")
        {
            Fold(6, "<prog>");
            return;
        }

        throw new AnaliseException("State42");
    }

    #endregion

    public List<string> GetLexicList()
    {
        return lexicList;
    }

    public List<string> GetTypeList()
    {
        List<string> toReturn = new List<string>();
        foreach (var item in typeList)
        {
            toReturn.Add(item.ToString());
        }

        return toReturn;
    }

    public List<string> GetVariablesList()
    {
        return variables;
    }

    public List<string> GetNumbersList()
    {
        return numbers;
    }

    public List<string> GetLinksList()
    {
        return links;
    }

    public List<string> GetKeyWordsList()
    {
        return keyWords;
    }

    public List<string> GetSeparatorsList()
    {
        return separators;
    }

    public List<string> GetBauerOutput()
    {
        return bauerOuput;
    }
}
