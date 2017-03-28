using System;
using System.Windows.Forms;


namespace sw_router
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            
            new Controller();
            Controller.Instance.setForm(form);
            Application.Run(form);
            Utils.tests();
        }

    }
}
