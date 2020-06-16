using System;
using System.Runtime.CompilerServices;

namespace GerenciamentoMemoria
{
    public class FixedMemory
    {
        private string[] _memory;

        public FixedMemory(int size)
        {
            CreateMemoryFixed(size);
        }

        public void ProcessMessage(string operation, string id, int size)
        {
            if (operation.Equals("IN"))
            {
                int initialPosition = FindSpaceToAllocate(size);
                PrintMemoryRealTime();
                if (initialPosition >= 0) AllocateSpace(id, size, initialPosition);
                else Console.WriteLine("Insufficient memory space for the operation:  IN(" + id + "/" + size + ")");
            }
        }

        public void PrintMemory()
        {
            foreach (var space in _memory)
            {
                //Console.WriteLine("FixedMemory> " + space);
            }
        }

        private void PrintMemoryRealTime()
        {
            int allocatedSpaces = 0;
            int totalAllocated = 0;
            string id = _memory[0];
            
            if (id == null) Console.WriteLine("|"+_memory.Length+"|");
            else
            {
                foreach (var space in _memory)
                {
                    if (id != space)
                    {
                        Console.Write("|" + allocatedSpaces + "|");
                        allocatedSpaces = 0;
                    }
                    
                    if (space != null)
                    {
                        allocatedSpaces++;
                        totalAllocated++;
                    }

                    if (space == null) Console.WriteLine("|" + (_memory.Length-totalAllocated) + "|");
                    
                }
            }
        }
        
        private void CreateMemoryFixed(int size)
        {
            _memory = new string[(int) Math.Pow(2, size)];
        }
        
        private int FindSpaceToAllocate(int size)
        {
            int initialPosition = -1;
            int finalPosition = -1;
            
            Console.WriteLine("Memory size: " + _memory.Length);
            
            for (int i = 0; i < _memory.Length; i++)
            {
                if (_memory[i] == null)
                {
                    if (initialPosition == -1) initialPosition = i;
                    if (i - initialPosition + 1 == size) finalPosition = i;
                }
            }

            //Console.WriteLine("ip:" + initialPosition);
            //Console.WriteLine("fp:" + finalPosition);

            if (initialPosition >= 0 && finalPosition >= 0) return initialPosition;
            else return -1;
        }
        
        private void AllocateSpace(string id, int size, int initialPosition)
        {
            for (int i = initialPosition; i < _memory.Length; i++)
            {
                if (size > 0) _memory[i] = id;
                size--;
            }

            //PrintMemoryRealTime();

        }
    }
}