namespace MMASimulation.Shared.Engine.Comments.ReadTxt
{
    public static class ReadTxts
    {

        public static List<string> ReadFileToList(string nomeArquivo)
        {
            List<string> lines = new List<string>();

            try
            {
                string filePatch = $"";
                string filePath = $"Engine/Comments/Texts/{nomeArquivo}.txt";

                string[] fileContent = File.ReadAllLines(filePath);

                lines.AddRange(fileContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
            }

            return lines;
        }

    }
}
