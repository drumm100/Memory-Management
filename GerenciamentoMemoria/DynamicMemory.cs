using System;
using System.Collections.Generic;

namespace GerenciamentoMemoria
{
    public class DynamicMemory
    {
        private List<string> _memory;
        private int _size;
        private int _space;
        private int _firstFitIndex;
        
        public DynamicMemory(int size)
        {
            _memory = new List<string>();
            _size = (int) Math.Pow(2, size);
            _space = _size;
            _firstFitIndex = 0;

            for (int i = 0; i < _size; i++)
            {
                _memory.Add("");
            }
            
            Console.WriteLine("|"+_size+"|");
        }


        public void ProcessMessage(string method, string operation,  string messageId, int size = 0)
        {
            if (operation.Equals("OUT"))
            {
                ClearMemory(messageId);
                PrintRealTime();
            }
            else
            {
                switch (method)
                {
                    case "firstFit":
                        FirstFit(messageId, size);
                        PrintRealTime();
                        break;
                    case "worstFit":
                        WorstFit(messageId, size);
                        PrintRealTime();
                        break;
                    case "bestFit":
                        BestFit(messageId, size);
                        PrintRealTime();
                        break;
                    
                    case "nextFit":
                        NextFit(messageId, size);
                        PrintRealTime();
                        break;
                }
            }
        }

        private void ClearMemory(string messageId)
        {
            for (int i = 0; i < _memory.Count; i++)
            {
                if (_memory[i] == messageId)
                {
                    _memory[i] = "";
                    _space++;
                }
            }
        }

        private void PrintRealTime()
        {
            int nullSpaces = 0;
            
            foreach (var space in _memory)
            {
                if (space != "" && nullSpaces > 0)
                {
                    Console.Write("|" +nullSpaces + "|");
                    nullSpaces = 0;
                }
                
                if (space == "") nullSpaces++;
            }
            Console.WriteLine("|" + (nullSpaces) + "|");
        }
        
        
        
        
        public void PrintMemory()
        {
            foreach (var VARIABLE in _memory)
            {
                Console.WriteLine(VARIABLE);
            }
        }

        private (int, int) BestFitSmallestSpace(int messageSize)
        {
            int nullSpaces = 0;
            int indexSpace = 0;
            var space = 0;

            if (_space == _size) return (0, 16);

            int index = 0;

            for (int i = 0; i < _size; i++)
            {
                if (_memory[i] == "" && nullSpaces == 0)
                {
                    index = i;
                }

                if (_memory[i] != "" && nullSpaces > 0)
                {
                    if (space > 0)
                    {
                        if (nullSpaces < space && nullSpaces >= messageSize)
                        {
                            space = nullSpaces;
                            indexSpace = index;
                        }
                    }
                    else if (nullSpaces >= messageSize)
                    {
                        space = nullSpaces;
                        indexSpace = index;
                    }

                    nullSpaces = 0;
                }

                if (_memory[i] == "") nullSpaces++;

                if (i == _size - 1)
                {
                    if (space > 0)
                    {
                        if (nullSpaces < space)
                        {
                            space = nullSpaces;
                            indexSpace = index;
                        }
                    }
                    else
                    {
                        space = nullSpaces;
                        indexSpace = index;
                    }
                }
            }
            
            return (indexSpace, space);
            
        }
        
        // Variable (or dynamic) Partitioning
        private void BestFit(string messageId, int messageSize)
        {
            var space = BestFitSmallestSpace(messageSize);
            
            for (int i = space.Item1; i < _size; i++)
            {
                if (messageSize == 0) break;
                
                _memory[i] = messageId;
                messageSize--;
                _space--;
            }
            
        }


        private (int,int) WorstFitBiggerSpace()
        {
            int nullSpaces = 0;
            int indexBiggerSpace = 0;
            var biggerSpace = 0;

            if (_space == _size) return (0, 16);

            int index = 0;
            
            for (int i = 0; i < _size; i++)
            {
                if (_memory[i] == "" && nullSpaces == 0)
                {
                    index = i;
                }
                
                if (_memory[i] != "" && nullSpaces > 0)
                {
                    if (nullSpaces > biggerSpace)
                    {
                        biggerSpace = nullSpaces;
                        indexBiggerSpace = index;
                    }
                    nullSpaces = 0;
                }

                if (_memory[i] == "") nullSpaces++;

                if (i == _size - 1)
                {
                    if (nullSpaces > biggerSpace)
                    {
                        biggerSpace = nullSpaces;
                        indexBiggerSpace = index;
                    }
                }
            }

            return (indexBiggerSpace, biggerSpace);
        }
        
        void WorstFit(string messageId, int messageSize)
        {

            var biggerSpace = WorstFitBiggerSpace();

            if (messageSize > biggerSpace.Item2)
            {
                Console.WriteLine("Insufficient memory space");
                return;
            }

            for (int i = biggerSpace.Item1; i < _size; i++)
            {
                if (messageSize == 0) break;
                
                _memory[i] = messageId;
                messageSize--;
                _space--;
            }
        }

        void FirstFit(string messageId, int messageSize)
        {
            if (_space < messageSize)
            {
                Console.WriteLine("Insufficient memory space");    
                return;
            }
            
            int index = 0;
            int space = 0;
            
            for (int i = 0; i < _size; i++)
            {
                if (messageSize == 0) break;

                if (_memory[i] == "")
                {
                    if (space == 0) index = i;
                    space++;
                }
                
                if (space == messageSize) break;
                if (_memory[i] != "" && space > 0) space = 0;
            }
            
            for (int i = index; i <_size ; i++)
            {    
                if (messageSize == 0) break;
                if (_memory[i] == "")
                {
                    _memory[i] = messageId;
                    _space--;
                    messageSize--;
                }
            }
            
        }

        void NextFit(string messageId, int messageSize)
        {
            if (_space < messageSize)
            {
                Console.WriteLine("Insufficient memory space");    
                return;
            }
            
            int index = 0;
            int space = 0;
            
            for (int i = _firstFitIndex; i < _size; i++)
            {
                if (messageSize == 0) break;

                if (_memory[i] == "")
                {
                    if (space == 0) index = i;
                    space++;
                }
                
                if (space == messageSize) break;
                if (_memory[i] != "" && space > 0) space = 0;
            }



            _firstFitIndex = index;
            
            for (int i = index; i <_size ; i++)
            {    
                if (messageSize == 0) break;
                if (_memory[i] == "")
                {
                    _memory[i] = messageId;
                    _space--;
                    messageSize--;
                }
            }

        }
    }
}