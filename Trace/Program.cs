//#define TRACE
#undef TRACE



using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace TraceListenerApp
{
    class Program
    {
        private static TraceSource mySource = new TraceSource("TraceListenerApp");



        static void Activity1()
        {
            mySource.TraceEvent(TraceEventType.Error, 1, "Error message.");
            mySource.TraceEvent(TraceEventType.Warning, 2, "Warning message.");
            mySource.TraceEvent(TraceEventType.Critical, 3, "Critical message.");
            mySource.TraceInformation("Informational message A1.");
        }
        static void Activity2()
        {
            mySource.TraceEvent(TraceEventType.Error, 4, "Error message.");
            mySource.TraceEvent(TraceEventType.Warning, 5, "Warning message.");
            mySource.TraceEvent(TraceEventType.Critical, 6, "Critical message.");
            mySource.TraceInformation("Informational message A2.");
        }
        static void Activity3()
        {
            mySource.TraceEvent(TraceEventType.Error, 666, "Error message.");
            mySource.TraceInformation("Informational message A3.");
        }



        static void Main(string[] args)
        {
            BooleanSwitch dataSwitch = new BooleanSwitch("Data", "DataAccess module");
            TraceSwitch generalSwitch = new TraceSwitch("General", "Entire application");



            // If you do not set tracelistener, it will be 
            // printed in View -> Output window!



            // Testing debug!
            Debug.WriteLine("DEBUG: Program started!");
            // Testing trace!
            Trace.WriteLine("TRACE: Program started!");



            // this sets the trace to a console window (we currently use).
            /*
                        Trace.Listeners.Clear();
                        Trace.Listeners.Add(
                           new TextWriterTraceListener(Console.Out));
            */
            Trace.WriteLine("TRACELISTENER: Kokeilu!");



            Activity1();



            EventTypeFilter configFilter = (EventTypeFilter)mySource.Listeners["console"].Filter;



            mySource.Listeners["console"].Filter = new EventTypeFilter(SourceLevels.Critical);



            Activity2();



            mySource.Switch.Level = SourceLevels.All;



            mySource.Listeners["console"].Filter = configFilter;



            Activity3();



            //Trace.TraceInformation("TraceInformation Test!");
            //Trace.TraceWarning("TraceWarning Test!");
            //Trace.TraceError("TraceError Test!");



            int myvar = 123;
            int result = MyMethod(myvar);




            mySource.Close();



            Console.ReadLine();



        }



        private static int MyMethod(int myvar)
        {
            Trace.WriteLine("MyMethod has started");
            Trace.Assert(myvar > 100, "myvar should be bigger than 100");



            myvar++;
            myvar = 1;



            Trace.Assert(myvar == 0, "myvar is not zero");



            Trace.WriteLine("MyMethod has finished");
            return myvar;
        }
    }
}