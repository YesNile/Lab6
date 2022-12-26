using System;

namespace lab_6_ochkoshnik.menu
{
    public sealed class ExitMenuItem : MenuItem
    {
        public ExitMenuItem() : base("Выход") { }

        public override void Execute()
        {
            Console.WriteLine("Выход");
        }
    }
}