using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Capturer.ViewModel
{
    public class ServiceCtrl
    {
        public static bool restartService()
        {
            try
            {
                ServiceController sc = new ServiceController();
                sc.ServiceName = "Capturer";

                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);
                    Logger.Logs.Add(new Types.Log("Usługa uruchomiona.", 2));
                    return true;
                }
                else if (sc.Status == ServiceControllerStatus.Running)
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped);
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);
                    Logger.Logs.Add(new Types.Log("Usługa zrestartowana.", 2));
                    return true;
                }
            }
            catch (Exception e)
            {
                Logger.Logs.Add(new Types.Log("Wystąpił błąd podczas uruchamiania usługi.", 2));
                Logger.Logs.Add(new Types.Log(e.Message, 2));
            }
            return false;
        }
    }
}
