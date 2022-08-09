using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PascalTranslator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            tbInput.Text =
                $"var a,b: integer;{Environment.NewLine}" +
                $"begin{Environment.NewLine}" +
                $"   for i := 10 to 100 do begin{Environment.NewLine}" +
                $"       c := b + 4;{Environment.NewLine}" +
                $"           a := a;{Environment.NewLine}" +
                $"   end;{Environment.NewLine}" +
                $"end.{Environment.NewLine}";            
            Analise();
        }

        private void Analise()
        {
            Translator translator = new Translator(tbInput.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
            try
            {
                translator.LexicalAnalise();
                List<string> lexicList = translator.GetLexicList();
                List<string> typeList = translator.GetTypeList();
                List<string> linksList = translator.GetLinksList();
                for (int i = 0; i < lexicList.Count; i++)
                {
                    tbOutput.Text += $"{lexicList[i],-10}\t{typeList[i],-15}\t\t{linksList[i],-3}\r\n";
                }

                List<string> keyWords = translator.GetKeyWordsList();
                List<string> separators = translator.GetSeparatorsList();
                List<string> numbers = translator.GetNumbersList();
                List<string> variables = translator.GetVariablesList();

                translator.SyntaxAnalise();

                List<string> bauer = translator.GetBauerOutput();

                Print(tbKeyWords, keyWords);
                Print(tbSeparators, separators);
                Print(tbNumbers, numbers);
                Print(tbVariables, variables);
                Print(tbBauerOuput, bauer);

                MessageBox.Show("Completed");
            }
            catch (AnaliseException ex)
            {
                ClearTextFields();
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                ClearTextFields();
                MessageBox.Show($"Необработанное исключение: {ex.Message}");
            }

        }

        private void buttonAnalise_Click(object sender, EventArgs e)
        {

            ClearTextFields();
            Analise();
        }

        private void Print(TextBox textBox, List<string> list)
        {
            foreach (var item in list)
            {
                textBox.Text += item + Environment.NewLine;
            }
        }

        private void ClearTextFields()
        {
            tbOutput.Text = "";
            tbKeyWords.Text = "";
            tbNumbers.Text = "";
            tbSeparators.Text = "";
            tbVariables.Text = "";
            tbBauerOuput.Text = "";
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = openFileDialog.FileName;
            string fileText = System.IO.File.ReadAllText(filename);
            tbInput.Text = fileText;
        }
    }
}
