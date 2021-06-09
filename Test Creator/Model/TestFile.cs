using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test_Creator.Model
{
    class TestFile
    {

        public static bool IsFileCorrect(string text)
        {
            string[] lines = Regex.Split(text, "[\r\n]+");
            if ((lines.Length - 1) % 6 != 0) return false;
            for (int i = 1; i < lines.Length; i += 6)
            {
                int correctAnswers = 0;
                if (lines[i].Length < 2 || lines[i][lines[i].Length - 1] != '?') return false;
                for (int j = 1; j <= 4; j++)
                {
                    if (lines[i + j].Length < 3) return false;
                    string s = lines[i + j].Substring(0, 2);
                    if (s != "0|" && s != "1|") return false;
                    if (s[0] == '1') correctAnswers++;
                }
                if (correctAnswers != 1) return false;
                if (lines[i + 5] != "**********") return false;

            }
            return true;
        }
    }
}
