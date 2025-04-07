namespace org.altervista.numerone.framework;
/// <summary>
/// Classe che implementa il comportamento della CPU di livello 3
/// </summary>
public class GiocatoreHelperCpu2 : GiocatoreHelperCpu
{
    /// <summary>
    /// Inizializza la struttura ortandosi il seme di briscola
    /// </summary>
    /// <param name="b">intero indicante la carta di briscola</param>
    public GiocatoreHelperCpu2(ushort b) : base(b)
    {
    }
    /// <summary>
    /// retituisce il livello di gioco
    /// </summary>
    /// <returns>il livello di gioco</returns>
    public override ushort GetLivello()
    {
        return 3;
    }
    /// <summary>
    /// Se è il secondo di mano si cerca la carta di maggior valore con lo stesso seme, se non la si trova e si ha briscola e la carta ha peso sceglie se giocare la briscola oppure non può prendere per non far scoprire le carte, se non può prendere se deve rispondere al seme cerca la carta più piccola dello stesso seme, se no gioca la carta più piccola in assoluto
    /// </summary>
    /// <param name="x">indice della carta da giocare, qui non considerato</param>
    /// <param name="mano">vettore delle carte da giocare</param>
    /// <param name="numeroCarte">dimensione del vettore dellke carte</param>
    /// <param name="c">carta giocata dall'altro giocatore</param>
    /// <param name="stessoSeme">indica se bisogna rispondere al seme</param>
    /// <returns>indice di mano indicante la carta da giocare</returns>
    public override ushort Gioca(ushort x, Carta[] mano, ushort numeroCarte, Carta c, bool stessoSeme)
    {
        UInt16 i = (UInt16)ElaboratoreCarteBriscola.r.Next(0, UInt16.MaxValue);
        if (!briscola.StessoSeme(c))
        {
            if ((i = getSoprataglio(mano, numeroCarte, c, true)) < numeroCarte)
                return i;
            if (c.Punteggio > 0 && (i = GetBriscola(mano, numeroCarte)) < numeroCarte)
            {
                if (c.Punteggio > 4)
                    return i;
                if (mano[i].Punteggio > 0)
                    if (ElaboratoreCarteBriscola.r.Next() % 10 < 5)
                        return i;
            }
        }
        else
        {
            if (ElaboratoreCarteBriscola.r.Next() % 10 < 5 && (i = getSoprataglio(mano, numeroCarte, c, false)) < numeroCarte)
                return i;
        }
        if (stessoSeme)
            i = GetPrimaCartaConSeme(mano, numeroCarte, c);
        if (i >= numeroCarte)
            i = 0;
        return i;
    }
}
