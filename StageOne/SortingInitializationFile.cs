using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace StageOne
{
     class SortingInitializationFile
    {
        /// <summary>
        /// На вход => путь до текстового файла, формата стандартного ini-файла Windows, 
        /// содержащего секции вида[SECTION_NAME] и значения вида NAME=VALUE
        /// и путь сохранения файла того же формата,
        /// в котором все секции упорядочены по алфавиту, и содержимое каждой секции также отсортировано по алфавиту.
        /// </summary>
        /// <param name="pathRead">Путь до исходного файла.</param>
        /// <param name="pathWrite">Путь до обработаного файла.</param>
        public static void Alphabetically(string pathRead, string pathWrite)
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

            ReadFile(pathRead, dict);

            dict = dict.OrderBy(k => k.Key).ToDictionary(process => process.Key, process => process.Value);

            foreach (var item in dict)
            {
                dict[item.Key].Sort();
            }
                

            if (dict.Count != 0)
            {
                WriteFile(pathWrite, dict);
            }            
        }

        private static void ReadFile(string path, Dictionary<string, List<string>> dict)
        {            
            if (!File.Exists(path))
            {
                UnrecoverableError.ErrorReadFale("Файл не существует!");
                return;
            }

            using (StreamReader sr = File.OpenText(path))
            {
                string input;
                string section_name = null;
                string pattern_section_name = @"^\[\w+\]$";
                string pattern_name_value = @"\w+=\w+";
                string error_pattern = @"\S+";
                
                while ((input = sr.ReadLine()) != null)
                {
                    if (Regex.IsMatch(input, pattern_section_name, RegexOptions.IgnoreCase))
                    {
                        section_name = input;
                        dict.Add(input, new List<string>());
                    }
                    else if (Regex.IsMatch(input, pattern_name_value, RegexOptions.IgnoreCase))
                    {
                        dict[section_name].Add(input);
                    }
                    else if (Regex.IsMatch(input, error_pattern, RegexOptions.IgnoreCase))
                    {
                        UnrecoverableError.ErrorReadFale("Неверно сформированное имя секции или значение неверного формата!");
                        dict.Clear();
                        return;
                    }
                }
            }
        }

        private static void WriteFile(string path, Dictionary<string, List<string>> dict)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var item in dict)
                {
                    sw.Write(Convert.ToString(item.Key));
                    sw.Write(Environment.NewLine);
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        sw.Write(Convert.ToString(item.Value[i]) + Environment.NewLine);
                    }
                    sw.Write(Environment.NewLine);    
                }
            }
        }
    }
}
