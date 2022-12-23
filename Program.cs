using System.Text;
using System.Data;



enum MemoryMode { GET, SET }
enum NeuronType { Hidden, Output }

namespace network
{
    class Programm
    {
        
        class InputLayer
        {

            //int[] vector = Vectorizing.TextToVector().ToArray();

            private (int[], int[])[] _trainset = new (int[], int[])[]//да-да, массив кортежей из 2 массивов
            {
                //(new int[]{ 0, 0 }, new int[]{ 0, 1 }),
                //(new int[]{ 0, 1 }, new int[]{ 1, 0 }),
                //(new int[]{ 1, 0 }, new int[]{ 1, 0 }),
                //(new int[] { 1, 1 } , new int[] { 0, 1 })
            };

            public (int[], int[])[] Trainset { get => _trainset; }
        }


        static void Main(string[] args)
        {
            
            DataSet res= new DataSet();
            Vectorizing.MakeDataSet(res);
            Vectorizing.MakeDataSetRight(res);
        }
    }
   


}

