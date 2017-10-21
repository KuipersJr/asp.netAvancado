// kyle Simpson
// object literal

var Details = {
    produtoId: 0,
    nomeParcipante: "",

    inicializar: function (produtoId) {
        this.produtoId = produtoId;
        this.conectarLeilaoHub();
        this.vincularEventos();
    },
    
    conectarLeilaoHub: function () {
        var connection = $.hubConnection();
        var hub = connection.createHubProxy("LeilaoHub");        
        connection.start();
    },

    vincularEventos: function () {
        $("#entrarButton").on("click", function () { this.entrarLeilao();})
    },

    entrarLeilao: function () {
        this.nomeParcipante = $("#nomeParticipante").val();
        //this.LeilaoHub.invoke("Participar", this.nomeParcipante, this.produtoId);

        $("#participanteDiv").hide();
        $("#lanceDiv").show();
        $("#valorLance").focus();
    }
};
