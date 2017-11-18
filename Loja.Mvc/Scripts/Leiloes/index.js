var Index = {
    viewModel: {
        produtos: ko.observableArray()
    },

    inicializar: function () {
        this.connectarLeilaohub();
        ko.applyBindings(this.viewModel);
        this.obterOfertas();
    },

    connectarLeilaohub: function () {        
        var connection = $.hubConnection();
        var hub = connection.createHubProxy("LeilaoHub");

        //hub.on("atualizarOfertas", this.atualizarOfertas.bind(this) );
        hub.on("atualizarOfertas", this.obterOfertas.bind(this) );
        connection.start();
    },

    obterOfertas: function () {
        var self = this;
        $.getJSON("/api/Vendas/Leiloes", function (respose) {
            self.viewModel.produtos(respose)
        });
    }

    //atualizarOfertas: function() {
    //    this.viewModel.produtos.push({
    //        id: 1,
    //        nome: "Teste",
    //        preco: 11.50,
    //        estoque: 10,
    //        categorianome: "Papelaria"
    //    });
    //} 
};