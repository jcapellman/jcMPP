namespace jcMPP.Admin.Console {
    class Program {
        static void Main(string[] args) {
            var updater = new Updater();

            updater.Run();
        }
    }
}