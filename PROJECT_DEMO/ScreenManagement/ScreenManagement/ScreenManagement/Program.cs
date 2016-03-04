using System;

namespace ScreenManagement
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ScreenManagerTest game = new ScreenManagerTest())
            {
                game.Run();
            }
        }
    }
#endif
}

