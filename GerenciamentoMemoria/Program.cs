/*@Author: Marcelo Drumm*/

using System;
using System.IO;

namespace GerenciamentoMemoria
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("T2 - SISOP");
            
            string path = Directory.GetCurrentDirectory();
            string[] instructions = System.IO.File.ReadAllLines(path.Split("bin")[0] +"instructions.txt");
            
            Console.WriteLine("Memória 2^n. Informe o n para definir tamanho da memória principal.\n");
            string memorySize = Console.ReadLine();

            Console.WriteLine("Escolha o método");
            Console.WriteLine("1 - Partição Fixa \n2 - Partição Varíavel \n");
            string input = Console.ReadLine();

            if (input == "1")
            {
                Console.WriteLine("Partição 2^n. Informe o n para definir tamanho da partição.");
                string partitionSize = Console.ReadLine();
                
                var fmp = new FixedMemoryWhitPartition(Int32.Parse(memorySize), Int32.Parse(partitionSize));
                
                
                foreach (var instruction in instructions)
                {
                    var process = ReadLine(instruction);
                    fmp.ProcessMessage(process.Item1, process.Item2, process.Item3);
                }
            }
            else if (input == "2")
            {
                Console.WriteLine("Escolha a política de alocação: \n1 - Best Fit \n2 - Worst Fit \n3 - First Fit \n4 - Next Fit \n");
                string politicChoice = Console.ReadLine();
                string politic = "";
                
                switch (politicChoice)
                {
                    case "1":
                        politic = "bestFit";
                        break;
                    case "2":
                        politic = "worstFit";
                        break;
                    case "3":
                        politic = "firstFit";
                        break;
                    case "4":
                        politic = "nextFit";
                        break;
                }

                if (politic == "")
                {
                    Console.WriteLine("Comando inválido. Programa finalizado");
                }
                
                var dm = new DynamicMemory(Int32.Parse(memorySize));
                
                foreach (var instruction in instructions)
                {
                    var process = ReadLine(instruction);
                    dm.ProcessMessage(politic,process.Item1, process.Item2, process.Item3);
                }
                
            }
            else Console.WriteLine("Comando inválido. Programa finalizado");
            
            
            
        }

        static private (string, string, int) ReadLine(string line)
        {
            string messageId = "";
            string messageSizeString = "";
            string operation = line.Split("(")[0];
            if (operation == "OUT")
            {
                messageId = line.Split("(")[1].Split(")")[0];
            }
            else
            {
                messageId = line.Split("(")[1].Split(",")[0];
                messageSizeString = line.Split("(")[1].Split(",")[1].Split(")")[0];
            }
                    
            //Console.WriteLine("Executing process: " + operation + "," + messageId + "," + messageSizeString);


            int messageSize = 0;
            if (messageSizeString != "") messageSize = Int32.Parse(messageSizeString);

            return (operation, messageId, messageSize);
        }
        
    }
}