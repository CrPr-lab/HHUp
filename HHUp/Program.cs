using System;
using System.Threading;

namespace HHUp
{
    class Program
    {
        const int UpFreqencyMinutes = 250; 

        static void Main(string[] args)
        {
            if (args.Length < 2)
                throw new Exception("В параметры запуска необходимо передать логин и пароль");

            while (true)
            {
                UpResume(args[0], args[1]);
                Thread.Sleep(new TimeSpan(0,  UpFreqencyMinutes, 0));
            }
        }

        static void UpResume(string userName, string pass)
        {
            var HHNavigator = new HHNavigator(false);
            HHNavigator.LogIn(userName, pass);
            HHNavigator.NavigateMyResumes();
            HHNavigator.UpResume(0);
            HHNavigator.LogOut();
        }
    }
}
