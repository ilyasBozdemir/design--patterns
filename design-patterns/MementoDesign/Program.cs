// Memento nesnesi - Oyun durumunu temsil eder
public class ChessMemento
{
    public string FenString { get; }

    public ChessMemento(string fenString)
    {
        FenString = fenString;
    }
}

// Caretaker sınıfı - Memento nesnelerini saklar
public class ChessCaretaker
{
    public Stack<ChessMemento> Mementos { get; }

    public ChessCaretaker()
    {
        Mementos = new Stack<ChessMemento>();
    }

    // Tüm mementoları temizle
    public void ClearMementos() => Mementos.Clear();

    public void PrintMementos()
    {
        Console.WriteLine();
        Console.WriteLine("Geçmiş Hamleler:");
        foreach (var memento in Mementos)
            Console.WriteLine(memento.FenString);
        Console.WriteLine();
    }


}

// Originator sınıfı - Oyun durumunu yönetir
public class ChessGame
{
    private string fenString;
    private ChessCaretaker caretaker;

    public ChessGame()
    {
        this.fenString = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        this.caretaker = new ChessCaretaker();
    }

    public void SetPosition(string fenString)
    {
        this.fenString = fenString;
        caretaker.Mementos.Push(new ChessMemento(fenString));
    }

    public string GetPosition() => fenString;

    public ChessCaretaker GetCaretaker() => caretaker;

    public void Restore(ChessMemento memento)
    {
        fenString = memento.FenString;
    }

    public void UndoMoves(int numMoves)
    {
        if (caretaker.Mementos.Count == 0)
        {
            Console.WriteLine("Geri alınacak hamle bulunamadı.");
            return;
        }

        if (numMoves <= 0)
        {
            Console.WriteLine("Geçersiz hamle sayısı.");
            return;
        }

        numMoves = Math.Min(numMoves, caretaker.Mementos.Count);

        for (int i = 0; i < numMoves; i++)
        {
            caretaker.Mementos.Pop();
        }

        if (caretaker.Mementos.Count > 0)
        {
            this.Restore(caretaker.Mementos.Peek());
            Console.WriteLine($"{numMoves} hamle geri alındı. Yeni pozisyon: {this.GetPosition()}");
        }
        else
        {
            Console.WriteLine($"{numMoves} hamle geri alındı. Yeni pozisyon: Başlangıç pozisyonu");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        string board =
            @"
            r n b q k b n r
            p p p p p p p p
            . . . . . . . .
            . . . . . . . .
            . . . . . . . .
            . . . . . . . .
            P P P P P P P P
            R N B Q K B N R
            ";

        string info =
            @"
                  
            Burada her bir harf bir satranç taşı temsil eder:
            Büyük harfler beyaz taşları, küçük harfler siyah taşları temsil eder.
            r: siyah kale (rook)
            n: siyah at (knight)
            b: siyah fil (bishop)
            q: siyah vezir (queen)
            k: siyah şah (king)
            p: siyah piyon (pawn)
            aynı şekilde büyük harfler beyaz taşları temsil eder.
            Noktalı alanlar boş kareleri temsil eder.

            ";

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(board);
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine(info);
        Console.ForegroundColor = ConsoleColor.Gray;

        // Başlangıç durumu
        ChessGame game = new ChessGame();
        Console.WriteLine(game.GetPosition());


        // Geri alma mekanizması için caretaker oluştur
        ChessCaretaker caretaker = game.GetCaretaker();

        // Hamle yap ve pozisyonu kaydet
        game.SetPosition("rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/RNBQKBNR");
        Console.WriteLine(game.GetPosition());


        // İkinci hamle
        game.SetPosition("rnbqkbnr/ppp2ppp/4p3/3p4/3PP3/8/PPP2PPP/RNBQKBNR");
        Console.WriteLine(game.GetPosition());


        // Üçüncü hamle
        game.SetPosition("rnbqkbnr/ppp2ppp/4p3/3p4/3PP3/4P3/PPP2PPP/RNBQKBNR");
        Console.WriteLine(game.GetPosition());


        // Dördüncü hamle
        game.SetPosition("rnbqkbnr/ppp2ppp/4p3/3pP3/3P4/4P3/PPP2PPP/RNBQKBNR");
        Console.WriteLine(game.GetPosition());


        // Beşinci hamle
        game.SetPosition("rnbqkbnr/ppp2ppp/4p3/3pP3/3P4/2N1P3/PPP2PPP/R1BQKBNR");
        Console.WriteLine(game.GetPosition());


        // Geçmiş hamleleri göster
        caretaker.PrintMementos();


        Console.WriteLine("Mementos  Count : " + caretaker.Mementos.Count);

        game.UndoMoves(1);

        Console.WriteLine("Mementos  Count : " + caretaker.Mementos.Count);

        // Altıncı hamle
        game.SetPosition("rnbqkbnr/ppp2ppp/4p3/3pP3/3P4/2N1P3/PPP2PPP/R1BQKBNR");
        Console.WriteLine(game.GetPosition());


        // Geçmiş hamleleri göster
        caretaker.PrintMementos();
 

        Console.WriteLine("Mementos Count : " + caretaker.Mementos.Count);
        Console.ReadLine();
    }
}
