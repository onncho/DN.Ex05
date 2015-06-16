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
            GameSettings myform = new GameSettings(); 
            myform.ShowDialog();


            //subscriber
            GameController gc = new GameController(false);
            //register and pass the referance to the method


        }
    }
}
