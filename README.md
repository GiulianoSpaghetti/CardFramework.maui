Framework di numerone in maui per la realizzazione di giochi di carte.
La codebase è in .net, con l'aggiunta di un resourcedictionary da passare.
Il resource dictionary deve includere 4 campi: bastoni, coppe, spade e denari da tradurre dall'italiano nella lingua desiderata, i 4 semi dei mazzi di carte italiane, o eventualmente francesi.
Il codice di apertura deve essere:

e = new ElaboratoreCarteBriscola(briscolaDaPunti);
m = new Mazzo(e);
Carta.Inizializza(numerocarte, CartaHelperBriscola.GetIstanza(e), d);
g = new Giocatore(new GiocatoreHelperUtente(), nomegiocatore, dimensionemano);
switch (indicatore di livello)
{
	case 1: helper = new GiocatoreHelperCpu0(ElaboratoreCarteBriscola.GetCartaBriscola()); break;
        case 2: helper = new GiocatoreHelperCpu1(ElaboratoreCarteBriscola.GetCartaBriscola()); break;
        default: helper = new GiocatoreHelperCpu2(ElaboratoreCarteBriscola.GetCartaBriscola()); break;
}
cpu = new Giocatore(helper, nomegiocatore, dimensionemano);
briscola = Carta.GetCarta(ElaboratoreCarteBriscola.GetCartaBriscola());
for (UInt16 i = 0; i < dimensionemano; i++)
{
	g.AddCarta(m);
        cpu.AddCarta(m);
}

una volta fatto questo, in carta si avrà un vettore di numerocarte elementi, in g e cpu si avrà un vettore di tre dimensionemanoelementi corrispondenti alle prime 2*dimensionemano carte del mazzo, 
che saranno riempite con addcarta.
Quando addcarta restituisce un IndexOutOfRangeException exception si avrà la fine del mazzo.