using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

namespace Agente
{
    public partial class Agente : ServiceBase
    {
        private int eventId = 1;

        public Agente()
        {
            InitializeComponent();
            eventLog = new EventLog();
            if (!EventLog.SourceExists("AgentePrueba"))
                EventLog.CreateEventSource("AgentePrueba", "AgentePruebaLog");

            eventLog.Source = "AgentePrueba";
            eventLog.Log = "AgentePruebaLog";
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.  
            eventLog.WriteEntry("Monitoreando el sistema", EventLogEntryType.Information, eventId++);
        }

        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("Se activa el agente");
            var timer = new Timer(10000);
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
            timer.Start();
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("Se detiene el agente");
        }
    }
}
