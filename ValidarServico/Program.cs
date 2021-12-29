// See https://aka.ms/new-console-template for more information


using System.ServiceProcess;

try
{
    ServiceController myService = new ServiceController();
    myService.ServiceName = "Infofisco x ClickAtente | API";

    if(myService.Status != ServiceControllerStatus.Running)
        //mensagem
        Console.WriteLine( $"{myService.ServiceName} {myService.DisplayName} {myService.ServiceType} {myService.Status}");
}
catch (InvalidOperationException ix)
{
    Console.WriteLine(ix.Message);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
