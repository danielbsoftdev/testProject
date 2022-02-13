using Test.LiveCoding;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ICharacter Ordeus = new Character("Ordeus", 2 ,2, 2, 2);
            ICharacter Bestie = new Character("Bestie", 3, 4, 5, 2);
            var game = new Game(Ordeus, Bestie, 20);
            game.Play();
        }
    }
      
}