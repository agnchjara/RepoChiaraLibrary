using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Library
{
    static public class LoginUI
    {
        static public void SuccessfulLogin()
        {
            Console.WriteLine("Congratulations, you are successfully logged in.");
        }
        static public void LoginFailure()
        {
            Console.WriteLine("Invalid Username / Password Combination Entered. Please Correct and Reenter.");
        }
    }
}
