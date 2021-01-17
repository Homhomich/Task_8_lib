﻿using System;
using System.Drawing;
using System.Windows.Forms;
 using Task_8_Form;

 namespace ItTask8_3
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1 {Size = new Size(1500, 800)});
        }
    }
}