// Memento nesnesi - Oyun durumunu temsil eder
using System.Text.RegularExpressions;

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
        if (Mementos.Count > 0)
            Console.WriteLine("Geçmiş Hamleler:");
        else
            Console.WriteLine("Hiç Hamle Yapmadınız");

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
    private string _startFenString = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";

    public ChessGame()
    {
        this.fenString = _startFenString;
        this.caretaker = new ChessCaretaker();
    }

    public ChessMemento Save()
    {
        return new ChessMemento(fenString);
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

            Console.WriteLine(
                $"{numMoves} hamle geri alındı. Mevcut pozisyon: {this.GetPosition()}"
            );
        }
        else
        {
            Console.WriteLine(
                $"{numMoves} hamle geri alındı. Mevcut pozisyon: Başlangıç pozisyonu"
            );
        }
    }


    public void DrawChessboard(string fen)
    {
        string[] rows = Regex.Split(fen, @"(?<=\d)(?=[a-zA-Z])");

        foreach (string row in rows)
        {
            int column = 1;
            foreach (char c in row)
            {
                if (char.IsDigit(c))
                {
                    int emptySpaces = int.Parse(c.ToString());
                    for (int j = 0; j < emptySpaces; j++)
                    {
                        Console.Write(". ");
                        column++;
                    }
                }
                else
                {
                    Console.Write(c + " ");
                    column++;
                }
            }

            Console.WriteLine();
        }
    }
}

class Program
{
    private static ChessGame _game;
    private static ChessCaretaker _caretaker;

    static void Main(string[] args)
    {
        _game = new ChessGame();
        _caretaker = _game.GetCaretaker();

        List<string> FenList = new List<string>();
        FenList.Add(_game.GetPosition());
        FenList.Add("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR"); 
        FenList.Add("rnbqkbnr/ppp1pppp/3p4/8/4P3/8/PPPP1PPP/RNBQKBNR");
        FenList.Add("rnbqkbnr/ppp1pppp/3p4/1B6/4P3/8/PPPP1PPP/RNBQK1NR");
        FenList.Add("rnbqkbnr/pp2pppp/2pp4/1B6/4P3/8/PPPP1PPP/RNBQK1NR");
        FenList.Add("rnbqkbnr/pp2pppp/2pp4/8/B3P3/8/PPPP1PPP/RNBQK1NR");
        FenList.Add("rnbqkbnr/pp2pppp/2pp4/8/B3P3/5N2/PPPP1PPP/RNBQK2R");
        FenList.Add("rnbqkbnr/pp2pppp/2pp4/8/B3P3/3P1N2/PPP2PPP/RNBQK2R");
        FenList.Add("rnbqkbnr/pp2pppp/2pp4/8/B3P3/1P1P1N2/P1P2PPP/RNBQK2R");
        FenList.Add("rnbqkbnr/pp2pppp/2pp4/8/B3PB2/1P1P1N2/P1P2PPP/RN1QK2R");

        for (int i = 0; i < FenList.Count; i++)
        {
            _game.SetPosition($"{FenList[i]}");
            _caretaker.Mementos.Push(_game.Save());
        }



        //_game.UndoMoves(3);

        foreach (var memento in _caretaker.Mementos)
        {
           _game.DrawChessboard(memento.FenString);
            Console.WriteLine();
            Console.WriteLine("Fen Notasyonu : " + memento.FenString);
            Console.WriteLine();
        }

        Console.ReadLine();
    }
}
