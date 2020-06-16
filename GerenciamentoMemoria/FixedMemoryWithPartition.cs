using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GerenciamentoMemoria
{
    public class FixedMemoryWhitPartition
    {
        private string[][] _memory;
        private int _partitionSize;

        private List<int> printList;
        
        public FixedMemoryWhitPartition(int size, int partitionSize)
        {
            _partitionSize = (int) Math.Pow(2, partitionSize);
            CreateMemoryFixed(size);
        
        }

        public void ProcessMessage(string operation, string messageId, int messageSize = 0)
        {
            if (operation.Equals("IN"))
            {
                AllocateSpace(messageId, messageSize);
                RealTimePrint();
            }

            if (operation.Equals("OUT"))
            {
                ClearMemory(messageId);
                RealTimePrint();
            }
        }


        private void RealTimePrint()
        {
            printList = new List<int>();
            
            int nullCounter = 0;
            int ec = 0;

            
            int lastPopulatePartition = 0;
            int totalSize = _memory.Length * _partitionSize;
            
            for (int i = 0; i < _memory.Length; i++)
            {
                if (_memory[i][0] != "") lastPopulatePartition = i;
            }

            for (int i = 0; i < _memory.Length; i++)
            {
                for (int j = 0; j < _partitionSize; j++)
                {
                    if (_memory[i][j] == "") nullCounter++;
                    if (_memory[i][j] != "") ec++;
                }
                
                if (i != lastPopulatePartition)
                {
                    printList.Add(nullCounter);
                    ec = ec + nullCounter;
                    nullCounter = 0;
                }
                    
                if (i == lastPopulatePartition)
                {
                    printList.Add(totalSize-(ec));
                    break;
                }
            }

            List<int> print = PrintList();

            foreach (var VARIABLE in print)
            {
                Console.Write("|" + (VARIABLE) + "|");
            }
            Console.WriteLine("");
            
        }

        private List<int> PrintList()
        {    
            
            List<int> final = new List<int>();
            
            for (int i = 0; i < printList.Count; i++)
            {
                if (i > 0)
                {
                    if (printList[i] == 4)
                    {
                        printList[i] += printList[i - 1];
                        printList[i - 1] = 0;
                    }
                }
            }
            
            for (int i = 0; i < printList.Count; i++)
            {
                if (printList[i] > 0 ) final.Add(printList[i]);
            }

            return final;
        }

        
        public void printMemory()
        {
            for (int i = 0; i < _memory.Length; i++)
            {
                Console.WriteLine("Partition " + (i+1) + ":" );
                if (_memory[i][0] != "")
                {
                    for (int j = 0; j < _memory[i].Length; j++)
                    {
                        Console.WriteLine("Space: " + _memory[i][j]);
                    }    
                }
            }
        }

        
        private void CreateMemoryFixed(int size)
        {
            int memorySize = (int) Math.Pow(2, size);
            
            _memory = new string[(memorySize/_partitionSize)][];


            Console.WriteLine("DEBUG Memory size: " + _memory.Length * _partitionSize + " / partitions size: " + _partitionSize);

            Console.WriteLine("|" + (_memory.Length * _partitionSize) + "|");

            //allocate partitions
            for (int i = 0; i < _memory.Length; i++)
            {
                _memory[i] = new string[_partitionSize];
                for (int j = 0; j < _partitionSize; j++)
                {
                    _memory[i][j] = "";
                }
            }
            
        }

        private void ClearMemory(string messageId)
        {
            for (int i = 0; i < _memory.Length; i++)
            {
                for (int j = 0; j < _partitionSize; j++)
                {
                    if (_memory[i][j] == messageId) _memory[i][j] = "";
                }
            }
        }
        
        private void AllocateSpace(string messageId, int messageSize)
        {
            if (messageSize > _partitionSize)
            {
                Console.WriteLine("Insufficient memory space");
                return;
            }
            
            for (int i = 0; i < _memory.Length; i++)
            {

                if (messageSize == 0) return;

                if (_memory[i][0] == "")
                {

                    for (int j = 0; j < _partitionSize; j++)
                    {
                        if (messageSize > 0)
                        {
                            _memory[i][j] = messageId;
                            messageSize--;
                        }
                    }
                }
            }
        }


    }
}