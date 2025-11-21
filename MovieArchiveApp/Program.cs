namespace MovieArchiveApp
{
    internal static class Program
    {
        
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);


            ApplicationConfiguration.Initialize();
            Application.Run(new frmLogin());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception)?.Message, "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Uygulama Hatasý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}