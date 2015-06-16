using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace B15_Ex05
{
    class Program
    {
        public static void Main() 
        {
            

            getSettingsFromUser();
        }

        private static void getSettingsFromUser()
        {
            GameSettings gameSettingsForm = new GameSettings();
            gameSettingsForm.ShowDialog();
        }


        public void OnUserClickedButtonEventHandler(object source, EventArgs args)
        {
            //int[] buttonIndex = source as int[];
            Console.WriteLine("entered Program Class");
        }


    }
}
