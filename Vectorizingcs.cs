using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;
using Microsoft.Office.Interop.Excel;

namespace network
{
    internal class Vectorizing
    {
        public static void MakeDictionary(Dictionary<string, string> dictionary)
        {
            string dictionary_path = "C:\\Users\\gubar\\Desktop\\dict.txt";
            string dictText = File.ReadAllText(dictionary_path);
            int c_start = 0;
            string key = default;
            for (int c = 1; c < dictText.Length; c++)
            {
                if (dictText[c] == ',')
                {
                    key = Porter.TransformingWord(dictText.Substring(c_start, c - c_start));
                    c_start = c + 1;
                    c++;
                }
                else if (dictText[c] == '\r')
                {
                    string value = Porter.TransformingWord(dictText.Substring(c_start, c - c_start));
                    dictionary[key] = value;
                    c_start = c + 2;
                    c += 2;
                }

            }
        }

        public static void MakeDataSet(DataSet dat)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            // Создаем поток для чтения.
            var stream = File.Open(@"C:\Users\gubar\Desktop\dataset.xlsx", FileMode.Open, FileAccess.Read);

            // Читатель для файлов с расширением *.xlsx.
            var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            // Читаем, получаем DataSet и работаем с ним как обычно.
            dat = excelReader.AsDataSet();

            // После завершения чтения освобождаем ресурсы.
            excelReader.Close();
        }

        public static void MakeDataSetRight(DataSet dat)
        {
            //Read(dat);
        }

        public static List<int> TextToVector(string file)
        {
            //делаем словарь
            var dictionary = new Dictionary<string, string>();
            MakeDictionary(dictionary);

            //файл с текстом
            //string file = "C:\\Users\\gubar\\Desktop\\trainset.txt";

            //будущий вектор
            List<int> TextVector = new List<int>();

            if (File.Exists(file))
            {
                string fileText = File.ReadAllText(file);
                List<string> words = fileText.ToLower().Split(new char[] { ' ', '\n' }).ToList<string>(); //лист с исходным текстом
                List<string> wordsRight = new List<string>(); //лист с текстом после стемминга
                char[] specialSymbols = "`~!@\"#№$;%:^?&*()-_=+[]{}'\\|/.,\r".ToCharArray();
                string word;

                //цикл стемминга текста
                foreach(var w in words)
                {
                    word = w.Trim(specialSymbols);
                    word = Porter.TransformingWord(word);
                    wordsRight.Add(word);
                }

                //составление вектоа
                foreach (var w in dictionary)
                {
                    word = w.Key.Trim(specialSymbols); //убираем спец символы
                    word = Porter.TransformingWord(word); //стемминг
                    if (wordsRight.Contains(word)) //если текст после стемминга содержит слово из словаря
                    {
                        TextVector.Add(1);
                    }
                    else
                    {
                        TextVector.Add(0);
                    }
                }
            }
            return TextVector;
        }

    }
}
