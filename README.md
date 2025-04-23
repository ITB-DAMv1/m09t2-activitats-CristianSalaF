[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/LXcrfC_Y)

# T2 Activitats

## Investiga la llibreria System.Diagnostics. Enumera i explica els mètodes i propietats més rellevants o útils que trobis.

- Debugger
  - IsAttached: Propietat que indica si un debugger està connectat al procés. 
  - Launch(): Inicia el debugger i es connecta al procés actual (tal com has mencionat). 
  - Break(): Provoca una pausa en l'execució si un debugger està connectat.
- Process
  - Start(): Inicia un nou procés.
  - GetProcesses(): Obté tots els processos que s'executen en la màquina local.
  - GetProcessById(): Obté un procés específic per ID.
  - Kill(): Finalitza un procés.
  - WaitForExit(): Espera a que el procés finalitzi.
  - StandardOutput/StandardError/StandardInput: Propietats per manipular els fluxes d'entrada/sortida del procés.
- ProcessStartInfo: Configura les opcions per iniciar un procés: UseShellExecute, RedirectStandardOutput, Arguments, 
WorkingDirectory, etc.
- EventLog 
  - WriteEntry(): Escriu una entrada al registre d'esdeveniments de Windows.
  - CreateEventSource(): Crea una nova font d'esdeveniments.
  - GetEventLogs(): Obté els registres d'esdeveniments en la màquina local.
- Stopwatch
  - Start()/Stop(): Inicia/atura el cronòmetre.
  - Reset(): Reinicia el cronòmetre.
  - Elapsed: Propietat que retorna el temps transcorregut.
- TraceSource i Trace
  - TraceInformation/TraceWarning/TraceError(): Mètodes per registrar diferents nivells de missatges.
  - Listeners: Col·lecció d'objectes que reben els missatges de traça.
  - AutoFlush: Propietat que controla si els missatges s'escriuen immediatament.
- PerformanceCounter: Permet accedir als comptadors de rendiment del sistema.
  - NextValue(): Obté el valor actual del comptador.
  - RawValue: Propietat que retorna el valor brut.

## Exercici 3
```csharp
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
```
No sembla que crei nous fils al obrir la pestanya, 47/47 (abans/després)

## Comparativa Thread vs Task

- Start(): Inicia l'execució del fil.
- Abort(): (Obsolet) Finalitza forçosament un fil.
- Join(): Bloqueja el fil actual fins que el fil objectiu finalitzi.
- Sleep(): Suspèn el fil actual durant un temps determinat.
- Interrupt(): Interromp un fil en estat de bloqueig.
- CurrentThread: Propietat estàtica que retorna el fil actual.
- IsAlive: Indica si el fil està en execució.
- Priority: Permet establir o obtenir la prioritat d'execució del fil.
- IsBackground: Determina si el fil és background (no impedeix que l'aplicació finalitzi).
- ThreadState: Propietat que retorna l'estat actual del fil.

| Aspecte | Thread | Task |
|---------|--------|------|
| **Espai de noms** | System.Threading | System.Threading.Tasks |
| **Abstracció** | Baix nivell | Alt nivell |
| **Orientació** | Orientat a fils | Orientat a operacions |
| **Retorn de valors** | No pot retornar valors directament | Pot retornar resultats via `Task<T>` |
| **Excepcions** | Les excepcions no capturades finalitzen l'aplicació | Les excepcions són capturades i es propaguen quan es fa await |
| **Cancel·lació** | No suporta cancel·lació nativa | Suporta cancel·lació via CancellationToken |
| **Continuacions** | No té suport natiu, cal implementar manualment | Suporta continuacions i encadenament (ContinueWith) |
| **Sincronització** | Manual mitjançant monitors, mutex, etc. | Integració amb async/await |
| **Pool de threads** | Crea un nou fil del sistema | Utilitza el ThreadPool per defecte |
| **Recursos** | Més pesat en recursos | Més eficient en recursos |
| **Planificació** | Control explícit | Gestionat pel Task Scheduler |
| **Paral·lelisme** | Control individual de fils | Fàcil composició amb Task.WhenAll, Task.WhenAny |
| **Async/Await** | No compatible | Totalment integrat |
| **Creació** | `new Thread(ThreadStart)` | `Task.Run()`, `new Task()`, Factory methods |
| **Per a què s'usa** | Control de baix nivell, interfície de serveis del SO | La majoria d'operacions asíncrones, paral·lelisme modern |