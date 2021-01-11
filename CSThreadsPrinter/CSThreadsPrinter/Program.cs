using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace CSThreadsPrinter
{


    class Program
    {
        
        public static List<string> wordList = new List<string>(); //Liste des mots lus
        public static bool readOver = false; //Si vrai, la lecture dans le fichier est terminée

        /**
         * Lit les mots depuis un fichier et les mets dans une liste globale
         */
        public static void Read()
        {
            string _filePath = @"D:\hhers\Documents\Univ_2020-2021\S7\Info 708 - Parallélisme\csThreads\CSThreadsPrinter\CSThreadsPrinter\input.txt";

            List<string> lines = File.ReadAllLines(_filePath).ToList();

            int i = 0;

            foreach (string line in lines)
            {
                String[] words = line.Split(" ");
                foreach (string word in words)
                {
                    wordList.Add(word);
                    wordList.Add(" ");

                    //====== test pour voir l'ordre d'exécution des threads
                    //i += 2;
                    //Console.WriteLine("i : " + i);
                    //======
                }
                wordList.Add("\n");

            }
            readOver = true;
        }

        /**
         * Prends la liste globale et l'affiche dans la console
         */
        public static void Print()
        {
            int _wordIndex = 0;

            //Lorsque la lecture n'est pas terminée ou que l'affichage n'a pas terminé la liste
            while (readOver == false || _wordIndex < wordList.Count())
            {
                if (_wordIndex < wordList.Count())
                {
                    Console.Write(wordList[_wordIndex]);
                    _wordIndex++;

                    //====== test pour voir l'ordre d'exécution des threads
                    //Console.WriteLine("_wordindex : " + _wordIndex);
                    //======
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Début du main.");

            Stopwatch timer = new Stopwatch();
            timer.Start();

            //Démarrage des Threads
            Thread thread1 = new Thread(Read);
            Thread thread2 = new Thread(Print);
            thread1.Start();
            thread2.Start();


            //Attente de la fin des threads pour le retour au main
            thread1.Join();
            thread2.Join();

            timer.Stop();
            Console.WriteLine("fin du main. \ntemps pris : " + timer.ElapsedMilliseconds + "ms");
        }
    }
}
