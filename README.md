Framework di numerone in maui per la realizzazione di giochi di carte.
La codebase è in .net, con l'aggiunta di un resourcedictionary da passare.
Il resource dictionary deve includere 4 campi: bastoni, coppe, spade e denari da tradurre dall'italiano nella lingua desiderata, i 4 semi dei mazzi di carte italiane, o eventualmente francesi.
Il codice di apertura deve essere:

    e = new ElaboratoreCarteBriscola(briscolaDaPunti, 0, 39, 40);
    m = new Mazzo(e);
    Carta.Inizializza(numerocarte,new CartaHelperBriscola(e.GetCartaBriscola()), "bastoni", "coppe", "denari", "spade");
    g = new Giocatore(new GiocatoreHelperUtente(), nomegiocatore, dimensionemano);
    switch (indicatore di livello)
    {
	    case 1: helper = new GiocatoreHelperCpu0(e.GetCartaBriscola()); break;
        case 2: helper = new GiocatoreHelperCpu1(e.GetCartaBriscola()); break;
        default: helper = new GiocatoreHelperCpu2(e.GetCartaBriscola()); break;
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

Se avete un gioco di carte sul piatto, il modo di agire dei giocatori professionisti è quello di crearsi mentalmente il grafo di presa. Mi spiego: per la scopa in mano ho un 8, è prendibile tramite 7+1 e 6+2, entrambi vanno bene. Vanno messi in un grafo e va così scomposto l'8, per poi prendere sulla base del piatto quello che consuma il maggior numero di carte del piatto stesso. Quindi se ho 8 e 10 e posso prendere più carte che con l'8 invece che col 10, va giocato l'8.
