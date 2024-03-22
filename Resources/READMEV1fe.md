# Flowy App

<img src="https://raw.githubusercontent.com/rmacellaro/flowy-solution/master/Documentation/logo.jpg" width="100" height="100">

Si tratta di un applicativo angular che interfaccia l'api rest disponibile in questo repository: [flowy-solution](https://github.com/rmacellaro/flowy-solution).
Flowy vuole essere un sistema di workflow management basato su [Camunda 8 self-managed](https://docs.camunda.io/docs/self-managed/about-self-managed/), che consente di gestire schemi di processo che rispondono allo standard BPMN 2. che aggiunge funzionalità extra, come in salvataggio di bozze di processo e form di interazione utente, con la possibilità di editare gli schemi e le form direttamente da editor web.

![flowy dashboard](https://raw.githubusercontent.com/rmacellaro/flowy-app/main/documentation/flowy-dashboard.jpg)

## Tenants and Scopes
Il sistema è organizzato in tenants. I tenants possono essere ricercati e creati mediante l'interfaccia grafica nell'apposita sezione del portale, per ogni tenants possono esserci tanti scopes, lo scope rappresenta l'ambito di applicazione di un flosso di processo, all'interno degli scope sono presenti tutti gli elementi necessari al corretto funzionamento di un processo di business. nello specifico per ogni scope flowy consente di:
- gestire le bozze di processo
- gestire le distribuzioni di processo
- gestire le interazioni utente, come ad esempio le form di raccolta dati.
- consultare il dettaglio delle istanze di processo in esecuzione e i relativi task.
- avviare una nuova istanza di processo.

## Bozze
Camunda consente di distribuire sul motore di workflow gli schemi di processo BPMN 2, semplicemente fornendogli un file con estensione .bpmn all'interno del quale è codificato in xml il flusso di processo, per creare questi schemi è disponibile un applicativo desktop fornito sempre dal team di sviluppo camunda, questo però significa che questi schemi devono essere conservati direttamente sulla macchina del progettista, che ovviamente dovrà trovare il modo di condividere con i membri del team. Il sistema Flowy con la sezione bozze consente di salvare sulla piattaforma tutti gli schemi di processo, con la possibilità di modificarli direttamente tramite interfaccia web (basato sul progetto [bpmn-js](https://github.com/bpmn-io/bpmn-js)). e distribuirli sulla piattaforma camunda, gestendo anche il versionamento e il log delle operazioni fatte.

![flowy scope drafts](https://raw.githubusercontent.com/rmacellaro/flowy-app/main/documentation/flowy-scope-drafts.jpg)

![flowy scope draft editor](https://raw.githubusercontent.com/rmacellaro/flowy-app/main/documentation/flowy-scope-drafts-editor.jpg)

## Distribuzioni di processo
Nella sezione delle distribuzioni di processo è possibile consultare la lista delle vari versioni distribuite tramite flowy sulla piattaforma camunda, flowy per ogni versione distribuita visualizza anche lo schema di processo, visualizzando su ogni nodo dello stesso il numero di istanze aperte e lo stato in cui si trovano, se in attesa di task se in errore, cancellate o completate.

![flowy scope processes](https://raw.githubusercontent.com/rmacellaro/flowy-app/main/documentation/flowy-scope-processes.jpg)

## Interazioni utente
le interaction in flowy sono intese come le interfacce utente - sistema, sono le maschere che vengono proposte all'utente quando in un flusso di lavorazione si rende necessario interagire con il sistema, sono soprattutto form di raccolta dati, ma nelle potenzialità del sistema si ipotizza la possibilità di creare anche interfacce castomizzate. il sistema flowy consente anche di modificare le form direttamente sul web grazie all'editor integrato (basato sul progetto [form-js](https://github.com/bpmn-io/form-js)).

![flowy scope interactions editor](https://raw.githubusercontent.com/rmacellaro/flowy-app/main/documentation/flowy-scope-interactions-editor.jpg)

## Istanze di processo
Per consultare la lista delle istanze di processo in esecuzione sulla piattaforma camunda è disponibile poi la sezione delle istanze, che possono essere ricercate mediante l'interfaccia disponibile, per ogni istanza è possibile visualizzare il dettaglio con tutti i dati associati e la lista dei task sospesi che è necessario completare per far avanzare di stato l'istanza.

![flowy scope instances](https://raw.githubusercontent.com/rmacellaro/flowy-app/main/documentation/flowy-scope-instances.jpg)

![flowy scope instance detail](https://raw.githubusercontent.com/rmacellaro/flowy-app/main/documentation/flowy-scope-instance-detail.jpg)

## Avvio di una nuova istanza
L'attuale interfaccia sviluppata consente di selezionare una distribuzione di processo disponibile per lo scope corrente e avviare una nuova istanza, il sitema in futuro presenterà una maschera iniziale di interazione (configurabile sempre mediante il sistema delle interazioni) per rccogliere un set di dati da salvare con l'istanza a da fornire al processo in fase di avvio.

![flowy scope start](https://raw.githubusercontent.com/rmacellaro/flowy-app/main/documentation/flowy-scope-start.jpg)

## Development server

Per avviare il progetto angular eseguire il classico comando `ng serve`. Poi navigare all'indirizzo `http://localhost:4200/`. L'applicazione verrà automaticamente ricaricata se eseguirete delle modifiche al codice sorgente.

