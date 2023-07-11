using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace Dane
{
    public interface InterfejsLogger
    {
        internal void AddQueueLog(InterfaceKulka kulka);
        public void ustawTimer(double interval);
    }

    public class KulkiLogger : InterfejsLogger
    {
        private readonly string filePath;
        private readonly JArray fileJArray;
        private readonly Mutex filemutex = new();
        private readonly Mutex queuemutex = new();

        private Task? loggingTask;
        private static System.Timers.Timer? timer;
        private readonly ConcurrentQueue<JObject> kulkiQueue = new();

        public KulkiLogger()
        {
            string path = Path.GetTempPath();
            filePath = path + "Kulki.json";

            if (File.Exists(filePath))
            {
                filemutex.WaitOne();

                try
                {
                    string input = File.ReadAllText(filePath);
                    fileJArray = JArray.Parse(input);
                    return;
                }
                catch (JsonReaderException)
                {
                    fileJArray = new JArray();
                }
                finally
                {
                    filemutex.ReleaseMutex();
                }
            }

            fileJArray = new JArray();
            File.Create(filePath);

        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Elapsed event: {0:HH:mm:ss.fff}", e.SignalTime);
            LogToFile();
        }

        private void LogToFile() 
        { 
            while(kulkiQueue.TryDequeue(out JObject kulka))
            {
                fileJArray.Add(kulka);
            }

            string output = JsonConvert.SerializeObject(fileJArray);

            filemutex.WaitOne();

            try
            {
                File.WriteAllText(filePath, output);
            } finally
            {
                filemutex.ReleaseMutex();
            }
        }

        public void ustawTimer(double interval)
        {
            timer = new System.Timers.Timer(interval);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void AddQueueLog(InterfaceKulka kulka)
        {
            queuemutex.WaitOne();

            try
            {
                JObject doKolejki = JObject.FromObject(kulka);
                doKolejki["Time"] = DateTime.Now.ToString("HH:mm:ss");
                kulkiQueue.Enqueue(doKolejki);
            }
            finally
            {
                queuemutex.ReleaseMutex();
            }
        }

        ~KulkiLogger() 
        {
            filemutex.WaitOne();
            filemutex.ReleaseMutex();
        }
    }
}
