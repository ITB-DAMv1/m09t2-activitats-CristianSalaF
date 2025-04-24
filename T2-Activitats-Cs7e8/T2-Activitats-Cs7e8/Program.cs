using System;
using System.Diagnostics;

namespace T2_Activitats_Cs7e8
{
    internal class Program
    {
        private const string MenuText = "1. Exercici 2 - Llista de processos\n" +
                              "2. Exercici 3 - Detalls de PID (introduïr)\n" +
                              "3. Exercici 5 - Fils aleatoris\n" +
                              "4. Exercici 5b - Fils ordenats\n" +
                              "5. Exercici 6 - Cursa de Camells\n" +
                              "0. Sortir\n";
        private const string TextExitState = "Sortint del programa...";
        private const string TextInvalidOption = "Opció no vàlida. Torneu-ho a intentar.";
        private const string TextContinue = "Prem qualsevol tecla per continuar...";
        public static bool isPing = true;
        public static object locker = new object();

        public static int curRound = 0;
        public static int rounds = 10;
        static void Main(string[] args)
        {
            int opc = -1;
            
            while (opc != 0)
            {
                Console.Clear();
                Console.WriteLine(MenuText);
                Console.Write("Opció: ");
                opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        Exercise2();
                        break;
                    case 2:
                        Exercise3();
                        break;
                    case 3:
                        Exercise5();
                        break;
                    case 4:
                        Exercise5b();
                        break;
                    case 5:
                        Exercise6();
                        break;
                    case 0:
                        Console.WriteLine(TextExitState);
                        break;
                    default:
                        Console.WriteLine(TextInvalidOption);
                        break;
                }
                if(opc != 0)
                {
                    Console.WriteLine(TextContinue);
                    Console.ReadKey();
                }
            }
        }

        static void Exercise2()
        {
            var processList = Process.GetProcesses();

            foreach (Process process in processList)
            {
                Console.WriteLine($"Process ID: {process.Id}, name: {process.ProcessName}");
            }

            string filePath = "../../../Files/ProcessList.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Process process in processList)
                {
                    writer.WriteLine($"Process ID: {process.Id}, name: {process.ProcessName}");
                }
            }
        }


        static void Exercise3()
        {
            int pid = int.Parse(Console.ReadLine());

            try
            {
                Process proc = Process.GetProcessById(pid);
                ProcessThreadCollection threads = proc.Threads;

                proc.Refresh();
                PrintThreadInfo(proc, threads);

                Console.WriteLine(
                "\nObriu una nova pestanya manualment al navegador i premeu qualsevol tecla per analitzar de nou...");
                Console.ReadKey();
                proc.Refresh();
                threads = proc.Threads;
                PrintThreadInfo(proc, threads);

                proc.WaitForExit(5000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }            
        }

        private static void PrintThreadInfo(Process proc, ProcessThreadCollection threads)
        {
            Console.WriteLine(
                $"\nAnàlisi de fils per al procés {proc.ProcessName} (ID: {proc.Id}) amb {threads.Count} fil/-s");
            foreach (ProcessThread thread in threads)
            {
                Console.WriteLine(
                    $"ID del fil: {thread.Id}\tHora d'inici: {thread.StartTime}\tPrioritat: {thread.PriorityLevel}");
            }
        }

        static void Exercise5()
        {
            for (int i = 1; i <= 5; i++)
            {
                int threadNumber = i;
                Thread hilo = new Thread(() => ThreadTask(threadNumber));
                hilo.Start();
            }
        }

        static void ThreadTask(int threadNumber)
        {
            Console.WriteLine($"Hola! Soc el fil número {threadNumber}");
        }

        static void Exercise5b()
        {
            Console.WriteLine("Iniciant programa amb 5 fils en ordre invers");

            for (int i = 1; i <= 5; i++)
            {
                int numeroHilo = i;
                Thread hilo = new Thread(() => DelayThread(numeroHilo));
                hilo.Start();
            }
        }

        static void DelayThread(int threadNumber)
        {
            Thread.Sleep((6 - threadNumber) * 100);
            Console.WriteLine($"Hola! Soc el fil número {threadNumber}");
        }

        static void Exercise6()
        {
            Console.WriteLine("Iniciant carrera de camells");
            for (int i = 1; i <= 5; i++)
            {
                int numeroHilo = i;
                Thread hilo = new Thread(() => CamelRace(numeroHilo));
                hilo.Start();
            }
        }

        private static void CamelRace(int ThreadNumber)
        {
            string[] colors = {
               "\u001b[1;38;2;255;0;0m",    // Red  
               "\u001b[1;38;2;0;255;0m",    // Green  
               "\u001b[1;38;2;0;0;255m",    // Blue  
               "\u001b[1;38;2;255;255;0m",  // Yellow  
               "\u001b[1;38;2;255;0;255m"   // Magenta  
           };
            string[] colorsDim = {            //Dim colors
               "\u001b[2;38;2;255;0;0m",    // Red  
               "\u001b[2;38;2;0;255;0m",    // Green  
               "\u001b[2;38;2;0;0;255m",    // Blue  
               "\u001b[2;38;2;255;255;0m",  // Yellow  
               "\u001b[2;38;2;255;0;255m"   // Magenta  
           };
            string[] colorsUnderline = {
               "\u001b[1;4;38;2;255;0;0m",    // Red  
               "\u001b[1;4;38;2;0;255;0m",    // Green  
               "\u001b[1;4;38;2;0;0;255m",    // Blue  
               "\u001b[1;4;38;2;255;255;0m",  // Yellow  
               "\u001b[1;4;38;2;255;0;255m"   // Magenta  
           };

            string color = colors[(ThreadNumber - 1) % colorsDim.Length];
            string colorDim = colorsDim[(ThreadNumber - 1) % colorsDim.Length];
            string colorUnderline = colorsUnderline[(ThreadNumber - 1) % colorsDim.Length];
            Console.WriteLine($"{color}Camell {ThreadNumber} ha començat la carrera!\u001b[0m");

            Random random = new Random();
            int min = random.Next(50, 100);
            int max = random.Next(100, 200);

            for (int i = 0; i <= 100; i++)
            {
                Console.WriteLine($"{colorDim}Camell {ThreadNumber} va per la volta {i}\u001b[0m");
                Thread.Sleep(random.Next(min, max));
            }

            Console.WriteLine($"{colorUnderline}Camell {ThreadNumber} ha acabat la carrera!\u001b[0m");
        }
    }
}