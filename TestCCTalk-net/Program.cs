using dk.CctalkLib.Connections;
using dk.CctalkLib.Devices;
using System;
using System.Collections.Generic;

namespace TestCCTalk_net
{
    class Program
    {
        public static Dictionary<byte, CoinTypeInfo> UAConfig = new Dictionary<byte, CoinTypeInfo>()
        {
              { 1,  new CoinTypeInfo("UA100A", 1.00M) },  // старая 1 гривна
              { 3,  new CoinTypeInfo("UA050A", 0.50M) },  // 50 копеек не магнитная
              { 4,  new CoinTypeInfo("UA025A", 0.25M) }, // 25 копеек  (не берет)
              { 6,  new CoinTypeInfo("UA010A", 0.10M) }, // 10 копеек (не берет)
              { 7,  new CoinTypeInfo("UA005A", 0.05M) }, // 5 копеек (не берет)
              { 8,  new CoinTypeInfo("UA050B", 0.50M) }, // 50 копеек магнитная
              { 9,  new CoinTypeInfo("UA025B", 0.25M) }, // 25 копеек магнитная (не берет)
              { 10, new CoinTypeInfo("UA010B", 0.10M) }, // 10 копеек магнитная (не берет)
              { 11, new CoinTypeInfo("UA100B", 1.00M) }, // новая 1 гривна
              { 12, new CoinTypeInfo("UA200B", 2.00M) }, // новая 2 гривны
              { 13, new CoinTypeInfo("UA500B", 5.00M) }, // новая 5 гривен
              { 14, new CoinTypeInfo("UA1K0A", 10.00M) } // новая 10 гривен
        };


        static void Main(string[] args)
        {
            try
            {
                CoinAcceptor bv = new CoinAcceptor(2, new ConnectionRs232() { PortName = "COM7" });
                bv.Init();
                bv.ErrorMessageAccepted += Bv_ErrorMessageAccepted;
                bv.CoinAccepted += Bv_NotesAccepted;
                bv.PollInterval = new TimeSpan(0, 0, 0, 0, 200);
                bv.StartPoll();
                Console.ReadKey();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        private static void Bv_NotesAccepted(object sender, CoinAcceptorCoinEventArgs e)
        {
            Console.WriteLine($"{UAConfig[e.CoinCode].Name} -{UAConfig[e.CoinCode].Value}") ;

        }

        private static void Bv_ErrorMessageAccepted(object sender, CoinAcceptorErrorEventArgs e)
        {
            Console.WriteLine(e.ErrorMessage);
        }
    }
}
