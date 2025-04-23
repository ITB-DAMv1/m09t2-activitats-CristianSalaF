using System;
using System.Diagnostics;

namespace T2_Activitats_Cs7e8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Activitat2();
            //Activitat3();

            Activitat5();
        }

        static void Activitat2()
        {
            var processList = Process.GetProcesses();

            foreach (var process in processList)
            {
                Console.WriteLine($"Process ID: {process.Id}");
            }
        }


        static void Activitat3()
        {
            var proc = Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe");
            ProcessThreadCollection threads;

            Console.WriteLine("Esperant que el navegador s'inicialitzi completament...");
            Thread.Sleep(10000);
            threads = proc.Threads;
            proc.Refresh();
            PrintThreadInfo(proc, threads);

            Console.WriteLine("\nObriu una nova pestanya manualment al navegador i premeu qualsevol tecla per analitzar de nou...");
            Console.ReadKey();
            proc.Refresh();
            threads = proc.Threads;
            PrintThreadInfo(proc, threads);

            proc.WaitForExit(5000);
        }

        private static void PrintThreadInfo(Process proc, ProcessThreadCollection threads)
        {
            Console.WriteLine($"\nAnàlisi de fils per al procés {proc.ProcessName} (ID: {proc.Id}) amb {threads.Count} fil/-s");
            foreach (ProcessThread thread in threads)
            {
                Console.WriteLine($"ID del fil: {thread.Id}\tHora d'inici: {thread.StartTime}\tPrioritat: {thread.PriorityLevel}");
            }
        }

        static void Activitat5() 
        {
            for (int i = 1; i <= 5; i++)
            {
                int numeroHilo = i;
                Thread hilo = new Thread(() => TrabajoHilo(numeroHilo));
                hilo.Start();
            }

            Console.ReadLine();
        }

        static void TrabajoHilo(int numeroHilo)
        {
            Console.WriteLine($"Hola! Soc el fil número {numeroHilo}");
        }

        static void Activitat5b()
        {
            Console.WriteLine("Iniciando programa con 5 hilos en orden inverso");

            for (int i = 1; i <= 5; i++)
            {
                int numeroHilo = i;
                Thread hilo = new Thread(() => DelayThread(numeroHilo));
                hilo.Start();
            }
            Console.ReadLine();
        }

        static void DelayThread(int numeroHilo)
        {
            // El hilo con número mayor dormirá menos tiempo
            // Esto debería hacer que los hilos se ejecuten en orden inverso (5, 4, 3, 2, 1)
            Thread.Sleep((6 - numeroHilo) * 100);
            Console.WriteLine($"Hola! Soc el fil número {numeroHilo}");
        }
    }
    }
}