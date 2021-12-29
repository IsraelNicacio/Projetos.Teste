using Microsoft.Win32.TaskScheduler;
using System.Globalization;

namespace AgendadorTarefas
{
    public partial class Form1 : Form
    {
        private string database = @"D:\Documentos\Bancos\Infofisco\Infofisco\09687720000141\09687720000141.FDB";
        private string directory = @"D:\Backup Agendamento Teste";
        private string nickname = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}_Backup_Teste";

        private string userFirebird = "sysdba";
        private string passFirebird = "masterkey";

        string taskName = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            TaskService ts = new TaskService();
            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = $"Descricao da tarefa de teste";
            td.RegistrationInfo.Author = "Autor Mentalistas";
            DateTime startDate = DateTime.Now.AddMinutes(2);

            td.Triggers.Add(new DailyTrigger
            {
                //Data de inicio
                StartBoundary = startDate,
                //Intervalos em dias
                DaysInterval = 1,
                //Habilitar tarefa
                Enabled = true
            });

            //td.Triggers.Add(new MonthlyTrigger
            //{
            //    StartBoundary = startDate,
            //    MonthsOfYear = MonthsOfTheYear.AllMonths,
            //    Enabled = true
            //});

            //td.Triggers.Add(new WeeklyTrigger
            //{
            //    StartBoundary = startDate,
            //    DaysOfWeek = DaysOfTheWeek.Sunday,
            //    Enabled = true
            //});

            taskName = $"Tarefa teste {DateTime.Now.ToString("yyyyMMddHHmmss")}";

            string path = $"\"{Application.StartupPath}\\Resources\\BackupTeste.bat\"";
            string arguments = $"\"{database}\" \"{directory}\" \"{nickname}\" {userFirebird} {passFirebird}";
            string? workingDirectory = null;

            td.Actions.Add(new ExecAction(path, arguments, workingDirectory));
            ts.RootFolder.RegisterTaskDefinition(taskName, td);
        }

        private void btnStartTask_Click(object sender, EventArgs e)
        {
            using (TaskService ts = new TaskService())
            {
                Microsoft.Win32.TaskScheduler.Task task = ts.GetTask(taskName);
                task.Run(); //start
            }
        }
    }
}