# Flowy
<img src="https://raw.githubusercontent.com/rmacellaro/flowy/master/Resources/logo.svg?token=GHSAT0AAAAAACP6TYC35BXW65GLSW2WDXPYZP5LDCQ" width="100" height="100">

il progetto flowy è un gestore di processi di busniss, si articola in 3 moduli principali:

## flowy-engine
E' un api .net con il motore di workflow di flowy basato su una logica di lavorazione a stati, durante il flusso di lavorazione in base allo stato in cui si trova un istanza posso compiere determinate azioni per passare a uno stato successivo fino al completamento del flusso di processo.

## flowy-camunda
si tratta di un'api .net che interagisce con la piattaforma Camunda8 per gestire flussi di processo con sintassi BPMN 

## flowy-app
continene l'interfaccia utente scritta in angular per gestire e accedere alle funzionalità dei due motori.

