using NUnit.Framework;
using PascalTranslator;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    [TestFixture]
    public class Tests
    {

        [TestCase("1 + 1", true)]
        [TestCase("2 * 4", true)]
        [TestCase("4 - 3", true)]
        [TestCase("4 / 2", true)]
        [TestCase("2 - 8 *(2-4) - 3 + 8", true)]
        [TestCase("( 2 - 1", false)]
        [TestCase("((( 2 - 1) * 4)", false)]
        [TestCase("2 - 1)", false)]
        [TestCase(" 2 -/ 1)", false)]
        [TestCase(" 2 +- 1)", false)]
        [TestCase(" 1 - 2", true)]
        [TestCase("a - b", true)]
        [TestCase("(var - var2) * 8 - 4", true)]
        [TestCase("b + 4", true)]
        [TestCase("45", true)]
        [TestCase("I", true)]
        public void ArithmeticTranslatorTest(string expression, bool isCorrect)
        {
            List<string> result = new List<string>();
            if (!isCorrect)
            {
                try
                {
                    Bauer bauer = new Bauer();
                    bauer.Calculate(expression, out result);
                    Assert.Fail("Исключения не было");
                }
                catch
                {
                    string message = string.Empty;
                    for (int i = 0; i < result.Count; i++)
                    {
                        message += $"{result[i]}{Environment.NewLine}";
                    }

                    Assert.Pass(message);
                }
            }
            else
            {
                try
                {
                    Bauer bauer = new Bauer();
                    bauer.Calculate(expression, out result);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }

                string message = string.Empty;
                for (int i = 0; i < result.Count; i++)
                {
                    message += $"{result[i]}{Environment.NewLine}";
                }

                Assert.Pass(message);
            }
        }
    }
}