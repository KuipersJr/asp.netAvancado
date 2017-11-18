var Index = {
    viewModel: {
        produtos: ko.observableArray()
    },

    inicializar: function () {
        this.connectarLeilaohub();
        ko.applyBindings(this.viewModel);
    },

    connectarLeilaohub: function () {        
        var connection = $.hubConnection();
        var hub = connection.createHubProxy("LeilaoHub");

        hub.on("atualizarOfertas", this.atualizarOfertas.bind(this) );
        connection.start();
    },

    atualizarOfertas: function() {
        this.viewModel.produtos.push({
            id: 1,
            nome: "Teste",
            preco: 11.50,
            estoque: 10,
            categorianome: "Papelaria"
        });
    } 
};